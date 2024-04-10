using Words =  Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static RosinkaApp.Windows.ReportWindow;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace RosinkaApp.Classes
{
     class WordHelper
    {
        public string NewPath;
        private FileInfo _fileInfo;

        public WordHelper(string fileName)
        {
            if (File.Exists(fileName))
            {
                _fileInfo = new FileInfo(fileName);
            }
            else
            {
                throw new Exception("File not found");
            }
        }

        private Bitmap GetBitmapFromBitmapImage(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                encoder.Save(outStream);
                return new Bitmap(outStream);
            }
        }

        internal bool Process(Dictionary<string, ParameterData> items)
        {


            Words.Application app = null;
            try
            {
                app = new Words.Application();
                Object file = _fileInfo.FullName;

                Object missing = Type.Missing;

                app.Documents.Open(file);

                foreach (var item in items)
                {
                    Words.Find find = app.Selection.Find;
                    find.Text = item.Key;
                    if(item.Value.Data != null)
                    {
                        // Удаляем текст
                        find.Execute(Replace: Words.WdReplace.wdReplaceNone);

                        // Сохраняем временный файл изображения
                        string tempImagePath = System.IO.Path.GetTempFileName();
                        System.IO.File.WriteAllBytes(tempImagePath, item.Value.Data);

                        // Вставляем изображение
                        app.Selection.InlineShapes.AddPicture(tempImagePath, missing, missing, missing);

                        // Удаляем временный файл изображения
                        System.IO.File.Delete(tempImagePath);

                    }
                    else
                    {
                        find.Replacement.Text = item.Value.Text;

                        Object wrap = Words.WdFindWrap.wdFindContinue;
                        Object replace = Words.WdReplace.wdReplaceAll;

                        find.Execute(FindText: Type.Missing,
                            MatchCase: false,
                            MatchWholeWord: false,
                            MatchWildcards: false,
                            MatchSoundsLike: missing,
                            MatchAllWordForms: false,
                            Forward: true,
                            Wrap: wrap,
                            Format: false,
                            ReplaceWith: missing, Replace: replace);
                     }
                }
                Object newFileName = System.IO.Path.Combine(_fileInfo.DirectoryName, DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss ") + _fileInfo.Name);
                NewPath = newFileName.ToString();
                app.ActiveDocument.SaveAs2(newFileName);
                app.ActiveDocument.Close();
                app.Quit();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (app != null)
                {
                    app.Quit();
                }
            }
            return false;
        }
    }
}
