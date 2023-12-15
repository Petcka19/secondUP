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
    /// Логика взаимодействия для PurchasesPage.xaml
    /// </summary>
    public partial class PurchasesPage : Page
    {
        DBBOOKSEntities entities = new DBBOOKSEntities();
        List<Purchases> _pub = new List<Purchases>();
        public PurchasesPage()
        {
            InitializeComponent();
            _pub = entities.Purchases.ToList();
            dgPublish.ItemsSource = entities.Purchases.ToList();
        }
        private void TextChang(object sender, TextChangedEventArgs e)
        {
            _pub = entities.Purchases.ToList();
            if (textCh.Text.Length != 0)
            {
                _pub = _pub.Where(p => p.Date_order.ToString().ToLower().Contains(textCh.Text.ToLower())).ToList();
            }
            dgPublish.ItemsSource = _pub;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPub = dgPublish.SelectedItem as Purchases;
            var result = MessageBox.Show("Вы уверены, что хотите удалить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                entities.Purchases.Remove(selectedPub);
                entities.SaveChanges();
                dgPublish.ItemsSource = null;
                dgPublish.ItemsSource = entities.Purchases.ToList();
            }
        }
    }
}
