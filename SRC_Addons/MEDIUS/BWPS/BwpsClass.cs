﻿using DotNetty.Common.Internal.Logging;
using Newtonsoft.Json;
using PSMultiServer.SRC_Addons.MEDIUS.BWPS.Config;
using PSMultiServer.SRC_Addons.MEDIUS.Server.Common;

namespace PSMultiServer.SRC_Addons.MEDIUS.BWPS
{
    public class BwpsClass
    {
        private static string CONFIG_DIRECTIORY = "./loginformNtemplates/BWPS";

        public static string CONFIG_FILE = Path.Combine(CONFIG_DIRECTIORY, "config.json");

        static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<BwpsClass>();

        public static ServerSettings Settings = new ServerSettings();
        public static BWPServer BWPS = new BWPServer();

        static async Task StartServerAsync()
        {
            DateTime lastConfigRefresh = Utils.GetHighPrecisionUtcTime();

            Logger.Info("**************************************************");
            string datetime = DateTime.Now.ToString("MMMM/dd/yyyy hh:mm:ss tt");
            Logger.Info($"* Launched on {datetime}");

            Task.WaitAll(BWPS.Start());
            //string gpszVersion = "rt_bwprobe ReleaseVersion 3.02.200704101920";
            string gpszVersion2 = "3.02.200704101920";
            Logger.Info($"* Bandwidth Probe Server Version {gpszVersion2}");

            //* Process ID: %d , Parent Process ID: %d

            Logger.Info($"* Server Key Type: {Settings.EncryptMessages}");

            //* Diagnostic Profiling Enabled: %d Counts

            Logger.Info("**************************************************");

            Logger.Info($"UDP BWPS started on port {Settings.BWPSPort}.");

            try
            {
                while (true)
                {
                    // Reload config
                    if ((Utils.GetHighPrecisionUtcTime() - lastConfigRefresh).TotalMilliseconds > Settings.RefreshConfigInterval)
                    {
                        RefreshConfig();
                        lastConfigRefresh = Utils.GetHighPrecisionUtcTime();
                    }

                    await Task.Delay(100);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            finally
            {

            }
        }

        public static async Task BwpsMain()
        {
            // 
            Initialize();

            // 
            await StartServerAsync();
        }

        static void Initialize()
        {
            RefreshConfig();
        }

        /// <summary>
        /// 
        /// </summary>
        static void RefreshConfig()
        {
            // 
            var serializerSettings = new JsonSerializerSettings()
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };

            // Load settings
            if (File.Exists(CONFIG_FILE))
            {
                // Populate existing object
                JsonConvert.PopulateObject(File.ReadAllText(CONFIG_FILE), Settings, serializerSettings);
            }
            else
            {
                /*
                // Add default localhost entry
                Settings.Universes.Add(0, new UniverseInfo()
                {
                    Name = "sample universe",
                    Endpoint = "url",
                    Port = 10075,
                    UniverseId = 1
                });
                */

                if (!Directory.Exists(Directory.GetCurrentDirectory() + "/loginformNtemplates/BWPS"))
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/loginformNtemplates/BWPS");
                }

                // Save defaults
                File.WriteAllText(CONFIG_FILE, JsonConvert.SerializeObject(Settings, Formatting.Indented));
            }

            // Update default rsa key
            //Pipeline.Attribute.ScertClientAttribute.DefaultRsaAuthKey = Settings.DefaultKey;
        }
    }
}