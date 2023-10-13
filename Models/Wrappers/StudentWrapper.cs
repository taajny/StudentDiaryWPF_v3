using StudentDiaryWPF.ViewModels;
using StudentDiaryWPF.Models;
using StudentDiaryWPF.Commands;
using StudentDiaryWPF.Views;
using StudentDiaryWPF.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;

namespace StudentDiaryWPF.Models.Wrappers
{
    public  class StudentWrapper : ViewModelBase, IDataErrorInfo
    {
        public StudentWrapper()
        {
            Group = new GroupWrapper();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Comments { get; set; }
        public string Math { get; set; }
        public string Technology { get; set; }
        public string Physics { get; set; }
        public string PolishLang { get; set; }
        public string ForeignLang { get; set; }
        public bool Activities { get; set; }
        public GroupWrapper Group { get; set; }

        private bool _isFirstNameValid;
        private bool _isLastNameValid;
        private int _selectedGroupId;
        private bool _isGroupSelected;

        public int SelectedGroupId 
        { 
            get
            {
                return _selectedGroupId;
            }
            set
            {
                _selectedGroupId = value;
                Group.Id = value;
                OnPropertyChanged();
                OnPropertyChanged("IsValid");
            }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(FirstName):
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            Error = "Pole Imię jest wymagane";
                            _isFirstNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isFirstNameValid = true;
                            
                        }
                        OnPropertyChanged("IsValid");
                        break;

                    case nameof(LastName):
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            Error = "Pole Nazwisko jest wymagane";
                            _isLastNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isLastNameValid = true;
                        }
                        OnPropertyChanged("IsValid");
                        break;

                    case nameof(SelectedGroupId):
                        if (SelectedGroupId == 0)
                        {
                            Error = "Wybierz Grupę";
                            _isGroupSelected = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isGroupSelected = true;
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
        
        public bool IsValid {
            get 
            {
                return _isFirstNameValid && _isLastNameValid && _isGroupSelected;
            }
        }
        
    }

}
