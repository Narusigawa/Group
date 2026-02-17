using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cond
{
    public partial class AuthForm : Form
    {
        private TextBox txtLogin;
        private TextBox txtPassword;
        private Button btnLogin;

        public AuthForm()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Вход";
            this.Size = new Size(300, 200);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = ThemeColors.Background;

            TableLayoutPanel table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(20)
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

            // Логин
            table.Controls.Add(new Label { Text = "Логин:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 0);
            txtLogin = new TextBox { Dock = DockStyle.Fill };
            table.Controls.Add(txtLogin, 1, 0);

            // Пароль
            table.Controls.Add(new Label { Text = "Пароль:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 1);
            txtPassword = new TextBox { Dock = DockStyle.Fill, UseSystemPasswordChar = true };
            table.Controls.Add(txtPassword, 1, 1);

            // Кнопка
            btnLogin = new Button
            {
                Text = "Войти",
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeColors.Accent,
                ForeColor = ThemeColors.Text,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnLogin.Click += (s, e) =>
            {
                // Пока проверка: если поля не пустые, считаем успехом
                if (!string.IsNullOrWhiteSpace(txtLogin.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
                    this.DialogResult = DialogResult.OK;
                else
                    MessageBox.Show("Введите логин и пароль", "Ошибка");
            };
            table.Controls.Add(btnLogin, 0, 2);
            table.SetColumnSpan(btnLogin, 2);

            this.Controls.Add(table);
        }
    }
}
