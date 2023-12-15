using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Общее_УП
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameWithinGrid.Navigate(new System.Uri("BookPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FrameWithinGrid.Navigate(new System.Uri("AuthorPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FrameWithinGrid.Navigate(new System.Uri("PubHusPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            FrameWithinGrid.Navigate(new System.Uri("PurchasesPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            FrameWithinGrid.Navigate(new System.Uri("DeliveriesPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            avt a = new avt();
            a.Show();
        }
    }
}
