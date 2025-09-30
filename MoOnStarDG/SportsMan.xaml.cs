using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Microsoft.EntityFrameworkCore;
using MoOnStarDG.DB;

namespace MoOnStarDG
{
    /// <summary>
    /// Логика взаимодействия для SportsMan.xaml
    /// </summary>
    public partial class SportsMan : Window
    {
        MoOnStarDgContext db;

        public SportsMan()
        {
            InitializeComponent();
            db = new MoOnStarDgContext();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var sportsmen = db.Sportsmen
                    .Include(s => s.IdCategoryNavigation)
                    .Include(s => s.IdLevelNavigation)
                    .ToList();
                SportsmenList.ItemsSource = sportsmen;

                var trainings = db.Training
                    .Include(t => t.Type)
                    .ToList();
                SportsmenList.ItemsSource = trainings;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackToMainBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}