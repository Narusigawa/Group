using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace cond
{
    public partial class CheckoutPage : UserControl
    {
        private User _currentUser;
        private List<CartItem> _cartItems;
        private TextBox txtAddress, txtComment, txtCardNumber, txtCardExpiry, txtCardCvv;
        private ListBox lstProducts;
        private Label lblTotal;

        public event EventHandler OrderCompleted;

        public CheckoutPage(User currentUser, List<CartItem> cartItems)
        {
            _currentUser = currentUser;
            _cartItems = cartItems;
            InitializeComponent();
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Введите адрес доставки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCardNumber.Text) ||
                string.IsNullOrWhiteSpace(txtCardExpiry.Text) ||
                string.IsNullOrWhiteSpace(txtCardCvv.Text))
            {
                MessageBox.Show("Заполните платёжные данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderId = DatabaseHelper.CreateOrder(_currentUser.Id, txtAddress.Text, txtComment.Text);
            if (orderId > 0)
            {
                MessageBox.Show($"Заказ №{orderId} успешно оформлен!", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OrderCompleted?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Не удалось оформить заказ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}