using System;
using System.Drawing;
using System.Windows.Forms;

namespace cond
{
    public partial class OrderDetailsPage : UserControl
    {
        private readonly int _orderId;
        private Order _order;
        private FlowLayoutPanel _itemsContainer;
        private Label _lblTotal;
        private const int HeaderHeight = 120;

        public event EventHandler BackRequested;

        public OrderDetailsPage(int orderId)
        {
            _orderId = orderId;
            InitializeComponent();
            LoadOrder();
            this.Load += (s, e) => PopulateItems();
        }

        private void LoadOrder()
        {
            _order = DatabaseHelper.GetOrderDetails(_orderId);
            if (_order == null)
            {
                MessageBox.Show("Заказ не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BackRequested?.Invoke(this, EventArgs.Empty);
            }
        }

        private void PopulateItems()
        {
            if (_order?.Items == null) return;

            _itemsContainer.Controls.Clear();

            foreach (var item in _order.Items)
            {
                var card = CreateOrderItemCard(item);
                _itemsContainer.Controls.Add(card);
            }

            UpdateCardsWidth();
        }

        private void UpdateCardsWidth()
        {
            if (_itemsContainer == null || _itemsContainer.Controls.Count == 0)
                return;

            int newWidth = this.ClientSize.Width - 40;
            if (newWidth < 650) newWidth = 650;

            foreach (Control card in _itemsContainer.Controls)
            {
                card.Width = newWidth;

                foreach (Control child in card.Controls)
                {
                    if (child is Label label)
                    {
                        if (label.Font.Bold && label.Text.Contains("₽"))
                        {
                            label.Location = new Point(newWidth - 140, 25);
                        }
                        else if (label.Font.Bold && !label.Text.Contains("×") && !label.Text.Contains("шт."))
                        {
                            label.MaximumSize = new Size(newWidth - 180, 0);
                        }
                    }
                }
            }
        }

        private Color GetStatusColor(string status)
        {
            switch (status?.ToLower())
            {
                case "новый": return Color.Blue;
                case "оплачен": return Color.Green;
                case "готов": return Color.Orange;
                case "выдан": return Color.Gray;
                default: return Color.Black;
            }
        }
    }
}