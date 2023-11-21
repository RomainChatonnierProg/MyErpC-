using System.Collections.Generic;
using System.Threading.Tasks;
using MyErp.Entities;

namespace MyErp.Repository
{
    public interface IUserRepository
    {
        Task Save(IList<Client> entities);

        //Task<IList<Client>> Load();
    }
}

