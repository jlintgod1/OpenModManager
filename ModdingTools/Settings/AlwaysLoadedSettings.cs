using ModdingTools.Engine;
using ModdingTools.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ModdingTools.Settings
{
    public class AlwaysLoadedSettings
    {
        public List<string> Classes = new List<string>();
        public List<int> ClassesInt = new List<int>();

        [XmlIgnore]
        public string OwnerModName; //The mod folder name can change, so do not serialize
        public void Save()
        {
            var cfgRoot = Path.Combine(GameFinder.GetModsDir(), OwnerModName);
            var appCfgPath = Path.Combine(cfgRoot, "AlwaysLoadedClasses.xml");
            if (!Directory.Exists(cfgRoot))
                Directory.CreateDirectory(cfgRoot);

            XmlSerializer serializer = new XmlSerializer(typeof(AlwaysLoadedSettings));
            Stream fs = new FileStream(appCfgPath, FileMode.Create);
            using (XmlWriter writer = new XmlTextWriter(fs, Encoding.Unicode))
            {
                serializer.Serialize(writer, this);
                writer.Close();
            }
        }
        public static AlwaysLoadedSettings Load(string ModFolderName) 
        {
            AlwaysLoadedSettings Instance = null;
            var cfgRoot = Path.Combine(GameFinder.GetModsDir(), ModFolderName);
            var appCfgPath = Path.Combine(cfgRoot, "AlwaysLoadedClasses.xml");

            if (File.Exists(appCfgPath))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(AlwaysLoadedSettings));
                    using (Stream reader = new FileStream(appCfgPath, FileMode.Open))
                    {
                        Instance = (AlwaysLoadedSettings)serializer.Deserialize(reader);
                        Instance.OwnerModName = ModFolderName;
                        return Instance;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Log(CUFramework.Shared.LogLevel.Error, ex.Message);
                    Logger.Log(CUFramework.Shared.LogLevel.Error, ex.ToString());
                }
            }
            else
            {
                if (!Directory.Exists(cfgRoot))
                    Directory.CreateDirectory(cfgRoot);
            }

            if (!(Instance is AlwaysLoadedSettings))
            {
                Instance = new AlwaysLoadedSettings();
                Instance.OwnerModName = ModFolderName;
                Instance.Save();
            }

            return Instance;
        }
    }
}
