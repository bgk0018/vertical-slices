using System;
using System.Threading.Tasks;
using Banking.Accounts.Core.Models;
using Banking.Accounts.Models;
using Highway.Data;
using Microsoft.AspNetCore.JsonPatch;
using Random.Infrastructure;

namespace Banking.Accounts.Core
{
    public class AccountsService
    {
        private readonly IRepository _repository;
        private readonly IEventBus _bus;

        public AccountsService(
            IRepository repository,
            IEventBus bus)
        {
            _repository = repository;
            _bus = bus;
        }

        public async Task<AccountModel> Get()
        {
            return _repository.Find()
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
