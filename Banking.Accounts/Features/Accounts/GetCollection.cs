using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Banking.Accounts.Domain.Accounts;
using Highway.Data;
using MediatR;
using Random.Infrastructure;

namespace Banking.Accounts.Features.Accounts
{
    public static class GetCollection
    {
        public class Request : IRequest<IList<AccountModel>>
        {

        }

        public class Handler : IRequestHandler<Request, IList<AccountModel>>
        {
            private readonly IReadOnlyRepository _repository;
            private readonly IMapper<Account, AccountModel> _mapper;

            public Handler(IReadOnlyRepository repository,
                IMapper<Account, AccountModel> mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<IList<AccountModel>> Handle(Request request, CancellationToken cancellationToken)
            {
                var results = (await _repository.FindAsync(new FilterOn()))
                    .Select(_mapper.Map)
                    .ToList();

                return results;
            }
        }

        public class FilterOn : Query<Account>
        {
            public FilterOn()
            {
                ContextQuery = x => x.AsQueryable<Account>();
            }
        }
    }
}
