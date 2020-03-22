namespace CShar_Vanin_04.Tools.Navigation
{
    internal enum ViewType
    {
        AddEdit = 0,
        PersonGrid = 1,
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}