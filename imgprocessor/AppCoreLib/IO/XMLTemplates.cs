using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace imgprocessor.AppCoreLib.IO
{
    public static class XMLTemplates
    {
        private static void ParseXML(string file, BlockingCollection<string> names, CheckedListBox cl1, BlockingCollection<ModSetting> toReturn)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Parse;
                settings.Async = true;
                XmlReader reader = XmlReader.Create(file, settings);
                try
                {
                    ModSetting oldMS = new ModSetting();
                    bool allOK = true;
                    reader.MoveToContent();
                    EditEnum editParsed = EditEnum.NoEdit;
                    pixelHolder PixH = new pixelHolder();
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                editParsed = Convertot.ParseEnum(reader.Name);
                                if (editParsed == EditEnum.NoEdit) allOK = false;
                                break;

                            case XmlNodeType.Text:
                                if (editParsed == EditEnum.NoEdit) allOK = false;
                                string val = reader.Value;
                                Convertot.AddOld(oldMS, editParsed, val, PixH);
                                break;

                            case XmlNodeType.EndElement:
                            case XmlNodeType.Whitespace: break;
                            default:
                                allOK = false;
                                break;
                        }
                    }
                    if (!allOK) { reader.Close(); return; }
                    Color InvisColor = Color.FromArgb(PixH.iA, PixH.iR, PixH.iG, PixH.iB);
                    Color reco = Color.FromArgb(PixH.rA, PixH.rR, PixH.rG, PixH.rB);
                    oldMS.InvisiblerColor = InvisColor;
                    oldMS.refarb = reco;
                    names.Add(Convertot.GetName(file));
                    cl1.Items.Add(Convertot.GetName(file));
                    toReturn?.Add(oldMS);
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                }
            }
            catch (Exception) { }
        }

        private static string[] GetFiles()
        {
            return Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml");
        }

        public static BindingList<ModSetting> Load(CheckedListBox checkedListBox1, List<string> namesTemplates)
        {
            BlockingCollection<string> blockNames = new BlockingCollection<string>();
            List<string> names = new List<string>();
            BindingList<ModSetting> toReturn = new BindingList<ModSetting>();
            BlockingCollection<ModSetting> blockTplt = new BlockingCollection<ModSetting>();
            string[] files = GetFiles();
            List<Task> taskList = new List<Task>();
            foreach (var file in files)
            {
                taskList.Add(Task.Run(() => ParseXML(file, blockNames, checkedListBox1, blockTplt)));
            }
            Task.WaitAll(taskList.ToArray());
            blockTplt.ToList().ForEach(toReturn.Add);
            blockNames.ToList().ForEach(namesTemplates.Add);
            blockTplt.Dispose(); blockNames.Dispose();
            return toReturn;
        }

        private static void wrShort(XmlWriter xw, string strName, string value)
        {
            xw.WriteStartElement(strName);
            xw.WriteValue(value);
            xw.WriteEndElement();
        }

        public static void Save(ModSetting MS, BindingList<ModSetting> templates, CheckedListBox checkedListBox1)
        {
            if (MS.AllPictureEdits.Count == 0) { ErrorsManagerG.ErrThrow("   šablona je prázdná   ", "empty tempate error"); return; }
            using (var form = new SablonaDialog())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string val = form.nameFile;
                    MS.Name = val;
                    string fileName = val + ".xml";
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    using (XmlWriter xw = XmlWriter.Create(fileName, settings))
                    {
                        Color helpColor = new Color();
                        xw.WriteStartDocument();
                        xw.WriteStartElement("Sablona_uprav");
                        wrShort(xw, "Name", val);
                        wrShort(xw, "Blur", MS.Blur.ToString());
                        wrShort(xw, "Brightness", MS.Brightness.ToString());
                        if (MS.refarb != Color.Empty) helpColor = MS.refarb;
                        wrShort(xw, "rA", helpColor.A.ToString());
                        wrShort(xw, "rR", helpColor.R.ToString());
                        wrShort(xw, "rG", helpColor.G.ToString());
                        wrShort(xw, "rB", helpColor.B.ToString());
                        if (MS.InvisiblerColor != Color.Empty) helpColor = MS.InvisiblerColor;
                        wrShort(xw, "iA", helpColor.A.ToString());
                        wrShort(xw, "iR", helpColor.R.ToString());
                        wrShort(xw, "iG", helpColor.G.ToString());
                        wrShort(xw, "iB", helpColor.B.ToString());
                        wrShort(xw, "Invisibler", MS.InvisiblerVariance.ToString());
                        wrShort(xw, "RGB", MS.RGBremove.ToString());
                        if (MS.Invert) wrShort(xw, "Invert", "1");
                        else wrShort(xw, "Invert", "0");
                        MS.AllPictureEdits.ForEach(x => wrShort(xw, "Edit", x.ToString()));
                        xw.WriteEndElement();
                        xw.WriteEndDocument();
                        xw.Flush();
                    }
                    var toSave = MS.Clone();
                    templates.Add(toSave);
                    checkedListBox1.Items.Add(toSave.Name);
                }
            }
        }
    }
}