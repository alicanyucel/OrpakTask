using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

public class InventoryManagement
{
    SqlConnection connection = new SqlConnection("Data Source=DESKTOP-L6NJT48\\SQLEXPRESS;Initial Catalog=IlkDers;Integrated Security=True");

    // Ensure the connection is open
    public void ConnectionKontrol()
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
    }

    // Yeni ürün ekleme
    public void AddItem(string itemName, int categoryId, int quantity, decimal unitPrice)
    {
        try
        {
            ConnectionKontrol();
            string query = "INSERT INTO Items (ItemName, CategoryID, Quantity, UnitPrice) VALUES (@ItemName, @CategoryID, @Quantity, @UnitPrice)";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ItemName", itemName);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            MessageBox.Show("An error occurred while adding the item: " + ex.Message);
        }
    }

    // Tüm ürünleri listeleme
    public DataTable GetAllItems()
    {
        try
        {
            ConnectionKontrol();
            string query = "SELECT I.ItemID, I.ItemName, C.CategoryName, I.Quantity, I.UnitPrice FROM Items I JOIN Categories C ON I.CategoryID = C.CategoryID";
            SqlDataAdapter da = new SqlDataAdapter(query, connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        catch (SqlException ex)
        {
            MessageBox.Show("An error occurred while fetching the items: " + ex.Message);
            return null;
        }
    }

    // Ürün güncelleme
    public void UpdateItem(int itemId, string itemName, int categoryId, int quantity, decimal unitPrice)
    {
        try
        {
            ConnectionKontrol();
            string query = "UPDATE Items SET ItemName = @ItemName, CategoryID = @CategoryID, Quantity = @Quantity, UnitPrice = @UnitPrice WHERE ItemID = @ItemID";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ItemID", itemId);
                cmd.Parameters.AddWithValue("@ItemName", itemName);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            MessageBox.Show("An error occurred while updating the item: " + ex.Message);
        }
    }

    // Ürün silme
    public void DeleteItem(int itemId)
    {
        try
        {
            ConnectionKontrol();
            string query = "DELETE FROM Items WHERE ItemID = @ItemID";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@ItemID", itemId);
                cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            MessageBox.Show("An error occurred while deleting the item: " + ex.Message);
        }
    }

    // Ürün arama
    public DataTable SearchItems(string keyword, int? categoryId = null)
    {
        try
        {
            ConnectionKontrol();
            string query = "SELECT I.ItemID, I.ItemName, C.CategoryName, I.Quantity, I.UnitPrice FROM Items I JOIN Categories C ON I.CategoryID = C.CategoryID WHERE I.ItemName LIKE @Keyword";
            if (categoryId.HasValue)
            {
                query += " AND C.CategoryID = @CategoryID";
            }

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                if (categoryId.HasValue)
                {
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId.Value);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        catch (SqlException ex)
        {
            MessageBox.Show("An error occurred while searching for the items: " + ex.Message);
            return null;
        }
    }

    // Düşük stok raporu
    public DataTable GetLowStockItems(int threshold)
    {
        try
        {
            ConnectionKontrol();
            string query = "SELECT I.ItemID, I.ItemName, I.Quantity FROM Items I WHERE I.Quantity < @Threshold";
            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Threshold", threshold);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        catch (SqlException ex)
        {
            MessageBox.Show("An error occurred while fetching low stock items: " + ex.Message);
            return null;
        }
    }

    // CSV dosyasına veri dışa aktarımı
    public void ExportLowStockToCSV(DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        foreach (DataColumn column in dt.Columns)
        {
            sb.Append(column.ColumnName + ",");
        }
        sb.AppendLine();

        foreach (DataRow row in dt.Rows)
        {
            foreach (var item in row.ItemArray)
            {
                sb.Append(item.ToString() + ",");
            }
            sb.AppendLine();
        }

        try
        {
            File.WriteAllText("LowStockReport.csv", sb.ToString());
            MessageBox.Show("CSV file has been created successfully!");
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred while exporting the data: " + ex.Message);
        }
    }

    // Stored Procedure ile ürün ekleme
    public void AddNewItemUsingStoredProc(string itemName, int categoryId, int quantity, decimal unitPrice)
    {
        try
        {
            ConnectionKontrol();
            using (SqlCommand cmd = new SqlCommand("AddNewItem", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ItemName", itemName);
                cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                cmd.ExecuteNonQuery();
            }
        }
        catch (SqlException ex)
        {
            MessageBox.Show("An error occurred while adding the item via stored procedure: " + ex.Message);
        }
    }
}
