using System.Drawing;
using System.Windows.Forms;

namespace cond
{
    partial class MainForm
    {
        private void SetupMainForm()
        {
            this.Text = "Кондитерская Sweet Delights";
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(900, 650);
            this.BackColor = ThemeColors.Background;

            headerControl = new HeaderControl();
            headerControl.MenuClick += ToggleMenu;
            headerControl.ProfileClick += OpenProfile;
            headerControl.CartClick += OpenCart;
            this.Controls.Add(headerControl);

            menuControl = new MenuControl();
            menuControl.NavigationRequested += Navigate;
            this.Controls.Add(menuControl);
            menuControl.BringToFront();

            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = ThemeColors.Background
            };
            this.Controls.Add(pnlContent);

            catalogControl = new CatalogControl();
            catalogControl.AddToCartClicked += OnAddToCart;
            catalogControl.ProductClicked += OnProductClicked;
            catalogControl.LoadProducts();
            ShowPage(catalogControl);
        }

        private void InitializeComponent()
        {
            SetupMainForm();
        }
    }
}