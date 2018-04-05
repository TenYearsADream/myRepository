namespace SinoSoft.Common.Upload
{
    using System;
    using System.IO;
    using System.Text;

    internal sealed class MimePushReader
    {
        private byte[] boundary;
        private Encoding encoding;
        private IMimePushHandler handler;
        private Stream stream;

        public MimePushReader(Stream s, byte[] b, IMimePushHandler h, Encoding e)
        {
            this.stream = s;
            this.handler = h;
            this.boundary = b;
            this.encoding = e;
        }

        private int IndexOf(byte[] buffer, byte[] pattern, int start, int end)
        {
            int index = 0;
            int num2 = Array.IndexOf(buffer, pattern[0], start);
            if (num2 != -1)
            {
                while ((num2 + index) < buffer.Length)
                {
                    if (buffer[num2 + index] == pattern[index])
                    {
                        index++;
                        if (index == pattern.Length)
                        {
                            return num2;
                        }
                    }
                    else
                    {
                        num2 = Array.IndexOf(buffer, pattern[0], num2 + index);
                        if (num2 == -1)
                        {
                            return num2;
                        }
                        index = 0;
                    }
                }
            }
            return num2;
        }

        public void Parse()
        {
            MimeReaderState readingHeaders = MimeReaderState.ReadingHeaders;
            MimeHeaderReader reader = new MimeHeaderReader(this.encoding);
            byte[] buffer = new byte[0x2000];
            int end = this.stream.Read(buffer, 0, 0x2000);
            int index = this.IndexOf(buffer, this.boundary, 0, end) + this.boundary.Length;
            if (buffer[index] == 13)
            {
                index += 2;
            }
            else if (buffer[index] == 10)
            {
                index++;
            }
            while ((end > 0) && (readingHeaders != MimeReaderState.Finished))
            {
                int num4;
                int num5;
                switch (readingHeaders)
                {
                    case MimeReaderState.ReadingHeaders:
                    {
                        int num3 = reader.Read(buffer, index);
                        index += num3;
                        if (reader.HeaderComplete)
                        {
                            readingHeaders = MimeReaderState.ReadingBody;
                            this.handler.BeginPart(reader.Headers);
                            reader.Reset();
                        }
                        goto Label_0299;
                    }
                    case MimeReaderState.ReadingBody:
                        num4 = this.IndexOf(buffer, this.boundary, index, end);
                        if (num4 != -1)
                        {
                            goto Label_0124;
                        }
                        if (index != 0)
                        {
                            break;
                        }
                        this.handler.PartData(ref buffer);
                        goto Label_0116;

                    case MimeReaderState.CheckingEnd:
                        if (!(this.encoding.GetString(buffer, 0, 2) == "--"))
                        {
                            goto Label_0282;
                        }
                        readingHeaders = MimeReaderState.Finished;
                        goto Label_028A;

                    default:
                        goto Label_0299;
                }
                byte[] dst = new byte[end - index];
                Buffer.BlockCopy(buffer, index, dst, 0, dst.Length);
                this.handler.PartData(ref dst);
            Label_0116:
                index += end - index;
                goto Label_0299;
            Label_0124:
                num5 = num4 - index;
                if (buffer[num4 - 2] == 13)
                {
                    num5 -= 2;
                }
                else if (buffer[num4 - 2] == 10)
                {
                    num5--;
                }
                byte[] buffer3 = new byte[num5];
                Buffer.BlockCopy(buffer, index, buffer3, 0, buffer3.Length);
                this.handler.PartData(ref buffer3);
                if (num4 < (buffer.Length - this.boundary.Length))
                {
                    if (num4 < (buffer.Length - (this.boundary.Length + 2)))
                    {
                        bool isLast = this.encoding.GetString(buffer, num4 + this.boundary.Length, 2) == "--";
                        if (isLast)
                        {
                            readingHeaders = MimeReaderState.Finished;
                        }
                        else
                        {
                            readingHeaders = MimeReaderState.ReadingHeaders;
                        }
                        this.handler.EndPart(isLast);
                        index += ((num4 + this.boundary.Length) - index) + 2;
                    }
                    else
                    {
                        readingHeaders = MimeReaderState.CheckingEnd;
                        if (((num4 + 2) - buffer.Length) == 1)
                        {
                            num4 = 1;
                            buffer[0] = buffer[buffer.Length - 1];
                        }
                        else
                        {
                            num4 = 0;
                        }
                        end = this.stream.Read(buffer, num4, buffer.Length - num4);
                        index = 0;
                    }
                }
                else
                {
                    num4 -= (num4 - index) - num5;
                    Buffer.BlockCopy(buffer, num4, buffer, 0, buffer.Length - num4);
                    end = this.stream.Read(buffer, buffer.Length - num4, buffer.Length - (buffer.Length - num4));
                    index = 0;
                }
                goto Label_0299;
            Label_0282:
                index += 2;
                readingHeaders = MimeReaderState.ReadingHeaders;
            Label_028A:
                this.handler.EndPart(readingHeaders == MimeReaderState.Finished);
            Label_0299:
                if ((readingHeaders != MimeReaderState.Finished) && ((index >= buffer.Length) || (index >= end)))
                {
                    end = this.stream.Read(buffer, 0, buffer.Length);
                    index = 0;
                }
            }
        }

        private enum MimeReaderState
        {
            ReadingHeaders,
            ReadingBody,
            CheckingEnd,
            Finished
        }
    }
}

