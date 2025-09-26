using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PPPP.Models;

namespace PPPP.Pages
{
    public partial class EditEquipment : Page
    {
        private D1E db;
        private int _equipmentId;

        public EditEquipment(int equipmentId)
        {
            InitializeComponent();
            db = new D1E();
            _equipmentId = equipmentId;
            LoadEquipmentData();
        }

        private void LoadEquipmentData()
        {
            try
            {
                var equipment = db.Equipment.FirstOrDefault(e => e.ID_Equipment == _equipmentId);
                if (equipment != null)
                {
                    txtEquipmentName.Text = equipment.EquipmentName;
                    txtDescription.Text = equipment.Description;
                    txtQuantity.Text = equipment.Quantity.ToString(); // Assuming Quantity is an integer
                    txtPrice.Text = equipment.Price.ToString(); // Assuming Price is a decimal
                }
                else
                {
                    MessageBox.Show("Оборудование не найдено!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    // Optionally navigate back if equipment not found
                    NavigationService.GoBack();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных оборудования: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var equipmentToUpdate = db.Equipment.FirstOrDefault(eq => eq.ID_Equipment == _equipmentId);
                if (equipmentToUpdate != null)
                {
                    // Validation
                    if (string.IsNullOrWhiteSpace(txtEquipmentName.Text) ||
                        string.IsNullOrWhiteSpace(txtDescription.Text) ||
                        string.IsNullOrWhiteSpace(txtQuantity.Text) ||
                        string.IsNullOrWhiteSpace(txtPrice.Text))
                    {
                        MessageBox.Show("Пожалуйста, заполните все обязательные поля!", "Ошибка", 
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Parse Quantity and Price (add more robust parsing and error handling if needed)
                    if (!int.TryParse(txtQuantity.Text.Trim(), out int quantity))
                    {
                        MessageBox.Show("Некорректное значение количества!", "Ошибка", 
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price))
                    {
                        MessageBox.Show("Некорректное значение цены!", "Ошибка", 
                            MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Update equipment properties
                    equipmentToUpdate.EquipmentName = txtEquipmentName.Text.Trim();
                    equipmentToUpdate.Description = txtDescription.Text.Trim();
                    equipmentToUpdate.Quantity = quantity;
                    equipmentToUpdate.Price = price;

                    db.SaveChanges();

                    MessageBox.Show("Данные оборудования успешно обновлены!", "Успех", 
                        MessageBoxButton.OK, MessageBoxImage.Information);

                    // Return to the previous page (sprav page)
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Ошибка при сохранении: Оборудование не найдено!", "Ошибка", 
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных оборудования: {ex.Message}", "Ошибка", 
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