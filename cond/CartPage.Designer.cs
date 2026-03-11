using System;
using System.Drawing;
using System.Windows.Forms;

namespace cond
{
    partial class CartPage
    {
        private void SetupUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeColors.Background;
            this.Padding = new Padding(0, 120, 0, 0);

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.Transparent
            };
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            table.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));

            var titlePanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            var lblTitle = new Label
            {
                Text = "Корзина",
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                Location = new Point(30, 15),
                AutoSize = true
            };
            titlePanel.Controls.Add(lblTitle);
            table.Controls.Add(titlePanel, 0, 0);

            var scrollPanel = new Panel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = Color.Transparent };
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
            table.Controls.Add(scrollPanel, 0, 1);

            var bottomPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            _lblTotal = new Label
            {
                Text = "Итого: 0 ₽",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ThemeColors.Accent,
                AutoSize = true,
                Location = new Point(30, 25)
            };
            _btnCheckout = new Button
            {
                Text = "Оформить заказ",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeColors.Accent,
                ForeColor = ThemeColors.Text,
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 },
                Size = new Size(220, 50),
                Location = new Point(bottomPanel.Width - 260, 15),
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            _btnCheckout.Click += BtnCheckout_Click;
            bottomPanel.Controls.Add(_lblTotal);
            bottomPanel.Controls.Add(_btnCheckout);
            table.Controls.Add(bottomPanel, 0, 2);

            this.Controls.Add(table);
        }

        private Panel CreateCartItemCard(CartItem item)
        {
            int cardWidth = Math.Max(600, _flowPanel.ClientSize.Width - 30);
            var card = new Panel
            {
                Width = cardWidth,
                Height = 170,
                BackColor = Color.White,
                Margin = new Padding(10, 5, 10, 5)
            };

            card.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.LightGray, 1))
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };

            var pb = new PictureBox
            {
                Size = new Size(110, 110),
                Location = new Point(15, 30),
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = LoadImage(item.Product.ImageFilename)
            };
            card.Controls.Add(pb);

            var lblName = new Label
            {
                Text = item.Product.Name,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(140, 25),
                AutoSize = true
            };
            card.Controls.Add(lblName);

            var lblDetails = new Label
            {
                Text = $"{item.Product.Price:F2} ₽ × {item.Quantity}",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.Gray,
                Location = new Point(140, 60),
                AutoSize = true
            };
            card.Controls.Add(lblDetails);

            var lblSum = new Label
            {
                Text = $"{(item.Product.Price * item.Quantity):F2} ₽",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = ThemeColors.Accent,
                AutoSize = true,
                Location = new Point(card.Width - 240, 30)
            };
            card.Controls.Add(lblSum);

            var btnPlus = new Button
            {
                Text = "+",
                Size = new Size(50, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeColors.Accent,
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Location = new Point(card.Width - 120, 20)
            };
            btnPlus.FlatAppearance.BorderSize = 0;
            btnPlus.Click += (s, e) => ChangeQuantity(item, 1);
            card.Controls.Add(btnPlus);

            var btnMinus = new Button
            {
                Text = "-",
                Size = new Size(50, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightGray,
                ForeColor = Color.Black,
                Cursor = Cursors.Hand,
                Enabled = item.Quantity > 1,
                Location = new Point(card.Width - 120, 75)
            };
            btnMinus.FlatAppearance.BorderSize = 0;
            btnMinus.Click += (s, e) => ChangeQuantity(item, -1);
            card.Controls.Add(btnMinus);

            var btnRemove = new Button
            {
                Text = "Удалить",
                Size = new Size(100, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(255, 150, 150),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,
                Location = new Point(card.Width - 120, 130)
            };
            btnRemove.FlatAppearance.BorderSize = 0;
            btnRemove.Click += (s, e) => RemoveItem(item);
            card.Controls.Add(btnRemove);

            return card;
        }

        private void InitializeComponent()
        {
            SetupUI();
        }
    }
}