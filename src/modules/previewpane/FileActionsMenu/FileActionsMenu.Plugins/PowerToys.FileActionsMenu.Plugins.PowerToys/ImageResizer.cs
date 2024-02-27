﻿// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Globalization;
using System.Text;
using FileActionsMenu.Interfaces;
using FileActionsMenu.Ui.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace PowerToys.FileActionsMenu.Plugins.PowerToys
{
    internal sealed class ImageResizer : IAction
    {
        private string[]? _selectedItems;

        public string[] SelectedItems { get => _selectedItems.GetOrArgumentNullException(); set => _selectedItems = value; }

        public string Header => "Resize images with Image Resizer";

        public IAction.ItemType Type => IAction.ItemType.SingleItem;

        public IAction[]? SubMenuItems => null;

        public int Category => 3;

        public IconElement? Icon => IconHelper.GetIconFromModuleName("ImageResizer");

        // Todo: Only visible if only Image Files are selected
        public bool IsVisible => true;

        public Task Execute(object sender, RoutedEventArgs e)
        {
            StringBuilder arguments = new();

            foreach (string item in SelectedItems)
            {
                arguments.Append(CultureInfo.InvariantCulture, $"\"{item}\" ");
            }

            ProcessStartInfo startInfo = new()
            {
                FileName = "PowerToys.ImageResizer.exe",
                Arguments = arguments.ToString(),
                UseShellExecute = true,
            };
            Process.Start(startInfo);
            return Task.CompletedTask;
        }
    }
}