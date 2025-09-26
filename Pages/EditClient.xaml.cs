using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PPPP.Models;

namespace PPPP.Pages
{
    public partial class EditClient : Page
    {
        private D1E db;
        private int _clientId;

        public EditClient(int clientId)
        {
            InitializeComponent();
            db = new D1E();
            _clientId = clientId;
            LoadClientData();
        }

        private void LoadClientData()
        {
            try
            {
                var client = db.Clients.FirstOrDefault(c => c.ID_Client == _clientId);
                if (client != null)
                {
                    txtCompanyName.Text = client.CompanyName; // This will be read-only
                    txtContactPerson.Text = client.ContactPerson;
                    txtPhone.Text = client.Phone;
                    txtEmail.Text = client.Email;
                }
                else
                {
                    MessageBox.Show("Клиент не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    // Optionally navigate back if client not found
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных клиента: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var clientToUpdate = db.Clients.FirstOrDefault(c => c.ID_Client == _clientId);
                if (clientToUpdate != null)
                {
                    // Validation (optional, can add more sophisticated validation)
                    if (string.IsNullOrWhiteSpace(txtContactPerson.Text) ||
                        string.IsNullOrWhiteSpace(txtPhone.Text) ||
                        string.IsNullOrWhiteSpace(txtEmail.Text))
                    {
                        MessageBox.Show("Пожалуйста, заполните все обязательные поля!", "Ошибка", 
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Update client properties
                    clientToUpdate.ContactPerson = txtContactPerson.Text.Trim();
                    clientToUpdate.Phone = txtPhone.Text.Trim();
                    clientToUpdate.Email = txtEmail.Text.Trim();

                    db.SaveChanges();

                    MessageBox.Show("Данные клиента успешно обновлены!", "Успех", 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    // Return to the previous page (sprav page)
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Ошибка при сохранении: Клиент не найден!", "Ошибка", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных клиента: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // Return to the previous page without saving
            NavigationService.GoBack();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Return to the previous page
            NavigationService.GoBack();
        }
    }
} 