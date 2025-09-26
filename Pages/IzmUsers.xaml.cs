using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PPPP.Models;

namespace PPPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для IzmUsers.xaml
    /// </summary>
    public partial class IzmUsers : Page
    {
        private D1E db;

        public IzmUsers()
        {
            InitializeComponent();
            db = new D1E();
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                dgUsers.ItemsSource = db.Users.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке пользователей: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Добавить окно для создания нового пользователя
            MessageBox.Show("Функция добавления пользователя будет реализована позже");
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button.DataContext as Users;
            if (user != null)
            {
                // TODO: Добавить окно для редактирования пользователя
                MessageBox.Show($"Редактирование пользователя {user.Login} будет реализовано позже");
            }
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button.DataContext as Users;
            if (user != null)
            {
                var result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить пользователя {user.Login}?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Users.Remove(user);
                        db.SaveChanges();
                        LoadUsers(); // Обновляем список пользователей
                        MessageBox.Show("Пользователь успешно удален!", "Успех", 
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}", "Ошибка", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}
