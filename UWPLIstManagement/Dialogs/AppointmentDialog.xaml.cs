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
    public sealed partial class AppointmentDialog : ContentDialog
    {

        private ObservableCollection<Item> _appointmentCollection;
        public AppointmentDialog()
        {
            this.InitializeComponent();
            _appointmentCollection = ItemService.Current.Items;
            DataContext = new Appointment();
        }
        public void AddAttendeeClick(object sender, RoutedEventArgs e)
        {
            Appointment item = DataContext as Appointment;
            item.Attendees.Add(nameSubmit.Text);
        }
        public AppointmentDialog(Item item)
        {
            this.InitializeComponent();
            _appointmentCollection = ItemService.Current.Items;
            DataContext = item;
        }
        public void makePriority(object sender, RoutedEventArgs e)
        {
            Item item = DataContext as Appointment;
            item.Priority = true;
            DataContext = item;
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var item = DataContext as Appointment;
            if (item == null)
                return;
            if (_appointmentCollection.Any(i => i.ID == item.ID))
            {
                var itemToUpdate = _appointmentCollection.FirstOrDefault(i => i.ID == item.ID);
                var index = _appointmentCollection.IndexOf(itemToUpdate);
                DateTime startTemp = new DateTime(startdatepicker.Date.Value.Year, startdatepicker.Date.Value.Month, startdatepicker.Date.Value.Day,
                    starttimepicker.Time.Hours, starttimepicker.Time.Minutes, starttimepicker.Time.Seconds);
                DateTime endTemp = new DateTime(enddatepicker.Date.Value.Year, enddatepicker.Date.Value.Month, enddatepicker.Date.Value.Day,
                    endtimepicker.Time.Hours, endtimepicker.Time.Minutes, endtimepicker.Time.Seconds);
                item.Start = startTemp;
                item.End = endTemp;
                _appointmentCollection.RemoveAt(index);
                _appointmentCollection.Insert(index, item);
            }
            else
            {
                DateTime startTemp = new DateTime(startdatepicker.Date.Value.Year, startdatepicker.Date.Value.Month, startdatepicker.Date.Value.Day,
                    starttimepicker.Time.Hours, starttimepicker.Time.Minutes, starttimepicker.Time.Seconds);
                DateTime endTemp = new DateTime(enddatepicker.Date.Value.Year, enddatepicker.Date.Value.Month, enddatepicker.Date.Value.Day,
                    endtimepicker.Time.Hours, endtimepicker.Time.Minutes, endtimepicker.Time.Seconds);
                item.Start = startTemp;
                item.End = endTemp;
                if (item == null || item.Name == null)
                    return;
                ItemService.Current.Add(DataContext as Appointment);
            }

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}

