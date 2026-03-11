using System;
using System.Drawing;
using System.Windows.Forms;

namespace cond
{
    public partial class ProductDetailsPage : UserControl
    {
        private Product _product;
        private PictureBox pbImage;
        private Label lblName, lblDescription, lblPrice;
        private Button btnAddToCart, btnBack;
        private User _currentUser;

        public event EventHandler BackRequested;

        public ProductDetailsPage(Product product, User currentUser)
        {
            _product = product ?? throw new ArgumentNullException(nameof(product));
            _currentUser = currentUser;
            SetupUI();
            LoadProductData();
        }

        private void SetupUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeColors.Background;
            this.AutoScroll = false;
            this.MinimumSize = new Size(600, 700);
            this.Padding = new Padding(0, 120, 0, 0);

            var container = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(30),
                BackColor = Color.Transparent
            };

            container.ColumnStyles.Clear();
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 500));
            container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            var leftPanel = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
            pbImage = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            leftPanel.Controls.Add(pbImage);
            container.Controls.Add(leftPanel, 0, 0);

            var rightPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                AutoScroll = true
            };
            rightPanel.HorizontalScroll.Enabled = false;
            rightPanel.HorizontalScroll.Visible = false;

            var rightFlow = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent,
                Padding = new Padding(30, 0, 30, 0)
            };

            lblName = new Label
            {
                Font = new Font("HONOR Sans", 36, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 100,
                Margin = new Padding(0, 0, 0, 20),
                AutoSize = false
            };
            rightFlow.Controls.Add(lblName);

            lblDescription = new Label
            {
                Font = new Font("HONOR Sans", 18),
                ForeColor = Color.FromArgb(60, 60, 60),
                TextAlign = ContentAlignment.TopLeft,
                Margin = new Padding(0, 0, 0, 20),
                AutoSize = false
            };
            rightFlow.Controls.Add(lblDescription);

            lblPrice = new Label
            {
                Font = new Font("HONOR Sans", 32, FontStyle.Bold),
                ForeColor = ThemeColors.Accent,
                TextAlign = ContentAlignment.MiddleLeft,
                Height = 70,
                Margin = new Padding(0, 0, 0, 30),
                AutoSize = false
            };
            rightFlow.Controls.Add(lblPrice);

            btnAddToCart = new Button
            {
                Text = "🛒 Добавить в корзину",
                Font = new Font("HONOR Sans", 24, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeColors.Accent,
                ForeColor = ThemeColors.Text,
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 },
                Height = 90,
                Margin = new Padding(0, 0, 0, 20),
                AutoSize = false
            };
            btnAddToCart.Click += BtnAddToCart_Click;
            rightFlow.Controls.Add(btnAddToCart);

            btnBack = new Button
            {
                Text = "← Назад в каталог",
                Font = new Font("HONOR Sans", 20, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightGray,
                ForeColor = ThemeColors.Text,
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 },
                Height = 90,
                AutoSize = false
            };
            btnBack.Click += (s, e) => BackRequested?.Invoke(this, EventArgs.Empty);
            rightFlow.Controls.Add(btnBack);

            rightPanel.Controls.Add(rightFlow);

            rightPanel.Resize += (s, e) =>
            {
                int flowWidth = rightPanel.ClientSize.Width - rightFlow.Padding.Horizontal;
                if (rightPanel.VerticalScroll.Visible)
                    flowWidth -= SystemInformation.VerticalScrollBarWidth;

                if (flowWidth > 0)
                {
                    foreach (Control ctrl in rightFlow.Controls)
                    {
                        if (ctrl != lblDescription)
                            ctrl.Width = flowWidth;
                    }

                    if (!string.IsNullOrEmpty(lblDescription.Text))
                    {
                        using (Graphics g = lblDescription.CreateGraphics())
                        {
                            SizeF size = g.MeasureString(lblDescription.Text, lblDescription.Font, flowWidth);
                            lblDescription.Height = (int)Math.Ceiling(size.Height) + 5;
                            lblDescription.Width = flowWidth;
                        }
                    }
                }

                rightFlow.PerformLayout();

                if (rightFlow.Height < rightPanel.ClientSize.Height)
                {
                    rightFlow.Location = new Point(
                        Math.Max(0, (rightPanel.ClientSize.Width - rightFlow.Width) / 2),
                        Math.Max(0, (rightPanel.ClientSize.Height - rightFlow.Height) / 2)
                    );
                }
                else
                {
                    rightFlow.Location = new Point(
                        Math.Max(0, (rightPanel.ClientSize.Width - rightFlow.Width) / 2),
                        0
                    );
                }
            };

            container.Controls.Add(rightPanel, 1, 0);
            this.Controls.Add(container);

            this.Resize += (s, e) =>
            {
                if (this.Width < 800)
                {
                    container.ColumnCount = 1;
                    container.RowCount = 2;
                    container.RowStyles.Clear();
                    container.RowStyles.Add(new RowStyle(SizeType.Absolute, 400));
                    container.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                    container.SetColumn(leftPanel, 0);
                    container.SetRow(leftPanel, 0);
                    container.SetColumn(rightPanel, 0);
                    container.SetRow(rightPanel, 1);
                }
                else
                {
                    container.ColumnCount = 2;
                    container.RowCount = 1;
                    container.RowStyles.Clear();
                    container.SetColumn(leftPanel, 0);
                    container.SetRow(leftPanel, 0);
                    container.SetColumn(rightPanel, 1);
                    container.SetRow(rightPanel, 0);
                }
            };
        }

        private void LoadProductData()
        {
            lblName.Text = _product.Name;
            lblDescription.Text = string.IsNullOrEmpty(_product.Description) ? "Описание отсутствует" : _product.Description;
            lblPrice.Text = $"{_product.Price:F2} ₽";
            pbImage.Image = LoadImage(_product.ImageFilename);

            if (this.Controls[0] is TableLayoutPanel container &&
                container.GetControlFromPosition(1, 0) is Panel rightPanel)
            {
                rightPanel.PerformLayout();
                rightPanel.HorizontalScroll.Value = 0;
                rightPanel.HorizontalScroll.Visible = false;
            }
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
                catch
                {
                    return CreateDefaultImage();
                }
            }
            return CreateDefaultImage();
        }

        private Image CreateDefaultImage()
        {
            Bitmap bmp = new Bitmap(400, 400);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightPink);
                g.DrawString("🍰", new Font("HONOR Sans", 100), Brushes.White, 100, 100);
            }
            return bmp;
        }

        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("Чтобы добавить товар в корзину, войдите в систему.",
                                "Требуется авторизация",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                return;
            }

            bool result = DatabaseHelper.AddToCart(_currentUser.Id, _product.Id, 1);
            if (result)
                MessageBox.Show($"Товар \"{_product.Name}\" добавлен в корзину!", "Готово",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Не удалось добавить товар в корзину.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}