﻿namespace Cavity.Net
{
    using System;
    using System.IO;
    using System.Text;
    using Cavity;
    using Cavity.Net.Mime;
    using Cavity.Text;
    using Microsoft.Practices.ServiceLocation;
    using Moq;
    using Xunit;

    public sealed class HttpMessageFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<HttpMessage>()
                .DerivesFrom<ComparableObject>()
                .IsAbstractBaseClass()
                .IsNotDecorated()
                .Implements<IHttpMessage>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new DerivedHttpMessage() as HttpMessage);
        }

        [Fact]
        public void prop_Body()
        {
            Assert.NotNull(new PropertyExpectations<DerivedHttpMessage>("Body")
                .TypeIs<IContent>()
                .ArgumentNullException()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Headers()
        {
            Assert.NotNull(new PropertyExpectations<DerivedHttpMessage>("Headers")
                .TypeIs<HttpHeaderCollection>()
                .ArgumentNullException()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void op_Read_TextReader()
        {
            HttpMessage message = new DerivedHttpMessage();
            HttpHeader connection = "Connection: close";

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(connection);
                    writer.WriteLine(string.Empty);
                    writer.WriteLine("body");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        message.Read(reader);
                    }
                }
            }

            Assert.Equal<int>(1, message.Headers.Count);
            Assert.True(message.Headers.Contains(connection));
        }

        [Fact]
        public void op_Read_TextReaderEmpty()
        {
            HttpMessage message = new DerivedHttpMessage();

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new StreamReader(stream))
                    {
                        message.Read(reader);
                    }
                }
            }

            Assert.Empty(message.Headers);
        }

        [Fact]
        public void op_Read_TextReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DerivedHttpMessage().Read(null as TextReader));
        }

        [Fact]
        public void op_ToString()
        {
            StringBuilder expected = new StringBuilder();
            expected.AppendLine("Content-Length: 4");
            expected.AppendLine("Content-Type: text/plain; charset=UTF-8");
            expected.AppendLine("Host: www.example.com");
            expected.AppendLine("Connection: keep-alive");
            expected.AppendLine(string.Empty);
            expected.Append("text");

            HttpMessage obj = new DerivedHttpMessage();

            try
            {
                var locator = new Mock<IServiceLocator>();
                locator.Setup(e => e.GetInstance<IMediaType>("text/plain")).Returns(new TextPlain()).Verifiable();
                ServiceLocator.SetLocatorProvider(new ServiceLocatorProvider(() => locator.Object));

                using (var stream = new MemoryStream())
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(expected);
                        writer.Flush();
                        stream.Position = 0;
                        using (var reader = new StreamReader(stream))
                        {
                            obj.Read(reader);
                        }
                    }
                }

                locator.VerifyAll();
            }
            finally
            {
                ServiceLocator.SetLocatorProvider(null);
            }

            string actual = obj.ToString();

            Assert.Equal<string>(expected.ToString(), actual);
        }
    }
}