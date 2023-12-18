using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyErp.Models;
using MyErp.Repository;

namespace TestProject.Mocks
{
    internal class DummyClientRepository: IClientRepository
    {
        public List<ClientModel> SavedClients { get; private set; } = new List<ClientModel>();

        public Task Save(IList<ClientModel> models)
        {
            SavedClients = models.ToList();
            return Task.CompletedTask;
        }

        public Task<IList<ClientModel>> Load()
        {
            return Task.FromResult((IList<ClientModel>)SavedClients);
        }
    }
}
