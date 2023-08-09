//JLINT-ADD: Class for selecting items with checkboxes. Hard coded to work with ModClass.
using CUFramework.Dialogs;
using CUFramework.Dialogs.Validators;
using CUFramework.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModdingTools.Engine;
using System.Linq;

namespace ModdingTools.Windows
{
    public partial class ArrayCheckboxWindow : CUFramework.Windows.CUWindow
    {
        public bool AllowDuplicates { get; set; }

        public ArrayCheckboxWindow()
        {
            InitializeComponent();
        }

        public static List<object> Ask(string title, string desc, object[] defaultItems)
        {
            var a = new ArrayCheckboxWindow();
            a.label1.Text = desc;
            a.Text = title;
            a.checkedListBox1.Items.Clear();
            a.checkedListBox1.ValueMember = "ClassName";
            a.checkBox1.Checked = true;
            if (defaultItems != null)
                foreach (var i in defaultItems)
                    a.checkedListBox1.Items.Add(i, true);
                
            var result = a.ShowDialog();
            if (result == DialogResult.OK)
            {
                var res = new List<object>();
                foreach (var i in a.checkedListBox1.CheckedItems)
                    res.Add(i);
                return res;
            }
            return defaultItems.ToList();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            Object Item = checkedListBox1.Items[e.Index];

            if (Item is ModClass && ((ModClass)Item).ClassType != ModClass.ModClassType.Generic && e.NewValue == CheckState.Unchecked)
            {
                e.NewValue = e.CurrentValue;
                //checkedListBox1.SetItemCheckState(e.Index, e.CurrentValue);
                CUMessageBox.Show( ((ModClass)Item).ClassName + " should be AlwaysLoaded because it is a " + ((ModClass)Item).ClassType.ToString() + "!" );

                return;
            }

            var CheckedLengthChange = checkedListBox1.CheckedItems.Count + (e.NewValue == CheckState.Checked ? 1 : -1);

            if (CheckedLengthChange >= checkedListBox1.Items.Count)
                checkBox1.CheckState = CheckState.Checked;
            else if (CheckedLengthChange <= 0)
                checkBox1.CheckState = CheckState.Unchecked;
            else
                checkBox1.CheckState = CheckState.Indeterminate;
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            var SelectAll = checkBox1.CheckState == CheckState.Checked;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, SelectAll);

            if (checkBox1.CheckState == CheckState.Indeterminate)
                checkBox1.CheckState = CheckState.Unchecked;
        }

        private void cuButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cuButton1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
