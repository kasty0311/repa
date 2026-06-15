using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace milk_product.Forms
{
    public partial class mainwindow : Form
    {
        // свойство для хранения роли пользователя
        public string Role { get; set; }

        // строка подключения к БД
        string connStr = "server=localhost;user=root;password=Americantanya89);database=milk_product";

        private HttpClient _httpClient;
        public mainwindow()
        {
            InitializeComponent();
        }

        private void mainwindow_Load(object sender, EventArgs e)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:4444/TransferSimulator/");
            // если не админ — скрываем вкладку управления пользователями
            if (Role != "admin")
                tabControl1.TabPages.Remove(tabPage1);

            // загружаем данные из БД в каждую таблицу
            LoadData("SELECT * FROM users", dgvUsers);
            LoadData("SELECT * FROM product", dgvProducts);
            LoadData("SELECT * FROM materials", dgvMaterials);
            LoadData("SELECT * FROM specification", dgvSpecification);
            LoadData("SELECT * FROM orders", dgvOrders);
            LoadData("SELECT * FROM customers", dgvCustomers);
            if (Role != "admin")
            {
                dgvProducts.ReadOnly = true;
                dgvMaterials.ReadOnly = true;
                dgvSpecification.ReadOnly = true;
                dgvOrders.ReadOnly = true;
                dgvCustomers.ReadOnly = true;
                btnSave.Visible = false;
                btnDelete.Visible = false;
            }
        }

        // метод загрузки данных в DataGridView
        private void LoadData(string command, DataGridView grid)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(command, conn);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                grid.DataSource = dt;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // получаем активную вкладку и берём из неё DataGridView
                DataGridView grid = tabControl1.SelectedTab.Controls
                    .OfType<DataGridView>().FirstOrDefault();

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string tableName = "";
                    string tabText = tabControl1.SelectedTab.Text;

                    if (tabText == "Управление пользователями") tableName = "users";
                    else if (tabText == "Продукты") tableName = "product";
                    else if (tabText == "Материалы") tableName = "materials";
                    else if (tabText == "Спецификации") tableName = "specification";
                    else if (tabText == "Заказы") tableName = "orders";
                    else if (tabText == "Поставщики") tableName = "customers";

                    MySqlDataAdapter adapter = new MySqlDataAdapter($"SELECT * FROM {tableName}", conn);
                    MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                    DataTable dt = grid.DataSource as DataTable;
                    adapter.Update(dt);
                    dt.AcceptChanges();
                    MessageBox.Show("Данные сохранены!", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                    MessageBox.Show("Пользователь уже существует!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show(ex.Message, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // получаем активный DataGridView
                DataGridView grid = tabControl1.SelectedTab.Controls
                    .OfType<DataGridView>().FirstOrDefault();

                // удаляем выделенные строки
                foreach (DataGridViewRow row in grid.SelectedRows)
                {
                    grid.Rows.Remove(row);
                }
                MessageBox.Show("Строки удалены! Нажмите Сохранить для применения.",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка удаления!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrdersForm ordersForm = new OrdersForm();
            ordersForm.ShowDialog();
        }

        private async void btnGetData_Click(object sender, EventArgs e)
        {
            var response = await _httpClient.GetAsync("fullName");

            string json = await response.Content.ReadAsStringAsync();

            JObject obj = JObject.Parse(json);

            lblFIO.Text = obj["value"].ToString();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            string fio = lblFIO.Text;

            if (Regex.IsMatch(fio, @"[^а-яА-ЯёЁ \-]"))
                lblResult.Text = "ФИО содержит запрещенные символы";

            else if (string.IsNullOrEmpty(fio))
                lblResult.Text = "ФИО не может быть пустым";

            else
                lblResult.Text = "ФИО корректно";
        }
    }
    
}
