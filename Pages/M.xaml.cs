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
using PPPP.Models;

namespace PPPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для M.xaml
    /// </summary>
    public partial class M : Page
    {
        public M()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new sprav());
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new ExcelOtch());
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new IzmUsers());
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new Filtr());
        }
    }
}
