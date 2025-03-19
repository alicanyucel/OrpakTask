using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrpakCase
{
    public partial class Form2 : Form
    {
        private InventoryManagement inventory;
        public Form2()
        {
            InitializeComponent();
            inventory = new InventoryManagement();
            LoadCategories();
            LoadItems();
        }
        private void LoadCategories()
        {
            // Burada, kategorileri veritabanından veya önceden tanımlı bir kaynaktan yükleyebilirsiniz
            cmbCategory.Items.Add("Electronics");
            cmbCategory.Items.Add("Clothing");
            cmbCategory.Items.Add("Furniture");
            cmbCategory.SelectedIndex = 0; // İlk kategori seçili
        }
        private void LoadItems()
        {
            DataTable dt = inventory.GetAllItems();
            dataGridView.DataSource = dt;
        }

        // Yeni ürün ekleme
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string itemName = txtItemName.Text;
            int categoryId = cmbCategory.SelectedIndex + 1; // Kategoriyi ID'ye dönüştürme
            int quantity = int.Parse(txtQuantity.Text);
            decimal unitPrice = decimal.Parse(txtInitPrice.Text);

            inventory.AddItem(itemName, categoryId, quantity, unitPrice);
            LoadItems();
        }

        // Ürün güncelleme
        private void btnUpdateItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int itemId = int.Parse(dataGridView.SelectedRows[0].Cells["ItemID"].Value.ToString());
                string itemName = txtItemName.Text;
                int categoryId = cmbCategory.SelectedIndex + 1;
                int quantity = int.Parse(txtQuantity.Text);
                decimal unitPrice = decimal.Parse(txtInitPrice.Text);

                inventory.UpdateItem(itemId, itemName, categoryId, quantity, unitPrice);
                LoadItems();
            }
        }

        // Ürün silme
        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                int itemId = int.Parse(dataGridView.SelectedRows[0].Cells["ItemID"].Value.ToString());
                inventory.DeleteItem(itemId);
                LoadItems();
            }
        }

        // Ürün arama
        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            DataTable dt = inventory.SearchItems(keyword);
            dataGridView.DataSource = dt;
        }

        // CSV'ye dışa aktarım
        private void btnExportCSV_Click(object sender, EventArgs e)
        {
            DataTable dt = inventory.GetLowStockItems(10); 
            inventory.ExportLowStockToCSV(dt);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView.DataSource =inventory.GetAllItems();
        }
    }
}
   