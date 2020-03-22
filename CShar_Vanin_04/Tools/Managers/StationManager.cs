using System;
using System.Windows;
using CShar_Vanin_04.Models;
using CShar_Vanin_04.Tools.DataStorage;

namespace CShar_Vanin_04.Tools.Managers
{
    internal static class StationManager
    {
        private static IDataStorage _dataStorage;

        internal static Person SelectedUser { get; set; }
        internal static IDataStorage DataStorage => _dataStorage;

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        internal static void CloseApp()
        {
            MessageBox.Show("ShutDown");
            Environment.Exit(1);
        }
    }
}