namespace ModdingTools.Windows.Tools
{
    partial class HatOvhBrowser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HatOvhBrowser));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.guiWorker1 = new ModdingTools.GUI.GUIWorker();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoScrollMargin = new System.Drawing.Size(10, 0);
            this.flowLayoutPanel1.AutoScrollMinSize = new System.Drawing.Size(10, 0);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(5, 37);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(605, 708);
            this.flowLayoutPanel1.TabIndex = 3;
            this.flowLayoutPanel1.Visible = false;
            // 
            // guiWorker1
            // 
            this.guiWorker1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guiWorker1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.guiWorker1.Location = new System.Drawing.Point(5, 37);
            this.guiWorker1.Name = "guiWorker1";
            this.guiWorker1.Size = new System.Drawing.Size(605, 708);
            this.guiWorker1.TabIndex = 1;
            // 
            // HatOvhBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 750);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.guiWorker1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "HatOvhBrowser";
            this.Text = "HAT.OVH BROWSER";
            this.Controls.SetChildIndex(this.guiWorker1, 0);
            this.Controls.SetChildIndex(this.flowLayoutPanel1, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private GUI.GUIWorker guiWorker1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}