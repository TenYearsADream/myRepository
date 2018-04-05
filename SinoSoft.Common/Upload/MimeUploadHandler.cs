namespace SinoSoft.Common.Upload
{
    using System;
    using System.Collections.Specialized;
    using System.IO;
    using System.Text;

    internal class MimeUploadHandler : IMimePushHandler
    {
        protected byte[] _boundary;
        protected Stream _s;
        protected StringBuilder _textParts;
        protected UploadedFileCollection _uploadedFiles;
        protected SinoSoft.Common.Upload.UploadStatus _uploadStatus;
        private Stream currentStream;
        private Encoding encoding;

        public MimeUploadHandler(Stream s, byte[] boundary, string uploadId, Encoding e)
        {
            this._s = s;
            this._boundary = boundary;
            this._uploadStatus = new SinoSoft.Common.Upload.UploadStatus(this.ContentLength, uploadId);
            this.encoding = e;
        }

        public void BeginPart(NameValueCollection headers)
        {
            string uploadFileName = this.GetUploadFileName(headers);
            if (uploadFileName != null)
            {
                UploadedFile file = new UploadedFile(uploadFileName, headers["content-type"]);
                this.UploadedFiles.Add(file);
                this.currentStream = file.IntermediateStream;
                this._uploadStatus.SetCurrentFileName(uploadFileName);
            }
            else
            {
                this._textParts.Append(this.encoding.GetString(this._boundary) + "\r\n");
                for (int i = 0; i < headers.Count; i++)
                {
                    this._textParts.Append(headers.Keys[i] + ": " + headers[i] + "\r\n");
                }
                this._textParts.Append("\r\n");
            }
        }

        public void EndPart(bool isLast)
        {
            if (this.currentStream != null)
            {
                this.UploadedFiles[this.UploadedFiles.Count - 1].SetUploadComplete(this.currentStream.Length);
                this.currentStream.Close();
                this.currentStream = null;
            }
            else
            {
                this._textParts.Append("\r\n");
            }
            if (isLast && (this._textParts.Length > 0))
            {
                this._textParts.Append(this.encoding.GetString(this._boundary) + "--\r\n\r\n");
            }
        }

        private string GetUploadFileName(NameValueCollection headers)
        {
            string str;
            string[] strArray = headers["content-disposition"].Split(new char[] { ';' });
            if (strArray.Length > 2)
            {
                str = strArray[2].Split(new char[] { '=' })[1];
            }
            else
            {
                return null;
            }
            if (str != "\"\"")
            {
                return str.Replace("\"", string.Empty);
            }
            return null;
        }

        public void Parse()
        {
            this._uploadedFiles = new UploadedFileCollection();
            this._textParts = new StringBuilder();
            MimePushReader reader = new MimePushReader(this._s, this._boundary, this, this.encoding);
            try
            {
                reader.Parse();
            }
            catch (DisconnectedException)
            {
                if (this.currentStream != null)
                {
                    this.currentStream.Close();
                }
                throw;
            }
        }

        public void PartData(ref byte[] data)
        {
            if (this.currentStream != null)
            {
                this.currentStream.Write(data, 0, data.Length);
            }
            else
            {
                this._textParts.Append(this.encoding.GetString(data));
            }
            this._uploadStatus.SetPosition(this._s.Position);
        }

        public long ContentLength
        {
            get
            {
                return this._s.Length;
            }
        }

        public bool IsComplete
        {
            get
            {
                return (this.Progress >= this.ContentLength);
            }
        }

        public int Progress
        {
            get
            {
                return (int) this._s.Position;
            }
        }

        public string TextParts
        {
            get
            {
                return this._textParts.ToString();
            }
        }

        public UploadedFileCollection UploadedFiles
        {
            get
            {
                return this._uploadedFiles;
            }
        }

        public SinoSoft.Common.Upload.UploadStatus UploadStatus
        {
            get
            {
                return this._uploadStatus;
            }
        }
    }
}

