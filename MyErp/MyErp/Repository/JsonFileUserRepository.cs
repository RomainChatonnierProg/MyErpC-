using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using MyErp.Entities;

namespace MyErp.Repository
{
    internal class JsonFileUserRepository : IUserRepository
    {
        private const string UserFileName = "Users.json";

        public async Task Save(IList<Client> models)
        {
            var serializedContent = JsonSerializer.Serialize(models);

            File.WriteAllText(UserFileName,serializedContent);
        }

        public async Task<IList<Client>> Load()
        {
            if (!File.Exists(UserFileName))
                return new List<Client>();

            var serializedContent = File.ReadAllText(UserFileName);

            var users = JsonSerializer.Deserialize<List<Client>>(serializedContent);
            return users ?? new List<Client>();
        }
    }
}

