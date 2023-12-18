using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyErp.Entities;
using MyErp.Repository;

namespace TestProject2.Mocks
{
    internal class DummyClientRepository: IUserRepository
    {
        public List<Client> SavedClients { get; private set; } = new List<Client>();

        public Task Save(IList<Client> models)
        {
            SavedClients = models.ToList();
            return Task.CompletedTask;
        }

        public Task<IList<Client>> Load()
        {
            return Task.FromResult((IList<Client>)SavedClients);
        }
    }
}
