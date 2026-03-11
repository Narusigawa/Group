using System;
using System.Windows.Forms;

namespace cond
{
    public partial class ProfilePage : UserControl
    {
        private User _currentUser;
        private Label lblTitle;
        private Label lblLogin;
        private TextBox txtLastName, txtFirstName, txtMiddleName, txtEmail, txtPhone, txtAddress;
        private Button btnEdit, btnCancelEdit, btnChangeAddress, btnCancelAddress;
        private bool _isEditMode = false, _isAddressEditMode = false;
        private string _originalLastName, _originalFirstName, _originalMiddleName,
                       _originalEmail, _originalPhone, _originalAddress;
        public event EventHandler LogoutRequested;

        public ProfilePage()
        {
            InitializeComponent();
        }

        public void LoadUser(User user)
        {
            _currentUser = user ?? throw new ArgumentNullException(nameof(user));
            lblLogin.Text = user.Login ?? "";
            txtLastName.Text = user.LastName ?? "";
            txtFirstName.Text = user.FirstName ?? "";
            txtMiddleName.Text = user.MiddleName ?? "";
            txtEmail.Text = user.Email ?? "";
            txtPhone.Text = user.Phone ?? "";
            txtAddress.Text = user.Address ?? "";

            _originalLastName = txtLastName.Text;
            _originalFirstName = txtFirstName.Text;
            _originalMiddleName = txtMiddleName.Text;
            _originalEmail = txtEmail.Text;
            _originalPhone = txtPhone.Text;
            _originalAddress = txtAddress.Text;

            SetEditMode(false);
            SetAddressEditMode(false);
        }

        private void SetEditMode(bool enable)
        {
            _isEditMode = enable;
            txtLastName.ReadOnly = !enable;
            txtFirstName.ReadOnly = !enable;
            txtMiddleName.ReadOnly = !enable;
            txtEmail.ReadOnly = !enable;
            txtPhone.ReadOnly = !enable;

            if (enable)
            {
                btnEdit.Text = "Сохранить";
                btnCancelEdit.Visible = true;
            }
            else
            {
                btnEdit.Text = "Редактировать";
                btnCancelEdit.Visible = false;
            }
        }

        private void SetAddressEditMode(bool enable)
        {
            _isAddressEditMode = enable;
            txtAddress.ReadOnly = !enable;

            if (enable)
            {
                btnChangeAddress.Text = "Сохранить адрес";
                btnCancelAddress.Visible = true;
            }
            else
            {
                btnChangeAddress.Text = "Изменить адрес";
                btnCancelAddress.Visible = false;
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (!_isEditMode)
            {
                SetEditMode(true);
            }
            else
            {
                bool success = DatabaseHelper.UpdateUserData(
                    _currentUser.Id,
                    txtLastName.Text,
                    txtFirstName.Text,
                    txtMiddleName.Text,
                    txtEmail.Text,
                    txtPhone.Text,
                    txtAddress.Text
                );

                if (success)
                {
                    _currentUser.LastName = txtLastName.Text;
                    _currentUser.FirstName = txtFirstName.Text;
                    _currentUser.MiddleName = txtMiddleName.Text;
                    _currentUser.Email = txtEmail.Text;
                    _currentUser.Phone = txtPhone.Text;
                    _currentUser.Address = txtAddress.Text;

                    MessageBox.Show("Данные обновлены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _originalLastName = txtLastName.Text;
                    _originalFirstName = txtFirstName.Text;
                    _originalMiddleName = txtMiddleName.Text;
                    _originalEmail = txtEmail.Text;
                    _originalPhone = txtPhone.Text;
                    _originalAddress = txtAddress.Text;

                    SetEditMode(false);
                    SetAddressEditMode(false);
                }
                else
                {
                    MessageBox.Show("Ошибка при сохранении", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnCancelEdit_Click(object sender, EventArgs e)
        {
            txtLastName.Text = _originalLastName;
            txtFirstName.Text = _originalFirstName;
            txtMiddleName.Text = _originalMiddleName;
            txtEmail.Text = _originalEmail;
            txtPhone.Text = _originalPhone;
            txtAddress.Text = _originalAddress;

            SetEditMode(false);
            SetAddressEditMode(false);
        }

        private void BtnChangeAddress_Click(object sender, EventArgs e)
        {
            if (!_isAddressEditMode)
            {
                SetAddressEditMode(true);
            }
            else
            {
                bool success = DatabaseHelper.UpdateUserData(
                    _currentUser.Id,
                    _currentUser.LastName,
                    _currentUser.FirstName,
                    _currentUser.MiddleName,
                    _currentUser.Email,
                    _currentUser.Phone,
                    txtAddress.Text
                );

                if (success)
                {
                    _currentUser.Address = txtAddress.Text;

                    MessageBox.Show("Адрес обновлён", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _originalAddress = txtAddress.Text;
                    SetAddressEditMode(false);
                }
                else
                {
                    MessageBox.Show("Ошибка при сохранении адреса", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnCancelAddress_Click(object sender, EventArgs e)
        {
            txtAddress.Text = _originalAddress;
            SetAddressEditMode(false);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите выйти?", "Выход",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                LogoutRequested?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}