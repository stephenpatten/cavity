﻿namespace Cavity.Net
{
    using Cavity.Net.Mime;

    public interface IHttpMessage
    {
        IContent Body { get; }

        IHttpHeaderCollection Headers { get; }
    }
}