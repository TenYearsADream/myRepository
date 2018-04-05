namespace SinoSoft.Common.Upload
{
    using System;

    public sealed class UploadStatus
    {
        private bool _autoDropState;
        private long _contentLength;
        private string _currentFileName;
        private long _position;
        private DateTime _start;
        private UploadState _state;
        private string _uploadId;

        internal UploadStatus(long contentLength, string uploadId)
        {
            this._contentLength = contentLength;
            this._start = DateTime.Now;
            this._uploadId = uploadId;
            this.AutoDropState = true;
        }

        internal void SetCurrentFileName(string fileName)
        {
            lock (this)
            {
                this._currentFileName = fileName;
            }
        }

        internal void SetPosition(long position)
        {
            lock (this)
            {
                this._position = position;
            }
        }

        internal void SetState(UploadState state)
        {
            lock (this)
            {
                this._state = state;
            }
        }

        public bool AutoDropState
        {
            get
            {
                lock (this)
                {
                    return this._autoDropState;
                }
            }
            set
            {
                lock (this)
                {
                    this._autoDropState = value;
                }
            }
        }

        public long ContentLength
        {
            get
            {
                return this._contentLength;
            }
        }

        public string CurrentFileName
        {
            get
            {
                return this._currentFileName;
            }
        }

        public long Position
        {
            get
            {
                return this._position;
            }
        }

        public DateTime Start
        {
            get
            {
                return this._start;
            }
        }

        public UploadState State
        {
            get
            {
                return this._state;
            }
        }

        internal string UploadId
        {
            get
            {
                return this._uploadId;
            }
        }
    }
}

