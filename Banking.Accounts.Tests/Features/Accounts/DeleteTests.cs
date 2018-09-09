using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Banking.Accounts.Domain.Accounts;
using Banking.Accounts.Exceptions;
using Banking.Accounts.Features.Accounts;
using Highway.Data;
using Moq;
using Random.Infrastructure;
using Xunit;

namespace Banking.Accounts.Tests.Features.Accounts
{
    public class DeleteTests
    {
        [Fact]
        public async Task With_Id_Equals_Zero_Throws_BadRequestException()
        {
            var request = new Delete.Request()
            {
                Id = 0
            };

            var sut = new Delete.Handler(
                new Mock<IRepository>().Object,
                new Mock<IMapper<Account, AccountModel>>().Object,
                new Mock<IEventBus>().Object);

            await Assert.ThrowsAsync<BadRequestException>(() => sut.Handle(request, new CancellationToken()));
        }

        [Fact]
        public async Task With_Valid_Id_And_No_Result_Throws_NotFoundException()
        {
            var request = new Delete.Request()
            {
                Id = 1
            };

            var repository = new Mock<IRepository>();

            repository
                .Setup(p => p.FindAsync(It.IsAny<Delete.FilterOn>()))
                .ReturnsAsync((Account)null);

            var sut = new Delete.Handler(
                new Mock<IRepository>().Object,
                new Mock<IMapper<Account, AccountModel>>().Object,
                new Mock<IEventBus>().Object);

            await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(request, new CancellationToken()));
        }
    }
}
