using MoOnStarDG.DB;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace MoOnStarDG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private MoOnStarDgContext db;
        private Sportsman insertSportsman;
        private Training insertTraining;
        private TrainingTime insertTrainingTime;
        private List<Sportsman> sportsmans;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            db = new MoOnStarDgContext();
            InsertSportsman = new Sportsman();
            InsertTraining = new Training();
            LoadData();

            SaveSportsmanCommand = new RelayCommand(SaveSportsman);
            SaveTrainingCommand = new RelayCommand(SaveTraining);
        }

        private void LoadData()
        {
            sportsmans = db.Sportsmen.ToList();
            LevelOfTrainings = db.LevelOfTrainings.ToList();
            Categories = db.Categories.ToList();
            TrainingTypes = db.Types.ToList();
        }

        public Sportsman InsertSportsman
        {
            get => insertSportsman;
            set
            {
                insertSportsman = value;
                OnPropertyChanged();
            }
        }

        public Training InsertTraining
        {
            get => insertTraining;
            set
            {
                insertTraining = value;
                OnPropertyChanged();
            }
        }

        public TrainingTime InsertTrainingTime
        {
            get => insertTrainingTime;
            set
            {
                insertTrainingTime = value;
                OnPropertyChanged();
            }
        }
        public List<Sportsman> Sportsmans
        {
            get => sportsmans;
            set
            {
                sportsmans = value;
                OnPropertyChanged();
            }
        }

        public List<LevelOfTraining> LevelOfTrainings { get; set; }
        public List<Category> Categories { get; set; }
        public List<DB.Type> TrainingTypes { get; set; }

        public ICommand SaveSportsmanCommand { get; }
        public ICommand SaveTrainingCommand { get; }
        public ICommand OpenSportsmanGradeCommand { get; }

        private void SaveSportsman()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(InsertSportsman.Name) || string.IsNullOrWhiteSpace(InsertSportsman.FirstName))
                {
                    MessageBox.Show("Пожалуйста, заполните имя и фамилию спортсмена", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (InsertSportsman.IdTraning == 0 || InsertSportsman.IdCategory == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите уровень подготовки и категорию", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (InsertSportsman.DataBirsDay == null)
                {
                    MessageBox.Show("Пожалуйста, выберите дату рождения", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                db.Sportsmen.Add(InsertSportsman);
                db.SaveChanges();

                Sportsmans = db.Sportsmen.ToList();
                InsertSportsman = new Sportsman();

                MessageBox.Show("Спортсмен успешно сохранен!", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении спортсмена: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveTraining()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(InsertTraining.Title))
                {
                    MessageBox.Show("Пожалуйста, введите название тренировки", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (InsertTraining.IdTrainingTime <= 0)
                {
                    MessageBox.Show("Пожалуйста, введите корректную длительность тренировки (число больше 0)", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (InsertTraining.TypeId == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите тип тренировки", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (InsertTraining.TrainingDate == null)
                {
                    MessageBox.Show("Пожалуйста, выберите дату тренировки", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                db.Training.Add(InsertTraining);
                db.SaveChanges();

                InsertTraining = new Training();

                MessageBox.Show("Тренировка успешно сохранена!", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении тренировки: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_ClickGradeSportsman(object sender, RoutedEventArgs e)
        {
            SportsMan sportsMan = new SportsMan();
            sportsMan.Show();
            this.Close();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}