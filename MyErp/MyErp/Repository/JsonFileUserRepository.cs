using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MyErp.Entities;

namespace MyErp.Repository
{
    public class JsonFileUserRepository : IUserRepository
    {
        private const string UserFileName = "Users.json";

        public async Task Save(IList<Client> models)
        {
            var serializedContent = JsonSerializer.Serialize(models);
             File.WriteAllText(UserFileName, serializedContent);
        }
        
        public async Task<Client?> GetUser(int userId)
        {
            var users = await Load();
            return users.FirstOrDefault(user => user.Id == userId);
        }

        public async Task<IList<Client>> Load()
        {
            if (!File.Exists(UserFileName))
                return new List<Client>();

            var serializedContent = File.ReadAllText(UserFileName);

            var users = JsonSerializer.Deserialize<List<Client>>(serializedContent);
            if (users == null)
                return new List<Client>();
            
            int nextId = 1;

            foreach (var user in users)
            {
                user.Id = nextId++;
            }

            return users;
        }
    }
}

