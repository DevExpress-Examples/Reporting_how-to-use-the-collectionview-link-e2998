﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DevExpress.Xpf.Printing;
// ...

namespace UseCollectionViewLinkSL {
    public partial class MainPage : UserControl {
        public MainPage() {
            InitializeComponent();
        }

        #region #CollectionViewLink
        private void Page_Loaded(object sender, RoutedEventArgs e) {
            // Create a link and bind it to the PrintPreview instance.
            CollectionViewLink link = new CollectionViewLink();
            preview.Model = new LinkPreviewModel(link);

            // Create an ICollectionView object.
            link.CollectionView = CreateMonthCollectionView();
            link.ExportServiceUri = "ExportService1.svc";

            // Provide export templates.
            link.DetailTemplate = (DataTemplate)Resources["monthNameTemplate"];
            link.GroupInfos.Add(new GroupInfo((DataTemplate)Resources["monthQuarterTemplate"]));

            // Create a document.
            link.CreateDocument(true);
        }

        private ICollectionView CreateMonthCollectionView() {
            const int monthCount = 12;
            string[] monthNames = CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;

            MonthItem[] data = new MonthItem[Math.Min(monthNames.Length, monthCount)];
            for (int i = 0; i < data.Length; i++) {
                data[i] = new MonthItem(monthNames[i], (i / 3) + 1);
            }

            CollectionViewSource source = new CollectionViewSource();
            source.Source = data;
            source.GroupDescriptions.Add(new PropertyGroupDescription("Quarter"));

            return source.View;
        }

        public class MonthItem {
            public string Name { get; private set; }
            public int Quarter { get; private set; }

            public MonthItem(string name, int quarter) {
                Name = name;
                Quarter = quarter;
            }
        }
        #endregion #CollectionViewLink

    }
}
