using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace z.Web.Service.IO
{
    public class File : IDisposable
    {
        List<string> Packets = new List<string>();
        public FileStream fs;

        public string SourceFile {get;set;}
        public string SourceFolder { get; set; }

        public File(string SourceFile, string SourceFolder) {
            this.SourceFile = SourceFile;
            this.SourceFolder = SourceFolder;
        }

        public bool SplitFile(float SizeInMB)
        {
            bool Split = false;
            int nNoofFiles = GetSplitCountBySize(this.SourceFile,  SizeInMB);
            try
            {
                using (FileStream fs = new FileStream(SourceFile, FileMode.Open, FileAccess.Read))
                {
                    int SizeofEachFile = (int)Math.Ceiling((double)fs.Length / nNoofFiles);
                    for (int i = 0; i < nNoofFiles; i++)
                    {
                        string baseFileName = Path.GetFileNameWithoutExtension(SourceFile);
                        string Extension = Path.GetExtension(SourceFile);
                        using (FileStream outputFile = new FileStream(System.IO.Path.Combine(SourceFolder, baseFileName + "." +
                             i.ToString().PadLeft(5, Convert.ToChar("0")) + Extension + ".tmp"), FileMode.Create, FileAccess.Write))
                        {
                            int bytesRead = 0;
                            byte[] buffer = new byte[SizeofEachFile];
                            if ((bytesRead = fs.Read(buffer, 0, SizeofEachFile)) > 0)
                            {
                                outputFile.Write(buffer, 0, bytesRead);
                                string packet = baseFileName + "." + i.ToString().PadLeft(3, Convert.ToChar("0")) + Extension.ToString();
                                Packets.Add(packet);
                            }
                            outputFile.Close();
                        }
                    }
                    fs.Close();
                }
            }
            catch (Exception Ex)
            {
                throw new ArgumentException(Ex.Message);
            }

            return Split;
        }

        public bool MergeFile() //string filePattern = "*.tmp"
        {
            bool Output = false;
            FileStream outPutFile = null;
            string filePattern = string.Format("{0}.*.tmp", Path.GetFileNameWithoutExtension(SourceFile));

            try
            {
                string[] tmpfiles = Directory.GetFiles(SourceFolder, filePattern);
                string PrevFileName = "";
                foreach (string tempFile in tmpfiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(tempFile);
                    string baseFileName = fileName.Substring(0, Path.GetFileNameWithoutExtension(fileName).LastIndexOf('.')); //fileName.Substring(fileName.LastIndexOf('.'), fileName.IndexOf(Convert.ToChar(".")));
                    string extension = Path.GetExtension(fileName);
                    if (!PrevFileName.Equals(baseFileName))
                    {
                        if (outPutFile != null)
                        {
                            outPutFile.Flush();
                            outPutFile.Close();
                        }
                        outPutFile = new FileStream(SourceFile, FileMode.OpenOrCreate, FileAccess.Write);
                    }
                    int bytesRead = 0;
                    byte[] buffer = new byte[1024];
                    FileStream inputTempFile = new FileStream(tempFile, FileMode.OpenOrCreate, FileAccess.Read);
                    while ((bytesRead = inputTempFile.Read(buffer, 0, 1024)) > 0)
                        outPutFile.Write(buffer, 0, bytesRead);
                    inputTempFile.Close();
                    System.IO.File.Delete(tempFile);
                    PrevFileName = baseFileName;
                }
                outPutFile.Close();
            }
            catch(IOException ex)
            {
                throw ex;
            }
            catch (AccessViolationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                 if(outPutFile != null) outPutFile.Dispose();
            }
            return Output;
        }

        public int GetSplitCountBySize(string filename, float chunksizeinmb)
        {
            long bytesize = new FileInfo(filename).Length;
            long lenht = bytesize / (1024 * 1024);
            //just return 1 in case that its less than 1 mb
            return (lenht == 0 && bytesize > 0) ? 1 : Convert.ToInt32(lenht / chunksizeinmb);
        }

        public void Dispose()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
        }

    }
}
