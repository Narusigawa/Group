using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace cond
{
    public partial class OrderHistoryPage : UserControl
    {
        private readonly User _currentUser;
        private FlowLayoutPanel _flowPanel;
        private List<Order> _orders;

        public OrderHistoryPage(User currentUser)
        {
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            InitializeComponent();
            this.Load += (s, e) => LoadOrders();
        }

        private void LoadOrders()
        {
            _orders = DatabaseHelper.GetUserOrders(_currentUser.Id);
            _flowPanel.Controls.Clear();

            if (_orders.Count == 0)
            {
                var lblEmpty = new Label
                {
                    Text = "У вас пока нет заказов",
                    Font = new Font("Segoe UI", 16, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Margin = new Padding(10)
                };
                _flowPanel.Controls.Add(lblEmpty);
                return;
            }

            _flowPanel.Controls.Add(new Panel
            {
                Height = 20,
                Width = 10,
                BackColor = Color.Transparent
            });

            foreach (var order in _orders)
            {
                var card = CreateOrderCard(order);
                _flowPanel.Controls.Add(card);
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

        private void ShowOrderDetails(int orderId)
        {
            var parent = this.Parent as Panel;
            if (parent != null)
            {
                var detailsPage = new OrderDetailsPage(orderId);
                detailsPage.BackRequested += (s, e) =>
                {
                    parent.Controls.Clear();
                    parent.Controls.Add(new OrderHistoryPage(_currentUser));
                };
                parent.Controls.Clear();
                parent.Controls.Add(detailsPage);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_flowPanel != null && _flowPanel.Controls.Count > 0)
            {
                LoadOrders();
            }
        }
    }
}