using System;
using PersonGridView = CShar_Vanin_04.Views.PersonGridView;
using AddEditPersonView = CShar_Vanin_04.Views.AddEditPersonView;

namespace CShar_Vanin_04.Tools.Navigation
{
    internal class PersonListNavigationModel : BaseNavigationModel
    {
        
        public PersonListNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {

        }

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.PersonGrid:
                    AddView(ViewType.PersonGrid, new PersonGridView());
                    break;
                case ViewType.AddEdit:
                    AddView(ViewType.AddEdit, new AddEditPersonView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}