using System;
using System.Drawing;
using System.Windows.Forms;

namespace cond
{
    partial class OrderDetailsPage
    {
        private void SetupUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeColors.Background;
            this.AutoScroll = true;
            this.Padding = new Padding(0, HeaderHeight, 0, 0);

            var mainContainer = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = Color.Transparent
            };

            mainContainer.RowStyles.Clear();
            mainContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
            mainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));

            this.Controls.Add(mainContainer);

            var titlePanel = new Panel
            {
                Height = 80,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            var lblTitle = new Label
            {
                Text = $"Заказ №{_orderId}",
                Font = new Font("HONOR Sans", 28, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                Location = new Point(30, 20),
                AutoSize = true
            };
            titlePanel.Controls.Add(lblTitle);
            mainContainer.Controls.Add(titlePanel, 0, 0);

            var infoPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                BackColor = Color.Transparent,
                Padding = new Padding(30, 10, 30, 20)
            };

            var infoFlowPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent,
                Dock = DockStyle.Fill,
                Padding = new Padding(0)
            };

            var datePanel = CreateInfoField("Дата:", _order.OrderDate.ToString("dd.MM.yyyy HH:mm"), false);
            infoFlowPanel.Controls.Add(datePanel);

            var statusPanel = CreateInfoField("Статус:", _order.Status, true);
            foreach (Control control in statusPanel.Controls)
            {
                if (control is Label label && label != statusPanel.Controls[0])
                {
                    label.ForeColor = GetStatusColor(_order.Status);
                }
            }
            infoFlowPanel.Controls.Add(statusPanel);

            var addressPanel = CreateInfoField("Адрес доставки:", _order.DeliveryAddress, false);
            infoFlowPanel.Controls.Add(addressPanel);

            if (!string.IsNullOrWhiteSpace(_order.Comment))
            {
                var commentPanel = CreateInfoField("Комментарий:", _order.Comment, false);
                infoFlowPanel.Controls.Add(commentPanel);
            }

            infoPanel.Controls.Add(infoFlowPanel);
            mainContainer.Controls.Add(infoPanel, 0, 1);

            _itemsContainer = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent,
                Margin = new Padding(0),
                Padding = new Padding(20, 10, 20, 10),
                Dock = DockStyle.Fill
            };

            mainContainer.Controls.Add(_itemsContainer, 0, 2);

            var bottomPanel = new Panel
            {
                Height = 90,
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(30, 0, 30, 0)
            };

            var total = _order.TotalAmount;
            _lblTotal = new Label
            {
                Text = $"Итого: {total:F2} ₽",
                Font = new Font("HONOR Sans", 22, FontStyle.Bold),
                ForeColor = ThemeColors.Accent,
                AutoSize = true,
                Location = new Point(0, 25)
            };

            var btnBack = new Button
            {
                Text = "← Назад",
                Font = new Font("HONOR Sans", 16, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightGray,
                ForeColor = ThemeColors.Text,
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 },
                Size = new Size(170, 60),
                Location = new Point(bottomPanel.Width - 200, 15),
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            btnBack.Click += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);

            bottomPanel.Controls.Add(_lblTotal);
            bottomPanel.Controls.Add(btnBack);

            bottomPanel.Resize += (s, e) =>
            {
                btnBack.Location = new Point(bottomPanel.Width - 200, 15);
            };

            mainContainer.Controls.Add(bottomPanel, 0, 3);

            PopulateItems();

            this.Resize += (s, e) =>
            {
                UpdateInfoFieldsWidth(infoFlowPanel);
                UpdateCardsWidth();
            };
        }

        private Panel CreateInfoField(string labelText, string valueText, bool isStatus)
        {
            var panel = new Panel
            {
                AutoSize = true,
                Width = this.ClientSize.Width - 100,
                Margin = new Padding(0, 0, 0, 15)
            };

            var label = new Label
            {
                Text = labelText,
                Font = new Font("HONOR Sans", 14, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                AutoSize = true,
                Location = new Point(0, 0),
                TextAlign = ContentAlignment.TopLeft,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            var value = new Label
            {
                Text = valueText,
                Font = new Font("HONOR Sans", 14, isStatus ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = isStatus ? GetStatusColor(valueText) : ThemeColors.Text,
                AutoSize = true,
                MaximumSize = new Size(this.ClientSize.Width - 250, 0),
                Location = new Point(label.Width + 5, 0),
                TextAlign = ContentAlignment.TopLeft,
                Padding = new Padding(0),
                Margin = new Padding(0),
                AutoEllipsis = true
            };

            panel.Controls.Add(label);
            panel.Controls.Add(value);

            label.TextChanged += (s, e) =>
            {
                value.Location = new Point(label.Width + 5, 0);
            };

            value.TextChanged += (s, e) =>
            {
                panel.Width = Math.Max(label.Width + value.Width + 10, this.ClientSize.Width - 100);
            };

            return panel;
        }

        private void UpdateInfoFieldsWidth(FlowLayoutPanel infoFlowPanel)
        {
            foreach (Control control in infoFlowPanel.Controls)
            {
                if (control is Panel panel)
                {
                    panel.Width = this.ClientSize.Width - 100;

                    Label label = null;
                    Label value = null;

                    foreach (Control child in panel.Controls)
                    {
                        if (child is Label lbl)
                        {
                            if (lbl.Font.Bold && lbl != panel.Controls[1])
                                label = lbl;
                            else
                                value = lbl;
                        }
                    }

                    if (label != null && value != null)
                    {
                        value.MaximumSize = new Size(this.ClientSize.Width - label.Width - 150, 0);
                        value.Location = new Point(label.Width + 5, 0);
                    }
                }
            }
        }

        private Panel CreateOrderItemCard(OrderItem item)
        {
            var card = new Panel
            {
                Width = this.ClientSize.Width - 40,
                Height = 100,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 15)
            };

            card.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.LightGray, 1))
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };

            var lblName = new Label
            {
                Text = item.ProductName,
                Font = new Font("HONOR Sans", 13, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                Location = new Point(15, 15),
                AutoSize = true,
                MaximumSize = new Size(card.Width - 180, 0),
                AutoEllipsis = true
            };
            card.Controls.Add(lblName);

            var lblDetails = new Label
            {
                Text = $"{item.Quantity} шт. × {item.PriceAtTime:F2} ₽",
                Font = new Font("HONOR Sans", 11),
                ForeColor = Color.Gray,
                Location = new Point(15, 45),
                AutoSize = true
            };
            card.Controls.Add(lblDetails);

            var lblSum = new Label
            {
                Text = $"{(item.Quantity * item.PriceAtTime):F2} ₽",
                Font = new Font("HONOR Sans", 15, FontStyle.Bold),
                ForeColor = ThemeColors.Accent,
                AutoSize = true,
                Location = new Point(card.Width - 140, 25)
            };
            card.Controls.Add(lblSum);

            card.Resize += (s, e) =>
            {
                lblSum.Location = new Point(card.Width - 140, 25);
                lblName.MaximumSize = new Size(card.Width - 180, 0);
            };

            return card;
        }

        private void InitializeComponent()
        {
            SetupUI();
        }
    }
}