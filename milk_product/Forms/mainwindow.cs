using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace milk_product.Forms
{
    public partial class mainwindow : Form
    {
        // свойство для хранения роли пользователя
        public string Role { get; set; }

        // строка подключения к БД
        string connStr = "server=localhost;user=root;password=Americantanya89);database=milk_product";
        public mainwindow()
        {
            InitializeComponent();
        }

        private void mainwindow_Load(object sender, EventArgs e)
        {
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

        private void tabControl1_Click(object sender, EventArgs e)
        {
            ValidationForm validationForm = new ValidationForm();
            validationForm.ShowDialog();
        }
    }
    
}
