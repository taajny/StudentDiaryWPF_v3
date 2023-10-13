using StudentDiaryWPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows;
using StudentDiaryWPF.Models;
using System.Collections.ObjectModel;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using StudentDiaryWPF.Views;
using StudentDiaryWPF.Models.Wrappers;
using System.Runtime.Remoting.Contexts;
using StudentDiaryWPF.Models.Domains;

namespace StudentDiaryWPF.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();
        public MainViewModel()
        {           
            AddStudentCommand = new RelayCommand(AddEditStudents);
            EditStudentCommand = new RelayCommand(AddEditStudents, CanEditDeleteStudents);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudents, CanEditDeleteStudents);
            RefreshStudentCommand = new RelayCommand(RefreshStudents);
            RefreshDiary();
            InitGroups();
        }
        public ICommand RefreshStudentCommand { get; set; }
        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }

        private StudentWrapper _selectedStudent;

        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StudentWrapper> _students;

        public ObservableCollection<StudentWrapper> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }
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

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);
            
            SelectedGroupId = 0;
        }
        private void AddEditStudents(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += AddEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void AddEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private async Task DeleteStudents(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Usuwanie ucznia", $"Czy na pewno chcesz usunąć ucznia {SelectedStudent.FirstName} {SelectedStudent.LastName}?", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
            {
                return;
            }

            _repository.DeleteStudent(SelectedStudent.Id);
            RefreshDiary();
        }
        private bool CanEditDeleteStudents(object obj)
        {
            return SelectedStudent != null;
        }
        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }
        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>(_repository.GetStudents(SelectedGroupId));
        }

    }
}
