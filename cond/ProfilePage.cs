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
    public partial class ProfilePage : UserControl
    {
        public ProfilePage()
        {
            InitializeComponent();
            SetupProfilePage();
        }

        private void SetupProfilePage()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeColors.Background;
            this.AutoScroll = true;

            // Основной контейнер
            Panel container = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MaximumSize = new Size(800, 0),
                MinimumSize = new Size(300, 0),
                BackColor = Color.White,
                Padding = new Padding(30)
            };

            // Центрируем контейнер
            container.Location = new Point((this.ClientSize.Width - container.Width) / 2, 30);
            this.Resize += (s, e) =>
            {
                container.Location = new Point(Math.Max(10, (this.ClientSize.Width - container.Width) / 2), 30);
            };

            // Аватар
            Label lblAvatar = new Label
            {
                Text = "👤",
                Font = new Font("Segoe UI", 60),
                ForeColor = ThemeColors.Accent,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 100
            };

            // Имя пользователя
            Label lblName = new Label
            {
                Text = "Имя пользователя",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };

            // Email
            Label lblEmail = new Label
            {
                Text = "email@example.com",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 30
            };

            // Статистика
            FlowLayoutPanel statsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 100,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 20, 0, 20)
            };

            statsPanel.Controls.Add(CreateStatCard("5", "Заказов"));
            statsPanel.Controls.Add(CreateStatCard("1200", "Бонусов"));
            statsPanel.Controls.Add(CreateStatCard("3", "Любимых"));

            // Кнопка выхода
            Button btnLogout = new Button
            {
                Text = "Выйти из аккаунта",
                Dock = DockStyle.Top,
                Height = 45,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(255, 200, 200),
                ForeColor = ThemeColors.Text,
                Font = new Font("Segoe UI", 12),
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 20, 0, 0)
            };
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Click += (s, e) =>
            {
                // Здесь будет выход из аккаунта
                MessageBox.Show("Вы вышли из аккаунта", "Инфо");
            };

            // Собираем
            container.Controls.Add(btnLogout);
            container.Controls.Add(statsPanel);
            container.Controls.Add(lblEmail);
            container.Controls.Add(lblName);
            container.Controls.Add(lblAvatar);

            this.Controls.Add(container);
        }

        private Panel CreateStatCard(string value, string label)
        {
            Panel card = new Panel
            {
                Size = new Size(100, 80),
                Margin = new Padding(10),
                BackColor = ThemeColors.Background
            };

            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = ThemeColors.Accent,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };

            Label lblLabel = new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            card.Controls.Add(lblLabel);
            card.Controls.Add(lblValue);

            return card;
        }
    }
}
