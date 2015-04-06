namespace AFEditor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listView1 = new System.Windows.Forms.ListView();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Japanese = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.English = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Translated = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.listView3 = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBoxReplaced = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelType = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelHash = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openPackageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePackageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePackageAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importtxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addsohAutomaticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.exporttxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog3 = new System.Windows.Forms.OpenFileDialog();
            this.button3 = new System.Windows.Forms.Button();
            this.saveFileDialog3 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.Japanese,
            this.English,
            this.Translated});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(8, 7);
            this.listView1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1005, 269);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // Id
            // 
            this.Id.Text = "Id";
            this.Id.Width = 43;
            // 
            // Japanese
            // 
            this.Japanese.Text = "Japanese";
            this.Japanese.Width = 300;
            // 
            // English
            // 
            this.English.Text = "English";
            this.English.Width = 338;
            // 
            // Translated
            // 
            this.Translated.Text = "Translated?";
            this.Translated.Width = 69;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(16, 33);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1033, 550);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.listView3_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox2);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(1025, 521);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Dialogue";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(8, 401);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(1005, 109);
            this.textBox2.TabIndex = 2;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 284);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(1005, 109);
            this.textBox1.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox6);
            this.tabPage3.Controls.Add(this.textBox5);
            this.tabPage3.Controls.Add(this.listView3);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1025, 521);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "User interface";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(8, 401);
            this.textBox6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox6.Multiline = true;
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(1005, 109);
            this.textBox6.TabIndex = 4;
            this.textBox6.TextChanged += new System.EventHandler(this.textBox6_TextChanged);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(8, 284);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(1005, 109);
            this.textBox5.TabIndex = 3;
            // 
            // listView3
            // 
            this.listView3.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listView3.FullRowSelect = true;
            this.listView3.Location = new System.Drawing.Point(8, 7);
            this.listView3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listView3.MultiSelect = false;
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(1005, 269);
            this.listView3.TabIndex = 1;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            this.listView3.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView3.SelectedIndexChanged += new System.EventHandler(this.listView3_SelectedIndexChanged);
            this.listView3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView3_MouseClick);
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Id";
            this.columnHeader5.Width = 43;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Japanese";
            this.columnHeader6.Width = 300;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "English";
            this.columnHeader7.Width = 338;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Translated?";
            this.columnHeader8.Width = 69;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.comboBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(1025, 521);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Images";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBox2);
            this.groupBox3.Location = new System.Drawing.Point(503, 277);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(512, 234);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Replacement";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(8, 22);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(496, 204);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Location = new System.Drawing.Point(503, 42);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(512, 234);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Original image";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(8, 23);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(496, 204);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.checkBoxReplaced);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBoxTitle);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.labelType);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.labelSize);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labelHash);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 41);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(485, 470);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Image properties";
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(316, 197);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(161, 28);
            this.button1.TabIndex = 0;
            this.button1.Text = "Replace image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(13, 197);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 28);
            this.button2.TabIndex = 2;
            this.button2.Text = "Restore";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBoxReplaced
            // 
            this.checkBoxReplaced.AutoSize = true;
            this.checkBoxReplaced.Enabled = false;
            this.checkBoxReplaced.Location = new System.Drawing.Point(128, 158);
            this.checkBoxReplaced.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxReplaced.Name = "checkBoxReplaced";
            this.checkBoxReplaced.Size = new System.Drawing.Size(18, 17);
            this.checkBoxReplaced.TabIndex = 9;
            this.checkBoxReplaced.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 158);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Replaced:";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(128, 118);
            this.textBoxTitle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.ReadOnly = true;
            this.textBoxTitle.Size = new System.Drawing.Size(348, 22);
            this.textBoxTitle.TabIndex = 7;
            this.textBoxTitle.TextChanged += new System.EventHandler(this.textBoxTitle_TextChanged);
            this.textBoxTitle.Leave += new System.EventHandler(this.textBoxTitle_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 122);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Title:";
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(124, 89);
            this.labelType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(67, 17);
            this.labelType.TabIndex = 5;
            this.labelType.Text = "Opaque1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 89);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Texture type:";
            // 
            // labelSize
            // 
            this.labelSize.AutoSize = true;
            this.labelSize.Location = new System.Drawing.Point(124, 57);
            this.labelSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(99, 17);
            this.labelSize.TabIndex = 3;
            this.labelSize.Text = "Width x Height";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 57);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Width x Height:";
            // 
            // labelHash
            // 
            this.labelHash.AutoSize = true;
            this.labelHash.Location = new System.Drawing.Point(124, 25);
            this.labelHash.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHash.Name = "labelHash";
            this.labelHash.Size = new System.Drawing.Size(41, 17);
            this.labelHash.TabIndex = 1;
            this.labelHash.Text = "Hash";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hash:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(8, 7);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(1005, 24);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openPackageToolStripMenuItem,
            this.savePackageToolStripMenuItem,
            this.savePackageAsToolStripMenuItem,
            this.importExportToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1065, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openPackageToolStripMenuItem
            // 
            this.openPackageToolStripMenuItem.Name = "openPackageToolStripMenuItem";
            this.openPackageToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+O";
            this.openPackageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openPackageToolStripMenuItem.Size = new System.Drawing.Size(117, 24);
            this.openPackageToolStripMenuItem.Text = "Open package";
            this.openPackageToolStripMenuItem.Click += new System.EventHandler(this.openClick);
            // 
            // savePackageToolStripMenuItem
            // 
            this.savePackageToolStripMenuItem.Name = "savePackageToolStripMenuItem";
            this.savePackageToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.savePackageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.savePackageToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.savePackageToolStripMenuItem.Text = "Save package";
            this.savePackageToolStripMenuItem.Click += new System.EventHandler(this.savePackageToolStripMenuItem_Click);
            // 
            // savePackageAsToolStripMenuItem
            // 
            this.savePackageAsToolStripMenuItem.Name = "savePackageAsToolStripMenuItem";
            this.savePackageAsToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.savePackageAsToolStripMenuItem.Text = "Save package as";
            this.savePackageAsToolStripMenuItem.Click += new System.EventHandler(this.savePackageAsToolStripMenuItem_Click);
            // 
            // importExportToolStripMenuItem
            // 
            this.importExportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importtxtToolStripMenuItem,
            this.addsohAutomaticallyToolStripMenuItem,
            this.toolStripMenuItem2,
            this.exporttxtToolStripMenuItem});
            this.importExportToolStripMenuItem.Name = "importExportToolStripMenuItem";
            this.importExportToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.importExportToolStripMenuItem.Text = "Import / Export";
            // 
            // importtxtToolStripMenuItem
            // 
            this.importtxtToolStripMenuItem.Name = "importtxtToolStripMenuItem";
            this.importtxtToolStripMenuItem.Size = new System.Drawing.Size(237, 24);
            this.importtxtToolStripMenuItem.Text = "Import .txt";
            this.importtxtToolStripMenuItem.Click += new System.EventHandler(this.importtxtToolStripMenuItem_Click);
            // 
            // addsohAutomaticallyToolStripMenuItem
            // 
            this.addsohAutomaticallyToolStripMenuItem.CheckOnClick = true;
            this.addsohAutomaticallyToolStripMenuItem.Name = "addsohAutomaticallyToolStripMenuItem";
            this.addsohAutomaticallyToolStripMenuItem.Size = new System.Drawing.Size(237, 24);
            this.addsohAutomaticallyToolStripMenuItem.Text = "Add {soh} automatically";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(234, 6);
            // 
            // exporttxtToolStripMenuItem
            // 
            this.exporttxtToolStripMenuItem.Name = "exporttxtToolStripMenuItem";
            this.exporttxtToolStripMenuItem.Size = new System.Drawing.Size(237, 24);
            this.exporttxtToolStripMenuItem.Text = "Export .txt";
            this.exporttxtToolStripMenuItem.Click += new System.EventHandler(this.exporttxtToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(45, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "pkg";
            this.openFileDialog1.Filter = "AFHook packages|*.pkg|All files|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "pkg";
            this.saveFileDialog1.Filter = "AFHook packages|*.pkg|All files|*.*";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.DefaultExt = "txt";
            this.openFileDialog2.Filter = "Exported text|*.txt|All files|*.*";
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.DefaultExt = "txt";
            this.saveFileDialog2.Filter = "Exported text|*.txt|All files|*.*";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 600);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1065, 25);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(50, 20);
            this.statusLabel.Text = "Ready";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 28);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(122, 24);
            this.toolStripMenuItem1.Text = "Delete";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(123, 28);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(122, 24);
            this.toolStripMenuItem3.Text = "Delete";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // openFileDialog3
            // 
            this.openFileDialog3.Filter = "PNG files|*.png|JPEG files|*.jpg";
            this.openFileDialog3.Title = "Select image";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 233);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(178, 28);
            this.button3.TabIndex = 10;
            this.button3.Text = "Export replaced as PNG";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // saveFileDialog3
            // 
            this.saveFileDialog3.DefaultExt = "png";
            this.saveFileDialog3.Filter = "PNG images|*.png|All files|*.*";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 625);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "AFHook Package Editor";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader Japanese;
        private System.Windows.Forms.ColumnHeader English;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ColumnHeader Translated;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem importExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importtxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem exporttxtToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.ToolStripMenuItem openPackageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePackageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePackageAsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripMenuItem addsohAutomaticallyToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog3;
        private System.Windows.Forms.Label labelHash;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxReplaced;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog3;
    }
}

