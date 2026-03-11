using System;
using System.Windows.Forms;

namespace cond
{
    public partial class MenuControl : UserControl
    {
        public event EventHandler<string> NavigationRequested;

        public MenuControl()
        {
            InitializeComponent();
        }
    }
}