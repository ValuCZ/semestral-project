using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace imgprocessor
{
    public enum EditEnum
    {
        Brightness = 0,
        ReColor = 1,
        Invisibler = 2,
        Invert = 3,
        Blur = 4,
        RGB = 5,
        NoEdit = 6,
        iA = 7,
        iR = 8,
        iG = 9,
        iB = 10,
        rA = 11,
        rR = 12,
        rG = 13,
        rB = 14,
        Edit = 15,
        Name = 16
    }

    public class ModSetting
    {
        public string Name { get; set; } = null;
        public List<EditEnum> AllPictureEdits = new List<EditEnum>();
        public int RGBremove { get; set; }
        public int Brightness { get; set; }
        public int Blur { get; set; }
        public bool Invert { get; set; }
        public Color refarb { get; set; }
        public Color InvisiblerColor { get; set; }
        public int InvisiblerVariance { get; set; }

        public ModSetting()
        {
            Brightness = 0;
            Blur = 0;
            Invert = false;
            refarb = Color.FromArgb(0, 0, 0, 0);
            InvisiblerColor = Color.FromArgb(0, 0, 0, 0);
            InvisiblerVariance = 0;
            RGBremove = 0;
        }

        public ModSetting Clone()
        {
            ModSetting mod = new ModSetting();
            mod.Blur = Blur;
            mod.Brightness = Brightness;
            mod.RGBremove = RGBremove;
            mod.InvisiblerVariance = InvisiblerVariance;
            mod.Name = Name;
            mod.InvisiblerColor = Color.FromArgb(InvisiblerColor.ToArgb());
            mod.refarb = Color.FromArgb(InvisiblerColor.ToArgb());
            mod.Invert = Invert;
            foreach (var item in AllPictureEdits) mod.AllPictureEdits.Add(item);
            return mod;
        }

        private void ChangeImageByEdit(EditEnum choice, int amount, Color color, PictureHolder PH, bool overWriteOld)
        {
            Bitmap bitmap;
            if (overWriteOld)
            {
                PH.SameEdit.Dispose();
                PH.SameEdit = PH.ActPic;
                bitmap = new Bitmap(PH.ActPic);
            }
            else
            {
                bitmap = new Bitmap(PH.SameEdit);
                PH.ActPic.Dispose();
            }
            try
            {
                switch (choice)
                {
                    case EditEnum.ReColor:
                        PH.ActPic = PictureManipulation.Shader(bitmap, color);
                        return;

                    case EditEnum.Brightness:
                        PH.ActPic = PictureManipulation.Brightness(bitmap, amount);
                        break;

                    case EditEnum.Blur:
                        PH.ActPic = PictureManipulation.Blur2(bitmap, amount);
                        break;

                    case EditEnum.Invert:
                        PH.ActPic = PictureManipulation.Invert(bitmap);
                        break;

                    case EditEnum.RGB:
                        PH.ActPic = PictureManipulation.ColorRemove(bitmap, amount);
                        break;

                    case EditEnum.Invisibler:
                        PH.ActPic = PictureManipulation.Transparenter(bitmap, color, amount);
                        break;

                    default:
                        throw new ArgumentException("uknown edit action");
                }
            }
            catch (Exception) { }
        }

        public void preAction(EditEnum choice, int amount, Color color, PictureHolder PH)
        {
            switch (choice)
            {
                case EditEnum.ReColor:
                    refarb = color;
                    break;

                case EditEnum.Brightness:
                    Brightness = amount;
                    break;

                case EditEnum.Blur:
                    Blur = amount;
                    break;

                case EditEnum.Invert:
                    Invert = !Invert;
                    break;

                case EditEnum.RGB:
                    RGBremove = amount;
                    break;

                case EditEnum.Invisibler:
                    InvisiblerColor = color;
                    InvisiblerVariance = amount;
                    break;

                default:
                    throw new ArgumentException("uknown edit action");
            }
            if (!AllPictureEdits.Contains(choice))
            {
                AllPictureEdits.Add(choice);
                ChangeImageByEdit(choice, amount, color, PH, true);
            }
            else
            {
                if (AllPictureEdits.ElementAt(AllPictureEdits.Count() - 1) == choice)
                {
                    ChangeImageByEdit(choice, amount, color, PH, false);
                }
            }
        }
    }
}