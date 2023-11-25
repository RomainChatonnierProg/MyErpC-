using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
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
        
        public ObservableCollection<Client> Users { get; set; }
        
        private Client? _selectedUser;
        public Client? SelectedUser
        {
            get => _selectedUser;
            set
            {
                SetProperty(ref _selectedUser, value);
                DeleteCommand.NotifyCanExecuteChanged();
                ToggleActivationCommand.NotifyCanExecuteChanged();
            }
        }
        
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand AddClient { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand ToggleActivationCommand { get; private set; }

        public MainViewModel(UserService userService)
        {
            _userService = userService;
            
            SaveCommand = new RelayCommand(OnSave);
            AddClient = new RelayCommand(OnAdd);
            DeleteCommand = new RelayCommand(OnDelete,CanDelete);
            ToggleActivationCommand = new RelayCommand(OnToggleActivation, CanToggleActivation);
            
            
            
            AvailableLanguages = CultureInfo
                .GetCultures(CultureTypes.NeutralCultures)
                .Select(x => x.DisplayName)
                .ToList();

            Users = new ObservableCollection<Client>(_userService.Load());
            
            SortUsers();

        }

        private void OnAdd()
        {
            Users.Add(UserService.CreateClient());
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
            if (_selectedUser != null && _userService.CanDeleteClient(_selectedUser))
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
            else
            {
                MessageBox.Show("Unable to delete an active client.");
            }
        }

        private bool CanDelete()
        {
            return _selectedUser != null && _userService.CanDeleteClient(_selectedUser);
        }
        
        private void SortUsers()
        {
            Users = new ObservableCollection<Client>(Users.OrderBy(user => user.FullName));
        }
        
        private void OnToggleActivation()
        {
            if (SelectedUser != null)
            {
                SelectedUser.IsActive = !SelectedUser.IsActive;
                try
                {
                    _userService.Save(Users);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private bool CanToggleActivation()
        {
            return SelectedUser != null;
        }
        
        public void ToggleVisibility(bool showActiveClients)
        {
            foreach (var clients in Users)
            {
                clients.IsVisible = !showActiveClients || clients.IsActive;
            }
        }
        
        
    }
}
