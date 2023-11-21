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
        private long _sirret;
        private string _societyName;
        private DateTime _createDate;
        private string _city;
        private double _CP;
        private double _phoneNumber;
        private Boolean _isActive;
        
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
        
        public long Sirret
        {
            get => _sirret;
            set => SetProperty(ref _sirret, value);
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
        
        public double PostalCode
        {
            get => _CP;
            set => SetProperty(ref _CP, value);
        }
        
        public double PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }
        public string toString()
        {
            return FullName;
        }
    }
}

