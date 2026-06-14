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
    public partial class ValidationForm : Form
    {
        private HttpClient _httpClient;
        public ValidationForm()
        {
            InitializeComponent();
        }

        private void ValidationForm_Load(object sender, EventArgs e)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:4444/TransferSimulator/");
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
                lblResult.Text = "ФИО не содержит запрещенные символы";
        }
    }
}
