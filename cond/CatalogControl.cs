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
    public partial class CatalogControl : UserControl
    {
        private Panel scrollPanel;
        private TableLayoutPanel tablePanel;

        public CatalogControl()
        {
            InitializeComponent();
            SetupCatalog();
        }

        private void SetupCatalog()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ThemeColors.Background;

            // Панель с прокруткой (занимает всё место)
            scrollPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = ThemeColors.Background,
                Padding = new Padding(0, 600, 0, 0)   // ← отступ сверху 600px, чтобы карточки не прилипали к шапке
            };

            // Таблица с тремя колонками
            tablePanel = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 3,
                Dock = DockStyle.Top,          // таблица растёт вниз, но не заполняет всю панель
                BackColor = ThemeColors.Background,
                Margin = new Padding(0)         // убираем лишние отступы
            };

            // Колонки равной ширины
            for (int i = 0; i < 3; i++)
            {
                tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            }

            scrollPanel.Controls.Add(tablePanel);
            this.Controls.Add(scrollPanel);

            // При изменении размеров таблицы пересчитываем высоту строк
            tablePanel.Resize += (s, e) => AdjustRowHeights();
            // Также пересчитываем при изменении размеров самого контрола (окна)
            this.Resize += (s, e) => AdjustRowHeights();
        }

        public void AddProduct(ProductCard card)
        {
            card.Dock = DockStyle.Fill;
            card.Margin = new Padding(10);   // отступы между карточками

            int rowCount = tablePanel.RowCount;
            int colCount = tablePanel.ColumnCount; // всегда 3

            if (rowCount == 0)
            {
                tablePanel.RowCount = 1;
                tablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 250));
                rowCount = 1;
            }

            // Ищем свободную ячейку в последней строке
            bool added = false;
            for (int col = 0; col < colCount; col++)
            {
                if (tablePanel.GetControlFromPosition(col, rowCount - 1) == null)
                {
                    tablePanel.Controls.Add(card, col, rowCount - 1);
                    added = true;
                    break;
                }
            }

            // Если строка заполнена – создаём новую
            if (!added)
            {
                tablePanel.RowCount++;
                tablePanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 250));
                tablePanel.Controls.Add(card, 0, rowCount);
            }

            AdjustRowHeights();
        }

        private void AdjustRowHeights()
        {
            if (tablePanel.RowCount == 0) return;

            // Получаем реальную ширину первой колонки (все равны)
            int[] colWidths = tablePanel.GetColumnWidths();
            if (colWidths.Length == 0) return;
            int colWidth = colWidths[0];

            // Желаемая высота строки: чуть меньше, чем ширина (чтобы карточки были компактнее)
            int desiredHeight = (int)(colWidth * 0.9);  // ← здесь 0.9 – коэффициент размера. Можешь менять.

            // Применяем ко всем строкам
            for (int i = 0; i < tablePanel.RowStyles.Count; i++)
            {
                tablePanel.RowStyles[i].SizeType = SizeType.Absolute;
                tablePanel.RowStyles[i].Height = desiredHeight;
            }

            // Обновляем таблицу
            tablePanel.PerformLayout();

            // ВАЖНО: принудительно задаём минимальный размер прокрутки,
            // чтобы AutoScroll сработал, когда таблица выше панели
            scrollPanel.AutoScrollMinSize = new Size(0, tablePanel.Height);
        }

        public void Clear()
        {
            tablePanel.Controls.Clear();
            tablePanel.RowCount = 0;
            tablePanel.RowStyles.Clear();
        }
    }
}
