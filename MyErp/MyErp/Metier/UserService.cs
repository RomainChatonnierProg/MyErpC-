using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyErp.Entities;
using MyErp.Repository;

namespace MyErp.Metier
{
    public class UserService
    {
        private readonly IUserRepository _repository;
        
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        
        public void Save(IList<Client> users)
        {
            if(users.Any(x=>string.IsNullOrEmpty(x.FirstName) && (string.IsNullOrEmpty(x.FirstName)) || (string.IsNullOrEmpty(x.LastName))))
                throw new Exception("A user must have a society name OR a first name and last name.");

            var duplicateSociety = users
                .GroupBy(x => x.Society)
                .FirstOrDefault(g => !string.IsNullOrEmpty(g.Key) && g.Count() > 1);
            if (duplicateSociety != null)
                throw new Exception($"Duplicate society name: {duplicateSociety.Key}");
            
            if (!IsUniqueNames(users))
                throw new Exception("Duplicate name or full name among users");

            if (users.Any(x => string.IsNullOrEmpty(x.PostalCode) && (x.PostalCode.Length >= 10)))
                throw new Exception("The postal code must be less than 10 characters long.");

            if (users.Any(user => !string.IsNullOrEmpty(user.Society) && string.IsNullOrEmpty(user.Siret)))
            {
                throw new Exception("The Siret number is mandatory if the company name is filled in.");
            }

            if (users.Any(x=> !string.IsNullOrEmpty(x.Siret) && (x.Siret.Length !=14)))
            {
                throw new Exception("The Siret number must be 14 characters long.");
            }

            if (users.Any(x=> string.IsNullOrEmpty(x.PhoneNumber) || (!x.PhoneNumber.StartsWith("0") || !IsNumeric(x.PhoneNumber) || (x.PhoneNumber.Length !=10))))
            {
                throw new Exception("The telephone number is mandatory and must have 10 digits starting with 0.");
            }
            
            if(users.Any(x=>x.CreateDate<new DateTime(1200,1,1)))
                throw new Exception("Juste pour test tkt, c'est la création de date");

            if(users.Any(x=>x.CreateDate>DateTime.Today))
                throw new Exception("A birth date is after today");

            _repository.Save(users).Wait();
        }

         public IEnumerable<Client> Load()
         { 
             return _repository.Load().Result;
         }

        public static Client CreateClient()
        {

            return new Client
            {
                FirstName = "AAA",
                LastName = "AAA",
                IsActive = true,
                CreateDate = new DateTime(2021, 1, 1),
                Siret = "987654321",
                Society = "BBB",
                City = "Tours",
                PhoneNumber = "0698765432",
                PostalCode = "67000"
            };
        }
        
        private static bool IsUniqueNames(IEnumerable<Client> users)
        {
            var uniqueNames = new HashSet<string>();

            return users.Select(user => user.FullName).All(fullName => uniqueNames.Add(fullName));
        }
        
        private static bool IsNumeric(string value)
        {
            return value.All(char.IsDigit);
        }
        
        public bool CanDeleteClient(Client client)
        {
            return client != null && !client.IsActive;
        }
    }
}

