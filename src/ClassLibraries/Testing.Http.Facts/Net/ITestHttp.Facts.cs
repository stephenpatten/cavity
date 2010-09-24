﻿namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class ITestHttpFacts
    {
        [Fact]
        public void ITestHttp_prop_Result()
        {
            try
            {
                var value = (new ITestHttpDummy() as ITestHttp).Result;
                Assert.True(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(ITestHttp).IsInterface);
        }
    }
}