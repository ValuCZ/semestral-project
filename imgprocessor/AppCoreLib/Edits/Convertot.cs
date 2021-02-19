using System;
using System.IO;

namespace imgprocessor
{
    public static class Convertot
    {
        public static string GetAGoodName(string path)
        {
            string date = DateTime.Now.ToString("yyyy_MM_dd_h_m_ss_ffff");

            return date + new FileInfo(path).Extension;
        }

        public static bool HasIlegalChar(string name)
        {
            string[] illegal = new string[] { ">", ":", "\"", "/", "\\", "|", "?", "*" };
            foreach (var item in illegal) if (name.Contains(item)) return true;
            return false;
        }

        public static string GetName(string path)
        {
            string root = Directory.GetCurrentDirectory();
            string withountRoot = path.Replace(root, "");
            string almost = withountRoot.Replace(".xml", "");
            return almost.Replace("\\", "");
        }

        public static bool AddOld(ModSetting oldMS, EditEnum edit, string val, pixelHolder PH)
        {
            int myOut = 0;
            bool ans = true;
            if (edit == EditEnum.NoEdit) ans = false;

            switch (edit)
            {
                case EditEnum.Edit:
                    EditEnum edit1 = ParseEnum(val);
                    if (edit1 == EditEnum.NoEdit) return false;
                    oldMS.AllPictureEdits.Add(edit1);
                    break;

                case EditEnum.Invisibler:
                    if (!Int32.TryParse(val, out myOut) || myOut < 0) return false;
                    oldMS.InvisiblerVariance = myOut;
                    break;

                case EditEnum.Blur:
                    if (!Int32.TryParse(val, out myOut)) return false;
                    oldMS.Blur = myOut;
                    break;

                case EditEnum.Brightness:
                    if (!Int32.TryParse(val, out myOut)) return false;
                    oldMS.Brightness = myOut;
                    break;

                case EditEnum.iA:
                    if (!Int32.TryParse(val, out myOut) || myOut > 255 || myOut < 0) return false;
                    PH.iA = (Byte)myOut;
                    break;

                case EditEnum.iR:
                    if (!Int32.TryParse(val, out myOut) || myOut > 255 || myOut < 0) return false;
                    PH.iR = (Byte)myOut;
                    break;

                case EditEnum.iG:
                    if (!Int32.TryParse(val, out myOut) || myOut > 255 || myOut < 0) return false;
                    PH.iG = (Byte)myOut;
                    break;

                case EditEnum.iB:
                    if (!Int32.TryParse(val, out myOut) || myOut > 255 || myOut < 0) return false;
                    PH.iB = (Byte)myOut;
                    break;

                case EditEnum.Invert:
                    if (val == "1") oldMS.Invert = true;
                    else return false;
                    break;

                case EditEnum.RGB:
                    if (!Int32.TryParse(val, out myOut) || myOut > 3 || myOut < 0) return false;
                    PH.iB = (Byte)myOut;
                    break;

                case EditEnum.rA:
                    if (!Int32.TryParse(val, out myOut) || myOut > 255 || myOut < 0) return false;
                    PH.rA = (Byte)myOut;
                    break;

                case EditEnum.rR:
                    if (!Int32.TryParse(val, out myOut) || myOut > 255 || myOut < 0) return false;
                    PH.rR = (Byte)myOut;
                    break;

                case EditEnum.rG:
                    if (!Int32.TryParse(val, out myOut) || myOut > 255 || myOut < 0) return false;
                    PH.rG = (Byte)myOut;
                    break;

                case EditEnum.rB:
                    if (!Int32.TryParse(val, out myOut) || myOut > 255 || myOut < 0) return false;
                    PH.rB = (Byte)myOut;
                    break;

                case EditEnum.Name:
                    if (val == null || val == "") return false;
                    oldMS.Name = val;
                    break;
            }

            return ans;
        }

        public static EditEnum ParseEnum(string text)
        {
            EditEnum edit = EditEnum.Brightness;
            for (int i = 0; i < 17; i++)
            {
                if (edit.ToString() == text) return edit;
                edit++;
            }
            return EditEnum.NoEdit;
        }

        public static int ColorStart(int choice)
        {
            if (choice == 1) return 1;
            return 0;
        }

        public static int ColorStep(int choice)
        {
            if (choice == 2) return 2;
            return 1;
        }

        public static int ColorStop(int choice)
        {
            if (choice == 1) return 3;
            if (choice == 2) return 3;
            return 2;
        }

        public static int RightPixs(int maxWidth, int position, int amount)
        {
            int y = maxWidth - position;
            if (y >= amount) return amount;
            else return y;
        }

        public static int LeftPixs(int postiom, int amount)
        {
            if (amount <= postiom) return amount;
            else
            {
                return postiom;
            }
        }

        public static double GetQ(int Q)
        {
            if (Q > 0)
            {
                switch (Q)
                {
                    case 1:
                        return 5;

                    case 2:
                        return 4;

                    case 3:
                        return 3;

                    case 4:
                        return 2;

                    case 5:
                        return 1.5;
                }
            }
            else
            {
                switch (Q)
                {
                    case -1: return 1.2;
                    case -2: return 1.5;
                    case -3: return 2;
                    case -4: return 4;
                    case -5: return 7;
                }
            }
            return 0;
        }
    }
}