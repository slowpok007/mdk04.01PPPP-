using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PPPP.Models;

namespace PPPP.Pages
{
    public partial class EditServiceRequest : Page
    {
        private D1E db;
        private int _serviceRequestId;

        public EditServiceRequest(int serviceRequestId)
        {
            InitializeComponent();
            db = new D1E();
            _serviceRequestId = serviceRequestId;
            LoadServiceRequestData();
        }

        private void LoadServiceRequestData()
        {
            try
            {
                // Load equipment for the ComboBox
                cmbEquipment.ItemsSource = db.Equipment.ToList();

                var serviceRequest = db.ServiceRequests.FirstOrDefault(sr => sr.ID_ServiceRequest == _serviceRequestId);
                if (serviceRequest != null)
                {
                    // Select the correct equipment in the ComboBox
                    cmbEquipment.SelectedItem = db.Equipment.FirstOrDefault(e => e.ID_Equipment == serviceRequest.ID_Equipment);

                    // Set the service type in the ComboBox
                     foreach (ComboBoxItem item in cmbServiceType.Items)
                    {
                        if (item.Content.ToString() == serviceRequest.ServiceType)
                        {
                            cmbServiceType.SelectedItem = item;
                            break;
                        }
                    }

                    dpCallDate.SelectedDate = serviceRequest.CallDate;
                    txtExecutor.Text = serviceRequest.Executor;
                }
                else
                {
                    MessageBox.Show("Заявка на обслуживание не найдена!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    // Optionally navigate back if service request not found
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных заявки на обслуживание: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var serviceRequestToUpdate = db.ServiceRequests.FirstOrDefault(sr => sr.ID_ServiceRequest == _serviceRequestId);
                if (serviceRequestToUpdate != null)
                {
                    // Validation
                    if (cmbEquipment.SelectedItem == null ||
                        cmbServiceType.SelectedItem == null ||
                        !dpCallDate.SelectedDate.HasValue ||
                        string.IsNullOrWhiteSpace(txtExecutor.Text))
                    {
                        MessageBox.Show("Пожалуйста, заполните все обязательные поля!", "Ошибка", 
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Update service request properties
                    serviceRequestToUpdate.ID_Equipment = ((Equipment)cmbEquipment.SelectedItem).ID_Equipment;
                    serviceRequestToUpdate.ServiceType = ((ComboBoxItem)cmbServiceType.SelectedItem).Content.ToString();
                    serviceRequestToUpdate.CallDate = dpCallDate.SelectedDate.Value;
                    serviceRequestToUpdate.Executor = txtExecutor.Text.Trim();

                    db.SaveChanges();

                    MessageBox.Show("Данные заявки на обслуживание успешно обновлены!", "Успех", 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    // Return to the previous page (sprav page)
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Ошибка при сохранении: Заявка на обслуживание не найдена!", "Ошибка", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных заявки на обслуживание: {ex.Message}", "Ошибка", 
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