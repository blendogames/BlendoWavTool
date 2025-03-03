using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace blendowavtool
{
    public partial class filediffer : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 0x000B;

        string folderPath;

        public filediffer(string _folderpath)
        {
            InitializeComponent();

            this.listBox1.DoubleClick += ListBox1_DoubleClick;

            this.textBox_pastedvalues.TextChanged += Textbox_pastedvalues_TextChanged;

            if (!Directory.Exists(_folderpath))
            {
                AddLog("ERROR: folder {0} does not exist.", _folderpath);
                button1.Enabled = false;
                return;
            }

            folderPath = _folderpath;
            AddLog("File search path is: {0}", folderPath);
        }

        private void Textbox_pastedvalues_TextChanged(object sender, EventArgs e)
        {
            //Clean up the values to remove blank lines.

            string output = "";
            string[] rawLines = textBox_pastedvalues.Text.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < rawLines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(rawLines[i]))
                    continue;

                output += rawLines[i] + Environment.NewLine;
            }

            textBox_pastedvalues.Text = output;

            //scroll to end
            //if (!string.IsNullOrWhiteSpace(textBox_pastedvalues.Text))
            //{
            //    textBox_pastedvalues.Select(textBox_pastedvalues.Text.Length - 1, 0);
            //    textBox_pastedvalues.ScrollToCaret();
            //}

            button1_Click(null, null);
        }

        private void AddLog(string text, params string[] args)
        {
            string displaytext = string.Format(text, args);

            listBox1.Items.Add(displaytext);

            //scroll list down
            int nItems = (int)(listBox1.Height / listBox1.ItemHeight);
            listBox1.TopIndex = listBox1.Items.Count - nItems;

            this.Update();
            this.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> badFiles = new List<string>();

            //all .wav files in the game.
            string[] allfiles = System.IO.Directory.GetFiles(folderPath, "*.wav", System.IO.SearchOption.AllDirectories);
            for (int i = 0; i < allfiles.Length; i++)
            {
                FileInfo newfile = new FileInfo(allfiles[i]);

                allfiles[i] = newfile.Name;
                allfiles[i] = allfiles[i].ToLowerInvariant();

                if (newfile.Extension.Length <= 0)
                {
                    badFiles.Add(newfile.Name);
                }
            }

            //values pasted in the text box.
            string[] pastedValues = textBox_pastedvalues.Text.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);


            List<string> missingValues = new List<string>();
            for (int i = 0; i < pastedValues.Length; i++)
            {
                if (!pastedValues[i].EndsWith(".wav"))
                {
                    //this entry doesn't have a .wav file extension, so just skip it....
                    badFiles.Add(pastedValues[i]);
                    continue;
                }

                if (Array.IndexOf(allfiles, pastedValues[i]) >= 0)
                {
                    //Found it.
                }
                else
                {
                    missingValues.Add(pastedValues[i]);
                }
            }

            SendMessage(listBox1.Handle, WM_SETREDRAW, false, 0);

            AddLog("");
            AddLog("------------ UNABLE TO FIND {0} FILES (OUT OF {1}) ------------", missingValues.Count.ToString(), pastedValues.Length.ToString());
            AddLog("");

            if (missingValues.Count > 0)
            {
                AddLog("Missing files:");
                missingValues.Sort();
                for (int i = 0; i < missingValues.Count; i++)
                {
                    AddLog(missingValues[i]);
                }
            }

            if (badFiles.Count > 0)
            {
                AddLog("");

                AddLog("- {0} POSSIBLE BAD FILE NAMES -", badFiles.Count.ToString());
                badFiles.Sort();
                for (int i = 0; i < badFiles.Count; i++)
                {
                    AddLog(badFiles[i]);
                }
            }

            SendMessage(listBox1.Handle, WM_SETREDRAW, true, 0);
            listBox1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string output = string.Empty;

            foreach (object item in listBox1.Items)
            {
                output += item.ToString() + "\r\n";
            }

            if (string.IsNullOrWhiteSpace(output))
            {
                AddLog(string.Empty);
                AddLog("No log found.");
                return;
            }

            Clipboard.SetText(output);

            AddLog(string.Empty);
            AddLog("Copied entire log to clipboard ({0} lines).", listBox1.Items.Count.ToString());
        }

        private void ListBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string strToCopy = listBox1.SelectedItem.ToString();
                CopyToClipboard(strToCopy);
            }
        }

        void CopyToClipboard(string strToCopy)
        {
            try
            {
                Clipboard.SetText(strToCopy);
            }
            catch
            {
                AddLog("ERROR: failed to copy to clipboard.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string output = "";
            for (int i = 0; i < listBox1.SelectedItems.Count; i++)
            {
                output += listBox1.SelectedItems[i].ToString() + Environment.NewLine;
            }

            CopyToClipboard(output);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
