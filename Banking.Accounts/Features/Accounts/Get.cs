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
    public static class Get
    {
        public class Request : IRequest<AccountModel>
        {
            [FromRoute(Name = "id")]
            [Range(1, int.MaxValue)]
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Request, AccountModel>
        {
            private readonly IReadOnlyRepository _repository;
            private readonly IMapper<Account, AccountModel> _mapper;

            public Handler(IReadOnlyRepository repository, IMapper<Account, AccountModel> mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<AccountModel> Handle(Request request, CancellationToken cancellationToken)
            {
                if (request.Id <= 0)
                {
                    throw new BadRequestException("Id must be greater than 0");
                }

                var account = await _repository.FindAsync(new FilterOn(new AccountId(request.Id)));

                if (account == null)
                {
                    throw new NotFoundException("No account found by that id");
                }

                return _mapper.Map(account);
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
