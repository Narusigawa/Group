using System.Drawing;
using System.Windows.Forms;

namespace cond
{
    partial class AboutPage
    {
        private void InitializeComponent()
        {
            this.BackColor = ThemeColors.Background;
            this.AutoScroll = true;

            int leftMargin = 150;
            int topOffset = 120;

            Label titleLabel = new Label
            {
                Text = "О нас",
                Font = new Font("HONOR Sans", 48, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                Location = new Point(leftMargin, topOffset),
                AutoSize = true
            };
            this.Controls.Add(titleLabel);

            Panel underline = new Panel
            {
                Location = new Point(leftMargin, titleLabel.Bottom + 10),
                Size = new Size(120, 4),
                BackColor = Color.FromArgb(255, 120, 150)
            };
            this.Controls.Add(underline);

            int currentY = underline.Bottom + 50;

            Label historyTitle = new Label
            {
                Text = "🍰 Наша история",
                Font = new Font("HONOR Sans", 28, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                Location = new Point(leftMargin, currentY),
                AutoSize = true
            };
            this.Controls.Add(historyTitle);

            Label historyText = new Label
            {
                Text = "Добро пожаловать в Sweet Delights! Мы – небольшая кондитерская,\r\n" +
                      "которая начала свой путь в 2026 году с маленькой домашней кухни. Сейчас мы\r\n" +
                      "радуем своими десертами сотни клиентов, и вкладываем душу в каждое изделие.\r\n\r\n" +
                      "Наши торты, пирожные и капкейки пекутся только из натуральных продуктов,\r\n" +
                      "без консервантов, искусственных красителей и пальмового масла. Мы заботимся\r\n" +
                      "о вашем здоровье и дарим настоящий вкус детства!",
                Font = new Font("HONOR Sans", 16, FontStyle.Regular),
                ForeColor = Color.FromArgb(80, 80, 80),
                Location = new Point(leftMargin, historyTitle.Bottom + 20),
                AutoSize = true
            };
            this.Controls.Add(historyText);

            currentY = historyText.Bottom + 60;

            Label advantagesTitle = new Label
            {
                Text = "🧁 Наши преимущества",
                Font = new Font("HONOR Sans", 28, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                Location = new Point(leftMargin, currentY),
                AutoSize = true
            };
            this.Controls.Add(advantagesTitle);

            string[] advantages = {
                "✓ Натуральные ингредиенты без консервантов",
                "✓ Ручная работа и уникальные рецепты",
                "✓ Бесплатная доставка от 2000₽",
                "✓ Подарочная упаковка для каждого заказа",
            };

            int advantageY = advantagesTitle.Bottom + 20;
            for (int i = 0; i < advantages.Length; i++)
            {
                Label advantageItem = new Label
                {
                    Text = advantages[i],
                    Font = new Font("HONOR Sans", 16, FontStyle.Regular),
                    ForeColor = Color.FromArgb(60, 60, 60),
                    Location = new Point(leftMargin + 20, advantageY + i * 35),
                    AutoSize = true
                };
                this.Controls.Add(advantageItem);
            }

            currentY = advantageY + advantages.Length * 35 + 50;

            Label philosophyTitle = new Label
            {
                Text = "✨ Наша философия",
                Font = new Font("HONOR Sans", 28, FontStyle.Bold),
                ForeColor = ThemeColors.Text,
                Location = new Point(leftMargin, currentY),
                AutoSize = true
            };
            this.Controls.Add(philosophyTitle);

            Label philosophyText = new Label
            {
                Text = "❝ Мы верим, что сладости должны приносить не только удовольствие,\r\n" +
                      "но и пользу. Поэтому каждый ингредиент проходит тщательный отбор,\r\n" +
                      "а каждое пирожное создаётся с любовью и вниманием к деталям.\r\n" +
                      "Для нас важно видеть улыбки наших клиентов! ❞",
                Font = new Font("HONOR Sans", 18, FontStyle.Italic),
                ForeColor = Color.FromArgb(100, 100, 100),
                Location = new Point(leftMargin + 40, philosophyTitle.Bottom + 20),
                AutoSize = true
            };
            this.Controls.Add(philosophyText);

            this.AutoScrollMinSize = new Size(0, philosophyText.Bottom + 100);
        }
    }
}