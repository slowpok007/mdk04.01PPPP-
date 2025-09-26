using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PPPP.Models;

namespace PPPP.Pages
{
    public partial class EditOrder : Page
    {
        private D1E db;
        private int _orderId;

        public EditOrder(int orderId)
        {
            InitializeComponent();
            db = new D1E();
            _orderId = orderId;
            LoadOrderData();
        }

        private void LoadOrderData()
        {
            try
            {
                // Load clients for the ComboBox
                cmbClient.ItemsSource = db.Clients.ToList();

                var order = db.Orders.FirstOrDefault(o => o.ID_Order == _orderId);
                if (order != null)
                {
                    // Select the correct client in the ComboBox
                    cmbClient.SelectedItem = db.Clients.FirstOrDefault(c => c.ID_Client == order.ID_Client);

                    dpOrderDate.SelectedDate = order.OrderDate;
                    dpDeadline.SelectedDate = order.Deadline;

                    // Set the status in the ComboBox
                    foreach (ComboBoxItem item in cmbStatus.Items)
                    {
                        if (item.Content.ToString() == order.Status)
                        {
                            cmbStatus.SelectedItem = item;
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Заказ не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    // Optionally navigate back if order not found
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных заказа: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var orderToUpdate = db.Orders.FirstOrDefault(o => o.ID_Order == _orderId);
                if (orderToUpdate != null)
                {
                    // Validation
                    if (cmbClient.SelectedItem == null ||
                        !dpOrderDate.SelectedDate.HasValue ||
                        !dpDeadline.SelectedDate.HasValue ||
                        cmbStatus.SelectedItem == null)
                    {
                        MessageBox.Show("Пожалуйста, заполните все обязательные поля!", "Ошибка", 
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    
                    // Additional date validation (deadline after order date)
                    if (dpDeadline.SelectedDate.Value < dpOrderDate.SelectedDate.Value)
                    {
                        MessageBox.Show("Срок выполнения не может быть раньше даты заказа!", "Ошибка", 
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Update order properties
                    orderToUpdate.ID_Client = ((Clients)cmbClient.SelectedItem).ID_Client;
                    orderToUpdate.OrderDate = dpOrderDate.SelectedDate.Value;
                    orderToUpdate.Deadline = dpDeadline.SelectedDate.Value;
                    orderToUpdate.Status = ((ComboBoxItem)cmbStatus.SelectedItem).Content.ToString();

                    db.SaveChanges();

                    MessageBox.Show("Данные заказа успешно обновлены!", "Успех", 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    // Return to the previous page (sprav page)
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Ошибка при сохранении: Заказ не найден!", "Ошибка", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных заказа: {ex.Message}", "Ошибка", 
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