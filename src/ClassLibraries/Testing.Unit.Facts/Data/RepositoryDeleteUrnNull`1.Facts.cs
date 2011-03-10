﻿namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryDeleteUrnNullOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryDeleteUrnNull<int>>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IVerifyRepository<int>>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new RepositoryDeleteUrnNull<int>());
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "This is only for testing purposes.")]
        public void op_Verify_IRepository()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Delete(null))
                .Throws(new ArgumentNullException())
                .Verifiable();

            new RepositoryDeleteUrnNull<int>().Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryDeleteUrnNull<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenExceptionIsNotThrown()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Delete(null))
                .Returns(false)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => new RepositoryDeleteUrnNull<int>().Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenExceptionIsUnexpectedlyThrown()
        {
            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Delete(null))
                .Throws(new InvalidOperationException())
                .Verifiable();

            Assert.Throws<UnitTestException>(() => new RepositoryDeleteUrnNull<int>().Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}