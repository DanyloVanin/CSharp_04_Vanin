using System.Windows;
using CShar_Vanin_04.Tools.DataStorage;
using CShar_Vanin_04.Tools.Managers;
using CShar_Vanin_04.ViewModels;

namespace CShar_Vanin_04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StationManager.Initialize(new SerializedDataStorage());
            DataContext = new PersonGridViewModel();
        }
    }
}
