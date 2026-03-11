using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace cond
{
    partial class ProductCard
    {
        private void SetupControl()
        {
            this.BackColor = ThemeColors.Card;
            this.Size = new Size(200, 250);

            var topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(200, 255, 255, 255)
            };

            lblName = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("HONOR Sans", 12, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                Padding = new Padding(5)
            };
            topPanel.Controls.Add(lblName);

            pbImage = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            var bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 90,
                BackColor = Color.Transparent
            };

            var priceContainer = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.Transparent,
                Padding = new Padding(10, 5, 10, 5)
            };

            var priceBackground = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(30, ThemeColors.Accent),
                Region = CreateRoundedRegion(priceContainer.Width - 20, 30, 15)
            };

            lblPrice = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("HONOR Sans", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80),
                Text = "0 ₽",
                BackColor = Color.Transparent
            };

            priceBackground.Controls.Add(lblPrice);
            priceContainer.Controls.Add(priceBackground);

            priceContainer.Resize += (s, e) =>
            {
                priceBackground.Region = CreateRoundedRegion(priceContainer.Width - 20, 30, 15);
            };

            var btnPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 45,
                BackColor = Color.Transparent,
                Padding = new Padding(10, 0, 10, 10)
            };

            btnAddToCart = new Button
            {
                Text = "🛒 Добавить",
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeColors.Accent,
                ForeColor = Color.White,
                Font = new Font("HONOR Sans", 10, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 },
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnAddToCart.Click += (s, e) => AddToCartClicked?.Invoke(this, _product);

            btnAddToCart.Paint += (s, e) => RoundControl(btnAddToCart, 15);

            btnPanel.Controls.Add(btnAddToCart);

            bottomPanel.Controls.Add(btnPanel);
            bottomPanel.Controls.Add(priceContainer);

            this.Controls.Add(pbImage);
            this.Controls.Add(topPanel);
            this.Controls.Add(bottomPanel);

            this.Click += (s, e) => ProductClicked?.Invoke(this, _product);
            pbImage.Click += (s, e) => ProductClicked?.Invoke(this, _product);
            lblName.Click += (s, e) => ProductClicked?.Invoke(this, _product);
            lblPrice.Click += (s, e) => ProductClicked?.Invoke(this, _product);
            topPanel.Click += (s, e) => ProductClicked?.Invoke(this, _product);
            bottomPanel.Click += (s, e) => ProductClicked?.Invoke(this, _product);

            this.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.LightGray, 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
                }
            };
        }

        private Region CreateRoundedRegion(int width, int height, int radius)
        {
            using (var path = new GraphicsPath())
            {
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(width - radius, 0, radius, radius, 270, 90);
                path.AddArc(width - radius, height - radius, radius, radius, 0, 90);
                path.AddArc(0, height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                return new Region(path);
            }
        }

        private void RoundControl(Control ctrl, int radius)
        {
            using (var path = new GraphicsPath())
            {
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(ctrl.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(ctrl.Width - radius, ctrl.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, ctrl.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                ctrl.Region = new Region(path);
            }
        }

        private void InitializeComponent()
        {
            SetupControl();
        }
    }
}