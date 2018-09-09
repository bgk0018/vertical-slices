using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Banking.Accounts.Domain.Accounts;
using Highway.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Random.Infrastructure;

namespace Banking.Accounts.Features.Accounts
{
    public static class Create
    {
        public class Request : IRequest<AccountModel>
        {
            [FromBody]
            [Required]
            public TransientAccountModel Model { get; set; }
        }

        public class Handler : IRequestHandler<Request, AccountModel>
        {
            private readonly IWriteOnlyUnitOfWork _unitOfWork;
            private readonly IAccountFactory _factory;
            private readonly IEventBus _bus;
            private readonly IMapper<Account, AccountModel> _mapper;

            public Handler(
                IWriteOnlyUnitOfWork unitOfWork,
                IAccountFactory factory,
                IEventBus bus,
                IMapper<Account, AccountModel> mapper)
            {
                _unitOfWork = unitOfWork;
                _factory = factory;
                _bus = bus;
                _mapper = mapper;
            }

            public async Task<AccountModel> Handle(Request request, CancellationToken cancellationToken)
            {
                var account = _factory.Build(request.Model.HolderFirstName, request.Model.HolderLastName);

                _unitOfWork.Add(account);
                await _unitOfWork.CommitAsync();

                var result = _mapper.Map(account);

                await _bus.Queue(result, "AccountCreated");

                return result;
            }
        }
    }
}
