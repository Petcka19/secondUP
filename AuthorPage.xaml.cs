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

namespace Общее_УП.Properties
{
    /// <summary>
    /// Логика взаимодействия для AuthorPage.xaml
    /// </summary>
    public partial class AuthorPage : Page
    {
        public AuthorPage()
        {
            InitializeComponent();
            dgAitor.ItemsSource = entities.Authors.ToList();
            author = entities.Authors.ToList();
        }
        DBBOOKSEntities entities = new DBBOOKSEntities();
        List<Authors> author = new List<Authors>();

        private void TextChang(object sender, TextChangedEventArgs e)
        {
            author = entities.Authors.ToList();
            if (textCh.Text.Length != 0)
            {
                author = author.Where(p => p.name_author.ToLower().Contains(textCh.Text.ToLower())).ToList();
            }
            dgAitor.ItemsSource = author;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAuthor = dgAitor.SelectedItem as Authors;
            Authors NewAuthors = new Authors();
            entities.Authors.Add(NewAuthors);
            NewAuthors.Code_author = (author.Max(f => f.Code_author)) + 1;
            NewAuthors.name_author = selectedAuthor.name_author;
            NewAuthors.Birthday = selectedAuthor.Birthday;
            entities.SaveChanges();
            MessageBox.Show("Данные добавлены");
            dgAitor.ItemsSource = null;
            dgAitor.ItemsSource = entities.Authors.ToList();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAuthor = dgAitor.SelectedItem as Authors;
            entities.SaveChanges();
            MessageBox.Show("Данные обновлены");
            dgAitor.ItemsSource = null;
            dgAitor.ItemsSource = entities.Authors.ToList();

        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAuthor = dgAitor.SelectedItem as Authors;
            var result = MessageBox.Show("Вы уверены, что хотите удалить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                entities.Authors.Remove(selectedAuthor);
                entities.SaveChanges();
                dgAitor.ItemsSource = null;
                dgAitor.ItemsSource = entities.Authors.ToList();
            }
        }
    }
}
