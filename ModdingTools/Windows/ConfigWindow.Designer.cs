﻿using CUFramework.Controls;

namespace ModdingTools.Windows
{
    partial class ConfigWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigWindow));
            this.mButton3 = new CUFramework.Controls.CUButton();
            this.mButton2 = new CUFramework.Controls.CUButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.cuButton1 = new CUFramework.Controls.CUButton();
            this.checkBox11 = new System.Windows.Forms.CheckBox();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mButton3
            // 
            this.mButton3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(196)))), ((int)(((byte)(0)))));
            this.mButton3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.mButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mButton3.ForeColor = System.Drawing.Color.Black;
            this.mButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mButton3.Location = new System.Drawing.Point(458, 4);
            this.mButton3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mButton3.Name = "mButton3";
            this.mButton3.NoFocus = false;
            this.mButton3.Size = new System.Drawing.Size(447, 39);
            this.mButton3.TabIndex = 8;
            this.mButton3.Text = "CANCEL";
            this.mButton3.UseVisualStyleBackColor = false;
            this.mButton3.Click += new System.EventHandler(this.mButton3_Click);
            // 
            // mButton2
            // 
            this.mButton2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.mButton2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.mButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mButton2.ForeColor = System.Drawing.Color.White;
            this.mButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mButton2.Location = new System.Drawing.Point(4, 4);
            this.mButton2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mButton2.Name = "mButton2";
            this.mButton2.NoFocus = false;
            this.mButton2.Size = new System.Drawing.Size(446, 39);
            this.mButton2.TabIndex = 7;
            this.mButton2.Text = "SAVE";
            this.mButton2.UseVisualStyleBackColor = false;
            this.mButton2.Click += new System.EventHandler(this.mButton2_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.mButton2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mButton3, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 431);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(909, 47);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // checkBox2
            // 
            this.checkBox2.Location = new System.Drawing.Point(13, 117);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(400, 54);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Enable meme - needs some elevator music placed in the program directory - it must" +
    " be named \"lol.wav\"";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(13, 63);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(400, 48);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Don\'t load workshop mods on list\r\n(faster) // requires restart";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.Location = new System.Drawing.Point(471, 177);
            this.checkBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(433, 42);
            this.checkBox4.TabIndex = 10;
            this.checkBox4.Text = "[experimental, not fully implemented yet]\r\nEnable multilanguage cooking support";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.Enabled = false;
            this.checkBox5.Location = new System.Drawing.Point(19, 388);
            this.checkBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(425, 32);
            this.checkBox5.TabIndex = 11;
            this.checkBox5.Text = "Enable update checker (currently non-functional)";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.Location = new System.Drawing.Point(471, 119);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(433, 50);
            this.checkBox3.TabIndex = 12;
            this.checkBox3.Text = "Cleanup shadercache folder before cooking\r\n(may increase cooking time)";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.Location = new System.Drawing.Point(471, 63);
            this.checkBox6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(433, 48);
            this.checkBox6.TabIndex = 13;
            this.checkBox6.Text = "[experimental] Fast script cooking\r\n(do not re-cook maps when only scripts have b" +
    "een changed)";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.Location = new System.Drawing.Point(13, 175);
            this.checkBox7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(431, 89);
            this.checkBox7.TabIndex = 14;
            this.checkBox7.Text = "Enable Visual Studio Code integration\r\n- automatically creates VSC project for yo" +
    "ur mods\r\n- adds build tasks for OMM (CTRL+SHIFT+P)->Run Task\r\n- also adds \"Open " +
    "in VSCode\" context menu option for mods";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label1.Location = new System.Drawing.Point(33, 249);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Works best with this plugin";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.panel1.Location = new System.Drawing.Point(456, 46);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 378);
            this.panel1.TabIndex = 16;
            // 
            // checkBox8
            // 
            this.checkBox8.Location = new System.Drawing.Point(471, 222);
            this.checkBox8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(433, 42);
            this.checkBox8.TabIndex = 17;
            this.checkBox8.Text = "Kill game process before cooking with action";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox9
            // 
            this.checkBox9.Location = new System.Drawing.Point(471, 272);
            this.checkBox9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.Size = new System.Drawing.Size(433, 42);
            this.checkBox9.TabIndex = 18;
            this.checkBox9.Text = "Kill editor process before cooking with action";
            this.checkBox9.UseVisualStyleBackColor = true;
            // 
            // checkBox10
            // 
            this.checkBox10.Location = new System.Drawing.Point(471, 322);
            this.checkBox10.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(433, 42);
            this.checkBox10.TabIndex = 19;
            this.checkBox10.Text = "Make the Mafia Punch (tm) button also kill game instances";
            this.checkBox10.UseVisualStyleBackColor = true;
            // 
            // cuButton1
            // 
            this.cuButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cuButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cuButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cuButton1.ForeColor = System.Drawing.Color.White;
            this.cuButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cuButton1.Location = new System.Drawing.Point(200, 384);
            this.cuButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cuButton1.Name = "cuButton1";
            this.cuButton1.NoFocus = false;
            this.cuButton1.Size = new System.Drawing.Size(244, 37);
            this.cuButton1.TabIndex = 20;
            this.cuButton1.Text = "UPDATE NOW";
            this.cuButton1.UseVisualStyleBackColor = false;
            this.cuButton1.Visible = false;
            this.cuButton1.Click += new System.EventHandler(this.cuButton1_Click);
            // 
            // checkBox11
            // 
            this.checkBox11.Location = new System.Drawing.Point(471, 372);
            this.checkBox11.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox11.Name = "checkBox11";
            this.checkBox11.Size = new System.Drawing.Size(433, 55);
            this.checkBox11.TabIndex = 21;
            this.checkBox11.Text = "[experimental] AlwaysLoaded workaround\r\n(allow non-AlwaysLoaded classes to be ref" +
    "erenced along with making your own classes non-AlwaysLoaded)";
            this.checkBox11.UseVisualStyleBackColor = true;
            // 
            // checkBox12
            // 
            this.checkBox12.Location = new System.Drawing.Point(13, 272);
            this.checkBox12.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.Size = new System.Drawing.Size(400, 54);
            this.checkBox12.TabIndex = 22;
            this.checkBox12.Text = "Auto Workshop Blocker\r\n(requires restart and admin privileges)";
            this.checkBox12.UseVisualStyleBackColor = true;
            // 
            // ConfigWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 480);
            this.ControlBoxVisible = false;
            this.Controls.Add(this.checkBox12);
            this.Controls.Add(this.checkBox11);
            this.Controls.Add(this.cuButton1);
            this.Controls.Add(this.checkBox10);
            this.Controls.Add(this.checkBox9);
            this.Controls.Add(this.checkBox8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox7);
            this.Controls.Add(this.checkBox6);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox5);
            this.Controls.Add(this.checkBox4);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(915, 480);
            this.MinimumSize = new System.Drawing.Size(332, 479);
            this.Name = "ConfigWindow";
            this.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Text = "OMM Configuration";
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.checkBox2, 0);
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.Controls.SetChildIndex(this.checkBox4, 0);
            this.Controls.SetChildIndex(this.checkBox5, 0);
            this.Controls.SetChildIndex(this.checkBox3, 0);
            this.Controls.SetChildIndex(this.checkBox6, 0);
            this.Controls.SetChildIndex(this.checkBox7, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.checkBox8, 0);
            this.Controls.SetChildIndex(this.checkBox9, 0);
            this.Controls.SetChildIndex(this.checkBox10, 0);
            this.Controls.SetChildIndex(this.cuButton1, 0);
            this.Controls.SetChildIndex(this.checkBox11, 0);
            this.Controls.SetChildIndex(this.checkBox12, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CUButton mButton3;
        private CUButton mButton2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox checkBox10;
        private CUButton cuButton1;
        private System.Windows.Forms.CheckBox checkBox11;
        private System.Windows.Forms.CheckBox checkBox12;
    }
}