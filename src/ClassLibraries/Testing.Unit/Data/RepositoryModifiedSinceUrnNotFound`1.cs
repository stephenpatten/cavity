﻿namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryModifiedSinceUrnNotFound<T> : VerifyRepositoryBase<T> where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            if (repository.ModifiedSince("urn://example.com/" + Guid.NewGuid(), DateTime.MinValue))
            {
                throw new UnitTestException(Resources.Repository_ModifiedSince_ReturnsTrue_UnitTestExceptionMessage);
            }
        }
    }
}