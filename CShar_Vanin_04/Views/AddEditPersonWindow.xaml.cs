using System.Windows;
using CShar_Vanin_04.Models;
using CShar_Vanin_04.ViewModels;

namespace CShar_Vanin_04.Views
{
    /// <summary>
    /// Interaction logic for AddEditPersonWindow.xaml
    /// </summary>
    public partial class AddEditPersonWindow : Window
    {
        public AddEditPersonWindow()
        {
            InitializeComponent();
            var v = new AddEditViewModel();
            DataContext = v;
            if (v.CloseAction == null) v.CloseAction= this.Close;
        }

        public AddEditPersonWindow(Person p)
        {
            InitializeComponent();
            var v = new AddEditViewModel(p);
            DataContext = v;
            if (v.CloseAction == null) v.CloseAction = this.Close;
        }
    }

}
