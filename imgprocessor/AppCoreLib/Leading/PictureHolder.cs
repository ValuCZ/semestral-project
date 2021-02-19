using System.Drawing;

namespace imgprocessor
{
    public class PictureHolder
    {
        public Bitmap Original { get; set; } = null;
        public Bitmap ActPic { get; set; } = null;
        public Bitmap SameEdit { get; set; } = null;
    }

    public class pixelHolder
    {
        public byte iA = 0, iR = 0, iG = 0, iB = 0;
        public byte rA = 0, rR = 0, rG = 0, rB = 0;
    }
}