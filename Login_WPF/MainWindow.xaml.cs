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
using MaterialDesignThemes.Wpf;
using System.Data.SqlClient;

namespace Login_WPF
{
   
    public partial class MainWindow : Window
    {
        public MainWindow()
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

        private void isClick(object sender, RoutedEventArgs e)
        {
            string password = "";
            bool IsExist = false;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            string login = "SELECT * FROM dbo.Account WHERE ID='"+txtUserName.Text+"'";
            SqlCommand cmd = new SqlCommand(login,con);
            SqlDataReader dr = cmd.ExecuteReader();


            if (dr.Read() == true)
            {
                password = dr.GetString(1);   
                IsExist = true;
            
            }
            con.Close();

            if (IsExist)  
            {
                if (Cryptography.DecryptKey(password).Equals(txtPassword.Password.ToString()))
                {
                    new Dashboard().Show();
                    this.Hide();
                } 
            }
            else
            {
                MessageBox.Show("Invalid Username or Password, Please Try Again", "Login Failed", MessageBoxButton.OK);
                txtUserName.Text = "";
                txtPassword.Password = null;
                txtUserName.Focus();
            }
        }

        private void isCreateAccount(object sender, RoutedEventArgs e)
        {
            new Register().Show();
            this.Hide();
        }
    }
}
