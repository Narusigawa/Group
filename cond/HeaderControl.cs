using System;
using System.Windows.Forms;

namespace cond
{
    public partial class HeaderControl : UserControl
    {
        public event EventHandler MenuClick;
        public event EventHandler ProfileClick;
        public event EventHandler CartClick;

        private Button btnMenu;
        private Label lblTitle;
        private Button btnProfile;
        private Button btnCart;

        public HeaderControl()
        {
            InitializeComponent();
        }
    }
}