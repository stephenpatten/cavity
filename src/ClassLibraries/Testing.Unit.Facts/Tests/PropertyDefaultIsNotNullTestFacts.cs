﻿namespace Cavity.Tests
{
    using System.Reflection;
    using Cavity.Types;
    using Xunit;

    public class PropertyDefaultIsNotNullTestFacts
    {
        [Fact]
        public void is_PropertyTest()
        {
            Assert.IsAssignableFrom<PropertyTest>(new PropertyDefaultIsNotNullTest(typeof(PropertiedClass1).GetProperty("AutoString")));
        }

        [Fact]
        public void ctor_PropertyInfo_object()
        {
            Assert.NotNull(new PropertyDefaultIsNotNullTest(typeof(PropertiedClass1).GetProperty("AutoString")));
        }

        [Fact]
        public void op_Check_whenFalse()
        {
            Assert.Throws<TestException>(() => new PropertyDefaultIsNotNullTest(typeof(PropertiedClass1).GetProperty("AutoString")).Check());
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            Assert.True(new PropertyDefaultIsNotNullTest(typeof(PropertiedClass1).GetProperty("AutoBoolean")).Check());
        }
    }
}