using FluentAssertions;
using MyErp.Metier;
using MyErp.Entities;
using TestProject2.Mocks;

namespace TestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SaveClients_DuplicateClientName_ShouldThrowException()
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client
                {
                    FirstName = "Yoshew",
                    LastName = "Gribaldo"
                },
                new Client
                {
                    FirstName = "Yoshew",
                    LastName = "Gribaldo"
                }
            };
            var clientService = new UserService(new DummyClientRepository());

            // Act
            try
            {
                clientService.Save(clients);
                Assert.Fail("Doit lancer une exception");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Contains("Duplicate name or full name among users"));
            }
        }
        
        [TestMethod]
        public void SaveClients_ClientWithoutCompanyNameOrFullName_ShouldThrowException()
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client
                {
                    FirstName = "",
                    LastName = "",
                    Society = ""
                }
            };
            var clientService = new UserService(new DummyClientRepository());

            // Act
            try
            {
                clientService.Save(clients);
                Assert.Fail("Une exception doit être levée");
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsTrue(e.Message.Contains("A user must have a society name OR a first name and last name"));
            }
        }
        
        [TestMethod]
        public void SaveClients_ClientWithoutCompanyNameOrFullName_ShouldNotThrowException()
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client
                {
                    FirstName = "Yan",
                    LastName = "Candaes",
                    Society = "Candaes Consulting",
                    IsActive = true,
                    CreateDate = new DateTime(2021, 1, 1),
                    Siret = "98765432109876",
                    City = "Tours",
                    PhoneNumber = "0698765432",
                    PostalCode = "67000"
                }
            };
            var clientService = new UserService(new DummyClientRepository());

            // Act
            clientService.Save(clients);
        }
        
        [TestMethod]
        public void SaveClients_DuplicateCompanyName_ShouldThrowException()
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client
                {
                    FirstName = "Yan",
                    LastName = "Candaes",
                    Society = "Candaes Consulting",
                    IsActive = true,
                    CreateDate = new DateTime(2021, 1, 1),
                    Siret = "98765432109876",
                    City = "Tours",
                    PhoneNumber = "0698765432",
                    PostalCode = "67000"
                },
                new Client
                {
                    FirstName = "Romain",
                    LastName = "Chatonnier",
                    Society = "Candaes Consulting",
                    IsActive = true,
                    CreateDate = new DateTime(2021, 1, 1),
                    Siret = "98765432109876",
                    City = "Tours",
                    PhoneNumber = "0698765432",
                    PostalCode = "67000"
                }
            };
            var clientService = new UserService(new DummyClientRepository());

            // Act
            try
            {
                clientService.Save(clients);
                Assert.Fail("Une exception doit être levée pour les noms de société en double");
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsTrue(e.Message.Contains("Duplicate society name"));
            }
        }
        
        [TestMethod]
        public void SaveClients_PostalCodeTooLong_ShouldThrowException()
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client
                {
                    FirstName = "Jean",
                    LastName = "Dupont",
                    Society = "Dupont SARL",
                    IsActive = true,
                    CreateDate = new DateTime(2021, 1, 1),
                    Siret = "12345678901234",
                    City = "Paris",
                    PhoneNumber = "0123456789",
                    PostalCode = "12345678901"
                }
            };
            var clientService = new UserService(new DummyClientRepository());

            // Act & Assert
            var exception = Assert.ThrowsException<Exception>(() => clientService.Save(clients));
            StringAssert.Contains(exception.Message, "The postal code must be less than 10 characters long");
        }
        
        [TestMethod]
        public void SaveClients_MissingSiretWithCompanyName_ShouldThrowException()
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client
                {
                    FirstName = "Julien",
                    LastName = "Martin",
                    Society = "Martin SARL",
                    IsActive = true,
                    CreateDate = new DateTime(2021, 1, 1),
                    Siret = "",
                    City = "Lyon",
                    PhoneNumber = "0123456789",
                    PostalCode = "69000"
                }
            };
            var clientService = new UserService(new DummyClientRepository());

            // Act
            try
            {
                clientService.Save(clients);
                Assert.Fail("Une exception doit être levée si le Siret est manquant alors que le nom de société est rempli");
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsTrue(e.Message.Contains("The Siret number is mandatory if the company name is filled in"));
            }
        }
        
        [TestMethod]
        public void SaveClients_SiretLengthIncorrect_ShouldThrowException()
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client
                {
                    FirstName = "Marc",
                    LastName = "Dupont",
                    Society = "Dupont SA",
                    IsActive = true,
                    CreateDate = new DateTime(2021, 1, 1),
                    Siret = "1234567890123",
                    City = "Nantes",
                    PhoneNumber = "0123456789",
                    PostalCode = "44000"
                }
            };
            var clientService = new UserService(new DummyClientRepository());

            // Act
            try
            {
                clientService.Save(clients);
                Assert.Fail("Une exception doit être levée pour un Siret de longueur incorrecte");
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsTrue(e.Message.Contains("The Siret number must be 14 characters long"));
            }
        }
        
        
        [TestMethod]
        public void SaveClients_InvalidPhoneNumber_ShouldThrowException()
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client
                {
                    FirstName = "Anne",
                    LastName = "Bourgeois",
                    Society = "Bourgeois Ltd",
                    IsActive = true,
                    CreateDate = new DateTime(2021, 1, 1),
                    Siret = "12345678901234",
                    City = "Lille",
                    PhoneNumber = "123456789",
                    PostalCode = "59000"
                }
            };
            var clientService = new UserService(new DummyClientRepository());

            // Act
            try
            {
                clientService.Save(clients);
                Assert.Fail("Une exception doit être levée pour un numéro de téléphone invalide");
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsTrue(e.Message.Contains("The telephone number is mandatory and must have 10 digits starting with 0"));
            }
        }
        
        [TestMethod]
        public void DeleteClient_ActiveClient_ShouldThrowException()
        {
            // Arrange
            var clientService = new UserService(new DummyClientRepository());
            var activeClient = new Client
            {
                FirstName = "Laura",
                LastName = "Bertrand",
                Society = "Bertrand Co",
                IsActive = true, 
                CreateDate = new DateTime(2021, 1, 1),
                Siret = "12345678901234",
                City = "Marseille",
                PhoneNumber = "0123456789",
                PostalCode = "13000"
            };

            // Act && Assert
            var exception = Assert.ThrowsException<Exception>(() => clientService.CanDeleteClient(activeClient));
            Assert.AreEqual("Unable to delete an active client", exception.Message);
        }

    }


}