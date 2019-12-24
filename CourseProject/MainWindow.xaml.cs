﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CourseProject.Inserts;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void menuSettingsDB_Click(object sender, RoutedEventArgs e)
        {
            DBSettings dBSettings = new DBSettings();
            dBSettings.Show();
        }

        private void buttonShowWindowTables_Click(object sender, RoutedEventArgs e)
        {
            Tables tables = new Tables();
            tables.Show();
        }

        private void buttonShowInserts_Click(object sender, RoutedEventArgs e)
        {
            WindowInserts inserts = new WindowInserts();
            inserts.Show();
        }
    }
}
