namespace OrpakCase
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView = new DataGridView();
            txtSearch = new TextBox();
            label1 = new Label();
            txtItemName = new TextBox();
            textBox2 = new TextBox();
            txtInitPrice = new TextBox();
            txtQuantity = new TextBox();
            cmbCategory = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(211, 118);
            dataGridView.Name = "dataGridView";
            dataGridView.Size = new Size(554, 226);
            dataGridView.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(572, 55);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(100, 23);
            txtSearch.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(487, 59);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 2;
            label1.Text = "Arama Yap";
            // 
            // txtItemName
            // 
            txtItemName.Location = new Point(73, 161);
            txtItemName.Name = "txtItemName";
            txtItemName.Size = new Size(100, 23);
            txtItemName.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(73, 293);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 4;
            // 
            // txtInitPrice
            // 
            txtInitPrice.Location = new Point(73, 216);
            txtInitPrice.Name = "txtInitPrice";
            txtInitPrice.Size = new Size(100, 23);
            txtInitPrice.TabIndex = 5;
            // 
            // txtQuantity
            // 
            txtQuantity.Location = new Point(73, 245);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(100, 23);
            txtQuantity.TabIndex = 6;
            // 
            // cmbCategory
            // 
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(52, 85);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(121, 23);
            cmbCategory.TabIndex = 7;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(cmbCategory);
            Controls.Add(txtQuantity);
            Controls.Add(txtInitPrice);
            Controls.Add(textBox2);
            Controls.Add(txtItemName);
            Controls.Add(label1);
            Controls.Add(txtSearch);
            Controls.Add(dataGridView);
            Name = "Form2";
            Text = "Form2";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView;
        private TextBox txtSearch;
        private Label label1;
        private TextBox txtItemName;
        private TextBox textBox2;
        private TextBox txtInitPrice;
        private TextBox txtQuantity;
        private ComboBox cmbCategory;
    }
}