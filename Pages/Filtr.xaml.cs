using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PPPP.Models;

namespace PPPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для Filtr.xaml
    /// </summary>
    public partial class Filtr : Page
    {
        private D1E db;
        private IQueryable<Equipment> equipmentQuery;
        private IQueryable<Orders> ordersQuery;
        private IQueryable<ServiceRequests> serviceRequestsQuery;
        private IQueryable<Complaints> complaintsQuery;

        public Filtr()
        {
            InitializeComponent();
            db = new D1E();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Загрузка данных для фильтрации
                equipmentQuery = db.Equipment;
                ordersQuery = db.Orders;
                serviceRequestsQuery = db.ServiceRequests;
                complaintsQuery = db.Complaints;

                // Инициализация DataGrid'ов
                dgEquipment.ItemsSource = equipmentQuery.ToList();
                dgOrders.ItemsSource = ordersQuery.ToList();
                dgServiceRequests.ItemsSource = serviceRequestsQuery.ToList();
                dgComplaints.ItemsSource = complaintsQuery.ToList();

                // Установка начальных значений в ComboBox'ы
                cmbOrderStatus.SelectedIndex = 0;
                cmbServiceType.SelectedIndex = 0;
                cmbComplaintStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void txtEquipmentFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var filterText = txtEquipmentFilter.Text.ToLower();
                var filteredEquipment = equipmentQuery
                    .Where(eq => eq.Description.ToLower().Contains(filterText))
                    .ToList();
                dgEquipment.ItemsSource = filteredEquipment;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при фильтрации оборудования: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbOrderStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedStatus = (cmbOrderStatus.SelectedItem as ComboBoxItem)?.Content.ToString();
                var filteredOrders = ordersQuery;

                if (selectedStatus != "Все")
                {
                    filteredOrders = filteredOrders.Where(o => o.Status == selectedStatus);
                }

                dgOrders.ItemsSource = filteredOrders.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при фильтрации заказов: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbServiceType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedType = (cmbServiceType.SelectedItem as ComboBoxItem)?.Content.ToString();
                var filteredRequests = serviceRequestsQuery;

                if (selectedType != "Все")
                {
                    filteredRequests = filteredRequests.Where(sr => sr.ServiceType == selectedType);
                }

                dgServiceRequests.ItemsSource = filteredRequests.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при фильтрации заявок: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbComplaintStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedStatus = (cmbComplaintStatus.SelectedItem as ComboBoxItem)?.Content.ToString();
                var filteredComplaints = complaintsQuery;

                if (selectedStatus != "Все")
                {
                    filteredComplaints = filteredComplaints.Where(c => c.Status == selectedStatus);
                }

                dgComplaints.ItemsSource = filteredComplaints.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при фильтрации жалоб: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
