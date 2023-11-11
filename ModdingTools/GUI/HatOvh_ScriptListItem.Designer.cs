namespace ModdingTools.GUI
{
    partial class HatOvh_ScriptListItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cuButton1 = new CUFramework.Controls.CUButton();
            this.cuButton2 = new CUFramework.Controls.CUButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(437, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Death Wish Objective Kismet Node";
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(437, 113);
            this.label2.TabIndex = 1;
            this.label2.Text = "Trigger, complete, fail, or force unlock objectives for a Death Wish, using only " +
    "Kismet!\r\n";
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(3, 150);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(560, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "by SuperInkLink @ 2021-05-30 01:17:43 CET (452 views)";
            // 
            // cuButton1
            // 
            this.cuButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.cuButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.cuButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cuButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cuButton1.ForeColor = System.Drawing.Color.White;
            this.cuButton1.Image = global::ModdingTools.Properties.Resources.icon_005;
            this.cuButton1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cuButton1.Location = new System.Drawing.Point(446, 3);
            this.cuButton1.Name = "cuButton1";
            this.cuButton1.NoFocus = false;
            this.cuButton1.Size = new System.Drawing.Size(117, 75);
            this.cuButton1.TabIndex = 3;
            this.cuButton1.Text = "Download";
            this.cuButton1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cuButton1.UseVisualStyleBackColor = false;
            this.cuButton1.Click += new System.EventHandler(this.cuButton1_Click);
            // 
            // cuButton2
            // 
            this.cuButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cuButton2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.cuButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cuButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cuButton2.ForeColor = System.Drawing.Color.White;
            this.cuButton2.Location = new System.Drawing.Point(446, 84);
            this.cuButton2.Name = "cuButton2";
            this.cuButton2.NoFocus = false;
            this.cuButton2.Size = new System.Drawing.Size(117, 23);
            this.cuButton2.TabIndex = 4;
            this.cuButton2.Text = "More Info";
            this.cuButton2.UseVisualStyleBackColor = false;
            // 
            // HatOvh_ScriptListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.Controls.Add(this.cuButton2);
            this.Controls.Add(this.cuButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "HatOvh_ScriptListItem";
            this.Size = new System.Drawing.Size(566, 175);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private CUFramework.Controls.CUButton cuButton1;
        private CUFramework.Controls.CUButton cuButton2;
    }
}
