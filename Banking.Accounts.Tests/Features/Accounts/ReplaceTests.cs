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
    public class ReplaceTests
    {
        [Fact]
        public async Task With_Id_Equals_Zero_Throws_BadRequestException()
        {
            var request = new Replace.Request()
            {
                Id = 0,
                Model = new TransientAccountModel()
            };

            var sut = new Replace.Handler(
                new Mock<IRepository>().Object,
                new Mock<IEventBus>().Object,
                new Mock<IMapper<Account, AccountModel>>().Object);

            await Assert.ThrowsAsync<BadRequestException>(() => sut.Handle(request, new CancellationToken()));
        }
    }
}
