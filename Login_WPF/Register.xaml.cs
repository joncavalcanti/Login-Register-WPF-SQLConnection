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
using MaterialDesignThemes.Wpf;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;

namespace Login_WPF
{
    /// <summary>
    /// Lógica interna para Register.xaml
    /// </summary>
    public partial class Register : Window
    {

        public Register()
        {
            InitializeComponent();
        }

        public string conString = "Data Source=DESKTOP-C45AQE3;Initial Catalog=db_users;Integrated Security=True";

        public bool isDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
      
        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (isDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                isDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                isDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);

            }
            paletteHelper.SetTheme(theme);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }


        private void isClickRegister(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text != "" && txtPassword.Password.ToString() != "")    
            {
               
                    SqlConnection con = new SqlConnection(conString);
                    con.Open();
                    string register = "insert into dbo.Account(ID, Password) values('" + txtUserName.Text.ToString() + "' , '" + Cryptography.EncryptKey(txtPassword.Password.ToString()) + "')";
                    SqlCommand cmd = new SqlCommand(register, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("You account is registered", "Success", MessageBoxButton.OK);
                    new MainWindow().Show();
                    this.Hide();
                }
            else  {
                MessageBox.Show("Please fill all the fields!..", "Error", MessageBoxButton.OK); 
            }
        }

            private void backToLogin(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Hide();
        }

    }
}
