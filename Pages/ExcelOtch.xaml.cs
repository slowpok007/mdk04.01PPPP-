using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PPPP.Models;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace PPPP.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExcelOtch.xaml
    /// </summary>
    public partial class ExcelOtch : Page
    {
        private D1E db;

        public ExcelOtch()
        {
            InitializeComponent();
            db = new D1E();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void chkAllTables_Checked(object sender, RoutedEventArgs e)
        {
            chkEquipment.IsChecked = true;
            chkOrders.IsChecked = true;
            chkServiceRequests.IsChecked = true;
            chkComplaints.IsChecked = true;
        }

        private void chkAllTables_Unchecked(object sender, RoutedEventArgs e)
        {
            chkEquipment.IsChecked = false;
            chkOrders.IsChecked = false;
            chkServiceRequests.IsChecked = false;
            chkComplaints.IsChecked = false;
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (!chkEquipment.IsChecked.Value && !chkOrders.IsChecked.Value && 
                !chkServiceRequests.IsChecked.Value && !chkComplaints.IsChecked.Value)
            {
                MessageBox.Show("Выберите хотя бы одну таблицу для экспорта!", "Предупреждение", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Excel.Application app = new Excel.Application
                {
                    Visible = true,
                    SheetsInNewWorkbook = 1
                };

                Excel.Workbook workBook = app.Workbooks.Add(Type.Missing);
                app.DisplayAlerts = false;

                int sheetIndex = 1;

                if (chkEquipment.IsChecked.Value)
                {
                    ExportEquipment(app, workBook, ref sheetIndex);
                }

                if (chkOrders.IsChecked.Value)
                {
                    ExportOrders(app, workBook, ref sheetIndex);
                }

                if (chkServiceRequests.IsChecked.Value)
                {
                    ExportServiceRequests(app, workBook, ref sheetIndex);
                }

                if (chkComplaints.IsChecked.Value)
                {
                    ExportComplaints(app, workBook, ref sheetIndex);
                }

                MessageBox.Show("Экспорт в Excel успешно завершен!", "Успех", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте в Excel: {ex.Message}", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportEquipment(Excel.Application app, Excel.Workbook workBook, ref int sheetIndex)
        {
            Excel.Worksheet sheet = (Excel.Worksheet)workBook.Worksheets.Add(Type.Missing, workBook.Sheets[sheetIndex]);
            sheet.Name = "Оборудование";

            // Заголовок
            Excel.Range header = sheet.Cells[1, 1].Resize[1, 5];
            header.Merge();
            header.Value = "Отчет по оборудованию";
            header.Font.Bold = true;
            header.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Заголовки столбцов
            sheet.Cells[2, 1].Value = "ID";
            sheet.Cells[2, 2].Value = "Название";
            sheet.Cells[2, 3].Value = "Описание";
            sheet.Cells[2, 4].Value = "Количество";
            sheet.Cells[2, 5].Value = "Цена";

            // Данные
            var equipment = db.Equipment.ToList();
            int row = 3;
            foreach (var item in equipment)
            {
                sheet.Cells[row, 1].Value = item.ID_Equipment;
                sheet.Cells[row, 2].Value = item.EquipmentName;
                sheet.Cells[row, 3].Value = item.Description;
                sheet.Cells[row, 4].Value = item.Quantity;
                sheet.Cells[row, 5].Value = item.Price;
                row++;
            }

            sheet.Columns.AutoFit();
            sheetIndex++;
        }

        private void ExportOrders(Excel.Application app, Excel.Workbook workBook, ref int sheetIndex)
        {
            Excel.Worksheet sheet = (Excel.Worksheet)workBook.Worksheets.Add(Type.Missing, workBook.Sheets[sheetIndex]);
            sheet.Name = "Заказы";

            // Заголовок
            Excel.Range header = sheet.Cells[1, 1].Resize[1, 5];
            header.Merge();
            header.Value = "Отчет по заказам";
            header.Font.Bold = true;
            header.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Заголовки столбцов
            sheet.Cells[2, 1].Value = "ID";
            sheet.Cells[2, 2].Value = "ID Клиента";
            sheet.Cells[2, 3].Value = "Дата заказа";
            sheet.Cells[2, 4].Value = "Срок";
            sheet.Cells[2, 5].Value = "Статус";

            // Данные
            var orders = db.Orders.ToList();
            int row = 3;
            foreach (var item in orders)
            {
                sheet.Cells[row, 1].Value = item.ID_Order;
                sheet.Cells[row, 2].Value = item.ID_Client;
                sheet.Cells[row, 3].Value = item.OrderDate;
                sheet.Cells[row, 4].Value = item.Deadline;
                sheet.Cells[row, 5].Value = item.Status;
                row++;
            }

            sheet.Columns.AutoFit();
            sheetIndex++;
        }

        private void ExportServiceRequests(Excel.Application app, Excel.Workbook workBook, ref int sheetIndex)
        {
            Excel.Worksheet sheet = (Excel.Worksheet)workBook.Worksheets.Add(Type.Missing, workBook.Sheets[sheetIndex]);
            sheet.Name = "Заявки на обслуживание";

            // Заголовок
            Excel.Range header = sheet.Cells[1, 1].Resize[1, 5];
            header.Merge();
            header.Value = "Отчет по заявкам на обслуживание";
            header.Font.Bold = true;
            header.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Заголовки столбцов
            sheet.Cells[2, 1].Value = "ID";
            sheet.Cells[2, 2].Value = "ID Оборудования";
            sheet.Cells[2, 3].Value = "Тип услуги";
            sheet.Cells[2, 4].Value = "Дата вызова";
            sheet.Cells[2, 5].Value = "Исполнитель";

            // Данные
            var requests = db.ServiceRequests.ToList();
            int row = 3;
            foreach (var item in requests)
            {
                sheet.Cells[row, 1].Value = item.ID_ServiceRequest;
                sheet.Cells[row, 2].Value = item.ID_Equipment;
                sheet.Cells[row, 3].Value = item.ServiceType;
                sheet.Cells[row, 4].Value = item.CallDate;
                sheet.Cells[row, 5].Value = item.Executor;
                row++;
            }

            sheet.Columns.AutoFit();
            sheetIndex++;
        }

        private void ExportComplaints(Excel.Application app, Excel.Workbook workBook, ref int sheetIndex)
        {
            Excel.Worksheet sheet = (Excel.Worksheet)workBook.Worksheets.Add(Type.Missing, workBook.Sheets[sheetIndex]);
            sheet.Name = "Жалобы";

            // Заголовок
            Excel.Range header = sheet.Cells[1, 1].Resize[1, 5];
            header.Merge();
            header.Value = "Отчет по жалобам";
            header.Font.Bold = true;
            header.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Заголовки столбцов
            sheet.Cells[2, 1].Value = "ID";
            sheet.Cells[2, 2].Value = "ID Заказа";
            sheet.Cells[2, 3].Value = "Описание проблемы";
            sheet.Cells[2, 4].Value = "Дата регистрации";
            sheet.Cells[2, 5].Value = "Статус";

            // Данные
            var complaints = db.Complaints.ToList();
            int row = 3;
            foreach (var item in complaints)
            {
                sheet.Cells[row, 1].Value = item.ID_Complaint;
                sheet.Cells[row, 2].Value = item.ID_Order;
                sheet.Cells[row, 3].Value = item.IssueDescription;
                sheet.Cells[row, 4].Value = item.RegistrationDate;
                sheet.Cells[row, 5].Value = item.Status;
                row++;
            }

            sheet.Columns.AutoFit();
            sheetIndex++;
        }
    }
}
