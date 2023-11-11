using System.Windows.Forms;

namespace ModdingTools.Windows
{
    partial class ArrayCheckboxWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArrayCheckboxWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.cuButton2 = new CUFramework.Controls.CUButton();
            this.cuButton1 = new CUFramework.Controls.CUButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.borderPanel1 = new ModdingTools.GUI.BorderPanel();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.borderPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(19, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(428, 117);
            this.label1.TabIndex = 6;
            this.label1.Text = "Title T";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cuButton2
            // 
            this.cuButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.cuButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cuButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cuButton2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(128)))));
            this.cuButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cuButton2.ForeColor = System.Drawing.Color.White;
            this.cuButton2.Location = new System.Drawing.Point(4, 4);
            this.cuButton2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cuButton2.Name = "cuButton2";
            this.cuButton2.NoFocus = false;
            this.cuButton2.Size = new System.Drawing.Size(222, 39);
            this.cuButton2.TabIndex = 3;
            this.cuButton2.Text = "CANCEL";
            this.cuButton2.UseVisualStyleBackColor = false;
            this.cuButton2.Click += new System.EventHandler(this.cuButton2_Click);
            // 
            // cuButton1
            // 
            this.cuButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(196)))), ((int)(((byte)(0)))));
            this.cuButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cuButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.cuButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cuButton1.ForeColor = System.Drawing.Color.Black;
            this.cuButton1.Location = new System.Drawing.Point(234, 4);
            this.cuButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cuButton1.Name = "cuButton1";
            this.cuButton1.NoFocus = false;
            this.cuButton1.Size = new System.Drawing.Size(223, 39);
            this.cuButton1.TabIndex = 2;
            this.cuButton1.Text = "OK";
            this.cuButton1.UseVisualStyleBackColor = false;
            this.cuButton1.Click += new System.EventHandler(this.cuButton1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.cuButton2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cuButton1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 549);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(461, 47);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 154);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(85, 20);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Select All";
            this.checkBox1.ThreeState = true;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.checkBox1_Click);
            // 
            // borderPanel1
            // 
            this.borderPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.borderPanel1.BorderThickness = 2;
            this.borderPanel1.Controls.Add(this.checkedListBox1);
            this.borderPanel1.ForeColor = System.Drawing.Color.White;
            this.borderPanel1.Location = new System.Drawing.Point(5, 175);
            this.borderPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.borderPanel1.Name = "borderPanel1";
            this.borderPanel1.Size = new System.Drawing.Size(455, 367);
            this.borderPanel1.TabIndex = 7;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.ForeColor = System.Drawing.Color.White;
            this.checkedListBox1.FormatString = "peck";
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "checkListBox"});
            this.checkedListBox1.Location = new System.Drawing.Point(3, 4);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(449, 340);
            this.checkedListBox1.TabIndex = 0;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // ArrayCheckboxWindow
            // 
            this.AcceptButton = this.cuButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cuButton2;
            this.ClientSize = new System.Drawing.Size(467, 598);
            this.ControlBoxVisible = false;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.borderPanel1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsResizable = false;
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximumSize = new System.Drawing.Size(1536, 825);
            this.Name = "ArrayCheckboxWindow";
            this.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Text = "ArrayCheckboxWindow";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.borderPanel1, 0);
            this.Controls.SetChildIndex(this.tableLayoutPanel1, 0);
            this.Controls.SetChildIndex(this.checkBox1, 0);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.borderPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private GUI.BorderPanel borderPanel1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private CUFramework.Controls.CUButton cuButton2;
        private CUFramework.Controls.CUButton cuButton1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
