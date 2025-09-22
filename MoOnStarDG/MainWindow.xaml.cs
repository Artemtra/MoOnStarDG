using MoOnStarDG.DB;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
            cmdLevelOfTraining.ItemsSource = db.LevelOfTrainings.ToList();
            cmdLevelOfTraining.DisplayMemberPath = "Title";
            cmdLevelOfTraining.SelectedValue = 0;

            cmdCategory.ItemsSource = db.Categories.ToList();
            cmdCategory.DisplayMemberPath= "Title";
            cmdCategory.SelectedValue = 0;

            cmdTraningType.ItemsSource = db.Types.ToList();
            cmdTraningType.DisplayMemberPath = "Title";
            cmdTraningType.SelectedValue = 0;

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

        }

        private void Button_ClickSaveTraining(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ClickGradeSportsman(object sender, RoutedEventArgs e)
        {
            SportsMan sportsMan = new SportsMan();
            sportsMan.Show();
            this.Close();
        }
    }
}