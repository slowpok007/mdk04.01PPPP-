using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PPPP.Models;

namespace PPPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Page
    {
        private D1E db;

        public AddOrder()
        {
            InitializeComponent();
            db = new D1E();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Загрузка списка клиентов
                cmbClient.ItemsSource = db.Clients.ToList();
                cmbClient.SelectedIndex = 0;

                // Установка текущей даты как даты заказа
                dpOrderDate.SelectedDate = DateTime.Now;

                // Установка срока выполнения (текущая дата + 7 дней)
                dpDeadline.SelectedDate = DateTime.Now.AddDays(7);

                // Установка начального статуса
                cmbStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполнения полей
                if (cmbClient.SelectedItem == null ||
                    !dpOrderDate.SelectedDate.HasValue ||
                    !dpDeadline.SelectedDate.HasValue ||
                    cmbStatus.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Проверка дат
                if (dpDeadline.SelectedDate.Value < dpOrderDate.SelectedDate.Value)
                {
                    MessageBox.Show("Срок выполнения не может быть раньше даты заказа!", "Ошибка", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Создание нового заказа
                var newOrder = new Orders
                {
                    ID_Client = ((Clients)cmbClient.SelectedItem).ID_Client,
                    OrderDate = dpOrderDate.SelectedDate.Value,
                    Deadline = dpDeadline.SelectedDate.Value,
                    Status = ((ComboBoxItem)cmbStatus.SelectedItem).Content.ToString()
                };

                // Добавление в базу данных
                db.Orders.Add(newOrder);
                db.SaveChanges();

                MessageBox.Show("Заказ успешно добавлен!", "Успех", 
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Возврат на предыдущую страницу
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении заказа: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void cmbClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
