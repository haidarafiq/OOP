using MySql.Data.MySqlClient;
using System.Data;

namespace Latihan_OOP
{
    public partial class Form1 : Form
    {
        MySqlConnection koneksi = connection.getConnection();
        DataTable dataTable = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filldataTable();
        }
        public DataTable getDataSiswa()
        {
            dataTable.Reset();
            dataTable = new DataTable();
            using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM products", koneksi))
            {
                koneksi.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                dataTable.Load(reader);
            }
            return dataTable;

        }
        public void filldataTable()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.RowTemplate.Height = 150;
            dataGridView1.DataSource = getDataSiswa();


            Column1.DataPropertyName = "id";
            Column2.DataPropertyName = "nama_barang";
            Column3.DataPropertyName = "merk";
            Column4.DataPropertyName = "harga";
            Column5.DataPropertyName = "stok";
            Column6.DataPropertyName = "foto";
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // fungsi menghapus isi field
        public void clear()
        {
            id_produk.Clear();
            nama.Clear();
            merek.Clear();
            harga.Clear();
            stok.Clear();
            pictureBox2.Image = null;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void resetIncrement()
        {
            MySqlScript script = new MySqlScript(koneksi, "SET @ID := 0; UPDATE products SET id = @id := (@id+1); " + "ALTER TABLE products AUTO_INCREMENT = 1;");
            script.Execute();

        }

        // fungsi mencari data
        public void searchData(string ValueToFind)
        {
            string searchQuery = "SELECT * FROM products WHERE CONCAT (id, nama_barang, merk, harga, stok) LIKE '%" + ValueToFind + "%'";
            MySqlDataAdapter adapter = new MySqlDataAdapter(searchQuery, koneksi);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;
        }


        private void button4_Click(object sender, EventArgs e)
        {
            //MySqlConnection connection = new MySqlConnection("datasource+localhost;port3306;username=root;password=");
            //string insertQuery = "INSERT INTO test_db.users(id, Nama, Merek, Harga) VALUES('"+textBoxLName.Text+"')";
            //MySqlCommand command = new MySqlCommand(insertQuery, connection);

            MySqlCommand cmd;
            //conn.Open();
            try
            {
                // Convert image to byte array
                byte[] imageData;
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageData = ms.ToArray();
                }

                resetIncrement();
                cmd = koneksi.CreateCommand();
                cmd.CommandText = "INSERT INTO products(nama_barang, merk, harga, stok, foto) VALUE (@nama_barang, @merek, @harga, @stok, @foto)";
                cmd.Parameters.AddWithValue("@nama_barang", nama.Text);
                cmd.Parameters.AddWithValue("@merek", merek.Text);
                cmd.Parameters.AddWithValue("@harga", harga.Text);
                cmd.Parameters.AddWithValue("@stok", stok.Text);
                cmd.Parameters.AddWithValue("@foto", imageData);
                cmd.ExecuteNonQuery();
                // long id = cmd.LasInsertid;
                koneksi.Close();

                dataTable.Clear();
                filldataTable();
            }
            catch (MySqlException ex)
            {
               MessageBox.Show(ex.Message);
            }
            }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd;
            //conn.Open();
            try
            {
                // Convert image to byte array
                byte[] imageData;
                using (MemoryStream ms = new MemoryStream())
                {
                    pictureBox2.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    imageData = ms.ToArray();
                }

                resetIncrement();
                cmd = koneksi.CreateCommand();
                cmd.CommandText = "UPDATE products SET nama_barang = @nama_barang, merk = @merek, harga = @harga, harga = @harga, stok = @stok ,foto = @foto WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id_produk.Text);
                cmd.Parameters.AddWithValue("@nama_barang", nama.Text);
                cmd.Parameters.AddWithValue("@merek", id_produk.Text);
                cmd.Parameters.AddWithValue("@harga", harga.Text);
                cmd.Parameters.AddWithValue("@stok", stok.Text);
                cmd.Parameters.AddWithValue("@foto", imageData);
                cmd.ExecuteNonQuery();
                // long id = cmd.LasInsertid;
                koneksi.Close();

                dataTable.Clear();
                filldataTable();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex.ToString());
            id_produk.Text = dataGridView1.Rows[id].Cells[0].Value.ToString();
            nama.Text = dataGridView1.Rows[id].Cells[1].Value.ToString();
            merek.Text = dataGridView1.Rows[id].Cells[2].Value.ToString();
            harga.Text = dataGridView1.Rows[id].Cells[3].Value.ToString();
            stok.Text = dataGridView1.Rows[id].Cells[4].Value.ToString();
            // retrieve the BLOB image data for the clicked row and create an Image object
            Byte[] img = (Byte[])dataGridView1.CurrentRow.Cells[5].Value;
            MemoryStream ms = new MemoryStream(img);
            pictureBox2.Image = Image.FromStream(ms);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog showFD = new OpenFileDialog();
            if(showFD.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(showFD.FileName);
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            

        }

        private void btn_hapus_Click(object sender, EventArgs e)
        {
            MySqlCommand cmd;

            try
            {
                cmd = koneksi.CreateCommand();
                cmd.CommandText = "DELETE from products WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id_produk.Text);
                cmd.ExecuteNonQuery();

                resetIncrement();
                koneksi.Close();
                dataTable.Clear();
                filldataTable();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Data Gagal Dihapus = " + ex);
            }
        }

        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {
            searchData(textBox6.Text);
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }

 }
    
