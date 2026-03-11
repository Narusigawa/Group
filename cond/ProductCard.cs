using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace cond
{
    public partial class ProductCard : UserControl
    {
        private PictureBox pbImage;
        private Label lblName;
        private Label lblPrice;
        private Button btnAddToCart;
        private Product _product;

        public event EventHandler<Product> AddToCartClicked;
        public event EventHandler<Product> ProductClicked;

        public ProductCard()
        {
            InitializeComponent();
        }

        public void SetProduct(Product product)
        {
            _product = product;
            lblName.Text = product.Name;
            lblPrice.Text = $"{product.Price:F2} ₽";
            pbImage.Image = LoadImage(product.ImageFilename);
        }

        private Image LoadImage(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return CreateDefaultImage();

            string path = Path.Combine(Application.StartupPath, "Images", filename);
            if (File.Exists(path))
            {
                try
                {
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
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
            Bitmap bmp = new Bitmap(150, 150);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.FromArgb(240, 240, 240));

                using (var font = new Font("HONOR Sans", 40))
                using (var brush = new SolidBrush(Color.FromArgb(200, ThemeColors.Accent)))
                {
                    g.DrawString("🍰", font, brush, 35, 35);
                }
            }
            return bmp;
        }

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