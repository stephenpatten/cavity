﻿namespace Cavity.Data
{
    using System;
    using Cavity.Tests;
    using Moq;
    using Xunit;

    public sealed class RepositoryMatchKeyOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RepositoryMatchKey<int>>()
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
            Assert.NotNull(new RepositoryMatchKey<int>());
        }

        [Fact]
        public void op_Verify_IRepository()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositoryMatchKey<int>();
            obj.Record.Object.Key = key;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Match(key, obj.Record.Object.Etag))
                .Returns(true)
                .Verifiable();

            obj.Verify(repository.Object);

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepositoryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryMatchKey<int>().Verify(null));
        }

        [Fact]
        public void op_Verify_IRepository_whenFalse()
        {
            var key = AlphaDecimal.Random();

            var obj = new RepositoryMatchKey<int>();
            obj.Record.Object.Key = key;

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();
            repository
                .Setup(x => x.Match(key, obj.Record.Object.Etag))
                .Returns(false)
                .Verifiable();

            Assert.Throws<UnitTestException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }

        [Fact]
        public void op_Verify_IRepository_whenInvalidOperationException()
        {
            var obj = new RepositoryMatchKey<int>();

            var repository = new Mock<IRepository<int>>();
            repository
                .Setup(x => x.Insert(obj.Record.Object))
                .Returns(obj.Record.Object)
                .Verifiable();

            Assert.Throws<InvalidOperationException>(() => obj.Verify(repository.Object));

            repository.VerifyAll();
        }
    }
}