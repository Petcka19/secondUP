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
using System.Windows.Shapes;

namespace Общее_УП
{
    /// <summary>
    /// Логика взаимодействия для AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        DBBOOKSEntities entities = new DBBOOKSEntities();
        List<Books> book = new List<Books>();
        Books thisBook;
        List<Authors> authors = new List<Authors>();
        List<Publishing_house> publish = new List<Publishing_house>();
        bool addMet = false;
        public AddBook(Books thisBooks)
        {
            InitializeComponent();

            book = entities.Books.ToList();
            authors = entities.Authors.ToList();
            publish = entities.Publishing_house.ToList();
            cb1.ItemsSource = entities.Authors.ToList();
            cb2.ItemsSource = entities.Publishing_house.ToList();
            if (thisBooks == null)
            {
                addMet = true;
            }
            else
            {
                addMet = false;
                thisBook = thisBooks;
                DataContext = thisBook;
                int idt = Convert.ToInt32(book.Where(g => g.Code_author == thisBook.Code_author).Select(f => f.Code_author).FirstOrDefault());
                cb1.Text = authors.Where(f => f.Code_author == idt).Select(f => f.name_author).FirstOrDefault().ToString();
                int idC = Convert.ToInt32(book.Where(g => g.Code_publish == thisBook.Code_publish).Select(f => f.Code_publish).FirstOrDefault());
                cb2.Text = publish.Where(f => f.Code_publish == idC).Select(f => f.Publish).FirstOrDefault().ToString();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (addMet)
            {
                Books newBooks = new Books();
                entities.Books.Add(newBooks);
                newBooks.Code_book = entities.Books.Max(f => f.Code_book) + 1;
                var code_a = cb1.SelectedValue as Authors;
                newBooks.Code_author = code_a.Code_author;
                newBooks.Title_book = tb1.Text;
                var code_p = cb2.SelectedValue as Publishing_house;
                newBooks.Code_publish = code_p.Code_publish;
                newBooks.Pages = Convert.ToInt32(tb2.Text);
                entities.SaveChanges();
                this.Close();
            }
            else
            {
                try
                {
                    var customer = entities.Books.Where(c => c.Code_book == thisBook.Code_book).FirstOrDefault();
                    var code_a = cb1.SelectedValue as Authors;
                    customer.Code_author = code_a.Code_author;
                    customer.Title_book = tb1.Text.ToString();
                    var code_p = cb2.SelectedValue as Publishing_house;
                    customer.Code_publish = code_p.Code_publish;
                    customer.Pages = Convert.ToInt32(tb2.Text);
                    entities.SaveChanges();
                    MessageBox.Show("Данные обновлены");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
