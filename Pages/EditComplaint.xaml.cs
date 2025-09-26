using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PPPP.Models;

namespace PPPP.Pages
{
    public partial class EditComplaint : Page
    {
        private D1E db;
        private int _complaintId;

        public EditComplaint(int complaintId)
        {
            InitializeComponent();
            db = new D1E();
            _complaintId = complaintId;
            LoadComplaintData();
        }

        private void LoadComplaintData()
        {
            try
            {
                // Load orders for the ComboBox
                cmbOrder.ItemsSource = db.Orders.ToList();

                var complaint = db.Complaints.FirstOrDefault(c => c.ID_Complaint == _complaintId);
                if (complaint != null)
                {
                    // Select the correct order in the ComboBox
                    cmbOrder.SelectedItem = db.Orders.FirstOrDefault(o => o.ID_Order == complaint.ID_Order);

                    dpRegistrationDate.SelectedDate = complaint.RegistrationDate;
                    txtIssueDescription.Text = complaint.IssueDescription;

                    // Set the status in the ComboBox
                     foreach (ComboBoxItem item in cmbStatus.Items)
                    {
                        if (item.Content.ToString() == complaint.Status)
                        {
                            cmbStatus.SelectedItem = item;
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Жалоба не найдена!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    // Optionally navigate back if complaint not found
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных жалобы: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var complaintToUpdate = db.Complaints.FirstOrDefault(c => c.ID_Complaint == _complaintId);
                if (complaintToUpdate != null)
                {
                    // Validation
                    if (cmbOrder.SelectedItem == null ||
                        !dpRegistrationDate.SelectedDate.HasValue ||
                        string.IsNullOrWhiteSpace(txtIssueDescription.Text) ||
                        cmbStatus.SelectedItem == null)
                    {
                        MessageBox.Show("Пожалуйста, заполните все обязательные поля!", "Ошибка", 
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Update complaint properties
                    complaintToUpdate.ID_Order = ((Orders)cmbOrder.SelectedItem).ID_Order;
                    complaintToUpdate.RegistrationDate = dpRegistrationDate.SelectedDate.Value;
                    complaintToUpdate.IssueDescription = txtIssueDescription.Text.Trim();
                    complaintToUpdate.Status = ((ComboBoxItem)cmbStatus.SelectedItem).Content.ToString();

                    db.SaveChanges();

                    MessageBox.Show("Данные жалобы успешно обновлены!", "Успех", 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    // Return to the previous page (sprav page)
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Ошибка при сохранении: Жалоба не найдена!", "Ошибка", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных жалобы: {ex.Message}", "Ошибка", 
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