using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Drawing2D;


namespace cond
{
    public partial class RegisterForm : Form
    {
        private TextBox txtName;
        private TextBox txtEmail;
        private TextBox txtLogin;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private Button btnRegister;

        public RegisterForm()
        {
            SetupForm();
        }

        private void SetupForm()
        {
            // Настройки окна
            this.Text = "";
            this.Size = new Size(400, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = ThemeColors.Background;
            this.ShowInTaskbar = false;

            // Закруглённые углы
            this.Paint += (s, e) =>
            {
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, 20, 20, 180, 90);
                path.AddArc(this.Width - 20, 0, 20, 20, 270, 90);
                path.AddArc(this.Width - 20, this.Height - 20, 20, 20, 0, 90);
                path.AddArc(0, this.Height - 20, 20, 20, 90, 90);
                path.CloseFigure();
                this.Region = new Region(path);
            };

            // Основная панель
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(30)
            };

            // Заголовок
            Label lblTitle = new Label
            {
                Text = "Регистрация",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 50
            };

            // Подзаголовок
            Label lblSubtitle = new Label
            {
                Text = "Создайте аккаунт, чтобы получать бонусы",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 30
            };

            // Панель с полями
            Panel fieldsPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 320,
                Padding = new Padding(0, 10, 0, 0)
            };

            int yPos = 0;

            // Имя
            AddField(fieldsPanel, "Имя", ref txtName, ref yPos);
            // Email
            AddField(fieldsPanel, "Email", ref txtEmail, ref yPos);
            // Логин
            AddField(fieldsPanel, "Логин", ref txtLogin, ref yPos);
            // Пароль
            AddField(fieldsPanel, "Пароль", ref txtPassword, ref yPos, true);
            // Подтверждение пароля
            AddField(fieldsPanel, "Подтвердите пароль", ref txtConfirmPassword, ref yPos, true);

            // Кнопка регистрации
            btnRegister = new Button
            {
                Text = "Зарегистрироваться",
                Dock = DockStyle.Top,
                Height = 50,
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeColors.Accent,
                ForeColor = ThemeColors.Text,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 20, 0, 10)
            };
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Click += BtnRegister_Click;

            // Кнопка закрытия
            Button btnClose = new Button
            {
                Text = "×",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Size = new Size(30, 30),
                Location = new Point(this.Width - 35, 5),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = ThemeColors.Text,
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            // Собираем
            mainPanel.Controls.Add(btnRegister);
            mainPanel.Controls.Add(fieldsPanel);
            mainPanel.Controls.Add(lblSubtitle);
            mainPanel.Controls.Add(lblTitle);
            mainPanel.Controls.Add(btnClose);

            this.Controls.Add(mainPanel);
        }

        private void AddField(Panel panel, string labelText, ref TextBox textBox, ref int yPos, bool isPassword = false)
        {
            Label lbl = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = ThemeColors.Text,
                Location = new Point(0, yPos),
                Size = new Size(340, 20)
            };

            textBox = new TextBox
            {
                Location = new Point(0, yPos + 25),
                Size = new Size(340, 30),
                Font = new Font("Segoe UI", 12),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(250, 250, 250)
            };

            if (isPassword)
                textBox.UseSystemPasswordChar = true;

            panel.Controls.AddRange(new Control[] { lbl, textBox });
            yPos += 70;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // Простейшая валидация
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtLogin.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Заполните все поля", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Регистрация прошла успешно!\nТеперь вы можете войти",
                "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
