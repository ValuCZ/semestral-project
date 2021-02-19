using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace imgprocessor
{
    public static class PictureManipulation
    {
        public static Bitmap Invert(Bitmap bitmap)
        {
            unsafe
            {
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int heightinPixels = bitmap.Height;
                int widthInBytes = bitmap.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)data.Scan0;
                Parallel.For(0, heightinPixels, i =>
                {
                    byte* currentLine = ptrFirstPixel + i * data.Stride;
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        int oldR = currentLine[x];
                        int oldG = currentLine[x + 1];
                        int oldB = currentLine[x + 2];
                        currentLine[x] = (byte)(255 - oldR);
                        currentLine[x + 1] = (byte)(255 - oldG);
                        currentLine[x + 2] = (byte)(255 - oldB);
                    }
                });
                bitmap.UnlockBits(data);
            }
            return bitmap;
        }

        public static Bitmap Blur(Bitmap bitmap, int amount)
        {
            unsafe
            {
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int heightinPixels = bitmap.Height;
                int widthInBytes = bitmap.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)data.Scan0;
                float qoeficient = (float)Convertot.GetQ(amount);
                Parallel.For(0, heightinPixels, i =>
                {
                    byte* currentLine = ptrFirstPixel + i * data.Stride;
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        int delitel = 1;
                        int oldR = currentLine[x];
                        int oldG = currentLine[x + 1];
                        int oldB = currentLine[x + 2];
                        for (int c = bytesPerPixel; c < bytesPerPixel * (Convertot.LeftPixs(x, amount / bytesPerPixel)); c += bytesPerPixel)
                        {
                            oldR += currentLine[x - c];
                            oldG += currentLine[x - c + 1];
                            oldB += currentLine[x - c + 2];
                            delitel++;
                        }
                        for (int c = bytesPerPixel; c < bytesPerPixel * Convertot.RightPixs(data.Width + 1, x / bytesPerPixel, amount); c += bytesPerPixel)
                        {
                            oldR += currentLine[x + c];
                            oldG += currentLine[x + c + 1];
                            oldB += currentLine[x + c + 2];
                            delitel++;
                        }
                        currentLine[x] = (byte)(oldR / delitel);
                        currentLine[x + 1] = (byte)(oldG / delitel);
                        currentLine[x + 2] = (byte)(oldB / delitel);
                    }
                });
                bitmap.UnlockBits(data);
            }
            return bitmap;
        }

        public static Bitmap Blur2(Bitmap bitmap, int amount)
        {
            unsafe
            {
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int heightinPixels = bitmap.Height;
                int widthInBytes = bitmap.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)data.Scan0;
                float qoeficient = (float)Convertot.GetQ(amount);
                Parallel.For(0, heightinPixels, i =>
                {
                    byte* currentLine = ptrFirstPixel + i * data.Stride;
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        int delitel = 1;
                        int oldR = currentLine[x];
                        int oldG = currentLine[x + 1];
                        int oldB = currentLine[x + 2];
                        byte* lowerUpperLine;
                        if (i - amount >= 0)
                        {
                            lowerUpperLine = ptrFirstPixel + (i - amount) * data.Stride;
                            oldR += lowerUpperLine[x];
                            oldG += lowerUpperLine[x + 1];
                            oldB += lowerUpperLine[x + 2];
                            delitel++;
                        }
                        if (x - amount * bytesPerPixel >= 0)
                        {
                            oldR += currentLine[x - (amount * bytesPerPixel)];
                            oldG += currentLine[x - (amount * bytesPerPixel) + 1];
                            oldB += currentLine[x - (amount * bytesPerPixel) + 2];
                            delitel++;
                        }
                        if (x + amount * bytesPerPixel < widthInBytes)
                        {
                            oldR += currentLine[x + (amount * bytesPerPixel)];
                            oldG += currentLine[x + (amount * bytesPerPixel) + 1];
                            oldB += currentLine[x + (amount * bytesPerPixel) + 2];
                            delitel++;
                        }
                        if (i + amount < heightinPixels)
                        {
                            lowerUpperLine = ptrFirstPixel + (i + amount) * data.Stride;
                            oldR += lowerUpperLine[x];
                            oldG += lowerUpperLine[x + 1];
                            oldB += lowerUpperLine[x + 2];
                            delitel++;
                        }
                        currentLine[x] = (byte)(oldR / delitel);
                        currentLine[x + 1] = (byte)(oldG / delitel);
                        currentLine[x + 2] = (byte)(oldB / delitel);
                    }
                });
                bitmap.UnlockBits(data);
            }

            return bitmap;
        }

        public static Bitmap Transparenter(Bitmap bitmap, Color color, int variance)
        {
            unsafe
            {
                int patternR = color.R;
                int patternG = color.G;
                int patternB = color.B;
                variance *= 3;
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int heightinPixels = bitmap.Height;
                int widthInBytes = bitmap.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)data.Scan0;
                byte hard = 255;
                byte soft = 0;
                _ = Parallel.For(0, heightinPixels, i =>
                {
                    byte* currentLine = ptrFirstPixel + i * data.Stride;
                    for (int x = 0; x < widthInBytes - 4; x += bytesPerPixel)
                    {
                        int actualVariance = 0;
                        int oldB = currentLine[x];
                        int oldG = currentLine[x + 1];
                        int oldR = currentLine[x + 2];
                        actualVariance += Math.Abs(patternR - oldR);
                        actualVariance += Math.Abs(patternG - oldG);
                        actualVariance += Math.Abs(patternB - oldB);
                        currentLine[x] = (byte)oldB;
                        currentLine[x + 1] = (byte)oldG;
                        currentLine[x + 2] = (byte)oldR;
                        if (actualVariance > variance) currentLine[x + 3] = hard;
                        else currentLine[x + 3] = soft;
                    }
                });
                bitmap.UnlockBits(data);
            }
            return bitmap;
        }

        public static Bitmap Shader(Bitmap bitmap, Color color)
        {
            int sum = color.R + color.G + color.B;
            int patternR = color.R;
            int patternG = color.G;
            int patternB = color.B;
            unsafe
            {
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int heightinPixels = bitmap.Height;
                int widthInBytes = bitmap.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)data.Scan0;
                Parallel.For(0, heightinPixels, i =>
                {
                    byte* currentLine = ptrFirstPixel + i * data.Stride;
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        int oldR = currentLine[x];
                        int oldG = currentLine[x + 1];
                        int oldB = currentLine[x + 2];
                        int curSum = oldG + oldG + oldB;
                        float q = (float)curSum / sum;
                        oldR = (int)(patternR * q);
                        oldG = (int)(patternG * q);
                        oldB = (int)(patternB * q);
                        if (oldR > 255) oldR = 255;
                        if (oldG > 255) oldG = 255;
                        if (oldB > 255) oldB = 255;
                        currentLine[x] = (byte)oldB;
                        currentLine[x + 1] = (byte)oldG;
                        currentLine[x + 2] = (byte)oldR;
                    }
                });
                bitmap.UnlockBits(data);
            }
            return bitmap;
        }

        public static Bitmap ColorRemove(Bitmap bitmap, int choice)
        {
            unsafe
            {
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int heightinPixels = bitmap.Height;
                int widthInBytes = bitmap.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)data.Scan0;
                int start = Convertot.ColorStart(choice);
                int stop = Convertot.ColorStop(choice);
                int step = Convertot.ColorStep(choice);
                byte[] colorArray = new byte[3];
                Parallel.For(0, heightinPixels, i =>
                {
                    byte* currentLine = ptrFirstPixel + i * data.Stride;
                    for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                    {
                        currentLine[x + choice - 1] = 0;
                    }
                });
                bitmap.UnlockBits(data);
            }
            return bitmap;
        }

        public static Bitmap Brightness(Bitmap bitmap, int amount)
        {
            unsafe
            {
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);
                int bytesPerPixel = Bitmap.GetPixelFormatSize(bitmap.PixelFormat) / 8;
                int heightinPixels = bitmap.Height;
                int widthInBytes = bitmap.Width * bytesPerPixel;
                byte* ptrFirstPixel = (byte*)data.Scan0;
                if (amount > 0)
                {
                    float qoeficient = (float)Convertot.GetQ(amount);
                    Parallel.For(0, heightinPixels, i =>
                    {
                        byte* currentLine = ptrFirstPixel + i * data.Stride;
                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            int oldR = currentLine[x];
                            int oldG = currentLine[x + 1];
                            int oldB = currentLine[x + 2];
                            currentLine[x] = (byte)Convert.ToInt32(oldR + (255 - oldR) / qoeficient);
                            currentLine[x + 1] = (byte)Convert.ToInt32(oldG + (255 - oldG) / qoeficient);
                            currentLine[x + 2] = (byte)Convert.ToInt32(oldB + (255 - oldB) / qoeficient);
                        }
                    });
                }
                else
                {
                    float qoeficient = (float)Convertot.GetQ(amount);
                    if (qoeficient == 0) qoeficient = 1;
                    Parallel.For(0, heightinPixels, i =>
                    {
                        byte* currentLine = ptrFirstPixel + i * data.Stride;
                        for (int x = 0; x < widthInBytes; x += bytesPerPixel)
                        {
                            int oldR = currentLine[x];
                            int oldG = currentLine[x + 1];
                            int oldB = currentLine[x + 2];
                            currentLine[x] = (byte)Convert.ToInt32(oldR / qoeficient);
                            currentLine[x + 1] = (byte)Convert.ToInt32(oldG / qoeficient);
                            currentLine[x + 2] = (byte)Convert.ToInt32(oldB / qoeficient);
                        }
                    });
                }
                bitmap.UnlockBits(data);
            }
            return bitmap;
        }

        public static Bitmap MakeEdit(Bitmap pic, EditEnum edit, ModSetting mod)
        {
            switch (edit)
            {
                case EditEnum.Brightness: return Brightness(pic, mod.Brightness);
                case EditEnum.Blur: return Blur2(pic, mod.Blur);
                case EditEnum.Invert: return Invert(pic);
                case EditEnum.Invisibler: return Transparenter(pic, mod.InvisiblerColor, mod.InvisiblerVariance);
                case EditEnum.ReColor: return Shader(pic, mod.refarb);
                default: return ColorRemove(pic, mod.RGBremove);
            }
        }
    }
}