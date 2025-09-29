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
                    .Include(s => s.IdCategory)
                    .Include(s => s.Grades)
                    .ToList();
                SportsmenGrid.ItemsSource = sportsmen;

                var trainings = db.Training
                    .Include(t => t.Type)
                    .ToList();
                TrainingsGrid.ItemsSource = trainings;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshSportsmenBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var sportsmen = db.Sportsmen
                    .Include(s => s.IdCategory)
                    .Include(s => s.Grades)
                    .ToList();
                SportsmenGrid.ItemsSource = sportsmen;
                MessageBox.Show("Список спортсменов обновлен", "Информация",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении списка спортсменов: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RefreshTrainingsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var trainings = db.Training
                    .Include(t => t.Type)
                    .ToList();
                TrainingsGrid.ItemsSource = trainings;
                MessageBox.Show("Список тренировок обновлен", "Информация",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении списка тренировок: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteSportsmanBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SportsmenGrid.SelectedItem is Sportsman selectedSportsman)
            {
                try
                {
                    var result = MessageBox.Show($"Вы уверены, что хотите удалить спортсмена {selectedSportsman.Name} {selectedSportsman.FirstName}?","Подтверждение удаления",MessageBoxButton.YesNo,MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        db.Sportsmen.Remove(selectedSportsman);
                        db.SaveChanges();
                        LoadData(); 
                        MessageBox.Show("Спортсмен успешно удален", "Успех",MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении спортсмена: {ex.Message}", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите спортсмена для удаления", "Информация",MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteTrainingBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TrainingsGrid.SelectedItem is Training selectedTraining)
            {
                try
                {
                    var result = MessageBox.Show($"Вы уверены, что хотите удалить тренировку '{selectedTraining.Title}'?",
                                               "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        db.Training.Remove(selectedTraining);
                        db.SaveChanges();
                        LoadData(); 
                        MessageBox.Show("Тренировка успешно удалена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении тренировки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите тренировку для удаления", "Информация", MessageBoxButton.OK, MessageBoxImage.Warning);
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