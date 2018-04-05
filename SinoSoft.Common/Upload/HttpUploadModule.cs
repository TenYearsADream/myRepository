namespace SinoSoft.Common.Upload
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Web;
    using System.Data;

    public sealed class HttpUploadModule : IHttpModule
    {
        /// <summary>
        /// 清除上传信息
        /// </summary>
        /// <param name="context"></param>
        private void CleanupFiles(HttpContext context)
        {
            MimeUploadHandler uploadHandler = this.GetUploadHandler(context);
            if (uploadHandler != null)
            {
                foreach (UploadedFile file in uploadHandler.UploadedFiles)
                {
                    File.Delete(file.ServerPath);
                }
                uploadHandler.UploadedFiles.Clear();
            }
        }
        /// <summary>
        /// 清除上传状态
        /// </summary>
        private void ClearUploadStatus()
        {
            RemoveFrom(HttpContext.Current.Application, GetUploadStatus().UploadId);
        }
        /// <summary>
        /// BeginRequest 处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            string caseParameter = HttpContext.Current.Request["Parameter"];
            if (this.IsUploadRequest(application.Request)
                && caseParameter =="MyModule")
            {
                HttpWorkerRequest workerRequest = this.GetWorkerRequest(application.Context);
                Encoding contentEncoding = application.Context.Request.ContentEncoding;
                if (workerRequest != null)
                {
                    byte[] boundary = this.ExtractBoundary(application.Request.ContentType, contentEncoding);
                    string uploadId = application.Request.QueryString["uploadId"];
                    MimeUploadHandler handler = new MimeUploadHandler(new RequestStream(workerRequest), boundary, uploadId, contentEncoding);
                    if (uploadId != null)
                    {
                        this.RegisterIn(application.Context, handler);
                    }
                    try
                    {
                        this.SetUploadState(application.Context, UploadState.ReceivingData);
                        handler.Parse();
                        this.InjectTextParts(workerRequest, contentEncoding.GetBytes(handler.TextParts));
                    }
                    catch (DisconnectedException)
                    {
                        this.CleanupFiles(application.Context);
                    }
                }
            }
        }
        //private bool CheckDemandProcess()
        //{
        //    DataSet dst = new DataSet();
        //    string fileName = HttpContext.Current.Request.MapPath("~/App_Data/UploadConfig.xml");
        //    dst.ReadXml(fileName,XmlReadMode.IgnoreSchema);

        //    if (dst != null && dst.Tables.Count>0)
        //    {
        //        DataTable dt = new DataTable();
        //        dt = dst.Tables[0];

        //        foreach (DataRow row in dt.Rows)
        //        {
        //            string thisPath = HttpContext.Current.Request.ServerVariables["PATH_TRANSLATED"];
        //            string nowPath=HttpContext.Current.Request.MapPath(row[""])
        //        }
        //    }
        //}
        /// <summary>
        /// EndRequest 处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void context_EndRequest(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            if (this.IsUploadRequest(application.Request))
            {
                this.SetUploadState(application.Context, UploadState.Complete);
                this.CleanupFiles(application.Context);
            }
            string uploadId = (string)application.Context.Items["__removeUploadStatus"];
            if ((uploadId != null) && (uploadId.Length > 0))
            {
                RemoveFrom(application.Application, uploadId);
            }
        }
        /// <summary>
        /// RequestError 处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void context_Error(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            if (this.IsUploadRequest(application.Request))
            {
                this.SetUploadState(application.Context, UploadState.Error);
                this.CleanupFiles(application.Context);
            }
        }

        private byte[] ExtractBoundary(string contentType, Encoding encoding)
        {
            int index = contentType.IndexOf("boundary=");
            if (index > 0)
            {
                return encoding.GetBytes("--" + contentType.Substring(index + 9));
            }
            return null;
        }
        /// <summary>
        /// 获取上传文件集合
        /// </summary>
        /// <returns></returns>
        public static UploadedFileCollection GetUploadedFiles()
        {
            return GetUploadedFiles(HttpContext.Current);
        }
        /// <summary>
        /// 获取上传文件集合
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static UploadedFileCollection GetUploadedFiles(HttpContext context)
        {
            MimeUploadHandler handler = (MimeUploadHandler)context.Items["_uploadHandler"];
            if (handler != null)
            {
                return UploadedFileCollection.ReadOnly(handler.UploadedFiles);
            }
            return null;
        }
        /// <summary>
        /// 获取上传信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private MimeUploadHandler GetUploadHandler(HttpContext context)
        {
            return (MimeUploadHandler)context.Items["_uploadHandler"];
        }
        /// <summary>
        /// 获取上传状态
        /// </summary>
        /// <returns></returns>
        public static UploadStatus GetUploadStatus()
        {
            return GetUploadStatus(HttpContext.Current);
        }
        /// <summary>
        /// 获取上传状态
        /// </summary>
        /// <param name="uploadId"></param>
        /// <returns></returns>
        public static UploadStatus GetUploadStatus(string uploadId)
        {
            HttpContext current = HttpContext.Current;
            UploadStatus uploadStatus = GetUploadStatus(current.Application, uploadId);
            if (((uploadStatus != null) && (uploadStatus.State != UploadState.ReceivingData)) && uploadStatus.AutoDropState)
            {
                current.Items["__removeUploadStatus"] = uploadId;
            }
            return uploadStatus;
        }
        /// <summary>
        /// 获取上传状态
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static UploadStatus GetUploadStatus(HttpContext context)
        {
            return GetUploadStatus(context.Request.QueryString["uploadId"]);
        }
        /// <summary>
        /// 获取上传状态
        /// </summary>
        /// <param name="application"></param>
        /// <param name="uploadId"></param>
        /// <returns></returns>
        public static UploadStatus GetUploadStatus(HttpApplicationState application, string uploadId)
        {
            return (UploadStatus)application["_UploadStatus_" + uploadId];
        }

        private HttpWorkerRequest GetWorkerRequest(HttpContext context)
        {
            return (HttpWorkerRequest)((IServiceProvider)HttpContext.Current).GetService(typeof(HttpWorkerRequest));
        }

        private void InjectTextParts(HttpWorkerRequest request, byte[] textParts)
        {
            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Instance;
            Type baseType = request.GetType();
            while ((baseType != null) && (baseType.FullName != "System.Web.Hosting.ISAPIWorkerRequest"))
            {
                baseType = baseType.BaseType;
            }
            if (baseType != null)
            {
                baseType.GetField("_contentAvailLength", bindingAttr).SetValue(request, textParts.Length);
                baseType.GetField("_contentTotalLength", bindingAttr).SetValue(request, textParts.Length);
                baseType.GetField("_preloadedContent", bindingAttr).SetValue(request, textParts);
                baseType.GetField("_preloadedContentRead", bindingAttr).SetValue(request, true);
            }
        }

        private bool IsUploadRequest(HttpRequest request)
        {
            return request.ContentType.ToLower().StartsWith("multipart/form-data");
        }

        private void RegisterIn(HttpContext context, MimeUploadHandler handler)
        {
            context.Items["_uploadHandler"] = handler;
            context.Application["_UploadStatus_" + handler.UploadStatus.UploadId] = handler.UploadStatus;
        }

        public static void RemoveFrom(string uploadId)
        {
            RemoveFrom(HttpContext.Current.Application, uploadId);
        }
        /// <summary>
        /// 移出指定上传的应用程序变量信息
        /// </summary>
        /// <param name="application"></param>
        /// <param name="uploadId"></param>
        public static void RemoveFrom(HttpApplicationState application, string uploadId)
        {
            application.Remove("_UploadStatus_" + uploadId);
        }
        /// <summary>
        /// 设置上传状态
        /// </summary>
        /// <param name="context"></param>
        /// <param name="state"></param>
        private void SetUploadState(HttpContext context, UploadState state)
        {
            MimeUploadHandler uploadHandler = this.GetUploadHandler(context);
            if (uploadHandler != null)
            {
                uploadHandler.UploadStatus.SetState(state);
            }
        }

        void IHttpModule.Dispose()
        {
        }

        void IHttpModule.Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(this.context_BeginRequest);
            context.Error += new EventHandler(this.context_Error);
            context.EndRequest += new EventHandler(this.context_EndRequest);
        }
    }
}

