using System;
using System.Drawing;
using System.Windows.Forms;

namespace cond
{
    partial class ProfilePage
    {
        private void SetupProfilePage()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeColors.Background;
            this.AutoScroll = true;
            this.MinimumSize = new Size(650, 400);

            var container = new Panel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent
            };

            lblTitle = new Label
            {
                Text = "Личный кабинет",
                Font = new Font("HONOR Sans", 36, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                Height = 100,
                TextAlign = ContentAlignment.MiddleCenter,
                Top = 130,
                Left = 0
            };
            container.Controls.Add(lblTitle);

            lblLogin = new Label
            {
                Font = new Font("HONOR Sans", 20, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter,
                Top = lblTitle.Bottom + 10,
                Left = 0
            };
            container.Controls.Add(lblLogin);

            var table = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                Padding = new Padding(0),
                BackColor = Color.Transparent,
                Top = lblLogin.Bottom + 20
            };

            Panel CreateField(string labelText, out TextBox textBox, bool readOnly = false, bool multiline = false, int height = 50)
            {
                var panel = new Panel
                {
                    Height = height + 30,
                    Width = 600,
                    BackColor = Color.Transparent,
                    Margin = new Padding(0, 5, 0, 5)
                };

                var lbl = new Label
                {
                    Text = labelText,
                    Font = new Font("HONOR Sans", 10),
                    ForeColor = Color.FromArgb(80, 80, 80),
                    Location = new Point(0, 0),
                    Width = panel.Width,
                    Height = 20
                };

                var tb = new TextBox
                {
                    Location = new Point(0, 22),
                    Width = panel.Width,
                    Height = height,
                    Font = new Font("HONOR Sans", 14),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White,
                    ReadOnly = readOnly,
                    Multiline = multiline
                };
                if (multiline) tb.ScrollBars = ScrollBars.Vertical;

                panel.Controls.Add(lbl);
                panel.Controls.Add(tb);
                textBox = tb;
                return panel;
            }

            var nameTable = new TableLayoutPanel
            {
                ColumnCount = 3,
                RowCount = 2,
                Width = 600,
                Height = 80,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 5, 0, 5)
            };
            nameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            nameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            nameTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            nameTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 25));
            nameTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 45));

            var lblLastName = new Label { Text = "Фамилия", Font = new Font("HONOR Sans", 10), ForeColor = Color.FromArgb(80, 80, 80), Dock = DockStyle.Fill, TextAlign = ContentAlignment.BottomLeft };
            nameTable.Controls.Add(lblLastName, 0, 0);
            var lblFirstName = new Label { Text = "Имя", Font = new Font("HONOR Sans", 10), ForeColor = Color.FromArgb(80, 80, 80), Dock = DockStyle.Fill, TextAlign = ContentAlignment.BottomLeft };
            nameTable.Controls.Add(lblFirstName, 1, 0);
            var lblMiddleName = new Label { Text = "Отчество", Font = new Font("HONOR Sans", 10), ForeColor = Color.FromArgb(80, 80, 80), Dock = DockStyle.Fill, TextAlign = ContentAlignment.BottomLeft };
            nameTable.Controls.Add(lblMiddleName, 2, 0);

            txtLastName = new TextBox { Font = new Font("HONOR Sans", 14), BorderStyle = BorderStyle.FixedSingle, BackColor = Color.White, ReadOnly = true, Dock = DockStyle.Fill };
            nameTable.Controls.Add(txtLastName, 0, 1);
            txtFirstName = new TextBox { Font = new Font("HONOR Sans", 14), BorderStyle = BorderStyle.FixedSingle, BackColor = Color.White, ReadOnly = true, Dock = DockStyle.Fill };
            nameTable.Controls.Add(txtFirstName, 1, 1);
            txtMiddleName = new TextBox { Font = new Font("HONOR Sans", 14), BorderStyle = BorderStyle.FixedSingle, BackColor = Color.White, ReadOnly = true, Dock = DockStyle.Fill };
            nameTable.Controls.Add(txtMiddleName, 2, 1);

            table.Controls.Add(nameTable);

            table.Controls.Add(CreateField("Электронная почта", out txtEmail));
            table.Controls.Add(CreateField("Телефон", out txtPhone));

            var editPanelWrapper = new Panel
            {
                Width = 600,
                Height = 60,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 5, 0, 5)
            };
            var editPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                BackColor = Color.Transparent
            };
            btnEdit = new Button
            {
                Text = "Редактировать",
                Size = new Size(200, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeColors.Accent,
                ForeColor = ThemeColors.Text,
                Font = new Font("HONOR Sans", 14, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };
            btnEdit.Click += BtnEdit_Click;
            btnCancelEdit = new Button
            {
                Text = "Отмена",
                Size = new Size(140, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightGray,
                ForeColor = ThemeColors.Text,
                Font = new Font("HONOR Sans", 14),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 },
                Visible = false
            };
            btnCancelEdit.Click += BtnCancelEdit_Click;
            editPanel.Controls.Add(btnEdit);
            editPanel.Controls.Add(btnCancelEdit);
            editPanelWrapper.Controls.Add(editPanel);
            editPanel.Location = new Point((editPanelWrapper.Width - editPanel.Width) / 2, 5);
            table.Controls.Add(editPanelWrapper);

            table.Controls.Add(CreateSeparator(600));

            table.Controls.Add(CreateField("Адрес доставки", out txtAddress, false, true, 80));

            var addressPanelWrapper = new Panel
            {
                Width = 600,
                Height = 55,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 5, 0, 5)
            };
            var addressPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                BackColor = Color.Transparent
            };
            btnChangeAddress = new Button
            {
                Text = "Изменить адрес",
                Size = new Size(200, 45),
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeColors.Accent,
                ForeColor = ThemeColors.Text,
                Font = new Font("HONOR Sans", 12, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };
            btnChangeAddress.Click += BtnChangeAddress_Click;
            btnCancelAddress = new Button
            {
                Text = "Отмена",
                Size = new Size(140, 45),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightGray,
                ForeColor = ThemeColors.Text,
                Font = new Font("HONOR Sans", 12),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 },
                Visible = false
            };
            btnCancelAddress.Click += BtnCancelAddress_Click;
            addressPanel.Controls.Add(btnChangeAddress);
            addressPanel.Controls.Add(btnCancelAddress);
            addressPanelWrapper.Controls.Add(addressPanel);
            addressPanel.Location = new Point((addressPanelWrapper.Width - addressPanel.Width) / 2, 5);
            table.Controls.Add(addressPanelWrapper);

            table.Controls.Add(CreateSeparator(600));

            var bottomPanelWrapper = new Panel
            {
                Width = 600,
                Height = 60,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 10, 0, 30)
            };
            var bottomPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                BackColor = Color.Transparent
            };
            var btnHistory = new Button
            {
                Text = "История покупок",
                Size = new Size(200, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = ThemeColors.Accent,
                ForeColor = ThemeColors.Text,
                Font = new Font("HONOR Sans", 14, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };
            btnHistory.Click += (s, e) =>
            {
                if (_currentUser == null) return;
                var parent = this.Parent as Panel;
                if (parent != null)
                {
                    parent.Controls.Clear();
                    parent.Controls.Add(new OrderHistoryPage(_currentUser));
                }
            };
            var btnLogout = new Button
            {
                Text = "Выйти из аккаунта",
                Size = new Size(220, 50),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(255, 100, 100),
                ForeColor = Color.White,
                Font = new Font("HONOR Sans", 14, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };
            btnLogout.Click += BtnLogout_Click;
            bottomPanel.Controls.Add(btnHistory);
            bottomPanel.Controls.Add(btnLogout);
            bottomPanelWrapper.Controls.Add(bottomPanel);
            bottomPanel.Location = new Point((bottomPanelWrapper.Width - bottomPanel.Width) / 2, 5);
            table.Controls.Add(bottomPanelWrapper);

            container.Controls.Add(table);
            this.Controls.Add(container);

            void CenterContent()
            {
                int containerWidth = this.ClientSize.Width - (this.VScroll ? SystemInformation.VerticalScrollBarWidth : 0);
                container.Width = containerWidth;

                lblTitle.Width = containerWidth;
                lblLogin.Width = containerWidth;

                table.Left = Math.Max(0, (containerWidth - table.Width) / 2);
            }

            this.Resize += (s, e) => CenterContent();
            CenterContent();
        }

        private Panel CreateSeparator(int width)
        {
            var sep = new Panel
            {
                Width = width,
                Height = 2,
                BackColor = Color.LightGray,
                Margin = new Padding(0, 5, 0, 5)
            };
            return sep;
        }

        private void InitializeComponent()
        {
            SetupProfilePage();
        }
    }
}