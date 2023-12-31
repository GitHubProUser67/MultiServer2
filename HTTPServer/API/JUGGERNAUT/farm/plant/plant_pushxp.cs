﻿using System.Xml;

namespace HTTPServer.API.JUGGERNAUT.farm.plant
{
    public class plant_pushxp
    {
        public static string? ProcessPushXp(Dictionary<string, string>? QueryParameters)
        {
            if (QueryParameters != null)
            {
                string? user = QueryParameters["user"];
                string? level = QueryParameters["level"];
                string? xp = QueryParameters["xp"];

                if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(level) && !string.IsNullOrEmpty(xp))
                {
                    Directory.CreateDirectory($"{HTTPServerConfiguration.HTTPStaticFolder}/juggernaut/farm/User_Data");

                    if (File.Exists($"{HTTPServerConfiguration.HTTPStaticFolder}/juggernaut/farm/User_Data/{user}.xml"))
                    {
                        // Load the XML string into an XmlDocument
                        XmlDocument xmlDoc = new();
                        xmlDoc.Load($"{HTTPServerConfiguration.HTTPStaticFolder}/juggernaut/farm/User_Data/{user}.xml");

                        // Find the <level> element
                        XmlElement? levelElement = xmlDoc.SelectSingleNode("/xml/resources/level") as XmlElement;

                        if (levelElement != null)
                        {
                            try
                            {
                                // Replace the value of <level> with a new value
                                levelElement.InnerText = level;
                            }
                            catch (Exception)
                            {
                                // Not Important
                            }

                            // Find the <xp> element
                            XmlElement? xpElement = xmlDoc.SelectSingleNode("/xml/resources/xp") as XmlElement;

                            if (xpElement != null)
                            {
                                try
                                {
                                    // Replace the value of <xp> with a new value
                                    xpElement.InnerText = xp;
                                }
                                catch (Exception)
                                {
                                    // Not Important
                                }

                                File.WriteAllText($"{HTTPServerConfiguration.HTTPStaticFolder}/juggernaut/farm/User_Data/{user}.xml", xmlDoc.OuterXml);
                            }
                        }
                    }

                    return string.Empty;
                }
            }

            return null;
        }
    }
}
