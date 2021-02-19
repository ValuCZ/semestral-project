using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace imgprocessor.AppCoreLib.IO
{
    public static class EditedPicture
    {
        public static void Load(PictureBox pictureBox1, PictureHolder PHolder)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp;)|*.jpg; *.jpeg; *.png; *.bmp;";
            if (open.ShowDialog() == DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(open.FileName);
                pictureBox1.Visible = true;

                PHolder.ActPic = bitmap;
                PHolder.SameEdit = new Bitmap(bitmap);
                PHolder.Original = new Bitmap(bitmap);
                pictureBox1.Image = PHolder.ActPic;
            }
        }

        public static void Save(PictureBox pictureBox1)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|Png Image|*.png";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                FileStream fs =
                    (FileStream)saveFileDialog1.OpenFile();
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        pictureBox1.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        pictureBox1.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        pictureBox1.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Gif);
                        break;

                    case 4:
                        pictureBox1.Image.Save(fs,
                            System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }
                fs.Close();
            }
        }
    }
}