using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imgprocessor
{
    internal class BulkProcessing
    {
        private Int64 TotalBytes { get; set; } = 0;
        private Int64 CompletedYet { get; set; } = 0;
        private List<string> Paths { get; } = null;
        private ModSetting Mod { get; }

        private void SavePic(Bitmap pic, string path)
        {
            string dir = "processed";
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            string name = Convertot.GetAGoodName(path);
            pic.Save(Path.Combine(dir, name));
        }

        private int GetPercent()
        {
            if (TotalBytes == 0) return 100;
            int result = (int)(CompletedYet / TotalBytes) * 100;
            return result;
        }

        public BulkProcessing(ModSetting mod, List<string> paths)
        {
            int editsNum = mod.AllPictureEdits.Count();
            Mod = mod;
            foreach (var item in paths) { var _ = Bitmap.FromFile(item); TotalBytes += _.Width * _.Height * editsNum; _.Dispose(); }
            Paths = paths;
        }

        public async Task Run(ProgressBar bar)
        {
            await Task.Run(() =>
            {
                foreach (var item in Paths)
                {
                    Bitmap pic = (Bitmap)Bitmap.FromFile(item);
                    Mod.AllPictureEdits.ForEach(x =>
                    {
                        pic = PictureManipulation.MakeEdit(pic, x, Mod);
                        CompletedYet += pic.Width * pic.Height;
                        bar.Value = GetPercent();
                    });
                    SavePic(pic, item);
                    pic.Dispose();
                }
            });
            var loc = Path.Combine(Directory.GetCurrentDirectory().ToString(), "processed");
            ProcessStartInfo startInformation = new ProcessStartInfo();
            startInformation.FileName = loc;
            Process process = Process.Start(startInformation);
        }
    }
}