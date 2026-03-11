using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace cond
{
    public partial class CartPage : UserControl
    {
        private readonly User _currentUser;
        private FlowLayoutPanel _flowPanel;
        private Label _lblTotal;
        private Button _btnCheckout;
        private List<CartItem> _cartItems;
        public event EventHandler OrderCompleted;

        public CartPage(User currentUser)
        {
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
            InitializeComponent();
            this.Load += (s, e) => LoadCart();
        }

        private void LoadCart()
        {
            _cartItems = DatabaseHelper.GetCart(_currentUser.Id);
            _flowPanel.Controls.Clear();

            if (_cartItems.Count == 0)
            {
                var lblEmpty = new Label
                {
                    Text = "Ваша корзина пуста",
                    Font = new Font("Segoe UI", 16, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Margin = new Padding(30, 20, 0, 0)
                };
                _flowPanel.Controls.Add(lblEmpty);
                _btnCheckout.Enabled = false;
            }
            else
            {
                foreach (var item in _cartItems)
                {
                    var card = CreateCartItemCard(item);
                    _flowPanel.Controls.Add(card);
                }
                _btnCheckout.Enabled = true;
            }
            UpdateTotal();
        }

        private void ChangeQuantity(CartItem item, int delta)
        {
            int newQty = item.Quantity + delta;
            if (newQty < 1) return;
            if (DatabaseHelper.UpdateCartItemQuantity(_currentUser.Id, item.ProductId, newQty))
            {
                item.Quantity = newQty;
                LoadCart();
            }
        }

        private void RemoveItem(CartItem item)
        {
            if (DatabaseHelper.RemoveFromCart(_currentUser.Id, item.ProductId))
                LoadCart();
        }

        private void UpdateTotal()
        {
            decimal total = _cartItems.Sum(item => item.Product.Price * item.Quantity);
            _lblTotal.Text = $"Итого: {total:F2} ₽";
        }

        private Image LoadImage(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return CreateDefaultImage();

            string path = System.IO.Path.Combine(Application.StartupPath, "Images", filename);
            if (System.IO.File.Exists(path))
            {
                try
                {
                    using (var fs = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read))
                        return Image.FromStream(fs);
                }
                catch { }
            }
            return CreateDefaultImage();
        }

        private Image CreateDefaultImage()
        {
            Bitmap bmp = new Bitmap(110, 110);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightPink);
                g.DrawString("🍰", new Font("Segoe UI", 44), Brushes.White, new PointF(20, 20));
            }
            return bmp;
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            var parent = this.Parent as Panel;
            if (parent != null)
            {
                var checkoutPage = new CheckoutPage(_currentUser, _cartItems);
                checkoutPage.OrderCompleted += (s, args) =>
                {
                    OrderCompleted?.Invoke(this, EventArgs.Empty);
                };
                parent.Controls.Clear();
                parent.Controls.Add(checkoutPage);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_flowPanel != null && _cartItems != null && _cartItems.Count > 0)
            {
                LoadCart();
            }
        }
    }
}