﻿using ModdingTools.UEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ModdingTools
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static readonly ProcessFactory ProcFactory = new ProcessFactory();

        [STAThread]
        static void Main()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(LoadFromSameFolder);
            Directory.SetCurrentDirectory(Path.GetDirectoryName(UEngine.GameFinder.FindGameDir()));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

        static Assembly LoadFromSameFolder(object sender, ResolveEventArgs args)
        {
            string folderPath = Path.GetDirectoryName(UEngine.GameFinder.FindGameDir());
            var name = new AssemblyName(args.Name).Name;
            Debug.WriteLine(name);

            Debug.WriteLine("test: " + folderPath);

            string ext = name.EndsWith(".resources") ? ".resources" : name.EndsWith(".dll") ? ".dll" : name.EndsWith(".exe") ? ".exe" : "";

            if (name == "ModManager")
            {
                ext = ".dll";
            }

            string assemblyPath = Path.Combine(folderPath, name + ext);

            Debug.WriteLine("test: " + assemblyPath);

            if (!File.Exists(assemblyPath))
            {
                assemblyPath = Path.Combine(folderPath, name + ext);
                if (!File.Exists(assemblyPath)) return null;
            }

            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            return assembly;
        }
    }
}
