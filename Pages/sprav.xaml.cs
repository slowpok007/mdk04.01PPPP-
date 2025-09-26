using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PPPP.Models;

namespace PPPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для sprav.xaml
    /// </summary>
    public partial class sprav : Page
    {
        private D1E db;

        public sprav()
        {
            InitializeComponent();
            db = new D1E();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Загрузка данных клиентов
                dgClients.ItemsSource = db.Clients.ToList();

                // Загрузка данных оборудования
                dgEquipment.ItemsSource = db.Equipment.ToList();

                // Загрузка данных заказов
                dgOrders.ItemsSource = db.Orders.ToList();

                // Загрузка данных заявок на обслуживание
                dgServiceRequests.ItemsSource = db.ServiceRequests.ToList();

                // Загрузка данных жалоб
                dgComplaints.ItemsSource = db.Complaints.ToList();
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData(); // Refresh data every time the page is loaded
        }

        #region Клиенты
        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddClient());
        }

        private void btnEditClient_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var client = button.DataContext as Clients;
            if (client != null)
            {
                // Переход на страницу редактирования клиента, передавая ID клиента
                Nav.MainFrame.Navigate(new EditClient(client.ID_Client));
            }
        }

        private void btnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
             var button = sender as Button;
            var client = button.DataContext as Clients;
            if (client != null)
            {
                if (MessageBox.Show($"Вы уверены, что хотите удалить клиента {client.CompanyName} ?", "Подтверждение удаления",
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Clients.Remove(client);
                        db.SaveChanges();
                        MessageBox.Show("Клиент успешно удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData(); // Refresh data after deletion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении клиента: {ex.Message}", "Ошибка", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        #endregion

        #region Оборудование
        private void btnAddEquipment_Click(object sender, RoutedEventArgs e)
        {
            
                Nav.MainFrame.Navigate(new AddEquipment());
            
        }

        private void btnEditEquipment_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var equipment = button.DataContext as Equipment;
            if (equipment != null)
            {
                // Переход на страницу редактирования оборудования, передавая ID оборудования
                Nav.MainFrame.Navigate(new EditEquipment(equipment.ID_Equipment));
            }
        }

        private void btnDeleteEquipment_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var equipment = button.DataContext as Equipment;
            if (equipment != null)
            {
                if (MessageBox.Show($"Вы уверены, что хотите удалить оборудование \"{equipment.EquipmentName}\" ?", "Подтверждение удаления",
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Equipment.Remove(equipment);
                        db.SaveChanges();
                        MessageBox.Show("Оборудование успешно удалено!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData(); // Refresh data after deletion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении оборудования: {ex.Message}", "Ошибка", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        #endregion

        #region Заказы
        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddOrder());
        }

        private void btnEditOrder_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var order = button.DataContext as Orders;
            if (order != null)
            {
                // Переход на страницу редактирования заказа, передавая ID заказа
                Nav.MainFrame.Navigate(new EditOrder(order.ID_Order));
            }
        }

        private void btnDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
             var button = sender as Button;
            var order = button.DataContext as Orders;
            if (order != null)
            {
                if (MessageBox.Show($"Вы уверены, что хотите удалить заказ № {order.ID_Order} ?", "Подтверждение удаления",
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Orders.Remove(order);
                        db.SaveChanges();
                        MessageBox.Show("Заказ успешно удален!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData(); // Refresh data after deletion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении заказа: {ex.Message}", "Ошибка", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        #endregion

        #region Заявки на обслуживание
        private void btnAddServiceRequest_Click(object sender, RoutedEventArgs e)
        {
           
            Nav.MainFrame.Navigate(new AddServiceRequest());
        }

        private void btnEditServiceRequest_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var request = button.DataContext as ServiceRequests;
            if (request != null)
            {
                // Переход на страницу редактирования заявки, передавая ID заявки
                Nav.MainFrame.Navigate(new EditServiceRequest(request.ID_ServiceRequest));
            }
        }

        private void btnDeleteServiceRequest_Click(object sender, RoutedEventArgs e)
        {
             var button = sender as Button;
            var request = button.DataContext as ServiceRequests;
            if (request != null)
            {
                if (MessageBox.Show($"Вы уверены, что хотите удалить заявку № {request.ID_ServiceRequest} ?", "Подтверждение удаления",
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.ServiceRequests.Remove(request);
                        db.SaveChanges();
                        MessageBox.Show("Заявка на обслуживание успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData(); // Refresh data after deletion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении заявки на обслуживание: {ex.Message}", "Ошибка", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        #endregion

        #region Жалобы
        private void btnAddComplaint_Click(object sender, RoutedEventArgs e)
        {
            Nav.MainFrame.Navigate(new AddComplaint());
        }

        private void btnEditComplaint_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var complaint = button.DataContext as Complaints;
            if (complaint != null)
            {
                // Переход на страницу редактирования жалобы, передавая ID жалобы
                Nav.MainFrame.Navigate(new EditComplaint(complaint.ID_Complaint));
            }
        }

        private void btnDeleteComplaint_Click(object sender, RoutedEventArgs e)
        {
             var button = sender as Button;
            var complaint = button.DataContext as Complaints;
            if (complaint != null)
            {
                if (MessageBox.Show($"Вы уверены, что хотите удалить жалобу № {complaint.ID_Complaint} ?", "Подтверждение удаления",
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        db.Complaints.Remove(complaint);
                        db.SaveChanges();
                        MessageBox.Show("Жалоба успешно удалена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData(); // Refresh data after deletion
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении жалобы: {ex.Message}", "Ошибка", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        #endregion
    }
}
