﻿using MahApps.Metro.Controls;
using StudentDiaryWPF.Models.Wrappers;
using StudentDiaryWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentDiaryWPF.Views
{
    /// <summary>
    /// Logika interakcji dla klasy EditSettingsView.xaml
    /// </summary>
    public partial class EditSettingsView : MetroWindow
    {
        public EditSettingsView(bool canCloseWindow)
        {
            InitializeComponent();
            DataContext = new EditSettingsViewModel(canCloseWindow);
        }
    }
}
