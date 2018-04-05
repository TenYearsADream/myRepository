namespace SinoSoft.Common.Upload
{
    using System;
    using System.Configuration;
    using System.IO;

    public sealed class UploadedFile
    {
        private string _clientName;
        private string _clientPath;
        private long _contentLength;
        private string _contentType;
        private byte[] _intermediateBuffer = null;
        private Stream _intermediateStream;
        private string _serverPath = GetTempFileName();

        internal UploadedFile(string clientPath, string contentType)
        {
            this._clientPath = clientPath;
            this._contentType = contentType;
            this._clientName = Path.GetFileName(this._clientPath);
        }

        internal bool GetIsMemoryUpload()
        {
            if (ConfigurationSettings.AppSettings["storageProvider"] != null)
            {
                switch (ConfigurationSettings.AppSettings["storageProvider"].ToLower())
                {
                    case "memory":
                        return true;
                }
            }
            return false;
        }

        internal static string GetTempFileName()
        {
            string tempPath = ConfigurationSettings.AppSettings["uploadPath"];
            if ((tempPath == null) || (tempPath.Length == 0))
            {
                tempPath = Path.GetTempPath();
            }
            return Path.Combine(tempPath, Guid.NewGuid().ToString("B"));
        }

        public void SaveAs(string path)
        {
            this.SaveAs(path, false);
        }

        public void SaveAs(string path, bool overwrite)
        {
            if (overwrite)
            {
                File.Delete(path);
            }
            if (this.GetIsMemoryUpload())
            {
                FileStream stream = File.Create(path);
                if (this._intermediateBuffer != null)
                {
                    stream.Write(this._intermediateBuffer, 0, this._intermediateBuffer.Length);
                    this._intermediateBuffer = null;
                    this._intermediateStream = null;
                }
                stream.Close();
            }
            else
            {
                File.Move(this._serverPath, path);
            }
        }
        /// <summary>
        /// 返回缓冲区数据
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            if (this.GetIsMemoryUpload())
            {
                if (this._intermediateBuffer != null)
                {
                    return _intermediateBuffer;
                }
            }
            else
            {
                FileStream stream = new FileStream(this._serverPath,FileMode.Open,FileAccess.Read,FileShare.Read);

                byte[] by = new byte[(int)stream.Length];
                stream.Read(by, 0, (int)stream.Length);
                stream.Close();
                return by;
            }

            return null;
        }

        internal void SetUploadComplete(long contentLength)
        {
            this._contentLength = contentLength;
            if (this.GetIsMemoryUpload())
            {
                MemoryStream stream = this._intermediateStream as MemoryStream;
                this._intermediateBuffer = new byte[contentLength];
                Buffer.BlockCopy(stream.GetBuffer(), 0, this._intermediateBuffer, 0, (int) this._contentLength);
            }
        }

        public string ClientName
        {
            get
            {
                return this._clientName;
            }
        }

        public string ClientPath
        {
            get
            {
                return this._clientPath;
            }
        }

        public long ContentLength
        {
            get
            {
                return this._contentLength;
            }
        }

        public string ContentType
        {
            get
            {
                return this._contentType;
            }
        }

        public Stream IntermediateStream
        {
            get
            {
                if (!this.GetIsMemoryUpload())
                {
                    return File.Create(this.ServerPath);
                }
                if (this._intermediateBuffer == null)
                {
                    if ((this._intermediateStream == null) || !this._intermediateStream.CanWrite)
                    {
                        this._intermediateStream = new MemoryStream();
                    }
                    return this._intermediateStream;
                }
                this._intermediateStream = null;
                return new MemoryStream(this._intermediateBuffer);
            }
        }

        public string ServerPath
        {
            get
            {
                return this._serverPath;
            }
        }
    }
}

