using System.ComponentModel;
using System.Windows;

namespace CShar_Vanin_04.Tools
{
    internal interface ILoaderOwner : INotifyPropertyChanged
    {
        Visibility LoaderVisibility { get; set; }
        bool IsControlEnabled { get; set; }
    }
}