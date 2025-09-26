using System;
using System.Windows;
using System.Windows.Controls;
using PPPP.Models;

namespace PPPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Page
    {
        private D1E db;

        public AddClient()
        {
            InitializeComponent();
            db = new D1E();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполнения полей
                if (string.IsNullOrWhiteSpace(txtCompanyName.Text) ||
                    string.IsNullOrWhiteSpace(txtContactPerson.Text) ||
                    string.IsNullOrWhiteSpace(txtPhone.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Создание нового клиента
                var newClient = new Clients
                {
                    CompanyName = txtCompanyName.Text.Trim(),
                    ContactPerson = txtContactPerson.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };

                // Добавление в базу данных
                db.Clients.Add(newClient);
                db.SaveChanges();

                MessageBox.Show("Клиент успешно добавлен!", "Успех", 
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Возврат на предыдущую страницу
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении клиента: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
