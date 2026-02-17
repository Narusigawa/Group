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
    public partial class HeaderControl : UserControl
    {
        // События для кнопок
        public event EventHandler MenuClick;
        public event EventHandler ProfileClick;
        public event EventHandler CartClick;

        private Button btnMenu;
        private Label lblTitle;
        private Button btnProfile;
        private Button btnCart;

        public HeaderControl()
        {
            InitializeComponent();
            SetupHeader();
        }

        private void SetupHeader()
        {
            // ===== НАСТРОЙКА ШАПКИ =====
            this.Dock = DockStyle.Top;
            this.Height = 120;                     // Увеличили высоту
            this.BackColor = ThemeColors.Header;

            // --- 1. Кнопка-бургер (слева) ---
            btnMenu = new Button
            {
                Text = "☰",
                Font = new Font("Segoe UI", 32, FontStyle.Regular),
                Size = new Size(90, 90),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = ThemeColors.Text,
                Dock = DockStyle.Left,
                Margin = new Padding(15, 15, 15, 15)  // Отступы со всех сторон
            };
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.Click += (s, e) => MenuClick?.Invoke(this, e);

            // --- 2. Заголовок по центру ---
            lblTitle = new Label
            {
                Text = "Sweet Delights",
                Font = new Font("Segoe Script", 28, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            // --- 3. Панель для правых кнопок ---
            FlowLayoutPanel rightPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent,
                Padding = new Padding(15, 15, 15, 15) // Отступы справа
            };

            // --- Кнопка профиля ---
            btnProfile = new Button
            {
                Text = "👤",
                Font = new Font("Segoe UI", 28),
                Size = new Size(90, 90),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = ThemeColors.Text
            };
            btnProfile.FlatAppearance.BorderSize = 0;
            btnProfile.Click += (s, e) => ProfileClick?.Invoke(this, e);
            rightPanel.Controls.Add(btnProfile);

            // --- Кнопка корзины ---
            btnCart = new Button
            {
                Text = "🛒",
                Font = new Font("Segoe UI", 28),
                Size = new Size(90, 90),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = ThemeColors.Text
            };
            btnCart.FlatAppearance.BorderSize = 0;
            btnCart.Click += (s, e) => CartClick?.Invoke(this, e);
            rightPanel.Controls.Add(btnCart);

            // --- Добавляем все элементы на шапку ---
            this.Controls.Add(lblTitle);
            this.Controls.Add(btnMenu);
            this.Controls.Add(rightPanel);
        }
    }
}
