using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace LoginPass
{
    /// <summary>
    /// Логика взаимодействия для DriverCr.xaml
    /// </summary>
    public partial class DriverCr : UserControl
    {
        public DriverCr()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<TextBox> ltb = new List<TextBox>()
            {
                q1, q2, q3, q4, q5, q6, q7, q8, q9, q10, q11, q12
            };
            connection(ltb);
        }

        private void connection(List<TextBox> ltb)
        {
            using (SqlConnection conn = new SqlConnection(Connect.jojo.ConnectionString))
            {
                conn.Open();
                string jotaro = "INSERT INTO dbo.drivers2(id, Fam, Name, Otch, Pass, RegAd, ProAd, JobPl, Dolj, Mobila, Email, Des, Foto) VALUES(";
                for (int i = 0; i < ltb.Count; i++)
                    jotaro = jotaro + "'" + ltb[i] + "', ";
                jotaro = jotaro + "'" + img.Source + "');";
                SqlCommand command = new SqlCommand(jotaro, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
