namespace SinoSoft.Common.Upload
{
    using System;
    using System.Collections.Specialized;
    using System.Text;

    internal class MimeHeaderReader
    {
        private bool _headerComplete;
        private Encoding encoding;
        private StringBuilder headers;

        public MimeHeaderReader(Encoding e)
        {
            this.encoding = e;
            this.Reset();
        }

        public int Read(byte[] buffer, int position)
        {
            int count = 0;
            HeaderReaderState reading = HeaderReaderState.Reading;
            for (int i = position; (i < buffer.Length) && (reading != HeaderReaderState.FoundSecondLF); i++)
            {
                switch (((char) buffer[i]))
                {
                    case '\r':
                        switch (reading)
                        {
                            case HeaderReaderState.Reading:
                                reading = HeaderReaderState.FoundFirstCR;
                                break;

                            case HeaderReaderState.FoundFirstCR:
                            case HeaderReaderState.FoundFirstLF:
                                reading = HeaderReaderState.FoundSecondCR;
                                break;
                        }
                        break;

                    case '\n':
                        switch (reading)
                        {
                            case HeaderReaderState.Reading:
                            case HeaderReaderState.FoundFirstCR:
                                reading = HeaderReaderState.FoundFirstLF;
                                break;

                            case HeaderReaderState.FoundFirstLF:
                            case HeaderReaderState.FoundSecondCR:
                                reading = HeaderReaderState.FoundSecondLF;
                                break;
                        }
                        break;

                    default:
                        reading = HeaderReaderState.Reading;
                        break;
                }
                count++;
            }
            this.headers.Append(this.encoding.GetString(buffer, position, count));
            if (reading == HeaderReaderState.FoundSecondLF)
            {
                this._headerComplete = true;
                string str = this.headers.ToString(this.headers.Length - 4, 4);
                if (str[2] == '\r')
                {
                    this.headers.Length -= 4;
                    return count;
                }
                if (str[2] == '\n')
                {
                    this.headers.Length -= 2;
                }
            }
            return count;
        }

        public void Reset()
        {
            this.headers = new StringBuilder();
            this._headerComplete = false;
        }

        public bool HeaderComplete
        {
            get
            {
                return this._headerComplete;
            }
        }

        public NameValueCollection Headers
        {
            get
            {
                NameValueCollection values = new NameValueCollection();
                foreach (string str in this.headers.ToString().Split(new char[] { '\n' }))
                {
                    int index = str.IndexOf(':');
                    values[str.Substring(0, index).Trim()] = str.Substring(index + 1).Trim();
                }
                return values;
            }
        }

        private enum HeaderReaderState
        {
            Reading,
            FoundFirstCR,
            FoundFirstLF,
            FoundSecondCR,
            FoundSecondLF
        }
    }
}

