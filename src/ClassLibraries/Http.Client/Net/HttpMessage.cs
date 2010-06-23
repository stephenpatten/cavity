﻿namespace Cavity.Net
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cavity.Net.Mime;

    public abstract class HttpMessage : ComparableObject, IHttpMessage
    {
        private IContent _body;
        private IHttpHeaderCollection _headers;

        protected HttpMessage(IHttpHeaderCollection headers, IContent body)
            : this()
        {
            this.Headers = headers;
            this.Body = body;
        }

        protected HttpMessage()
        {
        }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "The setter is protected rather than private for testability.")]
        public IContent Body
        {
            get
            {
                return this._body;
            }

            protected set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._body = value;
            }
        }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "The setter is protected rather than private for testability.")]
        public IHttpHeaderCollection Headers
        {
            get
            {
                return this._headers;
            }

            protected set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._headers = value;
            }
        }
    }
}