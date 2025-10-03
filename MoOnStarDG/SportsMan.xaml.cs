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
    public partial class SportsMan : Window, INotifyPropertyChanged
    {
        MoOnStarDgContext db;
        private List<Sportsman> sportsmen;
        private List<Training> training;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void Signal([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public SportsMan()
        {
            InitializeComponent();
            db = new MoOnStarDgContext();
            DataContext = this;
            LoadData();
        }

        public List<Sportsman> Sportsmen
        {
            get => sportsmen;
            set
            {
                sportsmen = value;
                Signal();
            }
        }
        public List<Training> Training
        {
            get => training;
            set
            {
                training = value;
                Signal();
            }
        }

        private void LoadData()
        {
            try
            {
                //var sportsmen = db.Sportsmen
                //    .Include(s => s.IdCategoryNavigation)
                //    .Include(s => s.IdLevelNavigation)
                //    .ToList();
                //SportsmenList.ItemsSource = sportsmen;

                //var trainings = db.Training
                //    .Include(t => t.Type)
                //    .ToList();
                //SportsmenList.ItemsSource = trainings;
                //Signal(nameof(SportsmenList));

                Sportsmen = db.Sportsmen
                    .Include(s => s.IdCategoryNavigation)
                    .Include(s => s.IdLevelNavigation)
                    .ToList();
                Training = db.Training
                    .Include(n => n.Type)
                    .ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Grade5(object sender, RoutedEventArgs e)
        {
            if (TrainingsList.SelectedItem == null)
            {
                MessageBox.Show("выберете кому хотите поставить оценку");
                return;
            }
            else if (TrainingsList.SelectedItem != null)
            {
                
            }
            else
            {
                Console.WriteLine("ты еблан, ты как это сделал");
            }
        }
        private void BattonRefresh_Click(object sender, RoutedEventArgs e)
        {
            Signal();
            LoadData();
        }
        private void BackToMainBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}