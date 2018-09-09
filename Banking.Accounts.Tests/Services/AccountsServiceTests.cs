using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Banking.Accounts.Domain;
using Banking.Accounts.Exceptions;
using Banking.Accounts.Models;
using Banking.Accounts.Persistence;
using Banking.Accounts.Services;
using Highway.Data;
using Moq;
using Random.Infrastructure;
using Xunit;

namespace Banking.Accounts.Tests.Services
{
    public class AccountsServiceTests
    {
        //Don't really need a unit test over the TheGetMethod

        public class TheGetByIdMethod
        {
            [Fact]
            public async Task With_Id_Equals_Zero_Throws_BadRequestException()
            {
                var sut = new AccountsService(
                    new Mock<IRepository>().Object,
                    new Mock<IEventBus>().Object,
                    new Mock<IMapper<Account, AccountModel>>().Object,
                    new Mock<IAccountFactory>().Object);

                await Assert.ThrowsAsync<BadRequestException>(() => sut.Get(0));
            }

            [Fact]
            public async Task With_Valid_Id_And_No_Result_Throws_NotFoundException()
            {
                var repository = new Mock<IRepository>();

                repository
                    .Setup(p => p.FindAsync(It.IsAny<GetAccountById>()))
                    .ReturnsAsync((Account)null);

                var sut = new AccountsService(
                    repository.Object,
                    new Mock<IEventBus>().Object,
                    new Mock<IMapper<Account, AccountModel>>().Object,
                    new Mock<IAccountFactory>().Object);

                await Assert.ThrowsAsync<NotFoundException>(() => sut.Get(1));
            }
        }

        public class TheReplaceMethod
        {
            [Fact]
            public async Task With_Id_Equals_Zero_Throws_BadRequestException()
            {
                var sut = new AccountsService(
                    new Mock<IRepository>().Object,
                    new Mock<IEventBus>().Object,
                    new Mock<IMapper<Account, AccountModel>>().Object,
                    new Mock<IAccountFactory>().Object);

                await Assert.ThrowsAsync<BadRequestException>(() => sut.Replace(0, new TransientAccountModel()));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task With_Id_Equals_Zero_Throws_BadRequestException()
            {
                var sut = new AccountsService(
                    new Mock<IRepository>().Object,
                    new Mock<IEventBus>().Object,
                    new Mock<IMapper<Account, AccountModel>>().Object,
                    new Mock<IAccountFactory>().Object);

                await Assert.ThrowsAsync<BadRequestException>(() => sut.Delete(0));
            }

            [Fact]
            public async Task With_Valid_Id_And_No_Result_Throws_NotFoundException()
            {
                var repository = new Mock<IRepository>();

                repository
                    .Setup(p => p.FindAsync(It.IsAny<GetAccountById>()))
                    .ReturnsAsync((Account)null);

                var sut = new AccountsService(
                    repository.Object,
                    new Mock<IEventBus>().Object,
                    new Mock<IMapper<Account, AccountModel>>().Object,
                    new Mock<IAccountFactory>().Object);

                await Assert.ThrowsAsync<NotFoundException>(() => sut.Delete(1));
            }
        }

        //Don't really need a unit test over the TheGetMethod
    }
}
