using System;
using System.Drawing;
using System.Windows.Forms;

namespace cond
{
    partial class OrderHistoryPage
    {
        private void SetupUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeColors.Background;
            this.Padding = new Padding(0, 120, 0, 0);

            var container = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(20),
                BackColor = Color.Transparent
            };
            container.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            container.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var lblTitle = new Label
            {
                Text = "История заказов",
                Font = new Font("HONOR Sans", 26, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill
            };
            container.Controls.Add(lblTitle, 0, 0);

            var scrollPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent
            };

            _flowPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Top,
                BackColor = Color.Transparent
            };
            scrollPanel.Controls.Add(_flowPanel);
            container.Controls.Add(scrollPanel, 0, 1);

            this.Controls.Add(container);
        }

        private Control CreateOrderCard(Order order)
        {
            int cardWidth = Math.Max(600, _flowPanel.Width - 40);
            if (cardWidth <= 0) cardWidth = 600;

            var card = new Panel
            {
                Width = cardWidth,
                Height = 100,
                BackColor = Color.White,
                Cursor = Cursors.Hand,
                Margin = new Padding(5, 5, 5, 5)
            };

            card.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.LightGray, 1))
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };

            card.Click += (s, e) => ShowOrderDetails(order.Id);

            var lblId = new Label
            {
                Text = $"Заказ №{order.Id}",
                Font = new Font("HONOR Sans", 14, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                Location = new Point(15, 15),
                AutoSize = true
            };
            card.Controls.Add(lblId);

            var lblDate = new Label
            {
                Text = order.OrderDate.ToString("dd.MM.yyyy HH:mm"),
                Font = new Font("HONOR Sans", 10),
                ForeColor = Color.Gray,
                Location = new Point(15, 45),
                AutoSize = true
            };
            card.Controls.Add(lblDate);

            var lblTotal = new Label
            {
                Text = $"{order.TotalAmount:F2} ₽",
                Font = new Font("HONOR Sans", 16, FontStyle.Bold),
                ForeColor = ThemeColors.Accent,
                AutoSize = true,
                Location = new Point(card.Width - 150, 20),
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            card.Controls.Add(lblTotal);

            var lblStatus = new Label
            {
                Text = order.Status,
                Font = new Font("HONOR Sans", 10, FontStyle.Bold),
                ForeColor = GetStatusColor(order.Status),
                AutoSize = true,
                Location = new Point(card.Width - 150, 55),
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            card.Controls.Add(lblStatus);

            card.Resize += (s, e) =>
            {
                lblTotal.Location = new Point(card.Width - 150, 20);
                lblStatus.Location = new Point(card.Width - 150, 55);
            };

            return card;
        }

        private void InitializeComponent()
        {
            SetupUI();
        }
    }
}