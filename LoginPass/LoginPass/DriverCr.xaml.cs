using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private List<string> DataBase1 = new List<string>();
        private List<string> cb1 = new List<string>();
        private List<string> cb2 = new List<string>();
        string imgName = "";
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            start();
            checkt(asdw);
            checkt(asdw1);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            List<string> ltb = new List<string>()
            {
                q1.Text, q2.Text, q3.Text, q4.Text, q5.Text, asdw1.Text, q6.Text, asdw.Text, q7.Text, q8.Text, q9.Text, q10.Text, q11.Text, q12.Text 
            };
            
            connection(ltb);
        }
        private void start()
        {
            using (SqlConnection conn = new SqlConnection(Connect.jojo.ConnectionString))
            {
                conn.Open();
                string jotaro = "SELECT MAX([id]) FROM dbo.drivers2";
                SqlCommand command = new SqlCommand(jotaro, conn);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                q1.Text = (Int16.Parse(reader[0].ToString())+1).ToString();
                conn.Close();
            }
        }
            private void connection(List<string> ltb)
        {
            using (SqlConnection conn = new SqlConnection(Connect.jojo.ConnectionString))
            {
                conn.Open();
                string jotaro = "INSERT INTO dbo.drivers2(id, Fam, Name, Otch, Pass, RegAd, ProAd, JobPl, Dolj, Mobila, Email, Des, Foto) VALUES(";
                for (int i = 0; i < ltb.Count; i++)
                    if (i == 5||i==7)
                    {
                        jotaro = jotaro + "'" + ltb[i]+", "+ ltb[i+1] + "', ";
                        i++;
                    }
                    else
                    {
                        jotaro = jotaro + "'" + ltb[i] + "', ";
                    }
                jotaro = jotaro + "'" + imgName + "');";
                Console.WriteLine(jotaro);
                SqlCommand command = new SqlCommand(jotaro, conn);
                command.ExecuteNonQuery();
                MessageBox.Show("Успешно!");
                conn.Close();
            }
        }

        


        private void checkt(ComboBox j)
        {
            j.Items.Clear();
            recAdd(0, 50, 50, j);

        }
        private void recAdd(int j, int b, int step, ComboBox city)
        {
            if (b < Connect.Cityes.Count)
                recAdd(j+step, b+step, step, city);
            else b = Connect.Cityes.Count;
           
            int k = 0;
            for (int i = j; i < b; i++)
            {
                //Console.WriteLine(DataBase1[i].Substring(0, asdw.Text.Length) + " " + asdw.Text);
                k = city.Text.Length > Connect.Cityes[Connect.Cityes.Count - i - 1].Length ? Connect.Cityes[Connect.Cityes.Count - i - 1].Length : city.Text.Length;

                if (!String.IsNullOrEmpty(city.Text))
                { 
                    if (Connect.Cityes[Connect.Cityes.Count - i - 1].ToLower().Contains(city.Text.ToString().ToLower()))
                    {

                        city.Items.Add(Connect.Cityes[Connect.Cityes.Count - i - 1]);
                    } 
                }
                else
                {
                    city.Items.Add(Connect.Cityes[Connect.Cityes.Count - i - 1]);
                }
                
            }

        }

        private void asdw_TextInput(object sender, TextCompositionEventArgs e)
        {
            
        }

        private void q9_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void asdw_KeyUp(object sender, KeyEventArgs e)
        {
            asdw.IsDropDownOpen = true;
            checkt(asdw);
        }

        private void asdw1_KeyUp(object sender, KeyEventArgs e)
        {
            asdw1.IsDropDownOpen = true;
            checkt(asdw1);
        }

        private void asdw1_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }

        private void asdw_LostFocus(object sender, RoutedEventArgs e)
        {
           
        }


        private void q1_TextInput(object sender, TextCompositionEventArgs e)
        {
            

        }

        private void q1_KeyUp(object sender, KeyEventArgs e)
        {
            if(!String.IsNullOrEmpty(q1.Text))
                if (new Regex("[^0-9]").IsMatch(q1.Text))
                 {
                    MessageBox.Show("Только цифры");
                    q1.Text = new Regex("[^0-9]").Replace(q1.Text, "");
                 }
        }

        private void q5_KeyUp(object sender, KeyEventArgs e)
        {
            if (!String.IsNullOrEmpty(q5.Text))
                if (new Regex("[^0-9 ]").IsMatch(q5.Text))
                {
                    MessageBox.Show("Только цифры и пробел");
                    q5.Text = new Regex("[^0-9 ]").Replace(q5.Text, "");
                }
        }

        private void q10_KeyUp(object sender, KeyEventArgs e)
        {
            if (!String.IsNullOrEmpty(q10.Text))
                if (new Regex(@"[^0-9 \+\-\(\)]").IsMatch(q10.Text))
                {
                    MessageBox.Show("Только цифры, пробел, +, -, ( и )");
                    q10.Text = new Regex(@"[^0-9 \+\-\(\)]").Replace(q10.Text, "");
                }
        }

        private void img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void img_MouseUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog()
            {
                Title = "Найти картинку"
            };
            if (od.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(od.FileName));
                if (bitmap.Width % 3 == bitmap.Height % 4)
                {
                    imgName = DateTime.Now.ToString("yyyy.MM.dd_HH-mm-ss") + od.FileName.Substring(od.FileName.Length - 4, 4);
                    File.Copy(od.FileName, AppDomain.CurrentDomain.BaseDirectory + @"img\" + imgName);
                    img.Source = bitmap;
                }
                else
                {
                    MessageBox.Show("Неправильный формат!");
                }
            }
        }
    }
}
