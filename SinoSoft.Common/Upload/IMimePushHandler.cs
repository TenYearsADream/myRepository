namespace SinoSoft.Common.Upload
{
    using System;
    using System.Collections.Specialized;

    internal interface IMimePushHandler
    {
        void BeginPart(NameValueCollection headers);
        void EndPart(bool isLast);
        void PartData(ref byte[] data);
    }
}

