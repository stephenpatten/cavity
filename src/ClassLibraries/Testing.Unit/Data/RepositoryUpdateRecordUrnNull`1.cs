﻿namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;
    using Moq;

    public sealed class RepositoryUpdateRecordUrnNull<T> : IVerifyRepository<T>
    {
        public RepositoryUpdateRecordUrnNull()
        {
            var key = AlphaDecimal.Random();

            var record = new Mock<IRecord<T>>()
                .SetupProperty(x => x.Urn);
            record
                .SetupGet(x => x.Key)
                .Returns(key);
            record.Object.Urn = "urn://example.com/" + Guid.NewGuid();
            Record = record;
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is required for mocking.")]
        public Mock<IRecord<T>> Record { get; set; }

        public void Verify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            using (new TransactionScope())
            {
                var record = repository.Insert(Record.Object);
                record.Urn = null;

                RepositoryException expected = null;
                try
                {
                    repository.Update(record);
                }
                catch (RepositoryException exception)
                {
                    expected = exception;
                }
                catch (Exception exception)
                {
                    throw new UnitTestException(Resources.Repository_Update_RecordUrnNull_UnitTestExceptionMessage, exception);
                }

                if (null == expected)
                {
                    throw new UnitTestException(Resources.Repository_Update_RecordUrnNull_UnitTestExceptionMessage);
                }
            }
        }
    }
}