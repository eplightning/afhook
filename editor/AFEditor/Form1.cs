using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ColumnSorter;
using System.IO;
using System.Reflection;

namespace AFEditor
{
    public partial class Form1 : Form
    {
        private bool isOpened;
        private string openedFile;
        private CResourceManager resman;
        private ListViewColumnSorter lvwColumnSorter;
        private string pkgLocation;

        private UInt32 selectedDialogue;
        private UInt32 selectedUi;
        private PackageImage selectedImage;

        public Form1()
        {
            isOpened = false;
            openedFile = "";
            pkgLocation = "";
            selectedDialogue = 0;
            selectedUi = 0;
            selectedImage = null;

            lvwColumnSorter = new ListViewColumnSorter();
            
            InitializeComponent();

            this.listView1.ListViewItemSorter = lvwColumnSorter;
            this.listView3.ListViewItemSorter = lvwColumnSorter;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void openClick(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                if (isOpened)
                {
                    selectedDialogue = 0;
                    selectedUi = 0;
                    selectedImage = null;

                    resman.dialogue.Clear();
                    resman.ui.Clear();
                    resman.images.Clear();
                    listView1.Items.Clear();
                    listView3.Items.Clear();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox5.Clear();
                    textBox6.Clear();
                    textBoxTitle.Clear();
                    comboBox1.Items.Clear();

                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                    textBox5.ReadOnly = true;
                    textBox6.ReadOnly = true;
                    textBoxTitle.ReadOnly = true;
                    comboBox1.Enabled = false;
                    button1.Enabled = false;
                    button2.Enabled = false;

                    if (pictureBox2.Image != null)
                    {
                        pictureBox2.Image.Dispose();
                    }

                    if (pictureBox1.Image != null)
                    {
                        pictureBox1.Image.Dispose();
                    }

                    pictureBox1.Image = null;
                    pictureBox2.Image = null;
                }

                statusLabel.Text = "Loading package ...";

                resman = new CResourceManager(openFileDialog1.FileName);

                listView1.BeginUpdate();
                listView1.ListViewItemSorter = null; // THIS THING MADE EVERYTHING SO FUCKING SLOW
                foreach (KeyValuePair<UInt32, PackageText> kv in resman.dialogue)
                {
                    ListViewItem item = new ListViewItem(kv.Key.ToString());
                    item.SubItems.Add(kv.Value.sourceString);
                    item.SubItems.Add(kv.Value.translation);
                    item.SubItems.Add(kv.Value.translation.Length >= 1 ? "Yes" : "No");

                    item.Tag = kv.Key;

                    listView1.Items.Add(item);
                }
                listView1.EndUpdate();
                listView1.ListViewItemSorter = lvwColumnSorter;

                listView3.BeginUpdate();
                listView3.ListViewItemSorter = null; // THIS THING MADE EVERYTHING SO FUCKING SLOW
                foreach (KeyValuePair<UInt32, PackageText> kv in resman.ui)
                {
                    ListViewItem item = new ListViewItem(kv.Key.ToString());
                    item.SubItems.Add(kv.Value.sourceString);
                    item.SubItems.Add(kv.Value.translation);
                    item.SubItems.Add(kv.Value.translation.Length >= 1 ? "Yes" : "No");

                    item.Tag = kv.Key;

                    listView3.Items.Add(item);
                }
                listView3.EndUpdate();
                listView3.ListViewItemSorter = lvwColumnSorter;

                comboBox1.BeginUpdate();

                foreach (KeyValuePair<string, PackageImage> kv in resman.images)
                {
                    comboBox1.Items.Add(kv.Value);
                }

                comboBox1.EndUpdate();

                statusLabel.Text = "Loading done";

                isOpened = true;
                comboBox1.Enabled = true;
                openedFile = openFileDialog1.FileName;
                pkgLocation = Path.GetDirectoryName(openedFile);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?", "Question", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void savePackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isOpened) return;

            statusLabel.Text = "Saving package ...";

            resman.SavePackage(openedFile);

            statusLabel.Text = "Package saved";
        }

        private void savePackageAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isOpened) return;

            DialogResult res = saveFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                statusLabel.Text = "Saving package ...";
                resman.SavePackage(saveFileDialog1.FileName);
                statusLabel.Text = "Package saved";
            }
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView myListView = (ListView)sender;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            myListView.Sort();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;

                selectedDialogue = 0;

                return;
            }

            UInt32 selectedItem = (UInt32)listView1.SelectedItems[0].Tag;
            selectedDialogue = selectedItem;

            PackageText text = resman.dialogue[selectedItem];

            textBox1.Text = text.sourceString;
            textBox2.Text = text.translation;

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = false;
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count <= 0)
            {
                textBox5.Clear();
                textBox6.Clear();
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;

                selectedUi = 0;

                return;
            }

            UInt32 selectedItem = (UInt32)listView3.SelectedItems[0].Tag;
            selectedUi = selectedItem;

            PackageText text = resman.ui[selectedItem];

            textBox5.Text = text.sourceString;
            textBox6.Text = text.translation;

            textBox5.ReadOnly = true;
            textBox6.ReadOnly = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                textBoxTitle.Clear();
                textBoxTitle.ReadOnly = true;
                comboBox1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                checkBoxReplaced.Checked = false;
                selectedImage = null;
                return;
            }

            selectedImage = (PackageImage) comboBox1.SelectedItem;

            button1.Enabled = true;
            textBoxTitle.Text = selectedImage.title;
            textBoxTitle.ReadOnly = false;
            labelHash.Text = selectedImage.hash;
            labelSize.Text = selectedImage.width + " x " + selectedImage.height;

            if (File.Exists(Path.Combine(pkgLocation, Path.Combine("Dump", selectedImage.hash + ".png"))))
            {
                pictureBox1.Image = Image.FromFile(Path.Combine(pkgLocation, Path.Combine("Dump", selectedImage.hash + ".png")));
            }
            else
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                }

                pictureBox1.Image = null;
            }

            if (selectedImage.replacementLen > 0)
            {
                checkBoxReplaced.Checked = true;
                button2.Enabled = true;

                if ((selectedImage.flags & 0xFF) == 2 || (selectedImage.flags & 0xFF) == 3)
                {
                    Bitmap bm = new Bitmap(selectedImage.width, selectedImage.height);

                    CBitReader cr = new CBitReader(selectedImage.replacement);

                    switch (selectedImage.flags & 0xFF)
                    {
                        case 2:
                            CR6Ti.DecodeOpaque1(cr, bm, selectedImage.width, selectedImage.height, selectedImage.depth);
                            break;

                        case 3:
                            CR6Ti.DecodeTransparent(cr, bm, selectedImage.width, selectedImage.height, selectedImage.depth);
                            break;
                    }

                    pictureBox2.Image = bm;
                }
                else
                {
                    if (pictureBox2.Image != null)
                    {
                        pictureBox2.Image.Dispose();
                    }

                    pictureBox2.Image = null;
                }
            }
            else
            {
                checkBoxReplaced.Checked = false;

                if (pictureBox2.Image != null)
                {
                    pictureBox2.Image.Dispose();
                }

                pictureBox2.Image = null;
            }

            switch (selectedImage.flags & 0xFF)
            {
                case 2:
                    if ((selectedImage.depth & 0x4) != 0)
                    {
                        labelType.Text = "Opaque1";
                    }
                    else
                    {
                        labelType.Text = "Opaque2";
                    }
                    break;

                case 3:
                    labelType.Text = "Transparent";
                    break;

                default:
                    labelType.Text = "Unknown";
                    break;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (selectedDialogue != 0 && listView1.SelectedItems.Count > 0)
            {
                resman.dialogue[selectedDialogue].translation = textBox2.Text;
                listView1.SelectedItems[0].SubItems[2].Text = textBox2.Text;

                if (textBox2.TextLength > 0)
                {
                    listView1.SelectedItems[0].SubItems[3].Text = "Yes";
                }
                else
                {
                    listView1.SelectedItems[0].SubItems[3].Text = "No";
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (selectedUi != 0 && listView3.SelectedItems.Count > 0)
            {
                resman.ui[selectedUi].translation = textBox6.Text;
                listView3.SelectedItems[0].SubItems[2].Text = textBox6.Text;

                if (textBox6.TextLength > 0)
                {
                    listView3.SelectedItems[0].SubItems[3].Text = "Yes";
                }
                else
                {
                    listView3.SelectedItems[0].SubItems[3].Text = "No";
                }
            }
        }

        private void exporttxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isOpened) return;

            DialogResult res = saveFileDialog2.ShowDialog();

            if (res == DialogResult.OK)
            {
                TxtExport.Export(resman, saveFileDialog2.FileName);
            }
        }

        private void importtxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isOpened) return;

            DialogResult res = openFileDialog2.ShowDialog();

            if (res == DialogResult.OK)
            {
                selectedDialogue = 0;
                selectedUi = 0;

                listView1.Items.Clear();
                listView3.Items.Clear();
                textBox1.Clear();
                textBox2.Clear();
                textBox5.Clear();
                textBox6.Clear();

                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;

                int result = TxtExport.Import(resman, openFileDialog2.FileName, addsohAutomaticallyToolStripMenuItem.Checked);

                listView1.BeginUpdate();
                listView1.ListViewItemSorter = null; // THIS THING MADE EVERYTHING SO FUCKING SLOW
                foreach (KeyValuePair<UInt32, PackageText> kv in resman.dialogue)
                {
                    ListViewItem item = new ListViewItem(kv.Key.ToString());
                    item.SubItems.Add(kv.Value.sourceString);
                    item.SubItems.Add(kv.Value.translation);
                    item.SubItems.Add(kv.Value.translation.Length >= 1 ? "Yes" : "No");

                    item.Tag = kv.Key;

                    listView1.Items.Add(item);
                }
                listView1.EndUpdate();
                listView1.ListViewItemSorter = lvwColumnSorter;

                listView3.BeginUpdate();
                listView3.ListViewItemSorter = null; // THIS THING MADE EVERYTHING SO FUCKING SLOW
                foreach (KeyValuePair<UInt32, PackageText> kv in resman.ui)
                {
                    ListViewItem item = new ListViewItem(kv.Key.ToString());
                    item.SubItems.Add(kv.Value.sourceString);
                    item.SubItems.Add(kv.Value.translation);
                    item.SubItems.Add(kv.Value.translation.Length >= 1 ? "Yes" : "No");

                    item.Tag = kv.Key;

                    listView3.Items.Add(item);
                }
                listView3.EndUpdate();
                listView3.ListViewItemSorter = lvwColumnSorter;

                MessageBox.Show("Updated " + result + " records");
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.FocusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (selectedDialogue != 0)
            {
                resman.dialogue.Remove(selectedDialogue);
                listView1.FocusedItem.Remove();
                contextMenuStrip1.Hide();
                selectedDialogue = 0;
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (selectedUi != 0)
            {
                resman.ui.Remove(selectedUi);
                listView3.FocusedItem.Remove();
                contextMenuStrip2.Hide();
                selectedUi = 0;
            }
        }

        private void listView3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView3.FocusedItem.Bounds.Contains(e.Location))
                {
                    contextMenuStrip2.Show(Cursor.Position);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedImage == null)
            {
                return;
            }

            DialogResult res = openFileDialog3.ShowDialog();

            if (res == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(selectedImage.width, selectedImage.height);

                statusLabel.Text = "Resizing image and creating bitmap ...";

                using (Graphics g = Graphics.FromImage((Image) bitmap))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                    g.DrawImage(Image.FromFile(openFileDialog3.FileName), 0, 0, selectedImage.width, selectedImage.height);
                }

                MemoryStream stream = new MemoryStream();
                CBitWriter bw = new CBitWriter();

                switch (selectedImage.flags & 0xFF)
                {
                    case 2:
                        if ((selectedImage.depth & 0x4) != 0)
                        {
                            statusLabel.Text = "Encoding opaque image (might take a while) ...";
                            CR6Ti.WriteOpaque1(bitmap, bw, selectedImage.width, selectedImage.height);
                        }
                        else
                        {
                            throw new NotSupportedException("Writing Opaque2 images is not supported");
                        }
                        break;

                    case 3:
                        statusLabel.Text = "Encoding transparent image (might take a while) ...";
                        CR6Ti.WriteTransparent(bitmap, bw, selectedImage.width, selectedImage.height);
                        break;

                    default:
                        throw new NotSupportedException("Unknown image type");
                }

                statusLabel.Text = "Saving in byte format ...";
                bw.CopyToStream(stream);
                stream.Flush();

                selectedImage.replacement = stream.ToArray();

                selectedImage.replacementLen = (UInt32)stream.Length;

                checkBoxReplaced.Checked = true;
                button2.Enabled = true;

                stream.Dispose();
                stream.Close();
                bitmap.Dispose();

                if ((selectedImage.flags & 0xFF) == 2 || (selectedImage.flags & 0xFF) == 3)
                {
                    Bitmap bm = new Bitmap(selectedImage.width, selectedImage.height);

                    CBitReader cr = new CBitReader(selectedImage.replacement);

                    switch (selectedImage.flags & 0xFF)
                    {
                        case 2:
                            CR6Ti.DecodeOpaque1(cr, bm, selectedImage.width, selectedImage.height, selectedImage.depth);
                            break;

                        case 3:
                            CR6Ti.DecodeTransparent(cr, bm, selectedImage.width, selectedImage.height, selectedImage.depth);
                            break;
                    }

                    pictureBox2.Image = bm;
                }
                else
                {
                    if (pictureBox2.Image != null)
                    {
                        pictureBox2.Image.Dispose();
                    }

                    pictureBox2.Image = null;
                }

                statusLabel.Text = "Ready";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedImage == null || selectedImage.replacementLen <= 0)
            {
                return;
            }

            selectedImage.replacement = null;
            selectedImage.replacementLen = 0;

            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose();
                pictureBox2.Image = null;
            }

            checkBoxReplaced.Checked = false;
            button2.Enabled = false;
        }

        private void textBoxTitle_TextChanged(object sender, EventArgs e)
        {
            if (selectedImage == null)
            {
                return;
            }

            selectedImage.title = textBoxTitle.Text;
        }

        private void textBoxTitle_Leave(object sender, EventArgs e)
        {
            if (selectedImage == null)
            {
                return;
            }

            if (textBoxTitle.Text.Length <= 0)
            {
                textBoxTitle.Text = selectedImage.hash;
                selectedImage.title = selectedImage.hash;
            }

            typeof(ComboBox).InvokeMember("RefreshItems", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod, null, comboBox1, new object[] { });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (selectedImage == null || selectedImage.replacementLen <= 0)
            {
                return;
            }

            DialogResult res = saveFileDialog3.ShowDialog();

            if (res != DialogResult.OK)
            {
                return;
            }

            Bitmap bm = new Bitmap(selectedImage.width, selectedImage.height);

            CBitReader cr = new CBitReader(selectedImage.replacement);

            switch (selectedImage.flags & 0xFF)
            {
                case 2:
                    CR6Ti.DecodeOpaque1(cr, bm, selectedImage.width, selectedImage.height, selectedImage.depth);
                    break;

                case 3:
                    CR6Ti.DecodeTransparent(cr, bm, selectedImage.width, selectedImage.height, selectedImage.depth);
                    break;
            }

            bm.Save(saveFileDialog3.FileName);

            bm.Dispose();
        }
    }
}
