using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

using System.Diagnostics;
using System.IO;
using System.Timers;

using Vosk;

using NAudio;
using NAudio.Wave;

using TagLib;


namespace blendowavtool
{
    struct WavFileInfo
    {
        public string folderpath;
        public string filename;
        public int hz;
        public int channels;
        public float length;
        public float filesize;
        public string metadata;
    }

    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 0x000B;

        enum COLUMNS
        {
            folderpath,
            filename,
            hz,
            channels,
            length,
            filesize,
            metadata            
        };

        const int SILENCE_ANALYSIS_DURATION = 10000;
        const float SILENCE_THRESHOLD = 0.1f;

        WavFileInfo[] wavfileInfos;

        private static System.Timers.Timer aTimer;

        public Form1()
        {
            InitializeComponent();

            this.dataGridView1.DefaultCellStyle.Font = new Font("Consolas", 9);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.onDatagridviewDoubleclick);
            this.dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            this.textBox_filenamefilter.TextChanged += Textbox_displaytextFilter_TextChanged;
            this.textBox_folderfilter.TextChanged += TextBox_folderfilter_TextChanged;

            this.listBox1.DoubleClick += ListBox1_DoubleClick;


            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);


            //LoadSoundFolder(textBox1.Text);


            //This is to fix the problem of running the tool and then nothing happens for a few seconds, as that feels bad.
            //So now we immediately display the window, and then load the data after 100 millseconds.
            aTimer = new System.Timers.Timer(100);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            MethodInvoker mi = delegate () { LoadSoundFolder(textBox1.Text);  };
            this.Invoke(mi);            
        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        //Drag file into window.
        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            SetListboxColor(Color.White);

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            AddLog("~  ~  ~  ~  ~  ~  ~  ~  ~  ~  ~  ~  ~  ~  ~  ~  ~  ~  ~  ~");
            AddLog("Dragged in {0} files...", files.Length.ToString());

            //we now have a list of files that were dragged in.

            //now we iterate through them to find the files that we want to overwrite.

            string[] wavFiles = System.IO.Directory.GetFiles(textBox1.Text, "*.wav", System.IO.SearchOption.AllDirectories);

            int filesOverwritten = 0;
            int errors = 0;
            for (int i = 0; i < files.Length; i++)
            {
                FileAttributes attr =  System.IO.File.GetAttributes(files[i]);

                if (attr.HasFlag(FileAttributes.Directory))
                {
                    AddLog("ERROR: cannot parse folders ('{0}'). Only drag files here.", files[i]);
                    errors++;
                    continue;
                }


                FileInfo currentfile = new FileInfo(files[i]);

                int[] fileIndexes = FindExistingFile(currentfile.Name, wavFiles);

                if (fileIndexes.Length <= 0)
                {
                    //Error. CANNOT FIND THE FILE.
                    AddLog("ERROR: cannot find existing '{0}'", currentfile.Name);
                    errors++;
                }
                else if (fileIndexes.Length >= 2)
                {
                    //Error. MULTIPLE INSTANCES.
                    AddLog("ERROR: multiple instances of '{0}'", currentfile.Name);
                    SetListboxColor(Color.Pink);
                    for (int k = 0; k < fileIndexes.Length; k++)
                    {
                        int index = fileIndexes[k];
                        AddLog(">> {0}", wavFiles[index]);
                    }
                    errors++;
                }
                else
                {
                    //Success. Copy and overwrite the file.
                    int index = fileIndexes[0];
                    try
                    {
                        currentfile.CopyTo(wavFiles[index], true);
                    }
                    catch (Exception ee)
                    {
                        AddLog("ERROR: failed to copy file '{0}' (error: '{1}')", currentfile.Name, ee.Message);
                        errors++;
                        continue;
                    }

                    filesOverwritten++;
                }
            }

            if (errors > 0)
            {
                AddLog("============== {0} ERRORS (see above) ==============", errors.ToString());
                SetListboxColor(Color.Pink);
            }
            else
            {
                SetListboxColor(Color.GreenYellow);
            }

            AddLog("Done. {0} files overwritten.", filesOverwritten.ToString());            
        }

        int[] FindExistingFile(string filename, string[] allFiles)
        {
            List<int> indexes = new List<int>();
            for (int i = 0; i < allFiles.Length; i++)
            {
                FileInfo existingFile = new FileInfo(allFiles[i]);

                if (string.Compare(filename, existingFile.Name, true) == 0)
                {
                    //Match.
                    indexes.Add(i);
                }
            }

            return indexes.ToArray();
        }


        private void ListBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string strToCopy = listBox1.SelectedItem.ToString();
                CopyToClipboard(strToCopy, false);
            }
        }

        //detect ctrl+C
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.C))
            {
                if (listBox1.Focused)
                {
                    if (listBox1.SelectedItem != null)
                    {
                        string strToCopy = listBox1.SelectedItem.ToString();
                        CopyToClipboard(strToCopy, false);
                    }

                    return true;
                }

                //if multiple cells selected, do nothing.
                int selectedCount = dataGridView1.SelectedCells.Count;
                if (selectedCount > 1)
                    return false;

                if (dataGridView1.SelectedCells[0].ColumnIndex == (int)COLUMNS.filename && checkBox_copylocalizedstring.Checked)
                {
                    //if it's the filename column, there's some special logic if the localized checkbox is checked.
                                        
                    int column = dataGridView1.SelectedCells[0].ColumnIndex;
                    int row = dataGridView1.SelectedCells[0].RowIndex;
                    string stringToCopy = dataGridView1.Rows[row].Cells[column].Value.ToString();

                    if (stringToCopy.EndsWith(".wav"))
                    {
                        stringToCopy = stringToCopy.Substring(0, stringToCopy.Length - 4); //remove .wav file extension.
                    }

                    //add localization prefix.
                    stringToCopy = string.Format("#str_{0}", stringToCopy);

                    CopyToClipboard(stringToCopy);
                }
                else
                {
                    int column = dataGridView1.SelectedCells[0].ColumnIndex;
                    int row = dataGridView1.SelectedCells[0].RowIndex;
                    string stringToCopy = dataGridView1.Rows[row].Cells[column].Value.ToString();

                    CopyToClipboard(stringToCopy);
                }                

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void CopyToClipboard(string strToCopy, bool showLog = true)
        {
            try
            {
                Clipboard.SetText(strToCopy);

                if (showLog)
                {
                    AddLog("[COPIED TO CLIPBOARD]: {0}", strToCopy);
                }
            }
            catch
            {
                AddLog("ERROR: failed to copy to clipboard.");
                SetListboxColor(Color.Pink);
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //int selectedCount = dataGridView1.SelectedCells.Count;

            int selectedCount = dataGridView1.SelectedCells.Cast<DataGridViewCell>()
                                       .Select(c => c.RowIndex).Distinct().Count();

            label_selected.Text = string.Format("Selected: {0}", selectedCount);            
        }


        private void TextBox_folderfilter_TextChanged(object sender, EventArgs e)
        {
            RefreshGridView();
        }

        private void Textbox_displaytextFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshGridView();
        }

        private void RefreshGridView()
        {
            //disable redrawing of the datagrid.
            SendMessage(dataGridView1.Handle, WM_SETREDRAW, false, 0);

            string filenameFilter = textBox_filenamefilter.Text;
            string folderFilter = textBox_folderfilter.Text;

            //fill out the datagridview.
            dataGridView1.Rows.Clear();
            dataGridView1.ClearSelection();
            int count = 0;
            for (int i = 0; i < wavfileInfos.Count(); i++)
            {
                if (!FitsFilter(wavfileInfos[i].filename, filenameFilter))
                    continue;

                if (!FitsFilter(wavfileInfos[i].folderpath, folderFilter))
                    continue;

                float duration = (float)Math.Round(wavfileInfos[i].length, 1);
                if (duration <= 0)
                    duration = 0.1f;

                //Populate the data grid fields.
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[(int)COLUMNS.folderpath].Value = wavfileInfos[i].folderpath;
                row.Cells[(int)COLUMNS.filename].Value = wavfileInfos[i].filename;
                row.Cells[(int)COLUMNS.hz].Value = wavfileInfos[i].hz;
                row.Cells[(int)COLUMNS.channels].Value = wavfileInfos[i].channels;
                row.Cells[(int)COLUMNS.length].Value = duration;
                row.Cells[(int)COLUMNS.filesize].Value = wavfileInfos[i].filesize;
                row.Cells[(int)COLUMNS.metadata].Value = wavfileInfos[i].metadata;
                dataGridView1.Rows.Add(row);

                count++;
            }

            label_status.Text = string.Format("Total rows: {0}", count);

            //re-enable redrawing of the datagrid.
            SendMessage(dataGridView1.Handle, WM_SETREDRAW, true, 0);
            dataGridView1.Refresh();
        }

        private bool FitsFilter(string _text, string _filter)
        {
            if (string.IsNullOrWhiteSpace(_filter))
                return true;

            string[] filterArray = _filter.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < filterArray.Length; i++)
            {
                if (_text.IndexOf(filterArray[i], StringComparison.InvariantCultureIgnoreCase) < 0)
                    return false;
            }

            return true;
        }

        private void onDatagridviewDoubleclick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)COLUMNS.filename)
            {
                //click on filename.
                //try to play it.
                string folder = dataGridView1.Rows[e.RowIndex].Cells[(int)COLUMNS.folderpath].Value.ToString();
                string file = dataGridView1.Rows[e.RowIndex].Cells[(int)COLUMNS.filename].Value.ToString();

                bool success = true;
                FileInfo fullpath = new FileInfo(Path.Combine(folder, file));
                if (fullpath.Exists)
                {
                    try
                    {
                        Process.Start(fullpath.FullName);
                    }
                    catch
                    {
                        success = false;
                    }
                }
                else
                {
                    success = false;
                }

                if (!success)
                {
                    AddLog("Error: failed to play: {0}", fullpath.FullName);
                    SetListboxColor(Color.Pink);
                }
            }
            else if (e.ColumnIndex == (int)COLUMNS.folderpath)
            {
                //click on folder.
                string folder = dataGridView1.Rows[e.RowIndex].Cells[(int)COLUMNS.folderpath].Value.ToString();
                string file = dataGridView1.Rows[e.RowIndex].Cells[(int)COLUMNS.filename].Value.ToString();

                bool success = true;
                FileInfo fullpath = new FileInfo(Path.Combine(folder, file));
                if (fullpath.Exists)
                {
                    try
                    {
                        string argument = "/select, \"" + fullpath.FullName + "\"";
                        System.Diagnostics.Process.Start("explorer.exe", argument);
                    }
                    catch
                    {
                        success = false;
                    }
                }
                else
                {
                    success = false;
                }

                if (!success)
                {
                    AddLog("Error: failed to open file explorer to: {0}", fullpath.FullName);
                    SetListboxColor(Color.Pink);
                }
            }
            
        }

        void LoadSoundFolder(string folderpath)
        {
            DateTime start = DateTime.Now;

            DirectoryInfo dir = new DirectoryInfo(folderpath);
            if (!dir.Exists)
                return;

            string[] wavFiles = System.IO.Directory.GetFiles(folderpath, "*.wav", System.IO.SearchOption.AllDirectories);

            if (wavFiles.Length <= 0)
                return;

            
            AddLog(string.Format("Loading {0} ({1} wav files), please wait...", folderpath, wavFiles.Length));

            dataGridView1.Rows.Clear();
            dataGridView1.ClearSelection();

            List<WavFileInfo> wavlist = new List<WavFileInfo>();

            int filesLoaded = 0;
            for (int i = 0; i < wavFiles.Length; i++)
            {
                TagLib.File tagFile = TagLib.File.Create(wavFiles[i], ReadStyle.Average);

                if (tagFile == null)
                {
                    AddLog("Error: failed to load: {0}", wavFiles[i]);
                    continue;
                }

                if (tagFile.Properties == null)
                {
                    AddLog("Error: file has no properties: {0}", wavFiles[i]);
                    continue;
                }

                FileInfo file = new FileInfo(wavFiles[i]);

                string metaData = string.Empty;

                if (tagFile != null)
                {
                    if (tagFile.Tag.AlbumArtists.Length > 0 || !string.IsNullOrEmpty(tagFile.Tag.Title) || !string.IsNullOrEmpty(tagFile.Tag.Album) || tagFile.Tag.Track > 0)
                    {
                        string artists = string.Empty;
                        for (int k = 0; k < tagFile.Tag.AlbumArtists.Length; k++)
                        {
                            artists += tagFile.Tag.AlbumArtists[k];
                            if (k + 1 < tagFile.Tag.AlbumArtists.Length)
                                artists += ", ";
                        }

                        metaData = string.Format("Artists: '{0}' Title: '{1}' Track: {2} Album: '{3}'", artists, tagFile.Tag.Title, tagFile.Tag.Track, tagFile.Tag.Album);
                    }
                }

                float filesize = file.Length;
                filesize /= 1000000;
                filesize = (float)Math.Round(filesize, 2);

                if (filesize <= 0)
                    filesize = .01f;


                WavFileInfo newwavinfo;
                newwavinfo.folderpath = file.DirectoryName;
                newwavinfo.filename = file.Name;
                newwavinfo.hz = tagFile.Properties.AudioSampleRate;
                newwavinfo.channels = tagFile.Properties.AudioChannels;
                newwavinfo.length = (float)tagFile.Properties.Duration.TotalSeconds;
                newwavinfo.filesize = filesize;
                newwavinfo.metadata = metaData;
                wavlist.Add(newwavinfo);

                filesLoaded++;

                if (newwavinfo.hz > 44100)
                {
                    string finalpath = Path.Combine(file.DirectoryName, file.Name);
                    AddLog("ERROR: invalid hZ ({0}) for: {1}", newwavinfo.hz.ToString(), finalpath);
                }
            }

            //Store all the file infos into an array, so that we can filter them if we want to.
            wavfileInfos = wavlist.ToArray();

            RefreshGridView();

            TimeSpan delta = DateTime.Now.Subtract(start);
            float secondsLoadtime = delta.Milliseconds / (float)1000.0f;
            AddLog("Loaded {0} wav files. Load time: {1} seconds.", new string[] { filesLoaded.ToString(), secondsLoadtime.ToString("0.00") });
            AddLog(string.Empty);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadSoundFolder(textBox1.Text);
        }

        private void AddLog(string text, params string[] args)
        {
            string displaytext = string.Format(text, args);

            //if (!string.IsNullOrWhiteSpace(text))
            //{
            //    DateTime now = DateTime.Now;
            //    displaytext = string.Format("[{0}] {1}", now.ToString("h:mm tt"), displaytext);
            //}

            listBox1.Items.Add(displaytext);

            //scroll list down
            int nItems = (int)(listBox1.Height / listBox1.ItemHeight);
            listBox1.TopIndex = listBox1.Items.Count - nItems;

            this.Update();
            this.Refresh();
        }

        private void suffixIntegerDuplicateCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSoundFolder(textBox1.Text);
            RefreshGridView();

            //checking for duplicate suffix integers

            AddLog("================================== Checking for duplicate suffix numbers ==================================");

            List<int> suffixDupes = new List<int>();
            List <int> suffixList = new List<int>();
            for (int i = 0; i < wavfileInfos.Length; i++)
            {
                string filename = Path.GetFileNameWithoutExtension(wavfileInfos[i].filename);

                int finalUnderscoreIndex = filename.LastIndexOf("_");
                if (finalUnderscoreIndex < 0)
                    continue; //no underscore.

                //get suffix.
                string suffix = filename.Substring(finalUnderscoreIndex + 1, filename.Length - finalUnderscoreIndex - 1);

                if (suffix.Length <= 2)
                    continue;

                int resultInteger;
                if (int.TryParse(suffix, out resultInteger))
                {
                    //the suffix is entirely integer.
                    if (suffixList.Contains(resultInteger))
                    {
                        //this suffix ALREADY EXISTS.
                        if (!suffixDupes.Contains(resultInteger))
                            suffixDupes.Add(resultInteger);
                    }
                    else
                    {
                        suffixList.Add(resultInteger);
                    }
                }                
            }


            

            for (int k = 0; k < suffixDupes.Count; k++)
            {
                AddLog("SUFFIX: {0}", suffixDupes[k].ToString());

                for (int i = 0; i < wavfileInfos.Length; i++)
                {
                    string filename = Path.GetFileNameWithoutExtension(wavfileInfos[i].filename);

                    int finalUnderscoreIndex = filename.LastIndexOf("_");
                    if (finalUnderscoreIndex < 0)
                        continue; //no underscore.

                    //get suffix.
                    string suffix = filename.Substring(finalUnderscoreIndex + 1, filename.Length - finalUnderscoreIndex - 1);

                    int resultInteger;
                    if (int.TryParse(suffix, out resultInteger))
                    {
                        if (resultInteger == suffixDupes[k])
                        {
                            string fullpath = Path.Combine(wavfileInfos[i].folderpath, wavfileInfos[i].filename);
                            AddLog(fullpath);
                        }
                    }
                }

                AddLog(string.Empty);
            }

            AddLog("Found {0} duplicates.", suffixDupes.Count().ToString());

        }


        private void volkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vosk.Model model;
            VoskRecognizer rec = null;

            var dir = AppDomain.CurrentDomain.BaseDirectory;
            string modelpath = Path.Combine(dir, "model");

            bool fail = false;

            if (!Directory.Exists(modelpath))
            {
                fail = true;
            }
            else
            {
                string[] filecount = System.IO.Directory.GetFiles(modelpath, "*.*", System.IO.SearchOption.AllDirectories);
                if (filecount.Count() <= 0)
                    fail = true;
            }

            if (fail)
            {
                AddLog("****** Failed to load Vosk speech library ******");
                AddLog("Download a speech library from:");
                AddLog("https://alphacephei.com/vosk/models");
                AddLog("and copy it into:");
                AddLog(modelpath);
                AddLog(string.Empty);

                return;
            }

            DateTime start = DateTime.Now;
            AddLog("Starting Vosk speech recognition. THIS WILL TAKE A WHILE...");
            AddLog("(about a minute to initialize, and 1-2 seconds per file)");

            model = new Vosk.Model(modelpath);


            TimeSpan loadDelta = DateTime.Now.Subtract(start);
            float secondsLoadtime1 = (float)loadDelta.TotalSeconds;

            AddLog("Loaded Vosk model ({0} sec)", secondsLoadtime1.ToString("0.00"));
            rec = new VoskRecognizer(model, 22050.0f);
            rec.SetMaxAlternatives(0);
            rec.SetWords(true);



            //AddLog("Starting Vosk speech recognition. THIS MIGHT TAKE A WHILE.");
            //MessageBox.Show("Press ok to start Vosk speech recognition. THIS MIGHT TAKE A WHILE.");


            AddLog("Analyzing wav {0} files...", dataGridView1.RowCount.ToString());
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                DateTime fileStarttime = DateTime.Now;

                string filefolder = dataGridView1.Rows[i].Cells[(int)COLUMNS.folderpath].Value.ToString();
                string filename = dataGridView1.Rows[i].Cells[(int)COLUMNS.filename].Value.ToString();

                string path = Path.Combine(filefolder, filename);

                using (Stream source = System.IO.File.OpenRead(path))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (rec.AcceptWaveform(buffer, bytesRead))
                        {
                            //Console.WriteLine(rec.Result());
                        }
                        else
                        {
                            //Console.WriteLine(rec.PartialResult());
                        }
                    }
                }
                
                string output = rec.FinalResult();

                string[] rawLines = output.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int k = 0; k < rawLines.Count(); k++)
                {
                    if (rawLines[k].IndexOf("\"text\"") < 0)
                        continue;

                    string display = rawLines[k].Substring(11, rawLines[k].Length - 11);
                    dataGridView1.Rows[i].Cells[(int)COLUMNS.metadata].Value = display;
                    dataGridView1.Update();
                    dataGridView1.Refresh();
                }

                TimeSpan filedelta = DateTime.Now.Subtract(fileStarttime);
                float fileLoadtime = (float)filedelta.TotalSeconds;
                AddLog("File {0} done ({1}/{2}) (load time: {3} seconds)", new string[] { filename, (i+1).ToString(), dataGridView1.RowCount.ToString(), fileLoadtime.ToString("0.00") });
            }


            TimeSpan delta = DateTime.Now.Subtract(start);
            float secondsLoadtime = (float)delta.TotalSeconds;
            AddLog("Processed: {0}  Load time: {1} seconds.", new string[] { (dataGridView1.RowCount - 1).ToString(), secondsLoadtime.ToString("0.00") });

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (Directory.Exists(textBox1.Text))
            {
                folderBrowserDialog1.SelectedPath = textBox1.Text;
            }            

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
                LoadSoundFolder(textBox1.Text);
            }
        }

        //Find wav files that aren't used in the script.
        //NOTE: this ASSUMES the wav file and sound shader name are identical.
        private void findOrphanedWavFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string baseFolder = textBox1.Text;
            int baseIdx = baseFolder.IndexOf("/base/");
            if (baseIdx < 0)
            {
                baseIdx = baseFolder.IndexOf("\\base\\");
            }

            if (baseIdx < 0)
            {
                AddLog("ERROR: unable to extrapolate base folder from filepath: {0}", textBox1.Text);
                return;
            }

            DateTime start = DateTime.Now;

            baseFolder = baseFolder.Substring(0, baseIdx);
            baseFolder = Path.Combine(baseFolder, "base");


            //Hardcoded to vignette files....
            string[] fileArray = System.IO.Directory.GetFiles(baseFolder, "vig_*.script", System.IO.SearchOption.AllDirectories);

            AddLog("Scanning {0} script files...", fileArray.Length.ToString());

            if (fileArray.Length <= 0)
                return;

            List<string> allWavFilenames = new List<string>();
            for (int k = 0; k < dataGridView1.RowCount - 1; k++)
            {
                string filename = dataGridView1.Rows[k].Cells[(int)COLUMNS.filename].Value.ToString();
                if (filename.EndsWith(".wav"))
                {
                    filename = filename.Substring(0, filename.Length - 4); //remove .wav file extension.
                }

                allWavFilenames.Add(filename);
            }
                    

            for (int i = 0; i < fileArray.Length; i++)
            {
                using (StreamReader sr = new StreamReader(fileArray[i]))
                {
                    //Load entire file.
                    string contents = sr.ReadToEnd();

                    for (int k = allWavFilenames.Count - 1; k >= 0; k--)
                    {
                        if (contents.IndexOf(allWavFilenames[k], StringComparison.InvariantCultureIgnoreCase) >= 0)
                        {
                            allWavFilenames.RemoveAt(k); //FOund a match. Remove it from the list.
                        }
                    }
                }            
            }



            AddLog("------------- Found {0} files possibly not defined in script -------------", allWavFilenames.Count.ToString());
            allWavFilenames.Sort();

            for (int i = 0; i < allWavFilenames.Count; i++)
            {
                AddLog(allWavFilenames[i]);
            }

            TimeSpan loadDelta = DateTime.Now.Subtract(start);
            float secondsLoadtime1 = (float)loadDelta.TotalSeconds;
            AddLog("");
            AddLog("Done ({0} sec)", secondsLoadtime1.ToString("0.00"));
        }

        private void findLargestSuffixNumberInFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSoundFolder(textBox1.Text);
            RefreshGridView();

            //Find smallest and largest suffix number in this set of files.

            List<int> suffixList = new List<int>();
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                string filename = dataGridView1.Rows[i].Cells[(int)COLUMNS.filename].Value.ToString();
                if (filename.EndsWith(".wav"))
                {
                    filename = filename.Substring(0, filename.Length - 4); //remove .wav file extension.
                }

                int finalUnderscoreIndex = filename.LastIndexOf("_");
                if (finalUnderscoreIndex < 0)
                    continue; //no underscore.

                //get suffix.
                string suffix = filename.Substring(finalUnderscoreIndex + 1, filename.Length - finalUnderscoreIndex - 1);

                if (suffix.Length <= 2)
                    continue;

                int resultInteger;
                if (int.TryParse(suffix, out resultInteger))
                {
                    //the suffix is entirely integer.
                    if (!suffixList.Contains(resultInteger))
                    {
                        suffixList.Add(resultInteger);
                    }
                }
            }

            //Sort the integer values.
            suffixList.Sort();

            if (suffixList.Count <= 0)
            {
                AddLog("No suffix integers found.");
                return;
            }

            AddLog("-------- Found {0} strings with suffix integers --------", suffixList.Count.ToString());



            AddLog("Suffix gaps:");
            int currentIndex = suffixList[0];

            int gapStart = -1;
            int gapEnd = -1;

            for (int i = suffixList[0]; i < suffixList[suffixList.Count - 1]; i++)
            {
                currentIndex++;

                if (!suffixList.Contains(currentIndex))
                {
                    if (gapStart < 0)
                        gapStart = currentIndex;

                    gapEnd = currentIndex;
                }
                else if (gapStart > 0 && gapEnd > 0)
                {
                    AddLog(string.Format("{0} - {1}", gapStart.ToString(), gapEnd.ToString()));
                    gapStart = -1;
                    gapEnd = -1;
                }
            }


            //int currentIndex = suffixList[0];
            //for (int i = 0; i < suffixList.Count; i++)
            //{
            //    currentIndex++;
            //    if (suffixList.Contains(currentIndex))
            //        continue;
            //
            //    AddLog("{0}", currentIndex.ToString());
            //}

            AddLog(string.Empty);
            AddLog("Smallest:");
            AddLog("{0}", suffixList[0].ToString());
            AddLog("Largest:");
            AddLog("{0}", suffixList[suffixList.Count - 1].ToString());
            
            



            



        }

        private void copyWholeLogToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Blendo Wav Tool\nby Brendon Chung\n\nAudio asset helper tool. Used to browse, find, and hear audio assets.\n\n- double-click folder to open folder.\n- double-click file to play .wav file.\n- drag .wav files into window to automatically copy/overwrite existing .wav files in game folder. Will automatically find correct subfolders.",
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void SetListboxColor(Color newColor)
        {
            MethodInvoker mi = delegate () { listBox1.BackColor = newColor; };
            this.Invoke(mi);
        }

        private void fileDifferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filediffer newWindow = new filediffer(textBox1.Text);
            newWindow.Show();
        }

        private void copyAllDisplayedFilesIntoAFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLog("------------------- COPYING FILES... -------------------");

            string newFolderName = "BlendoWavTool_VO_Files";

            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), newFolderName);

            if (!System.IO.Directory.Exists(path))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                catch (IOException ee)
                {
                    Console.WriteLine("ERROR: {0}", ee.Message);
                }
                catch (Exception eee)
                {
                    Console.WriteLine("ERROR: {0}", eee.Message);
                }
            }

            int num = 0;
            List<string> errors = new List<string>();
            for (int k = 0; k < dataGridView1.RowCount - 1; k++)
            {
                string filefolder = dataGridView1.Rows[k].Cells[(int)COLUMNS.folderpath].Value.ToString();
                string filename = dataGridView1.Rows[k].Cells[(int)COLUMNS.filename].Value.ToString();

                string filepath = Path.Combine(filefolder, filename);
                FileInfo newfile = new FileInfo(filepath);

                if (!newfile.Exists)
                {
                    errors.Add(newfile.FullName);
                    continue;
                }

                try
                {
                    string destination = Path.Combine(path, filename);
                    newfile.CopyTo(destination, true);
                }
                catch (Exception ee)
                {
                    AddLog("ERROR: {0}", ee.Message);
                }

                num++;
            }

            AddLog("Done. Copied {0} files to:", num.ToString());
            AddLog(path);

            if (errors.Count > 0)
            {
                AddLog("");
                AddLog("ERRORS: {0}", errors.Count.ToString());
                for (int i = 0; i < errors.Count; i++)
                {
                    AddLog(errors[i]);
                }
                AddLog("");
                SetListboxColor(Color.Pink);
            }
        }

        private void displayMaxAmplitudesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLog("---- Determining max amplitudes for {0} files ----", dataGridView1.RowCount.ToString());

            float globalMax = -1;
            float globalMin = 1;

            for (int k = 0; k < dataGridView1.RowCount - 1; k++)
            {
                float max = 0;
                var inPath = Path.Combine(dataGridView1.Rows[k].Cells[(int)COLUMNS.folderpath].Value.ToString(), dataGridView1.Rows[k].Cells[(int)COLUMNS.filename].Value.ToString());
                using (var reader = new AudioFileReader(inPath))
                {
                    // find the max peak
                    float[] buffer = new float[reader.WaveFormat.SampleRate];
                    int read;
                    do
                    {
                        read = reader.Read(buffer, 0, buffer.Length);
                        for (int n = 0; n < read; n++)
                        {
                            var abs = Math.Abs(buffer[n]);
                            if (abs > max)
                            {
                                max = abs;
                            }
                        }
                    }
                    while (read > 0);

                    dataGridView1.Rows[k].Cells[(int)COLUMNS.metadata].Value = string.Format("Peak: {0}", max.ToString("N2"));

                    if (max > globalMax)
                        globalMax = max;

                    if (max < globalMin)
                        globalMin = max;
                }
            }

            AddLog("Done. Max={0} Min={1}", globalMax.ToString(), globalMin.ToString());
        }

        private void detectSilenceatFileStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLog("---- Determining start silence for {0} files, please wait... ----", dataGridView1.RowCount.ToString());

            int silenceCount = 0;
            for (int k = 0; k < dataGridView1.RowCount - 1; k++)
            {
                var inPath = Path.Combine(dataGridView1.Rows[k].Cells[(int)COLUMNS.folderpath].Value.ToString(), dataGridView1.Rows[k].Cells[(int)COLUMNS.filename].Value.ToString());
                using (var reader = new AudioFileReader(inPath))
                {
                    bool isSilent = true;

                    // find the max peak
                    float[] buffer = new float[reader.WaveFormat.SampleRate];
                    int read;
                    do
                    {
                        read = reader.Read(buffer, 0, buffer.Length);
                        read = Math.Min(read, SILENCE_ANALYSIS_DURATION);
                        for (int n = 0; n < read; n++)
                        {
                            if (Math.Abs(buffer[n]) >= SILENCE_THRESHOLD)
                            {
                                isSilent = false;
                                break;
                            }                            
                        }
                    }
                    while (read > 0);

                    dataGridView1.Rows[k].Cells[(int)COLUMNS.metadata].Value = string.Format("Start silence: {0}", isSilent ? "YES" : "NO");

                    if (isSilent)
                        silenceCount++;
                }
            }

            AddLog("Done. Silences found: {0}", silenceCount.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetListboxColor(Color.White);
            int selectedCount = dataGridView1.SelectedCells.Count;
            if (selectedCount <= 0)
            {
                SetListboxColor(Color.Pink);
                AddLog("ERROR: no cell is selected.");
                return;
            }

            int row = dataGridView1.SelectedCells[0].RowIndex;

            string filepath = Path.Combine(dataGridView1.Rows[row].Cells[(int)COLUMNS.folderpath].Value.ToString(), dataGridView1.Rows[row].Cells[(int)COLUMNS.filename].Value.ToString());

            if (!System.IO.File.Exists(filepath))
            {
                SetListboxColor(Color.Pink);
                AddLog("ERROR: cannot find file {0}", filepath);
                return;
            }

            string wavEditorPath = string.Empty;
            try
            {
                wavEditorPath = GetFileContents("wav_editor_path.txt");
            }
            catch (Exception ee)
            {
                AddLog("ERROR: {0}", ee.Message);
            }

            wavEditorPath = wavEditorPath.Trim();

            if (string.IsNullOrWhiteSpace(wavEditorPath))
            {
                AddLog("ERROR: 'wav_editor_path.txt' is empty.");
                SetListboxColor(Color.Pink);
                return;
            }

            if (!System.IO.File.Exists(wavEditorPath))
            {
                AddLog("ERROR: Cannot find wav editor {0} (Please check your wav_editor_path.txt file)", wavEditorPath);
                SetListboxColor(Color.Pink);

                //for convenience, open the text file.
                try
                {
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.FileName = "wav_editor_path.txt";
                    Process.Start(info);
                }
                catch (Exception ee)
                {
                    AddLog("ERROR: {0}", ee.Message);
                }

                return;
            }

            try
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName = wavEditorPath;
                info.Arguments = filepath;
                Process.Start(info);

                AddLog("[Open in editor] {0} {1}", info.FileName, info.Arguments);
            }
            catch (Exception ee)
            {
                AddLog("ERROR: {0}", ee.Message);
            }
        }

        private string GetFileContents(string filepath)
        {
            string output = string.Empty;

            try
            {
                using (FileStream stream = System.IO.File.Open(filepath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        output = reader.ReadToEnd(); //dump file contents into a string.
                    }
                }
            }
            catch (Exception e)
            {
                AddLog("ERROR: problem reading text file ({0})", e.Message);
                SetListboxColor(Color.Pink);
                return string.Empty;
            }

            return output;
        }

        private void detectSilenceatFileEndToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddLog("---- Determining end silence for {0} files, please wait... ----", dataGridView1.RowCount.ToString());

            int silenceCount = 0;
            for (int k = 0; k < dataGridView1.RowCount - 1; k++)
            {
                var inPath = Path.Combine(dataGridView1.Rows[k].Cells[(int)COLUMNS.folderpath].Value.ToString(), dataGridView1.Rows[k].Cells[(int)COLUMNS.filename].Value.ToString());
                using (var reader = new AudioFileReader(inPath))
                {
                    bool isSilent = true;

                    // find the max peak
                    float[] buffer = new float[reader.WaveFormat.SampleRate];
                    int read;
                    do
                    {
                        read = reader.Read(buffer, 0, buffer.Length);
                        int startReadValue = Math.Max(read - SILENCE_ANALYSIS_DURATION, 0);
                        for (int n = startReadValue; n < read; n++)
                        {
                            if (Math.Abs(buffer[n]) >= SILENCE_THRESHOLD)
                            {
                                isSilent = false;
                                break;
                            }
                        }
                    }
                    while (read > 0);

                    dataGridView1.Rows[k].Cells[(int)COLUMNS.metadata].Value = string.Format("End silence: {0}", isSilent ? "YES" : "NO");

                    if (isSilent)
                        silenceCount++;
                }
            }

            AddLog("Done. Silences found: {0}", silenceCount.ToString());
        }
    }
}
