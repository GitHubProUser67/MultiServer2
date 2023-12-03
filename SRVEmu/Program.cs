using CustomLogger;
using Newtonsoft.Json.Linq;
using System.Runtime;
using SRVEmu;

public static class SRVEMUServerConfiguration
{
    public static string HTTPSCertificateFile { get; set; } = $"{Directory.GetCurrentDirectory()}/static/SSL/MultiServer.pfx";
    public static string DatabaseConfig { get; set; } = $"{Directory.GetCurrentDirectory()}/static/ea.db.json";

    /// <summary>
    /// Tries to load the specified configuration file.
    /// Throws an exception if it fails to find the file.
    /// </summary>
    /// <param name="configPath"></param>
    /// <exception cref="FileNotFoundException"></exception>
    public static void RefreshVariables(string configPath)
    {
        // Make sure the file exists
        if (!File.Exists(configPath))
        {
            LoggerAccessor.LogWarn("Could not find the srvemu.json file, using server's default.");
            return;
        }

        try
        {
            // Read the file
            string json = File.ReadAllText(configPath);

            // Parse the JSON configuration
            dynamic config = JObject.Parse(json);

            HTTPSCertificateFile = config.certificate_file;
            DatabaseConfig = config.database;
        }
        catch (Exception)
        {
            LoggerAccessor.LogWarn("srvemu.json file is malformed, using server's default.");
        }
    }
}

class Program
{
    static Task RefreshConfig()
    {
        while (true)
        {
            // Sleep for 5 minutes (300,000 milliseconds)
            Thread.Sleep(5 * 60 * 1000);

            // Your task logic goes here
            LoggerAccessor.LogInfo("Config Refresh at - " + DateTime.Now);

            SRVEMUServerConfiguration.RefreshVariables($"{Directory.GetCurrentDirectory()}/static/srvemu.json");
        }
    }

    static void Main()
    {
        if (!CryptoSporidium.MiscUtils.IsWindows())
            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;

        LoggerAccessor.SetupLogger("SRVEmu");

        SRVEMUServerConfiguration.RefreshVariables($"{Directory.GetCurrentDirectory()}/static/srvemu.json");

        CryptoSporidium.SSLUtils.InitCerts(SRVEMUServerConfiguration.HTTPSCertificateFile);

        SRVEmuServer server = new();

        _ = Task.Run(server.Run);

        _ = Task.Run(RefreshConfig);

        if (CryptoSporidium.MiscUtils.IsWindows())
        {
            while (true)
            {
                LoggerAccessor.LogInfo("Press any key to shutdown the server. . .");

                Console.ReadLine();

                LoggerAccessor.LogWarn("Are you sure you want to shut down the server? [y/N]");
                char input = char.ToLower(Console.ReadKey().KeyChar);

                if (input == 'y')
                {
                    LoggerAccessor.LogInfo("Shutting down. Goodbye!");
                    Environment.Exit(0);
                }
            }
        }
        else
        {
            LoggerAccessor.LogWarn("\nConsole Inputs are locked while server is running. . .");

            Thread.Sleep(Timeout.Infinite); // While-true on Linux are thread blocking if on main static.
        }
    }
}