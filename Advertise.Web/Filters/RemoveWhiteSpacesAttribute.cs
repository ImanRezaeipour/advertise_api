using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Advertise.Web.Filters
{
    public class RemoveWhiteSpacesAttribute : ActionFilterAttribute
    {
        #region Public Methods

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            bool skipRemoveWhiteSpacesAttribute = filterContext.ActionDescriptor.GetCustomAttributes(typeof(SkipRemoveWhiteSpacesAttribute), false).Any();

            if (skipRemoveWhiteSpacesAttribute)
            {
                return;
            }

            var response = filterContext.HttpContext.Response;

            if (filterContext.HttpContext.Request.RawUrl != "/sitemap.xml")
            {
                if (response.ContentType == "text/html" && response.Filter != null)
                {
                    response.Filter = new HelperClass(response.Filter);
                }
            }
        }

        #endregion Public Methods

        #region Private Classes

        private class HelperClass : Stream
        {
            #region Private Fields

            private System.IO.Stream Base;

            private StringBuilder s = new StringBuilder();

            #endregion Private Fields

            #region Public Constructors

            public HelperClass(System.IO.Stream ResponseStream)
            {
                if (ResponseStream == null)
                    throw new ArgumentNullException("ResponseStream");
                this.Base = ResponseStream;
            }

            #endregion Public Constructors

            #region Public Properties

            public override bool CanRead { get { return false; } }

            public override bool CanSeek { get { return false; } }

            public override bool CanWrite { get { return true; } }

            public override long Length { get { throw new NotSupportedException(); } }

            public override long Position
            {
                get { throw new NotSupportedException(); }
                set { throw new NotSupportedException(); }
            }

            #endregion Public Properties

            #region Public Methods

            public override void Flush()
            {
                Base.Flush();
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                throw new NotSupportedException();
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new NotSupportedException();
            }

            public override void SetLength(long value)
            {
                throw new NotSupportedException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                string HTML = Encoding.UTF8.GetString(buffer, offset, count);

                Regex reg = new Regex(@"(?<=\s)\s+(?![^<>]*</pre>)");
                HTML = reg.Replace(HTML, string.Empty);

                buffer = System.Text.Encoding.UTF8.GetBytes(HTML);
                this.Base.Write(buffer, 0, buffer.Length);
            }

            #endregion Public Methods
        }

        #endregion Private Classes
    }
}