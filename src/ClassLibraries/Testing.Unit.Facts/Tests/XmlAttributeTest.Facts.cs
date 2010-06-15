﻿namespace Cavity.Tests
{
    using Cavity.Types;
    using Xunit;

    public class XmlAttributeTestFacts
    {
        [Fact]
        public void is_AttributePropertyTest()
        {
            Assert.IsAssignableFrom<MemberTest>(new XmlAttributeTest(typeof(XmlSerializableClass1).GetProperty("Attribute")));
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new XmlAttributeTest(typeof(XmlSerializableClass1).GetProperty("Attribute")));
        }

        [Fact]
        public void prop_AttributeName()
        {
            var obj = new XmlAttributeTest(typeof(XmlSerializableClass1).GetProperty("Attribute"));

            string expected = "example";
            obj.AttributeName = expected;
            string actual = obj.AttributeName;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void prop_Namespace()
        {
            var obj = new XmlAttributeTest(typeof(XmlSerializableClass1).GetProperty("Attribute"));

            string expected = "example";
            obj.Namespace = expected;
            string actual = obj.Namespace;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            var obj = new XmlAttributeTest(typeof(XmlSerializableClass1).GetProperty("NamespacedAttribute"))
            {
                AttributeName = "attribute",
                Namespace = "urn:example.org"
            };

            Assert.True(obj.Check());
        }

        [Fact]
        public void op_Check_whenXmlAttributeMissing()
        {
            var obj = new XmlAttributeTest(typeof(PropertiedClass1).GetProperty("AutoBoolean"));

            Assert.Throws<TestException>(() => obj.Check());
        }

        [Fact]
        public void op_Check_whenNameWrong()
        {
            var obj = new XmlAttributeTest(typeof(XmlSerializableClass1).GetProperty("Attribute"))
            {
                AttributeName = "xxx"
            };

            Assert.Throws<TestException>(() => obj.Check());
        }

        [Fact]
        public void op_Check_whenNamespaceWrong()
        {
            var obj = new XmlAttributeTest(typeof(XmlSerializableClass1).GetProperty("Attribute"))
            {
                AttributeName = "attribute",
                Namespace = "xxx"
            };

            Assert.Throws<TestException>(() => obj.Check());
        }
    }
}