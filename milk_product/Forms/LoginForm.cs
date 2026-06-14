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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private string connStr = "server=localhost;user=root;password=Americantanya89);database=milk_product";
        private void LoginButton_Click(object sender, EventArgs e)
        {
            //проверка на ввод с пустыми полями
            if (LoginBox.Text == "" || PasswordBox.Text == "")
            {
                MessageBox.Show("Заполните все поля!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (MySqlConnection conn = new MySqlConnection(connStr)) //Создаём соединение с БД
            {
                conn.Open();//Открываем соединение
                MySqlCommand cmd = new MySqlCommand(
                    "SELECT Password, Captcha_attempts, Role FROM users WHERE Login=@login", conn); //Создаём SQL запрос
                cmd.Parameters.AddWithValue("@login", LoginBox.Text); //Подставляем реальное значение вместо @login
                MySqlDataReader reader = cmd.ExecuteReader();//результат
                if (reader.Read()) //если пользователь найден
                {
                    int attempts = reader.GetInt32("Captcha_attempts"); //читаем количество попыток
                    string role = reader.GetString("Role"); //читаем роль

                    // блокировка если попыток >= 3
                    if (attempts >= 3)
                    {
                        MessageBox.Show("Вы заблокированы. Обратитесь к администратору",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (reader.GetString("Password") == PasswordBox.Text)
                    {
                        reader.Close();
                        if (role == "user")
                        {
                            using (CaptchaForm captcha = new CaptchaForm(LoginBox.Text, attempts))
                            {
                                DialogResult result = captcha.ShowDialog();
                                if (result == DialogResult.Abort)
                                {
                                    MessageBox.Show("Вы заблокированы. Обратитесь к администратору",
                                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                else if (result == DialogResult.OK)
                                {
                                    MessageBox.Show("Вы успешно авторизовались!", "Успех",
             MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    mainwindow main = new mainwindow();
                                    main.Role = role;
                                    main.Show();
                                }
                            }
                      
                        }
                        else if (role == "admin")
                        {
                            MessageBox.Show("Вы успешно авторизовались!", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mainwindow main = new mainwindow();
                            main.Role = role;
                            main.Show();
                        }
                    }
                    else
                    {
                        reader.Close();
                        MySqlCommand updateCmd = new MySqlCommand(
                        // если пароль неверный — увеличиваем счётчик на 1 
                        "UPDATE users SET Captcha_attempts = Captcha_attempts + 1 WHERE Login=@login", conn);
                        updateCmd.Parameters.AddWithValue("@login", LoginBox.Text);
                        updateCmd.ExecuteNonQuery();
                        MessageBox.Show("Вы ввели неверный логин или пароль. Пожалуйста проверьте ещё раз введенные данные",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Пользователя не существует!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
