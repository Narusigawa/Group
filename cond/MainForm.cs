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
    public partial class MainForm : Form
    {
        private HeaderControl headerControl;
        private MenuControl menuControl;
        private Panel pnlContent;
        private CatalogControl catalogControl;

        private bool isAuthenticated = false; // флаг для демо

        public MainForm()
        {
            InitializeComponent();
            SetupMainForm();
        }

        private void SetupMainForm()
        {
            // Настройки формы
            this.Text = "Кондитерская Sweet Delights";
            this.WindowState = FormWindowState.Maximized; // сразу на весь экран
            this.MinimumSize = new Size(900, 650);
            this.BackColor = ThemeColors.Background;

            // 1. Шапка
            headerControl = new HeaderControl();
            headerControl.MenuClick += ToggleMenu;
            headerControl.ProfileClick += OpenProfile;
            headerControl.CartClick += OpenCart;
            this.Controls.Add(headerControl);

            // 2. Боковое меню
            menuControl = new MenuControl();
            menuControl.NavigationRequested += Navigate;
            this.Controls.Add(menuControl);
            menuControl.BringToFront(); // чтобы меню было поверх контента

            // 3. Панель для контента (заполняет оставшееся место)
            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = ThemeColors.Background
            };
            this.Controls.Add(pnlContent);

            // 4. Создаём каталог и заполняем тестовыми товарами
            catalogControl = new CatalogControl();
            LoadSampleProducts();
            ShowPage(catalogControl);
        }

        // --- Обработчики событий ---

        private void ToggleMenu(object sender, EventArgs e)
        {
            menuControl.Visible = !menuControl.Visible;
        }

        private void Navigate(object sender, string pageKey)
        {
            switch (pageKey)
            {
                case "catalog":
                    ShowPage(catalogControl);
                    break;
                case "profile":
                    ShowPage(new ProfilePage());
                    break;
                case "about":
                    ShowPage(new AboutPage());
                    break;
                case "contacts":
                    ShowPage(new ContactsPage());
                    break;
            }
            // Закрываем меню после навигации
            menuControl.Visible = false;
        }

        private void OpenProfile(object sender, EventArgs e)
        {
            if (!isAuthenticated)
            {
                // Создаём и показываем форму авторизации
                using (AuthForm auth = new AuthForm())
                {
                    // Позиционируем окно под иконкой профиля
                    Point screenPoint = headerControl.PointToScreen(new Point(headerControl.Width - 120, headerControl.Height));
                    auth.Location = new Point(screenPoint.X - auth.Width + 40, screenPoint.Y + 5);

                    if (auth.ShowDialog() == DialogResult.OK)
                    {
                        isAuthenticated = true;
                        ShowPage(new ProfilePage());
                    }
                }
            }
            else
            {
                ShowPage(new ProfilePage());
            }
            menuControl.Visible = false;
        }

        private void OpenCart(object sender, EventArgs e)
        {
            ShowPage(new CartPage());
            menuControl.Visible = false;
        }

        // --- Вспомогательные методы ---
        private void ShowPage(UserControl page)
        {
            pnlContent.Controls.Clear();
            page.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(page);
        }

        private void LoadSampleProducts()
        {
            for (int i = 1; i <= 12; i++)
            {
                ProductCard card = new ProductCard();
                card.SetProduct($"Торт «Нежность» {i}", null); // null - будет заглушка
                catalogControl.AddProduct(card);
            }
        }
    }
}
