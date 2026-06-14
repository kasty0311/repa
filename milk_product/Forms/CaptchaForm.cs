using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace milk_product.Forms
{
    public partial class CaptchaForm : Form
    {
        private int _attempts;
        private string _login;
        private readonly List<PictureBox> _boxes = new List<PictureBox>();
        private PictureBox _selected;
        private string _basePath;
        private string connStr = "server=localhost;user=root;password=Americantanya89);database=milk_product";

        private class Piece
        {
            public Image Image { get; set; }
            public string Id { get; set; }
        }

        private List<Piece> _pieces;

        public CaptchaForm(string login, int attempts)
        {
            InitializeComponent();
            _login = login;
            _attempts = attempts;

            _boxes.Add(pb1);
            _boxes.Add(pb2);
            _boxes.Add(pb3);
            _boxes.Add(pb4);

            foreach (var pb in _boxes)
            {
                pb.Click += Piece_Click;
            }

            _basePath = Path.Combine(Application.StartupPath, "Resources");
            LoadImages();
            Shuffle();
        }

        private void LoadImages()
        {
            _pieces = new List<Piece>
            {
                new Piece { Image = Image.FromFile(Path.Combine(_basePath, "1.png")), Id = "1" },
                new Piece { Image = Image.FromFile(Path.Combine(_basePath, "2.png")), Id = "2" },
                new Piece { Image = Image.FromFile(Path.Combine(_basePath, "3.png")), Id = "3" },
                new Piece { Image = Image.FromFile(Path.Combine(_basePath, "4.png")), Id = "4" }
            };
        }

        private void Shuffle()
        {
            Random rnd = new Random();
            var shuffled = _pieces.OrderBy(_ => rnd.Next()).ToList();
            for (int i = 0; i < _boxes.Count; i++)
            {
                _boxes[i].Image = shuffled[i].Image;
                _boxes[i].Tag = shuffled[i].Id;
            }
        }

        private void Piece_Click(object sender, EventArgs e)
        {
            var pb = (PictureBox)sender;

            if (_selected == null)
            {
                _selected = pb;
                pb.BorderStyle = BorderStyle.Fixed3D;
                return;
            }

            if (pb == _selected)
            {
                pb.BorderStyle = BorderStyle.FixedSingle;
                _selected = null;
                return;
            }

            // меняем местами картинку и тег
            var img = pb.Image;
            var tag = pb.Tag;

            pb.Image = _selected.Image;
            pb.Tag = _selected.Tag;

            _selected.Image = img;
            _selected.Tag = tag;

            _selected.BorderStyle = BorderStyle.None;
            _selected = null;
            pb.BorderStyle = BorderStyle.None;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            bool correct = true;
            for (int i = 0; i < _boxes.Count; i++)
            {
                if (_boxes[i].Tag.ToString() != (i + 1).ToString())
                {
                    correct = false;
                    break;
                }
            }

            if (!correct)
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(
                        "UPDATE users SET Captcha_attempts = Captcha_attempts + 1 WHERE Login=@login", conn);
                    cmd.Parameters.AddWithValue("@login", _login);
                    cmd.ExecuteNonQuery();
                }

                _attempts++;

                if (_attempts >= 3)
                {
                    DialogResult = DialogResult.Abort;
                    Close();
                    return;
                }

                MessageBox.Show($"Капча собран неверно. Осталось попыток: {3 - _attempts}",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
