using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }

    [ValueConversion(typeof(TreeViewItem), typeof(Thickness))]
    sealed class TreeViewItemAlimentConverter : IValueConverter
    {
        public Double Length { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var item = value as TreeViewItem;
            if (item == null)
                return new Thickness(0);

            return new Thickness(Length * item.GetDepth(), 0, 0, 0);

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    static class TreeViewItemExtension
    {
        public static TreeViewItem GetParent(TreeViewItem item)
        {
            var parent = VisualTreeHelper.GetParent(item);
            while (!((parent is TreeViewItem) || (parent is TreeView)))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as TreeViewItem;
        }

        public static Int32 GetDepth(this TreeViewItem item)
        {
            TreeViewItem parent;
            while ((parent = GetParent(item)) != null)
            {
                return GetDepth(parent) + 1;
            }

            return 0;
        }
    }
}
