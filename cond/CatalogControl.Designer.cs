using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace cond
{
    partial class CatalogControl
    {
        private const int HeaderHeight = 120;

        private void SetupCatalog()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeColors.Background;
            this.Padding = new Padding(0, HeaderHeight, 0, 0);

            var topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(255, 245, 235),
                Padding = new Padding(30, 20, 30, 20)
            };

            topPanel.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(255, 220, 210), 1))
                {
                    e.Graphics.DrawLine(pen, 0, topPanel.Height - 1, topPanel.Width, topPanel.Height - 1);
                }
            };

            var lblSort = new Label
            {
                Text = "Сортировать по цене:",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                AutoSize = true,
                Location = new Point(30, 24),
                BackColor = Color.Transparent
            };

            cmbSort = new RoundedComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                Width = 250,
                Height = 40,
                Location = new Point(lblSort.Right + 15, 20),
                BackColor = Color.White,
                ForeColor = ThemeColors.Text,
                FlatStyle = FlatStyle.Flat
            };

            cmbSort.Items.AddRange(new object[] {
                "✨ Без сортировки",
                "💰 По возрастанию цены ↑",
                "💰 По убыванию цены ↓"
            });
            cmbSort.SelectedIndex = 0;

            cmbSort.SelectedIndexChanged += (s, e) => ApplySort();

            topPanel.Controls.Add(lblSort);
            topPanel.Controls.Add(cmbSort);

            scrollPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = ThemeColors.Background,
                Padding = new Padding(0, 65, 0, 0)
            };

            tablePanel = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 3,
                Dock = DockStyle.Top,
                BackColor = Color.Transparent,
                Margin = new Padding(0)
            };

            for (int i = 0; i < 3; i++)
                tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));

            scrollPanel.Controls.Add(tablePanel);

            this.Controls.Add(scrollPanel);
            this.Controls.Add(topPanel);

            topPanel.BringToFront();

            tablePanel.Resize += (s, e) => AdjustRowHeights();
            this.Resize += (s, e) => AdjustRowHeights();

            this.Resize += (s, e) =>
            {
                topPanel.Width = this.ClientSize.Width;
                cmbSort.Location = new Point(lblSort.Right + 15, 20);
            };
        }

        private void InitializeComponent()
        {
            SetupCatalog();
        }
    }

    public class RoundedComboBox : ComboBox
    {
        private int _radius = 10;

        public RoundedComboBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(Parent?.BackColor ?? Color.White);

            using (var path = GetRoundedRect(ClientRectangle, _radius))
            using (var brush = new SolidBrush(BackColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            using (var pen = new Pen(Color.FromArgb(255, 210, 200), 1))
            using (var path = GetRoundedRect(ClientRectangle, _radius))
            {
                e.Graphics.DrawPath(pen, path);
            }

            if (SelectedItem != null)
            {
                string text = SelectedItem.ToString();
                using (var brush = new SolidBrush(ForeColor))
                {
                    e.Graphics.DrawString(text, Font, brush, new PointF(10, (Height - Font.Height) / 2));
                }
            }

            DrawArrow(e.Graphics);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            e.DrawBackground();

            using (var brush = new SolidBrush(e.State.HasFlag(DrawItemState.Selected) ? ThemeColors.Accent : e.BackColor))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }

            string text = Items[e.Index].ToString();
            using (var brush = new SolidBrush(e.State.HasFlag(DrawItemState.Selected) ? ThemeColors.Text : ForeColor))
            {
                e.Graphics.DrawString(text, Font, brush, e.Bounds.X + 5, e.Bounds.Y + 2);
            }

            e.DrawFocusRectangle();
        }

        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void DrawArrow(Graphics g)
        {
            int x = Width - 25;
            int y = Height / 2 - 3;

            using (var brush = new SolidBrush(Color.FromArgb(255, 150, 150)))
            {
                Point[] arrow = new Point[]
                {
                    new Point(x, y),
                    new Point(x + 10, y),
                    new Point(x + 5, y + 6)
                };
                g.FillPolygon(brush, arrow);
            }
        }
    }
}