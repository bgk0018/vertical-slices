using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Accounts.Core.Models;
using Banking.Accounts.Models;
using Highway.Data;
using Microsoft.AspNetCore.JsonPatch;
using Random.Infrastructure;
using Banking.Accounts.Domain;
using Banking.Accounts.Persistence;

namespace Banking.Accounts.Core
{
    public class AccountsService
    {
        private readonly IRepository _repository;
        private readonly IEventBus _bus;
        private readonly IMapper<Account, AccountModel> _mapper;
        private readonly IAccountFactory _factory;

        public AccountsService(
            IRepository repository,
            IEventBus bus,
            IMapper<Account, AccountModel> mapper,
            IAccountFactory factory)
        {
            _repository = repository;
            _bus = bus;
            _mapper = mapper;
            _factory = factory;
        }

        public async Task<IList<AccountModel>> Get()
        {
            var results = (await _repository.FindAsync(new GetAccounts()))
                                            .Select(_mapper.Map)
                                            .ToList();

            return results;
        }

        public async Task<AccountModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountModel> Create(TransientAccountModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountModel> Replace(int id, TransientAccountModel model)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountModel> Update(int id, JsonPatchDocument document)
        {
            throw new NotImplementedException();
        }
    }
}
