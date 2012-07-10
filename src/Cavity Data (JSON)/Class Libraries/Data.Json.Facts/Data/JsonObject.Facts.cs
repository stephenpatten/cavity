﻿namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Xunit;
    using Xunit.Extensions;

    public sealed class JsonObjectFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonObject>()
                            .DerivesFrom<JsonValue>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IEnumerable<JsonPair>>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new JsonObject());
        }

        [Fact]
        public void opIndexer_int()
        {
            var expected = new JsonPair("name", "value");

            var document = new JsonObject
                               {
                                   expected
                               };

            var actual = document[0];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opIndexer_intNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new JsonObject()[-1]);
        }

        [Fact]
        public void opIndexer_int_whenEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new JsonObject()[0]);
        }

        [Fact]
        public void opIndexer_string()
        {
            const string name = "name";
            var expected = new JsonPair(name, "value");

            var document = new JsonObject
                               {
                                   expected
                               };

            var actual = document[name];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opIndexer_string_whenNotFound()
        {
            Assert.Throws<KeyNotFoundException>(() => new JsonObject()["example"]);
        }

        [Fact]
        public void op_Add_JsonPair()
        {
            var expected = new JsonPair("name", "value");

            var document = new JsonObject
                               {
                                   expected
                               };

            var actual = document.First();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Add_JsonPairNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonObject().Add(null));
        }

        [Fact]
        public void op_Add_string_JsonValue()
        {
            const string name = "name";
            var expected = new JsonString("value");

            var document = new JsonObject
                               {
                                   { name, expected }
                               };

            var actual = document.String(name);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Array_string()
        {
            const string name = "name";
            var expected = new JsonArray();

            var document = new JsonObject
                               {
                                   new JsonPair(name, expected)
                               };

            var actual = document.Array(name);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Array_string_whenNullValue()
        {
            const string name = "name";

            var document = new JsonObject
                               {
                                   new JsonPair(name, new JsonNull())
                               };

            Assert.Null(document.Array(name));
        }

        [Fact]
        public void op_Boolean_string_whenFalse()
        {
            const string name = "name";

            var document = new JsonObject
                               {
                                   new JsonPair(name, new JsonFalse())
                               };

            Assert.False(document.Boolean(name));
        }

        [Fact]
        public void op_Boolean_string_whenInvalidCastException()
        {
            const string name = "name";

            var document = new JsonObject
                               {
                                   new JsonPair(name, new JsonNull())
                               };

            Assert.Throws<InvalidCastException>(() => document.Boolean(name));
        }

        [Fact]
        public void op_Boolean_string_whenTrue()
        {
            const string name = "name";

            var document = new JsonObject
                               {
                                   new JsonPair(name, new JsonTrue())
                               };

            Assert.True(document.Boolean(name));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var expected = new JsonPair("name", "value");

            IEnumerable document = new JsonObject
                                       {
                                           expected
                                       };

            foreach (var actual in document)
            {
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_IsNull_string_whenFalse()
        {
            const string name = "name";

            var document = new JsonObject
                               {
                                   new JsonPair(name, new JsonString("value"))
                               };

            Assert.False(document.IsNull(name));
        }

        [Fact]
        public void op_IsNull_string_whenTrue()
        {
            const string name = "name";

            var document = new JsonObject
                               {
                                   new JsonPair(name, new JsonNull())
                               };

            Assert.True(document.IsNull(name));
        }

        [Fact]
        public void op_Number_string()
        {
            const string name = "name";
            var expected = new JsonNumber("value");

            var document = new JsonObject
                               {
                                   new JsonPair(name, expected)
                               };

            var actual = document.Number(name);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Number_string_whenNullValue()
        {
            const string name = "name";

            var document = new JsonObject
                               {
                                   new JsonPair(name, new JsonNull())
                               };

            Assert.Null(document.Number(name));
        }

        [Fact]
        public void op_Object_string()
        {
            const string name = "name";
            var expected = new JsonObject();

            var document = new JsonObject
                               {
                                   new JsonPair(name, expected)
                               };

            var actual = document.Object(name);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Object_string_whenNullValue()
        {
            const string name = "name";

            var document = new JsonObject
                               {
                                   new JsonPair(name, new JsonNull())
                               };

            Assert.Null(document.Object(name));
        }

        [Theory]
        [InlineData("{\"name\" : \"value\", \"range\" : [1,2,3], \"id\" : 123, \"visible\" : true, \"enabled\" : false, \"check\" : null, \"child\" : { \"value\" : 1.23 }}")]
        public void op_ReadJson_JsonReader(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        reader.Read();

                        var obj = new JsonObject();
                        obj.ReadJson(reader);

                        Assert.Equal("value", obj.String("name").Value);
                        Assert.Equal("1", obj.Array("range").Number(0).Value);
                        Assert.Equal("2", obj.Array("range").Number(1).Value);
                        Assert.Equal("3", obj.Array("range").Number(2).Value);
                        Assert.Equal("123", obj.Number("id").Value);
                        Assert.True(obj.Boolean("visible"));
                        Assert.False(obj.Boolean("enabled"));
                        Assert.Null(obj.String("check"));
                        Assert.Equal("1.23", obj.Object("child").Number("value").Value);
                    }
                }
            }
        }

        [Fact]
        public void op_ReadJson_JsonReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonObject().ReadJson(null));
        }

        [Theory]
        [InlineData("{}")]
        public void op_ReadJson_JsonReader_whenEmpty(string json)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(json);
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        reader.Read();

                        var obj = new JsonObject();
                        obj.ReadJson(reader);

                        Assert.Empty(obj);
                    }
                }
            }
        }

        [Fact]
        public void op_ReadJson_JsonReader_whenNotObjectNode()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write("{}");
                    writer.Flush();
                    stream.Position = 0;
                    using (var reader = new JsonReader(stream))
                    {
                        // ReSharper disable AccessToDisposedClosure
                        Assert.Throws<InvalidOperationException>(() => new JsonObject().ReadJson(reader));

                        // ReSharper restore AccessToDisposedClosure
                    }
                }
            }
        }

        [Fact]
        public void op_String_string()
        {
            const string name = "name";
            var expected = new JsonString("value");

            var document = new JsonObject
                               {
                                   new JsonPair(name, expected)
                               };

            var actual = document.String(name);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_String_string_whenNullValue()
        {
            const string name = "name";

            var document = new JsonObject
                               {
                                   new JsonPair(name, new JsonNull())
                               };

            Assert.Null(document.String(name));
        }

        [Fact]
        public void prop_Count()
        {
            Assert.True(new PropertyExpectations<JsonObject>(x => x.Count)
                            .TypeIs<int>()
                            .DefaultValueIs(0)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Count_get()
        {
            var document = new JsonObject();
            Assert.Equal(0, document.Count);

            document.Add(new JsonPair("name", "value"));

            Assert.Equal(1, document.Count);
        }
    }
}