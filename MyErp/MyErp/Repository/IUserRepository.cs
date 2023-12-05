using System.Collections.Generic;
using System.Threading.Tasks;
using MyErp.Entities;

namespace MyErp.Repository
{
    public interface IUserRepository
    {
        void Save(IList<Client> entities);

        Task<IList<Client>> Load();
        
        Task<Client?> GetUser(int userId);
    }
}

