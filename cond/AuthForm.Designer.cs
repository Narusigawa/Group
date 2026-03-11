using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace cond
{
    partial class AuthForm
    {
        private void InitializeComponent()
        {
            this.Text = "Вход";
            this.Size = new Size(400, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = ThemeColors.Background;
            this.ShowInTaskbar = false;

            TableLayoutPanel mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 5,
                Padding = new Padding(40, 20, 40, 30),
                BackColor = Color.White
            };
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));

            Label lblTitle = new Label
            {
                Text = "Добро пожаловать!",
                Font = new Font("HONOR Sans", 20, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            mainPanel.Controls.Add(lblTitle, 0, 0);

            mainPanel.Controls.Add(CreateTextField("Логин", out txtLogin), 0, 1);
            mainPanel.Controls.Add(CreateTextField("Пароль", out txtPassword, true), 0, 2);

            FlowLayoutPanel linkPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                BackColor = Color.Transparent
            };
            linkPanel.Controls.Add(new Label
            {
                Text = "Впервые у нас? ",
                Font = new Font("HONOR Sans", 10),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true
            });
            lblRegisterLink = new Label
            {
                Text = "Зарегистрироваться",
                Font = new Font("HONOR Sans", 10, FontStyle.Underline),
                ForeColor = Color.FromArgb(210, 100, 120),
                AutoSize = true,
                Cursor = Cursors.Hand
            };
            lblRegisterLink.Click += LblRegister_Click;
            linkPanel.Controls.Add(lblRegisterLink);
            mainPanel.Controls.Add(linkPanel, 0, 3);

            Panel btnPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            btnLogin = new Button
            {
                Text = "Войти",
                Size = new Size(200, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeColors.Accent,
                ForeColor = ThemeColors.Text,
                Font = new Font("HONOR Sans", 14, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };
            btnLogin.Click += BtnLogin_Click;
            btnLogin.Paint += (s, e) => RoundControl(btnLogin, 20);

            btnPanel.Controls.Add(btnLogin);
            btnPanel.Resize += (s, e) =>
            {
                btnLogin.Location = new Point(
                    (btnPanel.Width - btnLogin.Width) / 2,
                    (btnPanel.Height - btnLogin.Height) / 2
                );
            };

            mainPanel.Controls.Add(btnPanel, 0, 4);
            this.Controls.Add(mainPanel);
        }

        private Panel CreateTextField(string labelText, out TextBox textBox, bool isPassword = false)
        {
            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            Label lbl = new Label
            {
                Text = labelText,
                Font = new Font("HONOR Sans", 9, FontStyle.Regular),
                ForeColor = Color.FromArgb(80, 80, 80),
                Location = new Point(0, 0),
                Size = new Size(320, 15)
            };

            textBox = new TextBox
            {
                Location = new Point(0, 20),
                Size = new Size(320, 35),
                Font = new Font("HONOR Sans", 14),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(245, 245, 245)
            };
            if (isPassword) textBox.UseSystemPasswordChar = true;

            Panel borderPanel = new Panel
            {
                Location = new Point(0, 20),
                Size = new Size(320, 35),
                BackColor = Color.FromArgb(245, 245, 245)
            };
            borderPanel.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(200, 200, 200), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, borderPanel.Width - 1, borderPanel.Height - 1);
                }
            };

            panel.Controls.Add(lbl);
            panel.Controls.Add(borderPanel);
            panel.Controls.Add(textBox);
            textBox.BringToFront();
            return panel;
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
    }
}