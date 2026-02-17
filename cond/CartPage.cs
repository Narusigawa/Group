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
    public partial class CartPage : UserControl
    {
        public CartPage()
        {
            InitializeComponent();
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeColors.Background;

            Label lbl = new Label
            {
                Text = "Корзина\n(страница в разработке)",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            this.Controls.Add(lbl);
        }
    }
}
