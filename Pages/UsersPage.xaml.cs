using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PPPP.Models;

namespace PPPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private D1E db;

        public UsersPage()
        {
            InitializeComponent();
            db = new D1E();
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                // Загружаем список пользователей из базы данных
                cmbLogin.ItemsSource = db.Users.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке пользователей: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbLogin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbLogin.SelectedItem != null)
            {
                var selectedUser = cmbLogin.SelectedItem as Users;
                if (selectedUser != null)
                {
                    // Заполняем поле роли выбранного пользователя
                    cmbRole.ItemsSource = new[] { selectedUser };
                    cmbRole.SelectedIndex = 0;
                }
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbLogin.SelectedItem == null || string.IsNullOrEmpty(txtPassword.Password))
                {
                    MessageBox.Show("Пожалуйста, выберите пользователя и введите пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var selectedUser = cmbLogin.SelectedItem as Users;
                if (selectedUser != null)
                {
                    // Проверяем пароль
                    if (selectedUser.Password == txtPassword.Password)
                    {
                        MessageBox.Show("Вход выполнен успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        Nav.MainFrame.Navigate(new M());
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при входе: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
