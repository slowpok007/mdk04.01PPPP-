using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PPPP.Models;

namespace PPPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddServiceRequest.xaml
    /// </summary>
    public partial class AddServiceRequest : Page
    {
        private D1E db;

        public AddServiceRequest()
        {
            InitializeComponent();
            db = new D1E();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Загрузка списка оборудования
                cmbEquipment.ItemsSource = db.Equipment.ToList();
                cmbEquipment.SelectedIndex = 0;

                // Установка текущей даты как даты вызова
                dpCallDate.SelectedDate = DateTime.Now;

                // Установка начального типа услуги
                cmbServiceType.SelectedIndex = 0;
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
                if (cmbEquipment.SelectedItem == null ||
                    !dpCallDate.SelectedDate.HasValue ||
                    string.IsNullOrWhiteSpace(txtExecutor.Text) ||
                    cmbServiceType.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Создание новой заявки на обслуживание
                var newServiceRequest = new ServiceRequests
                {
                    ID_Equipment = ((Equipment)cmbEquipment.SelectedItem).ID_Equipment,
                    ServiceType = ((ComboBoxItem)cmbServiceType.SelectedItem).Content.ToString(),
                    CallDate = dpCallDate.SelectedDate.Value,
                    Executor = txtExecutor.Text.Trim()
                };

                // Добавление в базу данных
                db.ServiceRequests.Add(newServiceRequest);
                db.SaveChanges();

                MessageBox.Show("Заявка на обслуживание успешно добавлена!", "Успех", 
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Возврат на предыдущую страницу
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении заявки на обслуживание: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
