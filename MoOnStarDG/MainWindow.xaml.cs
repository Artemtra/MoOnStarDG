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
    public partial class MainWindow : Window
    {
        MoOnStarDgContext db;
        private Sportsman insertSportsman = new Sportsman();
        private List<Sportsman> sportsmans;

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        public MainWindow()
        {
            InitializeComponent();
            db = new MoOnStarDgContext();
            LoadData();
        }
        private void LoadData()
        {
            sportsmans = new List<Sportsman>();
            LevelOfTraining.ItemsSource = db.LevelOfTrainings.ToList();
            LevelOfTraining.DisplayMemberPath = "Title";
            LevelOfTraining.SelectedValue = 0;

            Category.ItemsSource = db.Categories.ToList();
            Category.DisplayMemberPath = "Title";
            Category.SelectedValue = 0;

            TraningType.ItemsSource = db.Types.ToList();
            TraningType.DisplayMemberPath = "Title";
            TraningType.SelectedValue = 0;

            IdSportsman.ItemsSource = db.Sportsmen.ToList();
            IdSportsman.DisplayMemberPath = "Id";
            IdSportsman.SelectedValue = 0;
        }
        public List<Sportsman> Sportsmans
        {
            get => sportsmans;
            set
            {
                sportsmans = value;
                Signal();
            }
        }
        public Sportsman InsertSportsman
        {
            get => insertSportsman;
            set
            {
                insertSportsman = value;
                Signal();
            }
        }


        private void Button_ClickSaveSportsman(object sender, RoutedEventArgs e)
        {
            try
            {
                // Валидация данных
                if (string.IsNullOrWhiteSpace(Name.Text) || string.IsNullOrWhiteSpace(FirstName.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните имя и фамилию спортсмена", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                //if (LevelOfTraining.SelectedItem == null || Category.SelectedItem == null)
                //{
                //    MessageBox.Show("Пожалуйста, выберите уровень подготовки и категорию", "Ошибка",
                //                  MessageBoxButton.OK, MessageBoxImage.Warning);
                //    return;
                //}
                var newSportsman = new Sportsman
                {
                    Name = Name.Text.Trim(),
                    FirstName = FirstName.Text.Trim(),
                    //IdCategory = (int)Category.SelectedValue,
                };
                newSportsman.IdLevel = (LevelOfTraining.SelectedItem as LevelOfTraining).Id;

                if (newSportsman.IdLevel == 0)
                {

                    MessageBox.Show("Пожалуйста выберите что-то", "Ошибка",
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (DataBirsDay.SelectedDate == null)
                {
                    MessageBox.Show("Пожалуйста, выберите дату рождения", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                newSportsman.DdataBirsDay = DataBirsDay.SelectedDate?.ToShortDateString();

                // Создание нового спортсмена
                newSportsman.IdCategory = (Category.SelectedValue as Category).Id;

                if (newSportsman.IdCategory == 0)
                {

                    MessageBox.Show("Пожалуйста выберите тип тренировки", "Ошибка",
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                // Сохранение в базу данных
                db.Sportsmen.Add(newSportsman);
                db.SaveChanges();

                // Очистка формы
                ClearSportsmanForm();

                MessageBox.Show("Спортсмен успешно сохранен!", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении спортсмена: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_ClickSaveTraining(object sender, RoutedEventArgs e)
        {
            try
            {
                var newTraining = new Training
                {
                    Title = TrainingName.Text.Trim()
                };

                // Валидация данных
                if (string.IsNullOrWhiteSpace(TrainingName.Text))
                {
                    MessageBox.Show("Пожалуйста, введите название тренировки", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                //if (!int.TryParse(TraningType.Text, out int IdTrainingTime) || IdTrainingTime <= 0)
                //{
                //    MessageBox.Show("Пожалуйста, введите корректную длительность тренировки (число больше 0)", "Ошибка",
                //    MessageBoxButton.OK, MessageBoxImage.Warning);
                //    return;
                //}
                newTraining.IdSportsmen = (IdSportsman.SelectedItem as MoOnStarDG.DB.Sportsman).Id;
                if (newTraining.IdSportsmen == 0)
                {

                    MessageBox.Show("Пожалуйста выберите что-то", "Ошибка",
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                newTraining.TypeId = (TraningType.SelectedItem as MoOnStarDG.DB.Type).Id;

                if (newTraining.TypeId == 0)
                {

                    MessageBox.Show("Пожалуйста выберите что-то", "Ошибка",
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (TrainingData.SelectedDate == null)
                {

                    MessageBox.Show("Пожалуйста, выберите дату тренировки", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                newTraining.TrainingDate = TrainingData.SelectedDate?.ToShortDateString();

                if (DataBirsDay.SelectedDate != null)
                    newTraining.TrainingDate = DataBirsDay.SelectedDate.Value.ToShortDateString();
                newTraining.TrainingTime = TrainingTime.Text;
                //newTraining.TypeId = (TraningType.SelectedItem as MoOnStarDG.DB.Type).Id;



                // Создание новой тренировки


                // Сохранение в базу данных
                db.Training.Add(newTraining);
                db.SaveChanges();

                // Очистка формы
                ClearTrainingForm();

                MessageBox.Show("Тренировка успешно сохранена!", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении тренировки: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ClearSportsmanForm()
        {
            Name.Clear();
            FirstName.Clear();
            LevelOfTraining.SelectedIndex = -1;
            Category.SelectedIndex = -1;
            DataBirsDay.SelectedDate = null;
        }

        private void ClearTrainingForm()
        {
            TrainingName.Clear();
            TrainingTime.Clear();
            TraningType.SelectedIndex = -1;
            TrainingData.SelectedDate = null;
        }

        private void Button_ClickGradeSportsman(object sender, RoutedEventArgs e)
        {
            SportsMan sportsMan = new SportsMan();
            sportsMan.Show();
            this.Close();
        }
    }
}