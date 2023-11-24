using System;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using MyErp.Base;

namespace MyErp.Entities
{
    public class Client : ViewModelBase
    {
        private string _lastName;
        private string _firstName;
        private string _siret;
        private string _societyName;
        private DateTime _createDate;
        private string _city;
        private string _CP;
        private string _phoneNumber;
        private Boolean _isActive;
        private bool _isVisible;
        
        public string FirstName
        {
            get => _firstName;
            set
            {
                SetProperty(ref _firstName, value);
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                SetProperty(ref _lastName, value);
                OnPropertyChanged(nameof(FullName));
            }
        }
        
        public string FullName => FirstName + " " + LastName;
        
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }
        
        public string Siret
        {
            get => _siret;
            set => SetProperty(ref _siret, value);
        }
        
        public string Society
        {
            get => _societyName;
            set => SetProperty(ref _societyName, value);
        }
        
        public DateTime CreateDate
        {
            get => _createDate;
            set => SetProperty(ref _createDate, value);
        }
        
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }
        
        public string PostalCode
        {
            get => _CP;
            set => SetProperty(ref _CP, value);
        }
        
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }
        public string toString()
        {
            return FullName;
        }
        
        
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                SetProperty(ref _isVisible, value);
                OnPropertyChanged();
            }
        }
    }
}

