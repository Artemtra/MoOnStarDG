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
using MoOnStarDG.DB;

namespace MoOnStarDG
{
    /// <summary>
    /// Логика взаимодействия для SportsMan.xaml
    /// </summary>
    public partial class SportsMan : Window
    {
        private MoOnStarDgContext db;
        private List<Sportsman> sportsmans;
        private List<Training> trainings;
        private Sportsman selectedSportsman;
        private Training selectedTraining;

        public event PropertyChangedEventHandler PropertyChanged;

        public SportsMan()
        {
            db = new MoOnStarDgContext();
            LoadData();

            RefreshCommand = new RelayCommand(RefreshData);
            BackToMainCommand = new RelayCommand(BackToMain);
        }

        private void LoadData()
        {

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

        public List<Training> Trainings
        {
            get => trainings;
            set
            {
                trainings = value;
                OnPropertyChanged();
            }
        }

        public Sportsman SelectedSportsman
        {
            get => selectedSportsman;
            set
            {
                selectedSportsman = value;
                OnPropertyChanged();
            }
        }

        public Training SelectedTraining
        {
            get => selectedTraining;
            set
            {
                selectedTraining = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand { get; }
        public ICommand BackToMainCommand { get; }

        private void RefreshData()
        {
            LoadData();
            MessageBox.Show("Данные обновлены!", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BackToMain()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.MainWindow?.Close();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}

