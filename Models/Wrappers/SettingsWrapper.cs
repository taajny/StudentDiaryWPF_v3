using StudentDiaryWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDiaryWPF.Models.Wrappers
{
    public class SettingsWrapper : ViewModelBase, IDataErrorInfo
    {
        public SettingsWrapper()
        {
            dbServerAddress = Properties.Settings.Default.dbServerAddress;
            dbServerName = Properties.Settings.Default.dbServerName;
            dbName = Properties.Settings.Default.dbName;
            dbUserName = Properties.Settings.Default.dbUserName;
            dbPassWord = Properties.Settings.Default.dbPassWord;
        }

        public string dbServerAddress { get; set; }
        public string dbServerName { get; set; }
        public string dbName { get; set; }
        public string dbUserName { get; set; }
        public string dbPassWord { get; set; }

        private bool _isServerAddressValid;
        private bool _isServerNameValid;
        private bool _isDbNameValid;
        private bool _isUserNameValid;
        private bool _isPassWordValid;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(dbServerAddress):
                        if (string.IsNullOrWhiteSpace(dbServerAddress))
                        {
                            Error = "Pole Adres serwera jest wymagane";
                            _isServerAddressValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isServerAddressValid = true;
                        }
                        OnPropertyChanged("IsValid");
                        break;

                    case nameof(dbServerName):
                        if (string.IsNullOrWhiteSpace(dbServerName))
                        {
                            Error = "Pole Nazwa serwera jest wymagane";
                            _isServerNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isServerNameValid = true;
                        }
                        OnPropertyChanged("IsValid");
                        break;

                    case nameof(dbName):
                        if (string.IsNullOrWhiteSpace(dbName))
                        {
                            Error = "Pole Nazwa bazy danych jest wymagane";
                            _isDbNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isDbNameValid = true;
                        }
                        OnPropertyChanged("IsValid");
                        break;

                    case nameof(dbUserName):
                        if (string.IsNullOrWhiteSpace(dbUserName))
                        {
                            Error = "Pole Użytkownik jest wymagane";
                            _isUserNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isUserNameValid = true;
                        }
                        OnPropertyChanged("IsValid");
                        break;

                    case nameof(dbPassWord):
                        if (string.IsNullOrWhiteSpace(dbPassWord))
                        {
                            Error = "Pole hasło jest wymagane";
                            _isPassWordValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isPassWordValid = true;
                        }
                        OnPropertyChanged("IsValid");
                        break;

                    default:
                        break;
                }

                return Error;
            }
        }
        public string Error { get; set; }

        public bool IsValid
        {
            get
            {
                return _isDbNameValid && _isPassWordValid && _isServerAddressValid && _isServerNameValid && _isUserNameValid;
            }
        }
    }
}
