using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Accounts.Domain;
using Banking.Accounts.Exceptions;
using Banking.Accounts.Models;
using Banking.Accounts.Persistence;
using Highway.Data;
using Random.Infrastructure;

namespace Banking.Accounts.Services
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
            if (id <= 0)
            {
                throw new BadRequestException("Id must be greater than 0");
            }

            var account = await _repository.FindAsync(new GetAccountById(new AccountId(id)));

            if (account == null)
            {
                throw new NotFoundException("No account found by that id");
            }

            return _mapper.Map(account);
        }

        public async Task<AccountModel> Create(TransientAccountModel model)
        {
            var account = _factory.Build(model.HolderFirstName, model.HolderLastName);

            _repository.UnitOfWork.Add(account);
            await _repository.UnitOfWork.CommitAsync();

            var result = _mapper.Map(account);

            await _bus.Queue(result, "AccountCreated");

            return result;
        }

        public async Task<AccountModel> Replace(int id, TransientAccountModel model)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id must be greater than 0");
            }

            var target = await _repository.FindAsync(new GetAccountById(new AccountId(id)));

            if (target == null)
            {
                throw new NotFoundException("No account found by that id");
            }

            target.AccountHolder = new AccountHolder(new FirstName(model.HolderFirstName), new LastName(model.HolderLastName));

            await _repository.UnitOfWork.CommitAsync();

            var result = _mapper.Map(target);

            await _bus.Queue(result, "AccountUpdated");

            return result;
        }

        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Id must be greater than 0");
            }

            var account = await _repository.FindAsync(new GetAccountById(new AccountId(id)));

            if (account == null)
            {
                throw new NotFoundException("No account found by that id");
            }

            _repository.UnitOfWork.Remove(account);
            await _repository.UnitOfWork.CommitAsync();

            var result = _mapper.Map(account);

            await _bus.Queue(result, "AccountClosed");
        }

        
    }
}
