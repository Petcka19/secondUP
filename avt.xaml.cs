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
    /// Логика взаимодействия для avt.xaml
    /// </summary>
    public partial class avt : Window
    {
        public avt()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (DBBOOKSEntities db = new DBBOOKSEntities())
            {
                System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@name", Text.Text);
                var avtBooks = db.Database.SqlQuery<avt_Result>("avt @name", param);

                dgBook.ItemsSource = avtBooks.ToList();

            }
        }
    }
}
