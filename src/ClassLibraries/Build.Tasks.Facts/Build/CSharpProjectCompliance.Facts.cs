﻿namespace Cavity.Build
{
    using System.IO;
    using System.Reflection;
    using System.Xml;
    using Cavity.IO;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;
    using Moq;
    using Xunit;

    public sealed class CSharpProjectComplianceFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<CSharpProjectCompliance>()
                            .DerivesFrom<Task>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new CSharpProjectCompliance());
        }

        [Fact]
        public void op_Execute_IEnumerable()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(reader.ReadToEnd());
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Projects = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    },
                    XPath = "0=count(/b:Project/b:PropertyGroup[@Condition][not(b:WarningLevel[text()='4'])])"
                };

                Assert.True(obj.Execute());
            }
        }

        [Fact]
        public void op_Execute_IEnumerableEmpty()
        {
            var obj = new CSharpProjectCompliance
            {
                BuildEngine = new Mock<IBuildEngine>().Object,
                Projects = new ITaskItem[]
                {
                }
            };

            Assert.False(obj.Execute());
        }

        [Fact]
        public void op_Execute_IEnumerableNull()
        {
            var obj = new CSharpProjectCompliance
            {
                BuildEngine = new Mock<IBuildEngine>().Object
            };

            Assert.False(obj.Execute());
        }

        [Fact]
        public void op_Execute_IEnumerable_whenWarningLevelMissing()
        {
            using (var file = new TempFile())
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"Cavity.Build.CSharpProjectCompliance.xml"))
                {
                    if (null != resource)
                    {
                        using (var reader = new StreamReader(resource))
                        {
                            var xml = new XmlDocument();
                            xml.LoadXml(reader.ReadToEnd());
                            var namespaces = new XmlNamespaceManager(xml.NameTable);
                            namespaces.AddNamespace("b", "http://schemas.microsoft.com/developer/msbuild/2003");
                            var node = xml.SelectSingleNode("/b:Project/b:PropertyGroup[@Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \"]/b:WarningLevel", namespaces);
                            if (null != node &&
                                null != node.ParentNode)
                            {
                                node.ParentNode.RemoveChild(node);
                            }

                            using (var stream = file.Info.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                            {
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(xml.OuterXml);
                                }
                            }
                        }
                    }
                }

                var obj = new CSharpProjectCompliance
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Projects = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    },
                    XPath = "0=count(/b:Project/b:PropertyGroup[@Condition][not(b:WarningLevel[text()='4'])])"
                };

                Assert.False(obj.Execute());
            }
        }

        [Fact]
        public void prop_Projects()
        {
            Assert.True(new PropertyExpectations<CSharpProjectCompliance>(p => p.Projects)
                            .IsAutoProperty<ITaskItem[]>()
                            .IsDecoratedWith<RequiredAttribute>()
                            .Result);
        }

        [Fact]
        public void prop_XPath()
        {
            Assert.True(new PropertyExpectations<CSharpProjectCompliance>(p => p.XPath)
                            .IsAutoProperty<ITaskItem[]>()
                            .IsDecoratedWith<RequiredAttribute>()
                            .Result);
        }
    }
}