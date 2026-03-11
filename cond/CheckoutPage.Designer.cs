using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace cond
{
    partial class CheckoutPage
    {
        private void SetupUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeColors.Background;
            this.AutoScroll = true;
            this.Padding = new Padding(0, 120, 0, 0);

            var container = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 6,
                Padding = new Padding(20),
                BackColor = Color.Transparent
            };

            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

            container.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            container.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            container.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            container.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            container.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            container.RowStyles.Add(new RowStyle(SizeType.Absolute, 100));

            var lblTitle = new Label
            {
                Text = "Оформление заказа",
                Font = new Font("HONOR Sans", 28, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            container.Controls.Add(lblTitle, 1, 0);

            lstProducts = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("HONOR Sans", 10),
                Height = 120,
                SelectionMode = SelectionMode.None,
                BackColor = ThemeColors.Background,
                BorderStyle = BorderStyle.None,
                IntegralHeight = false
            };
            foreach (var item in _cartItems)
            {
                lstProducts.Items.Add($"{item.Product.Name} – {item.Quantity} шт. × {item.Product.Price:F2} ₽ = {(item.Product.Price * item.Quantity):F2} ₽");
            }
            container.Controls.Add(lstProducts, 1, 1);

            var addressPanel = CreateField("Адрес доставки", out txtAddress, _currentUser.Address ?? "");
            container.Controls.Add(addressPanel, 1, 2);

            var commentPanel = CreateField("Комментарий к заказу", out txtComment, "", true);
            container.Controls.Add(commentPanel, 1, 3);

            var paymentPanel = CreatePaymentPanel();
            container.Controls.Add(paymentPanel, 1, 4);

            var bottomPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            var total = _cartItems.Sum(item => item.Product.Price * item.Quantity);
            lblTotal = new Label
            {
                Text = $"Итого: {total:F2} ₽",
                Font = new Font("HONOR Sans", 18, FontStyle.Bold),
                ForeColor = ThemeColors.Accent,
                AutoSize = true
            };
            var btnConfirm = new Button
            {
                Text = "Подтвердить заказ",
                Font = new Font("HONOR Sans", 14, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeColors.Accent,
                ForeColor = ThemeColors.Text,
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 },
                Size = new Size(250, 50),
                Margin = new Padding(20, 0, 0, 0)
            };
            btnConfirm.Click += BtnConfirm_Click;

            var flow = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                BackColor = Color.Transparent
            };
            flow.Controls.Add(lblTotal);
            flow.Controls.Add(btnConfirm);
            bottomPanel.Controls.Add(flow);

            bottomPanel.Resize += (s, e) =>
            {
                flow.Location = new Point(
                    (bottomPanel.Width - flow.Width) / 2,
                    (bottomPanel.Height - flow.Height) / 2
                );
            };
            container.Controls.Add(bottomPanel, 1, 5);

            this.Controls.Add(container);
        }

        private Panel CreateField(string labelText, out TextBox textBox, string defaultValue = "", bool multiline = false)
        {
            var panel = new Panel
            {
                Height = multiline ? 100 : 70,
                Width = 500,
                BackColor = Color.Transparent
            };

            var lbl = new Label
            {
                Text = labelText,
                Font = new Font("HONOR Sans", 10, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                Location = new Point(0, 0),
                Width = panel.Width,
                Height = 20
            };

            var tb = new TextBox
            {
                Location = new Point(0, 22),
                Width = panel.Width,
                Height = multiline ? 70 : 30,
                Font = new Font("HONOR Sans", 12),
                BorderStyle = BorderStyle.FixedSingle,
                Text = defaultValue,
                Multiline = multiline
            };
            if (multiline) tb.ScrollBars = ScrollBars.Vertical;

            panel.Controls.Add(lbl);
            panel.Controls.Add(tb);
            textBox = tb;
            return panel;
        }

        private Panel CreatePaymentPanel()
        {
            var panel = new Panel
            {
                Height = 140,
                Width = 500,
                BackColor = Color.Transparent
            };

            var lblCard = new Label
            {
                Text = "Номер карты",
                Font = new Font("HONOR Sans", 10, FontStyle.Bold),
                Location = new Point(0, 0),
                Width = panel.Width,
                Height = 20
            };
            txtCardNumber = new TextBox
            {
                Location = new Point(0, 22),
                Width = panel.Width,
                Height = 30,
                Font = new Font("HONOR Sans", 12),
                BorderStyle = BorderStyle.FixedSingle,
                MaxLength = 19
            };
            txtCardNumber.KeyPress += (s, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                    e.Handled = true;
            };
            txtCardNumber.TextChanged += (s, e) =>
            {
                var text = txtCardNumber.Text.Replace(" ", "");
                if (text.Length > 16) text = text.Substring(0, 16);
                var formatted = "";
                for (int i = 0; i < text.Length; i++)
                {
                    if (i > 0 && i % 4 == 0)
                        formatted += " ";
                    formatted += text[i];
                }
                if (txtCardNumber.Text != formatted)
                {
                    var pos = txtCardNumber.SelectionStart;
                    txtCardNumber.Text = formatted;
                    txtCardNumber.SelectionStart = pos + (formatted.Length - text.Length);
                }
            };

            var lblExpiry = new Label
            {
                Text = "Срок действия (ММ/ГГ)",
                Font = new Font("HONOR Sans", 10, FontStyle.Bold),
                Location = new Point(0, 60),
                Width = 200,
                Height = 20
            };
            txtCardExpiry = new TextBox
            {
                Location = new Point(0, 82),
                Width = 80,
                Height = 30,
                Font = new Font("HONOR Sans", 12),
                BorderStyle = BorderStyle.FixedSingle,
                MaxLength = 5
            };
            txtCardExpiry.KeyPress += (s, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                    e.Handled = true;
            };
            txtCardExpiry.TextChanged += (s, e) =>
            {
                var text = txtCardExpiry.Text.Replace("/", "");
                if (text.Length > 4) text = text.Substring(0, 4);
                var formatted = "";
                for (int i = 0; i < text.Length; i++)
                {
                    if (i == 2)
                        formatted += "/";
                    formatted += text[i];
                }
                if (txtCardExpiry.Text != formatted)
                {
                    txtCardExpiry.Text = formatted;
                    txtCardExpiry.SelectionStart = formatted.Length;
                }
            };

            var lblCvv = new Label
            {
                Text = "CVV",
                Font = new Font("HONOR Sans", 10, FontStyle.Bold),
                Location = new Point(100, 60),
                Width = 50,
                Height = 20
            };
            txtCardCvv = new TextBox
            {
                Location = new Point(100, 82),
                Width = 60,
                Height = 30,
                Font = new Font("HONOR Sans", 12),
                BorderStyle = BorderStyle.FixedSingle,
                MaxLength = 3,
                PasswordChar = '*'
            };
            txtCardCvv.KeyPress += (s, e) =>
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                    e.Handled = true;
            };

            panel.Controls.Add(lblCard);
            panel.Controls.Add(txtCardNumber);
            panel.Controls.Add(lblExpiry);
            panel.Controls.Add(txtCardExpiry);
            panel.Controls.Add(lblCvv);
            panel.Controls.Add(txtCardCvv);

            return panel;
        }

        private void InitializeComponent()
        {
            SetupUI();
        }
    }
}