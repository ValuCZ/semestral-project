using System.Drawing;
using System.Windows.Forms;

namespace imgprocessor.AppCoreLib.Leading
{
    public static class ColorTargetWin
    {
        public static Color FromPicture(PictureBox pictureBox1, PictureBox pictureBox2, out Bitmap bm, MouseEventArgs me)
        {
            pictureBox2.Image?.Dispose();
            Point pt = pictureBox1.Location;
            int MouseWidth = me.X;
            int mouseHeight = me.Y;
            float withQ = (float)pictureBox1.Image.Width / pictureBox1.Width;
            float heithQ = (float)pictureBox1.Image.Height / pictureBox1.Height;
            MouseWidth = (int)(MouseWidth * withQ);
            mouseHeight = (int)(mouseHeight * heithQ);
            Bitmap b = new Bitmap(pictureBox1.Image);
            Color color = b.GetPixel(MouseWidth, mouseHeight);
            bm = new Bitmap(22, 22);

            for (int i = 0; i < bm.Width; i++)
            {
                for (int j = 0; j < bm.Height; j++)
                {
                    bm.SetPixel(i, j, color);
                }
            }
            pictureBox2.Image = bm;
            pictureBox2.Show();
            return color;
        }
    }
}