using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PPPP.Models;

namespace PPPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddComplaint.xaml
    /// </summary>
    public partial class AddComplaint : Page
    {
        private D1E db;

        public AddComplaint()
        {
            InitializeComponent();
            db = new D1E();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                // Загрузка списка заказов
                cmbEquipment.ItemsSource = db.Orders.ToList();
                cmbEquipment.SelectedIndex = 0;

                // Установка текущей даты как даты жалобы
                dpComplaintDate.SelectedDate = DateTime.Now;

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
                if (cmbEquipment.SelectedItem == null ||
                    !dpComplaintDate.SelectedDate.HasValue ||
                    string.IsNullOrWhiteSpace(txtDescription.Text) ||
                    cmbStatus.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", 
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Создание новой жалобы
                var newComplaint = new Complaints
                {
                    ID_Order = ((Orders)cmbEquipment.SelectedItem).ID_Order,
                    RegistrationDate = dpComplaintDate.SelectedDate.Value,
                    IssueDescription = txtDescription.Text.Trim(),
                    Status = ((ComboBoxItem)cmbStatus.SelectedItem).Content.ToString()
                };

                // Добавление в базу данных
                db.Complaints.Add(newComplaint);
                db.SaveChanges();

                MessageBox.Show("Жалоба успешно добавлена!", "Успех", 
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Возврат на предыдущую страницу
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении жалобы: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void cmbEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txtDescription_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
