﻿namespace Cavity.Net
{
    using System;
    using Cavity.Net.Mime;

    public class IHttpMessageDummy : IHttpMessage
    {
        public IContent Body
        {
            get
            {
                throw new NotSupportedException();
            }

            set
            {
                throw new NotSupportedException();
            }
        }

        public IHttpHeaderCollection Headers
        {
            get
            {
                throw new NotSupportedException();
            }
            
            set
            {
                throw new NotSupportedException();
            }
        }
    }
}