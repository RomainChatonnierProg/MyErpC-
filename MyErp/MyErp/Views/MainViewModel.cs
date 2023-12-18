using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using MyErp.Base;
using MyErp.Entities;
using MyErp.Metier;


namespace MyErp.Views
{
    internal class MainViewModel:ViewModelBase
    {
        private UserService _userService;
        //private bool _isModified;

        
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
        public RelayCommand CancelSaveCommand {get; private set; }

        public MainViewModel(UserService userService)
        {
            _userService = userService;
            
            SaveCommand = new RelayCommand(OnSave);
            AddClient = new RelayCommand(OnAdd);
            DeleteCommand = new RelayCommand(OnDelete,CanDelete);
            ToggleActivationCommand = new RelayCommand(OnToggleActivation, CanToggleActivation);
            // CancelSaveCommand = new RelayCommand(CancelSave, CanCancelSave);
            

            Users = new ObservableCollection<Client>(_userService.Load());
            
            SortUsers();

        }
        
        /*public bool IsModified
        {
            get => _isModified;
            set => SetProperty(ref _isModified, value);
        }
        
        private void _markAsModified()
        {
            _isModified = true;
        }

        private void _markAsNotModified()
        {
            _isModified = false;
        }*/
        
        private void OnAdd()
        {
            Users.Add(_userService.CreateClient());
            //_markAsModified();
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
            if (_selectedUser == null) return;

            try
            {
                _userService.Save(Users);
                //_markAsNotModified();
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
                //_markAsModified();
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
                //_markAsModified();
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
        
        // private async void CancelSave()
        // {
        //     if (_selectedUser != null)
        //     {
        //         // Recharge les informations du client sélectionné depuis le service
        //         var reloadedUserTask = _userService.GetUser(_selectedUser.Id);
        //
        //         // Attend que la tâche soit terminée et obtient le résultat
        //         var reloadedUser = await reloadedUserTask;
        //
        //         if (reloadedUser != null)
        //         {
        //             // Met à jour le client sélectionné avec les informations rechargées
        //             var index = Users.IndexOf(_selectedUser);
        //             Users[index] = reloadedUser;
        //
        //             // Informe la vue que le client sélectionné a été modifié
        //             OnPropertyChanged(nameof(Users));
        //         }
        //     }
        // }
        
        private bool CanCancelSave()
        {
            return _selectedUser != null;
        }
    }
}
