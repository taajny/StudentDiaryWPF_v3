using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using StudentDiaryWPF.Commands;
using StudentDiaryWPF.Models;
using StudentDiaryWPF.Models.Domains;
using StudentDiaryWPF.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudentDiaryWPF.ViewModels
{
    class AddEditStudentViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();
        public AddEditStudentViewModel(StudentWrapper student = null)
        {
            CloseCommand = new RelayCommand(Close);
            ConfirmCommand = new RelayCommand(Confirm);
            
            if (student == null)
            {
                Student = new StudentWrapper();
            }
            else
            {
                Student = student;
                Student.SelectedGroupId = student.Group.Id;
                IsUpdate = true;
            }
            InitGroups();
        }
        private bool _isUpdate;
        //private bool _isBtnConfirmEnabled;
        private StudentWrapper _student;

        public StudentWrapper Student
        {
            get { return _student; }
            set
            {
                _student = value;
                OnPropertyChanged();
            }
        }
        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChanged();
            }
        }
        /*public bool IsBtnConfirmEnabled
        {
            get { return _isBtnConfirmEnabled; }
            set
            {
                _isBtnConfirmEnabled = value;
                OnPropertyChanged();
            }
        }*/

        private ObservableCollection<Group> _groups;
        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }
        private int _selectedGroupId;
        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }
        public ICommand CloseCommand { get; set; }
        public ICommand ConfirmCommand { get; set; }
        private void Confirm(object obj)
        {

            if (!Student.IsValid)
            {
                return;
            }
            
            
            if (!IsUpdate)
            {
                _repository.AddStudent(Student);
            }
            else
            {
                _repository.UpdateStudent(Student);
            }

            CloseWindow(obj as Window);
        }
        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "--- brak ---" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = Student.Group.Id;
            //SelectedGroupId = Student.SelectedGroupId;
        }
    }
}
