using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using StudentDiaryWPF.Commands;
using StudentDiaryWPF.Models;
using StudentDiaryWPF.Models.Domains;
using StudentDiaryWPF.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace  StudentDiaryWPF.ViewModels
{
    public class EditSettingsViewModel : ViewModelBase
    {
        public ICommand CloseCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        private readonly bool _canCloseWindow;
        public EditSettingsViewModel(bool canCloseWindow)
        {
            CloseCommand = new RelayCommand(Close);
            SaveCommand = new RelayCommand(Save);

            Settings = new SettingsWrapper();
            _canCloseWindow = canCloseWindow;
        }

        private SettingsWrapper _settings;

        public SettingsWrapper Settings
        {
            get
            {
                return _settings;
            }
            set 
            {
                _settings = value;
                OnPropertyChanged();
            }
        }
        private void Save(object obj)
        {
            if (!Settings.IsValid)
            {
                return;
            }

            Properties.Settings.Default.dbServerAddress = Settings.dbServerAddress;
            Properties.Settings.Default.dbServerName = Settings.dbServerName;
            Properties.Settings.Default.dbName = Settings.dbName;
            Properties.Settings.Default.dbUserName = Settings.dbUserName;
            Properties.Settings.Default.dbPassWord = Settings.dbPassWord;
            Properties.Settings.Default.Save();

            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
             
            CloseWindow(obj as Window);
        }

        private void Close(object obj)
        {
            if (_canCloseWindow)
                CloseWindow(obj as Window);
            else
                Application.Current.Shutdown();

        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
