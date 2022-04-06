using ListManagement.models;
using ListManagement.services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPLIstManagement.Dialogs
{
    public sealed partial class ToDoDialog : ContentDialog
    {

        public bool yes;
        private ObservableCollection<Item> _toDoCollection;
        public ToDoDialog()
        {
            this.InitializeComponent();
            _toDoCollection = ItemService.Current.Items;
            DataContext = new Task();
        }

        public ToDoDialog(Item item)
        {
            this.InitializeComponent();
            _toDoCollection = ItemService.Current.Items;
            DataContext = item;
           
        }

        public void makePriority(object sender, RoutedEventArgs e)
        {
            Item item = DataContext as Task;
            item.Priority = true;
            DataContext = item; 
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            
            var item = DataContext as Task;
            if (item == null)
                return;
            if (_toDoCollection.Any(i => i.ID == item.ID))
            {
                var itemToUpdate = _toDoCollection.FirstOrDefault(i => i.ID == item.ID);
                var index = _toDoCollection.IndexOf(itemToUpdate);
                DateTime startTemp = new DateTime(deadLinePicker.Date.Value.Year, deadLinePicker.Date.Value.Month, deadLinePicker.Date.Value.Day,
                   starttimepicker.Time.Hours, starttimepicker.Time.Minutes, starttimepicker.Time.Seconds);
                item.DeadLine = startTemp;
                _toDoCollection.RemoveAt(index);
                _toDoCollection.Insert(index, item);
         
            }
            else
            {
                if (item.Name == null)
                    return;
                DateTime startTemp = new DateTime(deadLinePicker.Date.Value.Year, deadLinePicker.Date.Value.Month, deadLinePicker.Date.Value.Day,
                    starttimepicker.Time.Hours, starttimepicker.Time.Minutes, starttimepicker.Time.Seconds);
                item.DeadLine = startTemp;
                if (item == null || item.Name == null)
                    return;
                ItemService.Current.Add(DataContext as Task);
            }


        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }

    
}
