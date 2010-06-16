﻿namespace Cavity
{
    using System;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Types;
    using Xunit;

    public sealed class PropertyExpectationsOfTFacts
    {
        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new PropertyExpectations<PropertiedClass1>("AutoBoolean"));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PropertyExpectations<Class1>(null as string));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new PropertyExpectations<Class1>(string.Empty));
        }

        [Fact]
        public void prop_Result_whenAuto()
        {
            Assert.True(new PropertyExpectations<PropertiedClass1>("AutoString")
                .IsAutoProperty<string>()
                .Result);
        }

        [Fact]
        public void prop_Result_whenAutoBoolean()
        {
            Assert.True(new PropertyExpectations<PropertiedClass1>("AutoBoolean")
                .TypeIs<bool>()
                .DefaultValueIs(false)
                .DefaultValueIsNotNull()
                .Set<bool>()
                .Set(true)
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Result_whenAutoString()
        {
            Assert.True(new PropertyExpectations<PropertiedClass1>("AutoString")
                .TypeIs<string>()
                .DefaultValueIsNull()
                .Set<string>()
                .Set(string.Empty)
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Result_whenIsDecorated()
        {
            Assert.True(new PropertyExpectations<AttributedClass1>("Value")
                .TypeIs<string>()
                .DefaultValueIsNull()
                .IsDecoratedWith<Attribute2>()
                .Result);
        }

        [Fact]
        public void prop_Result_whenXmlAttribute()
        {
            Assert.True(new PropertyExpectations<XmlSerializableClass1>("Attribute")
                .TypeIs<string>()
                .DefaultValueIsNull()
                .XmlAttribute("attribute")
                .Result);
        }

        [Fact]
        public void prop_Result_whenIsDecoratedWithXmlAttribute()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PropertyExpectations<XmlSerializableClass1>("Attribute")
                .TypeIs<string>()
                .DefaultValueIsNull()
                .IsDecoratedWith<XmlAttributeAttribute>());
        }

        [Fact]
        public void prop_Result_whenNamespacedXmlAttribute()
        {
            Assert.True(new PropertyExpectations<XmlSerializableClass1>("NamespacedAttribute")
                .TypeIs<string>()
                .DefaultValueIsNull()
                .XmlAttribute("attribute", "urn:example.org")
                .Result);
        }

        [Fact]
        public void prop_Result_whenXmlElement()
        {
            Assert.True(new PropertyExpectations<XmlSerializableClass1>("Element")
                .TypeIs<string>()
                .DefaultValueIsNull()
                .XmlElement("element")
                .Result);
        }

        [Fact]
        public void prop_Result_whenIsDecoratedWithXmlElement()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PropertyExpectations<XmlSerializableClass1>("Element")
                .TypeIs<string>()
                .DefaultValueIsNull()
                .IsDecoratedWith<XmlElementAttribute>());
        }

        [Fact]
        public void prop_Result_whenNamespacedXmlElement()
        {
            Assert.True(new PropertyExpectations<XmlSerializableClass1>("NamespacedElement")
                .TypeIs<string>()
                .DefaultValueIsNull()
                .XmlElement("element", "urn:example.org")
                .Result);
        }

        [Fact]
        public void prop_Result_whenXmlIgnore()
        {
            Assert.True(new PropertyExpectations<XmlSerializableClass1>("Ignore")
                .TypeIs<string>()
                .DefaultValueIsNull()
                .XmlIgnore()
                .Result);
        }

        [Fact]
        public void prop_Result_whenIsDecoratedWithXmlIgnore()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PropertyExpectations<XmlSerializableClass1>("Ignore")
                .TypeIs<string>()
                .DefaultValueIsNull()
                .IsDecoratedWith<XmlIgnoreAttribute>());
        }

        [Fact]
        public void prop_Result_whenXmlText()
        {
            Assert.True(new PropertyExpectations<XmlSerializableClass1>("Text")
                .TypeIs<string>()
                .DefaultValueIsNull()
                .XmlText()
                .Result);
        }

        [Fact]
        public void prop_Result_whenIsDecoratedWithXmlText()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PropertyExpectations<XmlSerializableClass1>("Text")
                .TypeIs<string>()
                .DefaultValueIsNull()
                .IsDecoratedWith<XmlTextAttribute>());
        }

        [Fact]
        public void prop_Result_whenXmlArray()
        {
            Assert.True(new PropertyExpectations<XmlSerializableClass1>("Array1")
                .TypeIs<string[]>()
                .DefaultValueIsNull()
                .XmlArray("array1", "item1")
                .XmlNamespaceDeclarations()
                .Result);
        }

        [Fact]
        public void prop_Result_whenIsDecoratedWithXmlArray()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PropertyExpectations<XmlSerializableClass1>("Array1")
                .TypeIs<string[]>()
                .DefaultValueIsNull()
                .IsDecoratedWith<XmlArrayAttribute>());
        }

        [Fact]
        public void prop_Result_whenIsDecoratedWithXmlNamespaceDeclarations()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PropertyExpectations<XmlSerializableClass1>("Array1")
                .TypeIs<string[]>()
                .DefaultValueIsNull()
                .IsDecoratedWith<XmlNamespaceDeclarationsAttribute>());
        }

        [Fact]
        public void op_ArgumentNullException()
        {
            Assert.True(new PropertyExpectations<PropertiedClass1>("ArgumentNullExceptionValue")
                .ArgumentNullException()
                .Result);
        }

        [Fact]
        public void op_ArgumentOutOfRangeException_object()
        {
            Assert.True(new PropertyExpectations<PropertiedClass1>("ArgumentOutOfRangeExceptionValue")
                .ArgumentOutOfRangeException(string.Empty)
                .Result);
        }

        [Fact]
        public void op_FormatException_object()
        {
            Assert.True(new PropertyExpectations<PropertiedClass1>("FormatExceptionValue")
                .FormatException(string.Empty)
                .Result);
        }

        [Fact]
        public void op_Exception_object_Type()
        {
            Assert.True(new PropertyExpectations<PropertiedClass1>("ArgumentExceptionValue")
                .Exception(string.Empty, typeof(ArgumentException))
                .Result);
        }

        [Fact]
        public void op_Exception_object_TypeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PropertyExpectations<PropertiedClass1>("AutoBoolean")
                .Exception(string.Empty, null as Type));
        }

        [Fact]
        public void op_Exception_object_TypeInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new PropertyExpectations<PropertiedClass1>("AutoBoolean")
                .Exception(string.Empty, typeof(int)));
        }
    }
}