using CUFramework.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModdingTools.GUI;
using ModdingTools.Logging;
using System.Diagnostics;
using System.IO;
using CUFramework.Dialogs;

namespace ModdingTools.Windows.Tools
{
    public partial class HatOvhBrowser : CUWindow
    {
        struct ScriptInfo
        {
            string Name;
            string Description;
            string Author;
        }
        public HatOvhBrowser()
        {
            InitializeComponent();

            RefreshScriptList();
        }

        void RefreshScriptList()
        {
            string baseUrl = "https://hat.ovh/list";
            flowLayoutPanel1.Hide();
            guiWorker1.SetStatus("Downloading Script List...");

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("Content-Type", "text/html");
                wc.Headers.Add("User-Agent", "OpenModManager/1.0");
                wc.DownloadStringCompleted += (sender, e) =>
                {
                    //listPageData = e.Result;
                    flowLayoutPanel1.Show();
                    ParseHtmlData(e.Result);

                };
                wc.DownloadProgressChanged += (sender, e) =>
                {
                    guiWorker1.SetProgress(e.ProgressPercentage);
                };
                wc.DownloadStringAsync(new Uri(baseUrl));
            }
        }

        // m_cube please add public api that gives me a json instead of me having to parse html in a sketchy and potentially vulnerable way lol
        List<ScriptInfo> ParseHtmlData(string HTML)
        {
            string isolatedScriptList = HTML.Trim().Split(new[] { "<div class=\'scriptList\'>" }, StringSplitOptions.None)[1].Split(new[] { "<script type=\"text/javascript\">" }, StringSplitOptions.None )[0];
            string[] scriptElements = isolatedScriptList.Split(new[] { "<div class=\'row\' dataCategories=\'" }, StringSplitOptions.None);

            foreach (var item in scriptElements)
            {
                if (String.IsNullOrEmpty(item.Trim())) continue;
                string cleanItem = item.Trim();

                string[] titleLine = cleanItem.Substring(cleanItem.IndexOf("<a href='/s/") + 12).Split(new[] { "'>", "<" }, StringSplitOptions.RemoveEmptyEntries);

                string authorLine = cleanItem.Substring(cleanItem.IndexOf("<div class=\"infobar\">") + 21);
                authorLine = authorLine.Substring(0, authorLine.IndexOf('\n') - 6);

                string descriptionLine = cleanItem.Substring(cleanItem.IndexOf("<div class='desc'>") + 21);
                descriptionLine = descriptionLine.Substring(0, descriptionLine.IndexOf(descriptionLine.Contains("</pre>") ? "</pre></div>  \n" : "</p></div>  \n"));

                var scriptItemControl = new HatOvh_ScriptListItem(titleLine[1], descriptionLine, authorLine, int.Parse(titleLine[0]));
                scriptItemControl.Browser = this;
                flowLayoutPanel1.Controls.Add(scriptItemControl);
            }

            return null;
        }

        public static void ExtractZIP(string zipFilePath, string targetFolderPath)
        {
            var zip = GetShell32Folder(zipFilePath).Items();
            GetShell32Folder(targetFolderPath).CopyHere(zip, 16 | 4 | 8);
        }

        // Taken from ModdingTools.Updater.OMMHelper
        private static Shell32.Folder GetShell32Folder(string folderPath)
        {
            if (!Directory.Exists(folderPath) && !File.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            Type shellAppType = Type.GetTypeFromProgID("Shell.Application");
            Object shell = Activator.CreateInstance(shellAppType);
            return (Shell32.Folder)shellAppType.InvokeMember("NameSpace",
            System.Reflection.BindingFlags.InvokeMethod, null, shell, new object[] { folderPath });
        }

        public bool OnClickDownload(int ID, string scriptName)
        {
            List<string> scriptFileList;
            flowLayoutPanel1.Hide();
            guiWorker1.SetStatus("Downloading " + scriptName + "...");

            scriptFileList = DownloadAndUnzipScripts(ID);

            if (scriptFileList == null || scriptFileList.Count <= 0)
            {
                CUMessageBox.Show("No script files found!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            ArrayCheckboxWindow.Ask(scriptName + " - SCRIPT IMPORT", "Which mods should the downloaded scripts be imported to?", MainWindow.Instance.modListControl1.GetVisibleMods(), "Name", "", false);
            return true;
        }

        public List<string> DownloadAndUnzipScripts(int ID)
        {
            string url = "https://hat.ovh/s/" + ID.ToString() + "/bulk";
            string folderPath = Path.Combine(Program.GetAppRoot(), "hatovhscripts");
            string filePath = folderPath + ".zip";

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("User-Agent", "OpenModManager/1.0");
                wc.DownloadFile(new Uri(url), filePath);
            }

            if (File.Exists(filePath))
            {
                ExtractZIP(filePath, folderPath);

                List<string> scriptFiles = new List<string>();
                foreach (var file in Directory.GetFiles(folderPath, "*.uc", SearchOption.AllDirectories))
                {
                    scriptFiles.Add(file);
                }

                if (scriptFiles.Count > 0)
                    return scriptFiles;
            }

            return null;
        }
    }
}
