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
        private void SetGrade(int gradeValue)
        {
            if (TrainingsList.SelectedItem == null)
            {
                MessageBox.Show("Выберите тренировку для оценки");
                return;
            }

            try
            {
                var selectedTraining = (Training)TrainingsList.SelectedItem;

                // Находим оценку в базе данных
                var grade = db.Grades.FirstOrDefault(g => g.Id == gradeValue);

                if (grade != null)
                {
                    // Обновляем оценку у тренировки
                    selectedTraining.IdGrade = grade.Id;

                    // Сохраняем изменения в базе данных
                    db.SaveChanges();

                    // Обновляем отображение
                    LoadData();

                    MessageBox.Show($"Оценка {gradeValue} успешно поставлена");
                }
                else
                {
                    MessageBox.Show("Оценка не найдена в базе данных");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при установке оценки: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Grade5(object sender, RoutedEventArgs e) => SetGrade(5);
        private void Grade4(object sender, RoutedEventArgs e) => SetGrade(4);
        private void Grade3(object sender, RoutedEventArgs e) => SetGrade(3);
        private void Grade2(object sender, RoutedEventArgs e) => SetGrade(2);
        private void GradeMinus2(object sender, RoutedEventArgs e) => SetGrade(-2);

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