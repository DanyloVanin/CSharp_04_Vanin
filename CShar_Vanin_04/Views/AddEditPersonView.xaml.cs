using CShar_Vanin_04.Tools.Navigation;
using CShar_Vanin_04.ViewModels;

namespace CShar_Vanin_04.Views
{
    /// <summary>
    /// Interaction logic for AddEditPersonView.xaml
    /// </summary>
    public partial class AddEditPersonView:  INavigatable
    {
        public AddEditPersonView()
        {
            InitializeComponent();
            DataContext = new AddEditViewModel();
        }
    }

}
