namespace SinoSoft.Common.Upload
{
    using System;
    using System.IO;
    using System.Web;

    internal class RequestStream : Stream
    {
        protected long _position;
        private bool isInPreloaded = true;
        protected HttpWorkerRequest request;
        private byte[] tempBuff;

        public RequestStream(HttpWorkerRequest request)
        {
            this.request = request;
            this.tempBuff = request.GetPreloadedEntityBody();
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int num = 0;
            if (this.isInPreloaded)
            {
                num = this.ReadPreloaded(buffer, offset, count);
                if (num < count)
                {
                    if (this.request.IsClientConnected() && !this.request.IsEntireEntityBodyIsPreloaded())
                    {
                        num += this.ReadNormal(buffer, num, count - num);
                    }
                    this.isInPreloaded = false;
                }
            }
            else if (this.request.IsClientConnected() && !this.request.IsEntireEntityBodyIsPreloaded())
            {
                num = this.ReadNormal(buffer, offset, count);
            }
            this._position += num;
            if (num == 0)
            {
                throw new DisconnectedException();
            }
            return num;
        }

        private int ReadNormal(byte[] buffer, int offset, int count)
        {
            if ((this._position + count) > this.Length)
            {
                count = (int) (this.Length - this._position);
            }
            if (offset <= 0)
            {
                return this.request.ReadEntityBody(buffer, count);
            }
            if (count > this.tempBuff.Length)
            {
                this.tempBuff = new byte[count];
            }
            int num = this.request.ReadEntityBody(this.tempBuff, count);
            Buffer.BlockCopy(this.tempBuff, 0, buffer, offset, num);
            return num;
        }

        private int ReadPreloaded(byte[] buffer, int offset, int count)
        {
            int num;
            if ((this._position + count) < this.tempBuff.Length)
            {
                num = count;
            }
            else
            {
                num = this.tempBuff.Length - ((int) this._position);
            }
            Buffer.BlockCopy(this.tempBuff, (int) this._position, buffer, offset, num);
            return num;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override long Length
        {
            get
            {
                return long.Parse(this.request.GetKnownRequestHeader(11));
            }
        }

        public override long Position
        {
            get
            {
                return this._position;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}

