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
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Diagnostics;


namespace Общее_УП
{
    /// <summary>
    /// Логика взаимодействия для DeliveriesPage.xaml
    /// </summary>
    public partial class DeliveriesPage : Page
    {
        public DeliveriesPage()
        {
            InitializeComponent();
            dgPublish.ItemsSource = entities.Deliveries.ToList();
            purchases = entities.Deliveries.ToList();
        }
        DBBOOKSEntities entities = new DBBOOKSEntities();
        List<Deliveries> purchases = new List<Deliveries>();

        private void TextChang(object sender, TextChangedEventArgs e)
        {
            purchases = entities.Deliveries.ToList();

            if (textCh.Text.Length != 0)
            {
                purchases = purchases.Where(p => p.Name_company.ToString().ToLower().Contains(textCh.Text.ToLower())).ToList();
            }
            dgPublish.ItemsSource = purchases;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAuthor = dgPublish.SelectedItem as Deliveries;
            Deliveries NewAuthors = new Deliveries();
            entities.Deliveries.Add(NewAuthors);
            NewAuthors.Code_delivery = (purchases.Max(f => f.Code_delivery)) + 1;
            NewAuthors.Name_delivery = selectedAuthor.Name_delivery;
            NewAuthors.Name_company = selectedAuthor.Name_company;
            NewAuthors.Address = selectedAuthor.Address;
            NewAuthors.Phone = selectedAuthor.Phone;
            NewAuthors.INN = selectedAuthor.INN;
            entities.SaveChanges();
            MessageBox.Show("Данные добавлены");
            dgPublish.ItemsSource = null;
            dgPublish.ItemsSource = entities.Deliveries.ToList();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAuthor = dgPublish.SelectedItem as Authors;
            entities.SaveChanges();
            MessageBox.Show("Данные обновлены");
            dgPublish.ItemsSource = null;
            dgPublish.ItemsSource = entities.Deliveries.ToList();
        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAuthor = dgPublish.SelectedItem as Deliveries;
            var result = MessageBox.Show("Вы уверены, что хотите удалить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                entities.Deliveries.Remove(selectedAuthor);
                entities.SaveChanges();
                dgPublish.ItemsSource = null;
                dgPublish.ItemsSource = entities.Deliveries.ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("../../sample.pdf", FileMode.Create));
            document.Open();
            var logo = iTextSharp.text.Image.GetInstance(new FileStream(@"..\..\i.jpg", FileMode.Open));
            logo.SetAbsolutePosition(500, 800); // Задайте координаты верхнего левого угла изображения на документе
            logo.ScaleToFit(30, 30); // Задайте требуемую ширину и высоту изображения
            writer.DirectContent.AddImage(logo);
            BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            PdfPTable table = new PdfPTable(dgPublish.Columns.Count);
            PdfPCell cell = new PdfPCell(new Phrase("Список поставщиков", font));
            cell.Colspan = dgPublish.Columns.Count;
            cell.HorizontalAlignment = 1;
            cell.Border = 0;
            table.AddCell(cell);
            string[] name = { "Поставщик", "Компания", "Адрес", "Телефон", "ИНН" };
            for (int i = 0; i < dgPublish.Columns.Count; i++)
            {
                cell = new PdfPCell(new Phrase(name[i], font));
                cell.BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY;
                table.AddCell(cell);
            }
            for (int j = 0; j < dgPublish.Items.Count; j++)
            {
                for (int i = 0; i < dgPublish.Columns.Count; i++)
                {
                    TextBlock b = dgPublish.Columns[i].GetCellContent(dgPublish.Items[j]) as TextBlock;
                    table.AddCell(new Phrase(b.Text, font));
                }
            }
            document.Add(table);
            document.Close();
            Process.Start(@"..\..\sample.pdf");
        }
    }

    internal class DBBooksEntities
    {
    }
}
