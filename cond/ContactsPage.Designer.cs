using System.Drawing;
using System.Windows.Forms;

namespace cond
{
    partial class ContactsPage
    {
        private void InitializeComponent()
        {
            this.BackColor = ThemeColors.Background;
            this.Dock = DockStyle.Fill;

            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ThemeColors.Background,
                Padding = new Padding(50, 120, 50, 50)
            };

            TableLayoutPanel grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                BackColor = ThemeColors.Background
            };

            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            Label lblTitle = new Label
            {
                Text = "Контакты",
                Font = new Font("HONOR Sans", 34, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            grid.Controls.Add(lblTitle, 0, 0);
            grid.SetColumnSpan(lblTitle, 2);

            Label leftText = new Label
            {
                Text = "📍 Мы всегда рады видеть вас в наших кондитерских!\n\n" +
                       "Вы можете зайти к нам, чтобы насладиться свежей выпечкой и ароматным кофе, или связаться с нами любым удобным способом.\n\n" +
                       "Мы работаем ежедневно с 9:00 до 21:00.",
                Font = new Font("HONOR Sans", 22),
                ForeColor = Color.FromArgb(80, 80, 80),
                TextAlign = ContentAlignment.TopLeft,
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 140, 10, 0)
            };
            grid.Controls.Add(leftText, 0, 1);

            FlowLayoutPanel rightPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = ThemeColors.Background,
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 50, 10, 0)
            };

            rightPanel.Controls.Add(CreateContactLine("🏠 Адреса:", 24, FontStyle.Bold));
            rightPanel.Controls.Add(CreateContactLine("   ул. Сладкая, д. 15, Москва", 22));
            rightPanel.Controls.Add(CreateContactLine("   пр. Пряничный, д. 8, Санкт-Петербург", 22));
            rightPanel.Controls.Add(CreateContactLine("   ул. Вафельная, д. 3, Казань", 22));
            rightPanel.Controls.Add(CreateContactLine("   пер. Карамельный, д. 12, Новосибирск", 22));
            rightPanel.Controls.Add(CreateSeparator(200));

            rightPanel.Controls.Add(CreateContactLine("📞 Горячая линия  :", 24, FontStyle.Bold));
            rightPanel.Controls.Add(CreateContactLine("   +7 (495) 123-45-67", 22));
            rightPanel.Controls.Add(CreateContactLine("   +7 (800) 555-35-35", 22));
            rightPanel.Controls.Add(CreateSeparator(200));

            rightPanel.Controls.Add(CreateContactLine("✉️ Email:", 24, FontStyle.Bold));
            rightPanel.Controls.Add(CreateContactLine("   hello@sweetheartsonya.ru", 22));

            grid.Controls.Add(rightPanel, 1, 1);

            mainPanel.Controls.Add(grid);
            this.Controls.Add(mainPanel);
        }

        private Label CreateContactLine(string text, float fontSize, FontStyle style = FontStyle.Regular)
        {
            return new Label
            {
                Text = text,
                Font = new Font("HONOR Sans", fontSize, style),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Margin = new Padding(0, 5, 0, 5),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private Panel CreateSeparator(int width)
        {
            return new Panel
            {
                Height = 1,
                Width = width,
                BackColor = Color.FromArgb(180, 180, 180),
                Margin = new Padding(0, 10, 0, 10)
            };
        }
    }
}