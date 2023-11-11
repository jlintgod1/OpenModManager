using ModdingTools.Windows.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModdingTools.GUI
{
    public partial class HatOvh_ScriptListItem : UserControl
    {
        public HatOvhBrowser Browser;
        public int ScriptID { get; protected set; }
        public HatOvh_ScriptListItem(string Title, string Description, string Author, int ID)
        {
            InitializeComponent();

            label1.Text = Title;
            label2.Text = Description;
            label3.Text = Author;
            ScriptID = ID;
        }

        private void cuButton1_Click(object sender, EventArgs e)
        {
            Browser.OnClickDownload(ScriptID, label1.Text);
        }
    }
}
