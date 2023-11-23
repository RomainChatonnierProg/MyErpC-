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
            if(users.Any(x=>string.IsNullOrEmpty(x.FirstName)))
                throw new Exception("A user don't have any firstname");

            if(users.Any(x=>string.IsNullOrEmpty(x.LastName)))
                throw new Exception("A user don't have any lastname");

            if (users.DistinctBy(x => x.FullName).Count() != users.Count)
                throw new Exception("Duplicate user fullname");
            
            if(users.Any(x=>x.CreateDate<new DateTime(1200,1,1)))
                throw new Exception("Juste pour test tkt, c'est la création de date");

            if(users.Any(x=>x.CreateDate>DateTime.Today))
                throw new Exception("A birth date is after today");

            _repository.Save(users).Wait();
        }

         public IList<Client> Load()
         { 
             return _repository.Load().Result;
         }

        public Client CreateClient()
        {

            return new Client
            {
                FirstName = "Romain",
                LastName = "Chatonn",
                IsActive = true,
                CreateDate = new DateTime(2020, 1, 1),
                Siret = "123456789",
                Society = "Chato Corp",
                City = "Tours",
                PhoneNumber = "0612345678",
                PostalCode = "63000"
            };
        }
    }
}

