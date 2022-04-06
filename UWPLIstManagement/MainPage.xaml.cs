using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPLIstManagement.Dialogs;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ListManagement.models;
using ListManagement.services;
using System.Collections.ObjectModel;
using UWPListManagement.ViewModels;
using Newtonsoft.Json;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPLIstManagement
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MainPage : Page
    {
        private JsonSerializerSettings serializerSettings
           = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        public bool isSorted = false;
        public bool isLoaded = false;
        private ObservableCollection<Item> _itemCollection = ItemService.Current.Items;
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = new MainViewModel();
            SearchlistBox.ItemsSource = _itemCollection;
        }

        private async void AddToDoClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ToDoDialog();
            await dialog.ShowAsync();
            
        }

        private async void EditTodoClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ToDoDialog((DataContext as MainViewModel).SelectedItem);
            await dialog.ShowAsync();
            SearchlistBox.ItemsSource = _itemCollection;
        }

        private async void AddAppointmentClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AppointmentDialog();
            await dialog.ShowAsync();
            SearchlistBox.ItemsSource = _itemCollection;
        }

        private async void EditAppointmentClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AppointmentDialog((DataContext as MainViewModel).SelectedItem);
            await dialog.ShowAsync();
            SearchlistBox.ItemsSource = _itemCollection;
        }

        private async void DeleteItem(object sender, RoutedEventArgs e)
        {
            var dialog = new ItemDialog((DataContext as MainViewModel).SelectedItem);
            await dialog.ShowAsync();
            SearchlistBox.ItemsSource = _itemCollection;
        }

        public bool shouldShow(object sender, RoutedEventArgs e)
        {
            return false;
        }
        public void sortPriority(object sender, RoutedEventArgs e)
        {
            Item temp;
            if (!isSorted)
            {
                for (int i = 0; i < _itemCollection.Count; i++)
                {   
                    if (!_itemCollection[i].Priority)
                    {
                        temp = _itemCollection[i];
                        for (int j = i; j < _itemCollection.Count; j++)
                        {
                            if (_itemCollection[j].Priority)
                            {
                                _itemCollection[i] = _itemCollection[j];
                                _itemCollection[j] = temp;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < _itemCollection.Count; i++)
                {
                    if (_itemCollection[i].Priority)
                    {
                        temp = _itemCollection[i];
                        for (int j = i; j < _itemCollection.Count; j++)
                        {
                            if (!_itemCollection[j].Priority)
                            {
                                _itemCollection[j] = _itemCollection[i];
                                _itemCollection[i] = temp;
                            }
                        }
                    }
                }
                isSorted = false;
            }
        }

        public Item SelectedItem
        {
            get; set;
        }

        private void search_QueryChanged(SearchBox sender, SearchBoxQueryChangedEventArgs args)
        {
            if (ItemService.Current.Items != null)
            {
                SearchlistBox.ItemsSource = _itemCollection.Where(a => a.Name.ToUpper().Contains(search.QueryText.ToUpper()));

            }

        }

        public void saveClick(object sender, RoutedEventArgs e)
        {
            isLoaded = false;
            ItemService.Current.Save();
        }

        public void loadClick(object sender, RoutedEventArgs e)
        {
            string persistencePath;
            persistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SaveData.json";
            if (File.Exists(persistencePath))
            {
              
                    var state = File.ReadAllText(persistencePath);
                    if (state != null)
                    {
                        ItemService.Current.Items.Clear(); 
                        ObservableCollection<Item> _tempCollection= new ObservableCollection<Item>();
                        _tempCollection = JsonConvert.DeserializeObject<ObservableCollection<Item>>(state, serializerSettings);
                        for (int i = 0; i < _tempCollection.Count; i++)
                        {
                           ItemService.Current.Add(_tempCollection[i]);
                        }
                        SearchlistBox.ItemsSource = _itemCollection;
                    }

                SearchlistBox.ItemsSource = _itemCollection;
                isLoaded = true;
            }
            SearchlistBox.ItemsSource = _itemCollection;
            isLoaded = true;
        }

        public async void completeClick(object sender, RoutedEventArgs e)
        {
            var dialog = new TaskCompleteDialog((DataContext as MainViewModel).SelectedItem);
            await dialog.ShowAsync();
            SearchlistBox.ItemsSource = _itemCollection;
        }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public BooleanToVisibilityConverter()
        {
        }
        public object Convert(object value, Type targetType, object paramter, string language)
        { 
            var booleanVal = (bool)value;
            if (booleanVal)
                 return "High Priority";
            else
                 return "Low Priority";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    public class BooleanToCompleteConverter : IValueConverter
    {
        public BooleanToCompleteConverter()
        {
        }
        public object Convert(object value, Type targetType, object paramter, string language)
        {
            var booleanVal = (bool)value;
            if (booleanVal)
                return "IS COMPLETED";
            else
                return "NOT COMPLETED";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}