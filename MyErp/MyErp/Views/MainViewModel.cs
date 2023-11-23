using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using MyErp.Base;
using MyErp.Translation;
using MyErp.Entities;
using MyErp.Metier;


namespace MyErp.Views
{
    internal class MainViewModel:ViewModelBase
    {
        private UserService _userService;
        
        public List<string> AvailableLanguages { get; }
        
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
            
            AvailableLanguages = CultureInfo
                .GetCultures(CultureTypes.NeutralCultures)
                .Select(x => x.DisplayName)
                .ToList();

            Users = new ObservableCollection<Client>(_userService.Load());

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
