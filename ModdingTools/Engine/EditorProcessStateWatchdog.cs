using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ModdingTools.Engine
{
    internal class EditorProcessStateWatchdog
    {
        public bool[] IsEditorRunning { get; private set; }

        public event EventHandler EditorStateChanged;

        public EditorProcessStateWatchdog()
        {
            IsEditorRunning = new bool[2];
            var th = new Thread(ThreadWorker);
            th.IsBackground = true;
            th.Start();
        }

        private void ThreadWorker()
        {
            while (true)
            {
                try
                {
                    for (int i = 0; i < IsEditorRunning.Length; i++)
                    {
                        var x = Process.GetProcessesByName(i == 0 ? "HatinTimeEditor" : "HatinTimeGame");
                        var isRunning = x != null && x.Length > 0;
                        if (isRunning != IsEditorRunning[i])
                        {
                            IsEditorRunning[i] = isRunning;
                            // trigger state change
                            EditorStateChanged?.Invoke(this, EventArgs.Empty);
                        }
                    }
                }
                catch (Exception e) { } // nah
                Thread.Sleep(3000);
            }
        }
    }
}