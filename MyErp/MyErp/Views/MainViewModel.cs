using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyErp.Base;
using MyErp.entities;

namespace MyErp.Views
{
    internal class MainViewModel:ViewModelBase
    {
        private string _title="My title";

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        
        public ObservableCollection<Client> Users { get; }
        
        private Client? _selectedUser;
        public Client? SelectedUser
        {
            get => _selectedUser;
            set
            {
                SetProperty(ref _selectedUser, value);
                //DeleteCommand.NotifyCanExecuteChanged();
            }
        }
    }
}
