using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginPass
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
        List<LogPass> luc = new List<LogPass>();
        int err = 3, errCode = 0; 
        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Button_MouseDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PasswordBox ps = (PasswordBox)luc[1].W10.Children[luc[1].W10.Children.Count - 1];
            if (err == 0||err==4)
            {
                errCode = String.IsNullOrEmpty(ps.Password) ? 0 : new Regex(@"[.?*+^$[\]\\(){}|\`\'\""\-]").IsMatch(ps.Password) ? 2 : 1;
                if (errCode != 2)
                err = connection(luc[0].w13.Text, ps.Password) ? 2 : 4;
            }
            if (err == 3||err==1)
            {
                
                errCode = String.IsNullOrEmpty(luc[0].w13.Text) ? 0: new Regex(@"[.?*+^$[\]\\(){}|\`\'\""\-]").IsMatch(luc[0].w13.Text) ? 2 : 1;
                if(errCode!=2)
                err = connection(luc[0].w13.Text) ? 0 : 3;
                
            }
            use(err, errCode);

        }
        private bool connection(string i)
        {
            using (SqlConnection conn = new SqlConnection(Connect.jojo.ConnectionString))
            {
                bool test = false;
                conn.Open();
                string b = "SELECT * FROM dbo.Login WHERE login = '" + i + "'";
                SqlCommand command = new SqlCommand(b, conn);
                SqlDataReader r = command.ExecuteReader();
                if (r.HasRows) test = true;
                conn.Close();
                if (test) return true;
                
            }
            return false;
        }
        private bool connection(string i, string j)
        {
            using (SqlConnection conn = new SqlConnection(Connect.jojo.ConnectionString))
            {
                bool test = false;
                conn.Open();
                string b = "SELECT * FROM dbo.Login WHERE login = '" + i + "' AND pass = '" + j + "'";
                SqlCommand command = new SqlCommand(b, conn);
                SqlDataReader r = command.ExecuteReader();
                if (r.HasRows) test = true;
                conn.Close();
                if (test) return true;

            }
            return false;
        }
        private void use(int i, int a)
        {

            switch (i)
            {
                case 0:
                    {
                        var animation = new ThicknessAnimation();
                        animation.From = new Thickness(0, 70, 0, 90);
                        animation.To = new Thickness(-1000, 70, 0, 90);
                        animation.Duration = TimeSpan.FromSeconds(0.5);
                        luc[0].BeginAnimation(MarginProperty, animation);

                        luc[1].Visibility = Visibility.Visible;
                        var animation1 = new ThicknessAnimation();
                        animation1.From = new Thickness(1000, 70, 0, 90);
                        animation1.To = new Thickness(0, 70, 0, 90);
                        animation1.Duration = TimeSpan.FromSeconds(1);
                        luc[1].BeginAnimation(MarginProperty, animation1);
                        trible.Visibility = Visibility.Visible;
                        break;
                    }
                
                case 1:
                    {
                        var animation = new ThicknessAnimation();
                        animation.From = new Thickness(-1000, 70, 0, 90);
                        animation.To = new Thickness(0, 70, 0, 90);
                        animation.Duration = TimeSpan.FromSeconds(0.5);
                        luc[0].BeginAnimation(MarginProperty, animation);

                        luc[1].Visibility = Visibility.Visible;
                        var animation1 = new ThicknessAnimation();
                        animation1.From = new Thickness(0, 70, 0, 90);
                        animation1.To = new Thickness(1000, 70, 0, 90);
                        animation1.Duration = TimeSpan.FromSeconds(1);
                        luc[1].BeginAnimation(MarginProperty, animation1);
                        trible.Visibility = Visibility.Hidden;
                        break;
                        
                    }
                case 2:
                    {
                        this.Hide();
                        Window wn = new Main();
                        wn.Show();
                        break;
                    }
                case 3:
                    {

                        switch (a)
                        {
                            case 0:
                                luc[0].w12.Content = "Введите логин";
                                break;
                            case 1:
                                luc[0].w12.Content = "Неправильный логин";
                                break;
                            case 2:
                                luc[0].w12.Content = "Логин содержит недопустимые символы";
                                break;
                        }
                        luc[0].w11.Visibility = Visibility.Visible;
                        luc[0].w12.Visibility = Visibility.Visible;
                        var animation = new ThicknessAnimation();
                        animation.From = new Thickness(10, 60, 10, 50);
                        animation.To = new Thickness(10, 80, 10, 30);
                        animation.Duration = TimeSpan.FromSeconds(0.5);
                        luc[0].w11.BeginAnimation(MarginProperty, animation);
                        luc[0].w12.BeginAnimation(MarginProperty, animation);
                        break;
                    }
                case 4:
                    {

                        switch (a)
                        {
                            case 0:
                                luc[1].w12.Content = "Введите пароль";
                                break;
                            case 1:
                                luc[1].w12.Content = "Неправильный пароль";
                                break;
                            case 2:
                                luc[1].w12.Content = "Пароль содержит недопустимые символы";
                                break;
                        }
                        luc[1].w11.Visibility = Visibility.Visible;
                        luc[1].w12.Visibility = Visibility.Visible;
                        var animation = new ThicknessAnimation();
                        animation.From = new Thickness(10, 60, 10, 50);
                        animation.To = new Thickness(10, 80, 10, 30);
                        animation.Duration = TimeSpan.FromSeconds(0.5);
                        luc[1].w11.BeginAnimation(MarginProperty, animation);
                        luc[1].w12.BeginAnimation(MarginProperty, animation);
                        break;
                    }


            
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LogPass us = new LogPass();

            for (int i = 0; i < 2; i++)
            {
                luc.Add(new LogPass());
                W1.Children.Add(luc[i]);
                luc[i].SetValue(Grid.RowProperty, 2);
                luc[i].SetValue(Grid.ColumnProperty, 1);
            }
            luc[0].Margin = new Thickness(0, 70, 0, 90);
            
            luc[0].w2.Content = "Логин:";
            luc[1].w2.Content = "Пароль:";
            var pb = new PasswordBox();
         
            

            pb.Width = luc[1].w13.Width;
            pb.Height = luc[1].w13.Height;
            pb.Margin = luc[1].w13.Margin;
            pb.PasswordChar = '*';
            pb.FontStyle = luc[1].w13.FontStyle;
            pb.FontSize = 20;
            pb.FontWeight = luc[1].w13.FontWeight;
            pb.HorizontalAlignment = luc[1].w13.HorizontalAlignment;
            pb.VerticalAlignment = luc[1].w13.VerticalAlignment;
            pb.Background = luc[1].w13.Background;

            luc[1].W10.Children.Add(pb);
            luc[1].w13.Visibility = Visibility.Hidden;

            luc[1].Visibility = Visibility.Hidden;
            
        }

        private void Polygon_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void trible_MouseLeave(object sender, MouseEventArgs e)
        {
            
        }

        private void trible_MouseEnter(object sender, MouseEventArgs e)
        {
            trible.Fill = new SolidColorBrush(Color.FromArgb(255, 153, 180, 209));
        }

        private void trible_MouseDown(object sender, MouseButtonEventArgs e)
        {
            err = 1; errCode = 0;
            use(err, 0);
        }

        private void trible_MouseLeave_1(object sender, MouseEventArgs e)
        {
            trible.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 252, 214));
        }
    }
}
