﻿using ListManagement.models;
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
    public sealed partial class ItemDialog : ContentDialog
    {
        public ObservableCollection<Item> _itemCollection;
        public ItemDialog()
        {
            this.InitializeComponent();
            return;
        }
        public ItemDialog(Item item)
        {
            this.InitializeComponent();
            _itemCollection = ItemService.Current.Items; 
            DataContext = item;

        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var item = DataContext as Item;
            if (_itemCollection.Any(i => i.ID == item.ID))
            {
                var itemToUpdate = _itemCollection.FirstOrDefault(i => i.ID == item.ID); 
                var index = _itemCollection.IndexOf(itemToUpdate);
                ItemService.Current.Items.Remove(itemToUpdate);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
