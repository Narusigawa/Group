using System.Drawing;
using System.Windows.Forms;

namespace cond
{
    partial class MenuControl
    {
        private void SetupMenu()
        {
            this.Dock = DockStyle.Left;
            this.Width = 280;
            this.BackColor = Color.FromArgb(255, 245, 240);
            this.Visible = false;

            FlowLayoutPanel flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(0, 20, 20, 20),
                BackColor = Color.Transparent
            };

            flow.Controls.Add(CreateMenuButton("📋 Каталог", "catalog"));
            flow.Controls.Add(CreateMenuButton("👤 Личный кабинет", "profile"));
            flow.Controls.Add(CreateMenuButton("ℹ️ О нас", "about"));
            flow.Controls.Add(CreateMenuButton("📞 Контакты", "contacts"));

            this.Controls.Add(flow);
        }

        private Button CreateMenuButton(string text, string key)
        {
            Button btn = new Button
            {
                Text = text,
                TextAlign = ContentAlignment.MiddleLeft,
                Height = 80,
                Width = this.Width - 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = ThemeColors.Text,
                Font = new Font("HONOR Sans", 16, FontStyle.Bold),
                Padding = new Padding(0, 0, 0, 0),
                AutoSize = false,
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Click += (s, e) => NavigationRequested?.Invoke(this, key);
            return btn;
        }

        private void InitializeComponent()
        {
            SetupMenu();
        }
    }
}