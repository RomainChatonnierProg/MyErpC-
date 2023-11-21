using System;
using System.Collections.ObjectModel;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using MyErp.Base;
using MyErp.Entities;
using MyErp.Metier;


namespace MyErp.Views
{
    internal class MainViewModel:ViewModelBase
    {
        private UserService _userService;
        
        public ObservableCollection<Client> Users { get; }
        
        private Client? _selectedUser;
        public Client? SelectedUser
        {
            get => _selectedUser;
            set
            {
                SetProperty(ref _selectedUser, value);
                DeleteCommand.NotifyCanExecuteChanged();
            }
        }
        
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand AddClient { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }

        public MainViewModel(UserService userService)
        {
            _userService = userService;
            
            SaveCommand = new RelayCommand(OnSave);
            
            AddClient = new RelayCommand(OnAdd);
            
            DeleteCommand = new RelayCommand(OnDelete,CanDelete);
            
            Users = new ObservableCollection<Client>()
            {
                new Client()
                {
                    FirstName = "Isaac", LastName = "Newton",
                    CreateDate = new DateTime(1643, 10, 2),
                    Society = "Gravity Inc", IsActive = true,
                    Sirret =12356894100056, PostalCode = 53000,
                    City = "Kensington", PhoneNumber = 0612345678
                },
                new Client()
                {
                    FirstName = "Albert", LastName = "Einstein",
                    CreateDate = new DateTime(1879, 10, 2),
                    Society = "Restrain Gravity Motors", IsActive = false,
                    Sirret =12356894100057, PostalCode = 18000,
                    City = "Ulm", PhoneNumber = 0612345679
                },
                new Client()
                {
                    FirstName = "Louis", LastName = "Paster",
                    CreateDate = new DateTime(1822, 6, 28),
                    Society = "Vaccin cr", IsActive = true,
                    Sirret =12356894100058, PostalCode = 63000,
                    City = "Clermont", PhoneNumber = 0612345671
                },
            };
        }
        
        private void OnAdd()
        {
            Users.Add(_userService.CreateClient());
            try
            {
                _userService.Save(Users);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void OnSave()
        {
            try
            {
                _userService.Save(Users);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void OnDelete()
        {
            Users.Remove(_selectedUser);
            try
            {
                _userService.Save(Users);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private bool CanDelete()
        {
            return _selectedUser != null ;
        }
    }
}
