﻿namespace Cavity.Data
{
    using System;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryMatchUrnNotFound<T> : VerifyRepositoryBase<T>
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            if (repository.Match("urn://example.com/" + Guid.NewGuid(), Guid.NewGuid().ToString()))
            {
                throw new UnitTestException(Resources.Repository_Match_ReturnsTrue_UnitTestExceptionMessage);
            }
        }
    }
}