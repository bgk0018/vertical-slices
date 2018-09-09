using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Banking.Accounts.Domain.Accounts;
using Banking.Accounts.Exceptions;
using Highway.Data;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Random.Infrastructure;

namespace Banking.Accounts.Features.Accounts
{
    public static class Replace
    {
        public class Request : IRequest<AccountModel>
        {
            [FromRoute(Name = "id")]
            [Range(1, int.MaxValue)]
            public int Id { get; set; }

            [FromBody]
            [Required]
            public TransientAccountModel Model { get; set; }
        }

        public class Handler : IRequestHandler<Request, AccountModel>
        {
            private readonly IRepository _repository;
            private readonly IEventBus _bus;
            private readonly IMapper<Account, AccountModel> _mapper;

            public Handler(
                IRepository repository,
                IEventBus bus,
                IMapper<Account, AccountModel> mapper)
            {
                _repository = repository;
                _bus = bus;
                _mapper = mapper;
            }

            public async Task<AccountModel> Handle(Request request, CancellationToken cancellationToken)
            {
                if (request.Id <= 0)
                {
                    throw new BadRequestException("Id must be greater than 0");
                }

                var target = await _repository.FindAsync(new FilterOn(new AccountId(request.Id)));

                if (target == null)
                {
                    throw new NotFoundException("No account found by that id");
                }

                target.AccountHolder = new AccountHolder(
                    new FirstName(request.Model.HolderFirstName), 
                    new LastName(request.Model.HolderLastName));

                await _repository.UnitOfWork.CommitAsync();

                var result = _mapper.Map(target);

                await _bus.Queue(result, "AccountUpdated");

                return result;
            }
        }

        public class FilterOn : Scalar<Account>
        {
            public FilterOn(AccountId id)
            {
                ContextQuery = x => x
                    .AsQueryable<Account>()
                    .FirstOrDefault(p => p.Id.Value == id.Value);
            }
        }
    }
}
