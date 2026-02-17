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
    public partial class MenuControl : UserControl
    {
        public event EventHandler<string> NavigationRequested;

        public MenuControl()
        {
            InitializeComponent();
            SetupMenu();
        }

        private void SetupMenu()
        {
            // ===== НАСТРОЙКИ МЕНЮ =====
            this.Dock = DockStyle.Left;
            this.Width = 280;                    // увеличили ширину
            this.BackColor = Color.FromArgb(255, 245, 240);
            this.Visible = false;               // изначально скрыто

            // Контейнер для кнопок (вертикальный)
            FlowLayoutPanel flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,              // скролл, если кнопок много
                Padding = new Padding(0, 20, 20, 20),
                BackColor = Color.Transparent
            };

            // Создаём кнопки
            flow.Controls.Add(CreateMenuButton("📋 Каталог", "catalog"));
            flow.Controls.Add(CreateMenuButton("👤 Личный кабинет", "profile"));
            flow.Controls.Add(CreateMenuButton("ℹ️ О нас", "about"));
            flow.Controls.Add(CreateMenuButton("📞 Контакты", "contacts"));

            this.Controls.Add(flow);
        }

        /// <summary>
        /// Создаёт красивую крупную кнопку для меню
        /// </summary>
        private Button CreateMenuButton(string text, string key)
        {
            Button btn = new Button
            {
                Text = text,
                TextAlign = ContentAlignment.MiddleLeft,
                Height = 80,                    // высокая кнопка
                Width = this.Width - 40,        // ширина с учётом отступов
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = ThemeColors.Text,
                Font = new Font("Segoe UI", 16, FontStyle.Bold), // крупный жирный шрифт
                Padding = new Padding(0, 0, 0, 0),
                AutoSize = false,
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => NavigationRequested?.Invoke(this, key);
            return btn;
        }
    }
}
