using CShar_Vanin_04.Tools.Navigation;
using CShar_Vanin_04.ViewModels;

namespace CShar_Vanin_04.Views
{
    /// <summary>
    /// Interaction logic for PersonGridView.xaml
    /// </summary>
    public partial class PersonGridView : INavigatable
    {
        public PersonGridView()
        {
            InitializeComponent();
            DataContext = new PersonGridViewModel();
        }
    }
}
