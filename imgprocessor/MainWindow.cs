using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;

using System.Windows.Forms;

namespace imgprocessor
{
    public partial class MainWindow : Form
    {
        public List<string> NamesTemplates = new List<string>();
        public BindingList<ModSetting> Templates;
        public List<string> PathsOfPictures = new List<string>();
        private EditEnum ActEdit = EditEnum.NoEdit;
        private Color ActColor = Color.Empty;
        public PictureHolder PHolder = new PictureHolder();
        public ModSetting MS = new ModSetting();

        public MainWindow()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            Templates = AppCoreLib.IO.XMLTemplates.Load(checkedListBox1, NamesTemplates);
            dataGridView1.DataSource = Templates;
            dataGridView1.ReadOnly = true;
            listView1.View = View.Details;
            listView1.Columns.Add("Obrázky", 120);
            label4.Visible = false;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AppCoreLib.IO.EditedPicture.Load(pictureBox1, PHolder);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AppCoreLib.IO.EditedPicture.Save(pictureBox1);
        }

        private void InitialState()
        {
            trackBar1.Visible = false;
            pictureBox3.Visible = false;
            pictureBox2.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
        }

        private void pictureBox1_Click_4(object sender, EventArgs e)
        {
            Bitmap bm; MouseEventArgs me = (MouseEventArgs)e;
            Color color = AppCoreLib.Leading.ColorTargetWin.FromPicture(pictureBox1, pictureBox2, out bm, me);
            ActColor = color;
            MS.InvisiblerColor = color;
        }

        private void noPicPop()
        {
            var formPopup = new FormNoImage();
            formPopup.Show();
            return;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            //bright
            if (PHolder.ActPic == null)
            {
                noPicPop();
                return;
            }
            InitialState();
            trackBar1.Maximum = 5;
            trackBar1.Minimum = -5;
            trackBar1.Value = MS.Brightness;
            trackBar1.Visible = true;
            ActEdit = EditEnum.Brightness;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (PHolder.ActPic == null)
            {
                noPicPop();
                return;
            }
            InitialState();
            pictureBox3.Visible = true;
            ActEdit = EditEnum.ReColor;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            MS.preAction(ActEdit, trackBar1.Value, ActColor, PHolder);
            pictureBox1.Image = PHolder.ActPic;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //xml writing button
            AppCoreLib.IO.XMLTemplates.Save(MS, Templates, checkedListBox1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InitialState();
            if (ActEdit != EditEnum.Blur)
            {
                ActEdit = EditEnum.Blur;
                trackBar1.Minimum = 0;
                trackBar1.Maximum = 15;
                trackBar1.Value = MS.Blur;
                trackBar1.Visible = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (PHolder.ActPic == null) return;
            InitialState();
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            radioButton3.Visible = true;
            ActEdit = EditEnum.RGB;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (PHolder.ActPic == null)
            {
                noPicPop();
                return;
            }
            InitialState();
            ActEdit = EditEnum.Invisibler;
            trackBar1.Minimum = 0;
            trackBar1.Maximum = 40;
            trackBar1.Value = MS.InvisiblerVariance;
            trackBar1.Visible = true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Bitmap bm;
            MouseEventArgs me = (MouseEventArgs)e;
            Color color = AppCoreLib.Leading.ColorTargetWin.FromPicture(pictureBox3, pictureBox2, out bm, me);
            ActColor = color;
            MS.InvisiblerColor = color;
            MS.preAction(ActEdit, 0, color, PHolder);
            pictureBox1.Image = PHolder.ActPic;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            List<string> myList = new List<string>(); string str = "";
            foreach (string x in files) myList.Add(x);
            foreach (string file in files) str = str + file + " ";
            ImageList imgs = new ImageList(); imgs.ImageSize = new Size(60, 60);
            int counter = 0;
            List<string> myStrs = PathsOfPictures; List<string> errors = new List<string>();
            foreach (var path in files)
            {
                try
                {
                    var kk = Bitmap.FromFile(path);
                    imgs.Images.Add(Image.FromFile(path));
                    myStrs.Add(path);
                }
                catch (Exception)
                {
                    errors.Add(path);
                }
            }
            if (errors.Count > 0)
            {
                var form = new InvalidPicture(errors);
                form.Show();
            }
            PathsOfPictures = myStrs; listView1.SmallImageList = imgs;
            myStrs.ForEach(x => { listView1.Items.Add(Path.GetFileName(x), counter); counter++; });
        }

        private void button11_Click(object sender, EventArgs e)
        {
            InitialState();
            if (PHolder.ActPic == null)
            {
                noPicPop();
                return;
            }
            ActEdit = EditEnum.Invert;
            MS.preAction(EditEnum.Invert, 0, Color.Empty, PHolder);
            pictureBox1.Image = PHolder.ActPic;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (ActEdit != EditEnum.RGB) return;
            if (PHolder.ActPic == null)
            {
                noPicPop();
                return;
            }
            ActEdit = EditEnum.RGB;
            MS.preAction(EditEnum.RGB, 3, Color.Empty, PHolder);
            pictureBox1.Image = PHolder.ActPic;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (ActEdit != EditEnum.RGB) return;

            if (PHolder.ActPic == null)
            {
                noPicPop();
                return;
            }
            ActEdit = EditEnum.RGB;
            MS.preAction(EditEnum.RGB, 2, Color.Empty, PHolder);
            pictureBox1.Image = PHolder.ActPic;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (ActEdit != EditEnum.RGB) return;
            if (PHolder.ActPic == null)
            {
                noPicPop();
                return;
            }
            ActEdit = EditEnum.RGB;
            MS.preAction(EditEnum.RGB, 1, Color.Empty, PHolder);
            pictureBox1.Image = PHolder.ActPic;
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            MS = new ModSetting();
            PHolder.ActPic?.Dispose();
            PHolder.ActPic = new Bitmap(PHolder.Original);
            PHolder.SameEdit?.Dispose();
            PHolder.SameEdit = new Bitmap (PHolder.Original);
            pictureBox1.Image = PHolder.ActPic;
            InitialState();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MS.AllPictureEdits.Count == 0) return;
            if (PHolder.ActPic == PHolder.SameEdit) return;
            PHolder.ActPic.Dispose();
            PHolder.ActPic = new Bitmap(PHolder.SameEdit);
            trackBar1.Value = 0;
            pictureBox1.Image = PHolder.ActPic;
            switch (MS.AllPictureEdits[MS.AllPictureEdits.Count - 1])
            {
                case EditEnum.Blur: { MS.Blur = 0; break; }
                case EditEnum.Brightness: { MS.Brightness = 0; break; }
                case EditEnum.Invert: { MS.Invert = false; break; }
                case EditEnum.RGB: { MS.RGBremove = 0; break; }
                case EditEnum.ReColor: { MS.refarb = Color.FromArgb(0, 0, 0, 0); break; }
                case EditEnum.Invisibler: { MS.InvisiblerColor = Color.FromArgb(0, 0, 0, 0); MS.InvisiblerVariance = 0; break; }
            }
            MS.AllPictureEdits.RemoveAt(MS.AllPictureEdits.Count() - 1);
        }

        private void checkedListBox1_ItemCheck_1(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < checkedListBox1.Items.Count; ++ix)
                if (ix != e.Index) checkedListBox1.SetItemChecked(ix, false);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            string jj = checkedListBox1.CheckedItems.ToString();
            if (checkedListBox1.CheckedItems.Count == 0) { AppCoreLib.IO.ErrorsManagerG.ErrThrow("není vybraná žádná šablona", "Missing arg"); return; }
            var nameOfTeplate = checkedListBox1.SelectedItem.ToString();
            ModSetting template = new ModSetting(); bool error = true;
            if (PathsOfPictures.Count == 0) { AppCoreLib.IO.ErrorsManagerG.ErrThrow(null, null); return; }
            foreach (var item in Templates) { if (item.Name == nameOfTeplate) { template = item; error = false; break; } }
            if (error) throw new Exception("nenašel jsem šablonu");
            var kk = new BulkProcessing(template, PathsOfPictures);
            var task = kk.Run(progressBar1);
            label4.Visible = true;
            task.Wait();
            PathsOfPictures = new List<string>();
            progressBar1.Value = 0; progressBar1.Visible = false; label4.Visible = false; listView1.Items.Clear();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            int selectedCount = dataGridView1.SelectedRows.Count;
            while (selectedCount > 0)
            {
                if (!dataGridView1.SelectedRows[0].IsNewRow)
                {
                    int oo = dataGridView1.SelectedRows[0].Index;
                    var toDelete = Templates.ToArray()[oo];
                    string name = toDelete.Name;
                    if (File.Exists(@name + ".xml"))
                    {
                        Templates.Remove(toDelete);
                        File.Delete(@name + ".xml");
                        checkedListBox1.Items.RemoveAt(oo);
                    }
                    else throw new IOException("soubor nenalezen");
                    selectedCount--;
                }
            }
        }
    }
}