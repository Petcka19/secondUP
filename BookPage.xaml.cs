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
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
namespace Общее_УП.Properties
{
    /// <summary>
    /// Логика взаимодействия для BookPage.xaml
    /// </summary>
    public partial class BookPage : System.Windows.Controls.Page
    {
        public BookPage()
        {
            InitializeComponent();
            dgBook.ItemsSource = entities.Books.ToList();
            book = entities.Books.ToList();
        }
        DBBOOKSEntities entities = new DBBOOKSEntities();
        List<Books> book = new List<Books>();
        Microsoft.Office.Interop.Word._Application oWord;
        private void TextChang(object sender, TextChangedEventArgs e)
        {
            book = entities.Books.ToList();
            if (textCh.Text.Length != 0)
            {
                book = book.Where(p => p.Title_book.ToLower().Contains(textCh.Text.ToLower())).ToList();
            }
            dgBook.ItemsSource = book;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddBook add = new AddBook(null);
            add.ShowDialog();
            NavigationService.Refresh();
            dgBook.SelectedItem = null;
            dgBook.SelectedItem = entities.Books.ToList(); ;

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedPost = dgBook.SelectedItem as Books;
            AddBook add = new AddBook(selectedPost);
            add.ShowDialog();
            NavigationService.Refresh();
            dgBook.SelectedItem = null;
            dgBook.SelectedItem = entities.Books.ToList(); ;
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = dgBook.SelectedItem as Books;
            var result = MessageBox.Show("Вы уверены, что хотите удалить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                entities.Books.Remove(selectedBook);
                entities.SaveChanges();
                dgBook.ItemsSource = null;
                dgBook.ItemsSource = entities.Books.ToList();
            }
        }
        private _Document GetDoc(string path)
        {
            _Document oDoc = oWord.Documents.Add(path);
            SetTemplate(oDoc);
            return oDoc;
        }
        private void exp_Click(object sender, RoutedEventArgs e)
        {
            oWord = new Microsoft.Office.Interop.Word.Application();
            _Document oDoc = GetDoc(Environment.CurrentDirectory + "\\шаблонЦенник1.dot");
            oDoc.SaveAs(FileName: Environment.CurrentDirectory + "\\For_print.doc");
            oDoc = oWord.Documents.Add();
            oWord.Visible = true;
            oDoc.Close();
        }
        private void SetTemplate(Microsoft.Office.Interop.Word._Document oDoc)
        {
            var selectedBook = dgBook.SelectedItem as Books;
            oDoc.Bookmarks["Название"].Range.Text = selectedBook.Title_book.ToString();
            oDoc.Bookmarks["Автор"].Range.Text = selectedBook.name_author.ToString();
            oDoc.Bookmarks["Издательство"].Range.Text = selectedBook.name_publish.ToString();
        }

        private void exp_Copy_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            for (int i = 0; i < dgBook.Columns.Count; i++)
            {
                Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[1, i + 1];
                sheet1.Cells[1, i + 1].Font.Bold = true;
                myRange.Value2 = dgBook.Columns[i].Header;
            }
            for (int i = 0; i < dgBook.Columns.Count; i++)
            {
                for (int j = 0; j < dgBook.Items.Count; j++)
                {
                    TextBlock b = dgBook.Columns[i].GetCellContent(dgBook.Items[j]) as TextBlock;
                    if (b == null)
                    {
                        continue;
                    }
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
            sheet1.Columns.AutoFit();

        }
    }
}
