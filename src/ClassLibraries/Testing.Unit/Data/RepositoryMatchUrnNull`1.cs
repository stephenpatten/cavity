﻿namespace Cavity.Data
{
    using System;
    using System.Transactions;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class RepositoryMatchUrnNull<T> : IVerifyRepository<T>
    {
        public void Verify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            using (new TransactionScope())
            {
                ArgumentNullException expected = null;
                try
                {
                    repository.Match(null as AbsoluteUri, "\"abc\"");
                }
                catch (ArgumentNullException exception)
                {
                    expected = exception;
                }
                catch (Exception exception)
                {
                    throw new UnitTestException(Resources.Repository_Match_UrnNull_UnitTestExceptionMessage, exception);
                }

                if (null == expected)
                {
                    throw new UnitTestException(Resources.Repository_Match_UrnNull_UnitTestExceptionMessage);
                }
            }
        }
    }
}