using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cond
{
    public partial class ProductCard : UserControl
    {
        private PictureBox pbImage;
        private Label lblName;

        public ProductCard()
        {
            InitializeComponent(); // оставляем, он нужен
            SetupControl();
        }

        private void SetupControl()
        {
            this.BackColor = ThemeColors.Card;

            pbImage = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            lblName = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(180, 255, 255, 255),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ThemeColors.Text
            };

            this.Controls.Add(pbImage);
            this.Controls.Add(lblName);

            this.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                    Color.LightGray, 1, ButtonBorderStyle.Solid,
                    Color.LightGray, 1, ButtonBorderStyle.Solid,
                    Color.LightGray, 1, ButtonBorderStyle.Solid,
                    Color.LightGray, 1, ButtonBorderStyle.Solid);
            };
        }

        // Метод для заполнения данными
        public void SetProduct(string name, Image image)
        {
            lblName.Text = name;
            pbImage.Image = image ?? CreateDefaultImage(); // если картинки нет – заглушка
        }

        // Заглушка, если не передали картинку
        private Image CreateDefaultImage()
        {
            Bitmap bmp = new Bitmap(100, 100);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.LightPink);
                g.DrawString("🍰", new Font("Segoe UI", 30), Brushes.White, 25, 25);
            }
            return bmp;
        }

        // --- Анимация при наведении ---
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.BackColor = ThemeColors.CardHover;
            this.Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.BackColor = ThemeColors.Card;
            this.Refresh();
        }
    }
}

