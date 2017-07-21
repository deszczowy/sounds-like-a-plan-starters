using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starters
{
    class Process : INotifyPropertyChanged

    {
        private string fileName;
        private string content;

        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties
        public string FileName {
            get {
                if (this.fileName == "") return "untitled"; else return this.fileName;
            }
            private set {
                if (value == null) this.fileName = ""; else this.fileName = value;
                this.NotifyPropertyChanged("FileName");
            }
        }

        public string Content
        {
            get { return this.content; }
            set {
                this.content = value;
                this.NotifyPropertyChanged("Content");
                this.NotifyPropertyChanged("Count");
            }
        }

        public int Count
        {
            get { return this.content.Length; }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        public Process()
        {
            newFile();
        }

        #region Encoders

        public string code(string input)
        {
            string codded = "";
            byte[] buffer = Encoding.UTF8.GetBytes(input);

            using (var compressedStream = new MemoryStream())
            using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
            {
                zipStream.Write(buffer, 0, buffer.Length);
                zipStream.Close();
                codded = Convert.ToBase64String(compressedStream.ToArray());
            }
            return codded;
        }

        public string decode(string input)
        {
            byte[] decodded = Convert.FromBase64String(input);

            using (GZipStream stream = new GZipStream(new MemoryStream(decodded),
            CompressionMode.Decompress))
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(decodded, 0, decodded.Length);
                        if (count > 0)
                        {
                            memory.Write(decodded, 0, count);
                        }
                    }
                    while (count > 0);

                    memory.Position = 0;
                    StreamReader sr = new StreamReader(memory);
                    return sr.ReadToEnd();
                }
            }
        }

        #endregion

        #region File operations

        public void openFile(string fileName)
        {
            string content = File.ReadAllText(fileName);
            this.FileName = fileName;
            this.Content = decode(content);
        }

        public void saveFile(string fileName = "")
        {
            string codded = this.fileName + fileName;
            codded = codded.Trim();

            if (codded != "")
            {
                codded = code(this.Content);
                string file = fileName;
                if ("" == file)
                {
                    file = this.fileName;
                }
                else
                {
                    this.FileName = file;
                }
                File.WriteAllText(file, codded);                
            }
        }

        public void newFile()
        {
            this.FileName = String.Empty;
            this.Content = String.Empty;
        }

        #endregion

        #region Indicators
        public bool isNew()
        {
            return this.fileName == "";
        }

        #endregion

    }
}
