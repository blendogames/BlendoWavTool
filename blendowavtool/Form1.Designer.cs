namespace blendowavtool
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox_filenamefilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_folderfilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label_status = new System.Windows.Forms.Label();
            this.label_selected = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyWholeLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.suffixIntegerDuplicateCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.volkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findOrphanedWavFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findLargestSuffixNumberInFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileDifferToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllDisplayedFilesIntoAFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayMaxAmplitudesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detectSilenceatFileStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detectSilenceatFileEndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox_copylocalizedstring = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column7,
            this.Column6});
            this.dataGridView1.Location = new System.Drawing.Point(7, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1226, 487);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.TabStop = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Folder path";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 400;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Filename";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 300;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Hz";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 50;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Channels";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 60;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Duration";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 50;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Filesize";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 60;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.HeaderText = "Metadata";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(13, 38);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1067, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "D:\\games\\monstergame\\base\\sound";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1086, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 22);
            this.button1.TabIndex = 2;
            this.button1.TabStop = false;
            this.button1.Text = "Reload folder";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(3, 4);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1230, 74);
            this.listBox1.TabIndex = 3;
            this.listBox1.TabStop = false;
            // 
            // textBox_filenamefilter
            // 
            this.textBox_filenamefilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_filenamefilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBox_filenamefilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_filenamefilter.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_filenamefilter.Location = new System.Drawing.Point(717, 65);
            this.textBox_filenamefilter.Name = "textBox_filenamefilter";
            this.textBox_filenamefilter.Size = new System.Drawing.Size(534, 32);
            this.textBox_filenamefilter.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(638, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Filename filter:";
            // 
            // textBox_folderfilter
            // 
            this.textBox_folderfilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_folderfilter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBox_folderfilter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBox_folderfilter.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_folderfilter.Location = new System.Drawing.Point(79, 65);
            this.textBox_folderfilter.Name = "textBox_folderfilter";
            this.textBox_folderfilter.Size = new System.Drawing.Size(512, 32);
            this.textBox_folderfilter.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Folder filter:";
            // 
            // label_status
            // 
            this.label_status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_status.Location = new System.Drawing.Point(3, 495);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(85, 20);
            this.label_status.TabIndex = 8;
            this.label_status.Text = "Total rows:";
            // 
            // label_selected
            // 
            this.label_selected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_selected.AutoSize = true;
            this.label_selected.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_selected.Location = new System.Drawing.Point(233, 495);
            this.label_selected.Name = "label_selected";
            this.label_selected.Size = new System.Drawing.Size(76, 20);
            this.label_selected.TabIndex = 9;
            this.label_selected.Text = "Selected:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyWholeLogToolStripMenuItem,
            this.clearLogToolStripMenuItem,
            this.toolStripSeparator1,
            this.suffixIntegerDuplicateCheckToolStripMenuItem,
            this.volkToolStripMenuItem,
            this.findOrphanedWavFilesToolStripMenuItem,
            this.findLargestSuffixNumberInFolderToolStripMenuItem,
            this.fileDifferToolStripMenuItem,
            this.copyAllDisplayedFilesIntoAFolderToolStripMenuItem,
            this.displayMaxAmplitudesToolStripMenuItem,
            this.detectSilenceatFileStartToolStripMenuItem,
            this.detectSilenceatFileEndToolStripMenuItem,
            this.toolStripSeparator2,
            this.aboutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // copyWholeLogToolStripMenuItem
            // 
            this.copyWholeLogToolStripMenuItem.Name = "copyWholeLogToolStripMenuItem";
            this.copyWholeLogToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.copyWholeLogToolStripMenuItem.Text = "Copy whole log";
            this.copyWholeLogToolStripMenuItem.Click += new System.EventHandler(this.copyWholeLogToolStripMenuItem_Click);
            // 
            // clearLogToolStripMenuItem
            // 
            this.clearLogToolStripMenuItem.Name = "clearLogToolStripMenuItem";
            this.clearLogToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.clearLogToolStripMenuItem.Text = "Clear log";
            this.clearLogToolStripMenuItem.Click += new System.EventHandler(this.clearLogToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(219, 6);
            // 
            // suffixIntegerDuplicateCheckToolStripMenuItem
            // 
            this.suffixIntegerDuplicateCheckToolStripMenuItem.Name = "suffixIntegerDuplicateCheckToolStripMenuItem";
            this.suffixIntegerDuplicateCheckToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.suffixIntegerDuplicateCheckToolStripMenuItem.Text = "Suffix duplicate check";
            this.suffixIntegerDuplicateCheckToolStripMenuItem.Click += new System.EventHandler(this.suffixIntegerDuplicateCheckToolStripMenuItem_Click);
            // 
            // volkToolStripMenuItem
            // 
            this.volkToolStripMenuItem.Name = "volkToolStripMenuItem";
            this.volkToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.volkToolStripMenuItem.Text = "Vosk speech recognition";
            this.volkToolStripMenuItem.Click += new System.EventHandler(this.volkToolStripMenuItem_Click);
            // 
            // findOrphanedWavFilesToolStripMenuItem
            // 
            this.findOrphanedWavFilesToolStripMenuItem.Name = "findOrphanedWavFilesToolStripMenuItem";
            this.findOrphanedWavFilesToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.findOrphanedWavFilesToolStripMenuItem.Text = "Compare wav files to scripts";
            this.findOrphanedWavFilesToolStripMenuItem.Click += new System.EventHandler(this.findOrphanedWavFilesToolStripMenuItem_Click);
            // 
            // findLargestSuffixNumberInFolderToolStripMenuItem
            // 
            this.findLargestSuffixNumberInFolderToolStripMenuItem.Name = "findLargestSuffixNumberInFolderToolStripMenuItem";
            this.findLargestSuffixNumberInFolderToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.findLargestSuffixNumberInFolderToolStripMenuItem.Text = "Scan suffix numbers";
            this.findLargestSuffixNumberInFolderToolStripMenuItem.Click += new System.EventHandler(this.findLargestSuffixNumberInFolderToolStripMenuItem_Click);
            // 
            // fileDifferToolStripMenuItem
            // 
            this.fileDifferToolStripMenuItem.Name = "fileDifferToolStripMenuItem";
            this.fileDifferToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.fileDifferToolStripMenuItem.Text = "File Differ";
            this.fileDifferToolStripMenuItem.Click += new System.EventHandler(this.fileDifferToolStripMenuItem_Click);
            // 
            // copyAllDisplayedFilesIntoAFolderToolStripMenuItem
            // 
            this.copyAllDisplayedFilesIntoAFolderToolStripMenuItem.Name = "copyAllDisplayedFilesIntoAFolderToolStripMenuItem";
            this.copyAllDisplayedFilesIntoAFolderToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.copyAllDisplayedFilesIntoAFolderToolStripMenuItem.Text = "Copy all files into a folder";
            this.copyAllDisplayedFilesIntoAFolderToolStripMenuItem.Click += new System.EventHandler(this.copyAllDisplayedFilesIntoAFolderToolStripMenuItem_Click);
            // 
            // displayMaxAmplitudesToolStripMenuItem
            // 
            this.displayMaxAmplitudesToolStripMenuItem.Name = "displayMaxAmplitudesToolStripMenuItem";
            this.displayMaxAmplitudesToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.displayMaxAmplitudesToolStripMenuItem.Text = "Display Max Amplitudes";
            this.displayMaxAmplitudesToolStripMenuItem.Click += new System.EventHandler(this.displayMaxAmplitudesToolStripMenuItem_Click);
            // 
            // detectSilenceatFileStartToolStripMenuItem
            // 
            this.detectSilenceatFileStartToolStripMenuItem.Name = "detectSilenceatFileStartToolStripMenuItem";
            this.detectSilenceatFileStartToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.detectSilenceatFileStartToolStripMenuItem.Text = "Detect silence (at file start)";
            this.detectSilenceatFileStartToolStripMenuItem.Click += new System.EventHandler(this.detectSilenceatFileStartToolStripMenuItem_Click);
            // 
            // detectSilenceatFileEndToolStripMenuItem
            // 
            this.detectSilenceatFileEndToolStripMenuItem.Name = "detectSilenceatFileEndToolStripMenuItem";
            this.detectSilenceatFileEndToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.detectSilenceatFileEndToolStripMenuItem.Text = "Detect silence (at file end)";
            this.detectSilenceatFileEndToolStripMenuItem.Click += new System.EventHandler(this.detectSilenceatFileEndToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(219, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(222, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(15, 105);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.button3);
            this.splitContainer1.Panel1.Controls.Add(this.checkBox_copylocalizedstring);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label_status);
            this.splitContainer1.Panel1.Controls.Add(this.label_selected);
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1236, 611);
            this.splitContainer1.SplitterDistance = 518;
            this.splitContainer1.TabIndex = 11;
            this.splitContainer1.TabStop = false;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(389, 495);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(222, 23);
            this.button3.TabIndex = 12;
            this.button3.TabStop = false;
            this.button3.Text = "Edit .wav file";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBox_copylocalizedstring
            // 
            this.checkBox_copylocalizedstring.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_copylocalizedstring.AutoSize = true;
            this.checkBox_copylocalizedstring.Location = new System.Drawing.Point(969, 500);
            this.checkBox_copylocalizedstring.Name = "checkBox_copylocalizedstring";
            this.checkBox_copylocalizedstring.Size = new System.Drawing.Size(268, 17);
            this.checkBox_copylocalizedstring.TabIndex = 11;
            this.checkBox_copylocalizedstring.TabStop = false;
            this.checkBox_copylocalizedstring.Text = "add #str_ prefix when copying filename to clipboard";
            this.checkBox_copylocalizedstring.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(699, 497);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(242, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "ctrl+c to copy selection to clipboard";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(1172, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 22);
            this.button2.TabIndex = 12;
            this.button2.TabStop = false;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(609, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "empty space = AND";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 729);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_folderfilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_filenamefilter);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blendo Wav Tool";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox_filenamefilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_folderfilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Label label_selected;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suffixIntegerDuplicateCheckToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem volkToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox_copylocalizedstring;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem findOrphanedWavFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findLargestSuffixNumberInFolderToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem copyWholeLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileDifferToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAllDisplayedFilesIntoAFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem displayMaxAmplitudesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detectSilenceatFileStartToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripMenuItem detectSilenceatFileEndToolStripMenuItem;
    }
}

