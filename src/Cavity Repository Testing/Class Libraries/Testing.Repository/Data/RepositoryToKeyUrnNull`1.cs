﻿namespace Cavity.Data
{
    using System;

    using Cavity.Properties;

    public sealed class RepositoryToKeyUrnNull<T> : VerifyRepositoryBase<T>
        where T : new()
    {
        protected override void OnVerify(IRepository<T> repository)
        {
            if (null == repository)
            {
                throw new ArgumentNullException("repository");
            }

            ArgumentNullException expected = null;
            try
            {
                repository.ToKey(null);
            }
            catch (ArgumentNullException exception)
            {
                expected = exception;
            }
            catch (Exception exception)
            {
                throw new RepositoryTestException(Resources.Repository_UnexpectedException_ExceptionMessage, exception);
            }

            if (null == expected)
            {
                throw new RepositoryTestException(Resources.Repository_ExpectExceptionWhenUrnNull_ExceptionMessage.FormatWith("ToKey"));
            }
        }
    }
}