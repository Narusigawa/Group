using System;
using System.Windows.Forms;

namespace cond
{
    public partial class AuthForm : Form
    {
        private TextBox txtLogin;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblRegisterLink;

        public User CurrentUser { get; private set; }

        public AuthForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User user = DatabaseHelper.Login(login, password);
            if (user != null)
            {
                CurrentUser = user;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LblRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (RegisterForm registerForm = new RegisterForm())
            {
                registerForm.StartPosition = FormStartPosition.CenterParent;
                registerForm.ShowDialog(this);
            }
            this.Show();
            this.BringToFront();
        }
    }
}