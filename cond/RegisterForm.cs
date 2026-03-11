using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace cond
{
    public partial class RegisterForm : Form
    {
        private TextBox txtSurname, txtName, txtPatronymic;
        private TextBox txtEmail, txtPhone, txtAddress;
        private TextBox txtLogin, txtPassword, txtConfirmPassword;
        private Button btnRegister;

        public RegisterForm()
        {
            SetupForm();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void SetupForm()
        {
            this.Text = "Регистрация";
            this.Size = new Size(720, 750);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = ThemeColors.Background;
            this.ShowInTaskbar = false;

            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            Panel innerPanel = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent
            };

            FlowLayoutPanel flow = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent
            };

            // Заголовок
            Label lblTitle = new Label
            {
                Text = "Регистрация",
                Font = new Font("HONOR Sans", 20, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 50,
                Width = 560,
                Margin = new Padding(0, 0, 0, 20)
            };
            flow.Controls.Add(lblTitle);

            // --- Строка ФИО (3 равные колонки) ---
            flow.Controls.Add(CreateNameRow());

            // --- Строка Email + Телефон (пропорции 2:1) ---
            flow.Controls.Add(CreateEmailPhoneRow());

            // --- Остальные поля ---
            flow.Controls.Add(CreateFieldRow("Адрес", out txtAddress));
            flow.Controls.Add(CreateFieldRow("Логин *", out txtLogin));
            flow.Controls.Add(CreateFieldRow("Пароль *", out txtPassword, true));
            flow.Controls.Add(CreateFieldRow("Подтверждение пароля *", out txtConfirmPassword, true));

            // --- Панель для кнопки (центрирование) ---
            Panel btnPanel = new Panel
            {
                Width = 560,
                Height = 90,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 10, 0, 0)
            };

            btnRegister = new Button
            {
                Text = "Зарегистрироваться",
                Size = new Size(260, 55),
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeColors.Accent,
                ForeColor = ThemeColors.Text,
                Font = new Font("HONOR Sans", 14, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 },
                TabStop = false
            };
            btnRegister.Click += BtnRegister_Click;
            btnRegister.Paint += (s, e) => RoundControl(btnRegister, 25);

            btnPanel.Controls.Add(btnRegister);
            btnRegister.Location = new Point(
                (btnPanel.Width - btnRegister.Width) / 2,
                (btnPanel.Height - btnRegister.Height) / 2
            );
            btnPanel.Resize += (s, e) =>
            {
                btnRegister.Location = new Point(
                    (btnPanel.Width - btnRegister.Width) / 2,
                    (btnPanel.Height - btnRegister.Height) / 2
                );
            };

            flow.Controls.Add(btnPanel);

            innerPanel.Controls.Add(flow);
            mainPanel.Controls.Add(innerPanel);

            mainPanel.Resize += (s, e) =>
            {
                innerPanel.Location = new Point(
                    (mainPanel.Width - innerPanel.Width) / 2,
                    (mainPanel.Height - innerPanel.Height) / 2
                );
            };

            this.Controls.Add(mainPanel);
        }

        /// <summary>Строка ФИО (три равные колонки)</summary>
        private Panel CreateNameRow()
        {
            Panel rowPanel = new Panel
            {
                Width = 560,
                Height = 80,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 5, 0, 5)
            };

            TableLayoutPanel table = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 1,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));

            table.Controls.Add(CreateSingleField("Фамилия *", out txtSurname, 170), 0, 0);
            table.Controls.Add(CreateSingleField("Имя *", out txtName, 170), 1, 0);
            table.Controls.Add(CreateSingleField("Отчество", out txtPatronymic, 170), 2, 0);

            rowPanel.Controls.Add(table);
            return rowPanel;
        }

        /// <summary>Строка Email + Телефон (пропорция 2:1)</summary>
        private Panel CreateEmailPhoneRow()
        {
            Panel rowPanel = new Panel
            {
                Width = 560,
                Height = 80,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 5, 0, 5)
            };

            TableLayoutPanel table = new TableLayoutPanel
            {
                ColumnCount = 2,
                RowCount = 1,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66.666F));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));

            table.Controls.Add(CreateSingleField("Электронная почта *", out txtEmail, 363), 0, 0);
            table.Controls.Add(CreateSingleField("Телефон *", out txtPhone, 177), 1, 0);

            rowPanel.Controls.Add(table);
            return rowPanel;
        }

        /// <summary>Создаёт одно поле заданной ширины с подписью (для размещения в ячейке таблицы)</summary>
        private Panel CreateSingleField(string labelText, out TextBox textBox, int fieldWidth, bool isPassword = false)
        {
            Panel fieldContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(5, 0, 5, 0)
            };

            Label lbl = new Label
            {
                Text = labelText,
                Font = new Font("HONOR Sans", 8, FontStyle.Regular),
                ForeColor = Color.FromArgb(80, 80, 80),
                Location = new Point(0, 0),
                Size = new Size(fieldWidth, 15)
            };

            textBox = new TextBox
            {
                Location = new Point(0, 18),
                Size = new Size(fieldWidth, 40),
                Font = new Font("HONOR Sans", 10),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(245, 245, 245)
            };
            if (isPassword) textBox.UseSystemPasswordChar = true;

            Panel borderPanel = new Panel
            {
                Location = new Point(0, 18),
                Size = new Size(fieldWidth, 40),
                BackColor = Color.FromArgb(245, 245, 245)
            };
            borderPanel.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(200, 200, 200), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, borderPanel.Width - 1, borderPanel.Height - 1);
                }
            };

            fieldContainer.Controls.Add(lbl);
            fieldContainer.Controls.Add(borderPanel);
            fieldContainer.Controls.Add(textBox);
            textBox.BringToFront();
            return fieldContainer;
        }

        /// <summary>Поле на всю ширину строки (Адрес, Логин, Пароль...)</summary>
        private Panel CreateFieldRow(string labelText, out TextBox textBox, bool isPassword = false)
        {
            Panel rowPanel = new Panel
            {
                Width = 560,
                Height = 75,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 5, 0, 5)
            };

            Label lbl = new Label
            {
                Text = labelText,
                Font = new Font("HONOR Sans", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(80, 80, 80),
                Location = new Point(0, 0),
                Size = new Size(560, 15)
            };

            textBox = new TextBox
            {
                Location = new Point(0, 18),
                Size = new Size(560, 40),
                Font = new Font("HONOR Sans", 11),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(245, 245, 245)
            };
            if (isPassword) textBox.UseSystemPasswordChar = true;

            Panel borderPanel = new Panel
            {
                Location = new Point(0, 18),
                Size = new Size(560, 40),
                BackColor = Color.FromArgb(245, 245, 245)
            };
            borderPanel.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(200, 200, 200), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, borderPanel.Width - 1, borderPanel.Height - 1);
                }
            };

            rowPanel.Controls.Add(lbl);
            rowPanel.Controls.Add(borderPanel);
            rowPanel.Controls.Add(textBox);
            textBox.BringToFront();
            return rowPanel;
        }

        private void RoundControl(Control ctrl, int radius)
        {
            using (var path = new GraphicsPath())
            {
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(ctrl.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(ctrl.Width - radius, ctrl.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, ctrl.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                ctrl.Region = new Region(path);
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            // Собираем данные
            string surname = txtSurname.Text.Trim();
            string name = txtName.Text.Trim();
            string patronymic = txtPatronymic.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAddress.Text.Trim();
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;
            string confirm = txtConfirmPassword.Text;

            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Заполните все обязательные поля (отмечены *)", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка совпадения паролей
            if (password != confirm)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка на существование пользователя с таким логином или email
            if (DatabaseHelper.IsUserExists(login, email))
            {
                MessageBox.Show("Пользователь с таким логином или email уже существует", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Регистрация
            bool success = DatabaseHelper.RegisterUser(login, password, surname, name,
                                                        patronymic, email, phone, address);
            if (success)
            {
                MessageBox.Show("Регистрация прошла успешно!\nТеперь вы можете войти.",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // можно использовать для оповещения вызывающей формы
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка при регистрации. Попробуйте позже.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}