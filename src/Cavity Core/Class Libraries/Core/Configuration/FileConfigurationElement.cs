﻿namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using Cavity.Diagnostics;

    public sealed class FileConfigurationElement : ConfigurationElement
    {
        private static readonly ConfigurationProperty _file = new ConfigurationProperty("file",
                                                                                        typeof(FileInfo),
                                                                                        null,
                                                                                        new FileInfoConverter(),
                                                                                        new FileInfoValidator(),
                                                                                        ConfigurationPropertyOptions.IsRequired);

        public FileConfigurationElement()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            Properties.Add(_file);
        }

        public FileConfigurationElement(string name,
                                        FileInfo file)
            : this()
        {
            Name = name;
            File = file;
        }

        public FileInfo File
        {
            get
            {
                Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
                return (FileInfo)this["file"];
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
                this["file"] = value;
            }
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }

            set
            {
                this["name"] = value;
            }
        }
    }
}