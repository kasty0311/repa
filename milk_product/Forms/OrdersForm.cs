using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace milk_product.Forms
{
    public partial class OrdersForm : Form
    {
        string connStr = "server=localhost;user=root;password=Americantanya89);database=milk_product";

        public OrdersForm()
        {
            InitializeComponent();
        }

        private void OrdersForm_Load(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                string query = "SELECT c.name AS Покупатель, " +
    "SUM(ohp.quantity * m.quantity * m.price) AS Стоимость_заказа " +
    "FROM orders o " +
    "JOIN customers c ON o.Customers_id = c.id " +
    "JOIN orders_has_product ohp ON o.Id = ohp.Orders_Id " +
    "JOIN specification s ON ohp.Product_Id = s.Product_Id " +
    "JOIN materials m ON s.Materials_Id = m.Id " +
    "GROUP BY o.Id, c.name;";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvOrders.DataSource = dt;
            }
        }
    }
}