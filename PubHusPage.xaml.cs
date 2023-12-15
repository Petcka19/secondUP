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
    /// Логика взаимодействия для PubHusPage.xaml
    /// </summary>
    public partial class PubHusPage : Page
    {
        DBBOOKSEntities entities = new DBBOOKSEntities();
        List<Publishing_house> _pub = new List<Publishing_house>();
        public PubHusPage()
        {
            InitializeComponent();
            dgPublish.ItemsSource = entities.Publishing_house.ToList();
            _pub = entities.Publishing_house.ToList();
        }
        private void TextChang(object sender, TextChangedEventArgs e)
        {
            _pub = entities.Publishing_house.ToList();
            if (textCh.Text.Length != 0)
            {
                _pub = _pub.Where(p => p.Publish.ToLower().Contains(textCh.Text.ToLower())).ToList();
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
            var selectedPub = dgPublish.SelectedItem as Publishing_house;
            var result = MessageBox.Show("Вы уверены, что хотите удалить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                entities.Publishing_house.Remove(selectedPub);
                entities.SaveChanges();
                dgPublish.ItemsSource = null;
                dgPublish.ItemsSource = entities.Publishing_house.ToList();
            }
        }
    }
}
