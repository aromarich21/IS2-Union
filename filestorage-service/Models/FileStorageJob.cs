using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace filestorage_service.Models
{
    public class FileStorageJob
    {
        public string Token { get; set; }
        public List<string> Result { get; set; }

        public FileStorageJob(string token)
        {
            Token = token;
        }

        public FileStorageJob(string token, string dir)
        {
            Token = token;
            FileGenerate(dir);
        }

        public FileStorageJob(string token, string dir, string value)
        {
            Token = token;
            FileGenerate(dir);
        }

        public FileStorageJob(string token, string dir, List<string> sourceResult)
        {
            Token = token;
            FileGenerate(dir);
        }
        public void FileDelete(string dir, List<FileInfo> filestorage)
        {
            if (Directory.Exists(dir))
            {
                string[] files = Directory.GetFiles(dir);
                foreach (string item in files)
                {
                    if (item == (@"Files\" + Token + ".is2"))
                    {
                        File.Delete(item);
                        //filestorage.Find((x) => x.Uuid == Token).Strokes.Clear();
                    }            
                }
            }
        }
        public void FileGenerate(string dir)
        {
            var name = Token + ".is2";
            var path = dir + @"\" + name;
            if (!(File.Exists(path)))
            {
                try
                {
                    // Create the file, or overwrite if the file exists.
                    using (FileStream fileStream = File.Create(path))
                    {

                        byte[] info = new UTF8Encoding(true).GetBytes("");
                        fileStream.Write(info, 0, info.Length);
                    }
                    FileInfo file = new FileInfo(name, Token);
                    Startup.fileStorage.Add(file);
                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }

        public void FilePatch(string dir, string value)
        {
            var name = Token + ".is2";
            var path = dir + @"\" + name;

            try
            {
                using (StreamWriter file = new StreamWriter(path, true, Encoding.UTF8))
                {
                    file.WriteLine(value);
                }
            }
            catch
            {

            }
        }

        public void FileReWrite(string dir, List<FileInfo> filestorage)
        {
            var name = Token + ".is2";
            var path = dir + @"\" + name;

            try
            {
                FileGenerate(dir);
                File.WriteAllLines(path, filestorage.Find((x) => x.Uuid == Token).Strokes, Encoding.UTF8);
            }
            catch
            {

            }
        }

        public void FileStrokeAdd(List<FileInfo> filestorage, string stroke)
        {
            filestorage.Find((x)=> x.Uuid == Token).Strokes.Add(stroke);
        }

        public void FileStrokeClear(List<FileInfo> filestorage)
        {
            filestorage.Find((x) => x.Uuid == Token).Strokes.Clear();
        }
    }
}
