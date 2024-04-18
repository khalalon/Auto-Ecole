using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Auto_Ecole
{
    public partial class Examen : Form
    {
        public Examen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0I4VE1T;Initial Catalog=Auto Ecole;Persist Security Info=True;User ID=sa;Password=7355608k");
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Examens values(@Date, @Type, @Prix, @Payée, @PrénomClient, @CIN)", con);

            cmd.Parameters.AddWithValue("@Prix", int.Parse(textBox2.Text));
            cmd.Parameters.AddWithValue("@PrénomClient", textBox4.Text);
            cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value.Date);
            cmd.Parameters.AddWithValue("@Type", comboBox1.Text);
            cmd.Parameters.AddWithValue("@Payée", comboBox3.Text);
            cmd.Parameters.AddWithValue("@CIN", textBox1.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Un Examen Ajouté !");
            // affichage dans le Datagrid
            SqlCommand cmd2 = new SqlCommand("select * from Examens", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            textBox2.Text = "";
            textBox4.Text = "";
        }

        private void Examen_Load(object sender, EventArgs e)
        {
            comboBox3.SelectedItem = "Non";
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0I4VE1T;Initial Catalog=Auto Ecole;Persist Security Info=True;User ID=sa;Password=7355608k");
            SqlCommand cmd2 = new SqlCommand("select * from Examens", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0I4VE1T;Initial Catalog=Auto Ecole;Persist Security Info=True;User ID=sa;Password=7355608k");

            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Examens SET Payée = @Payée WHERE ID_Examen = @ID", con);

            cmd.Parameters.AddWithValue("@Payée", comboBox3.SelectedItem);
            cmd.Parameters.AddWithValue("@ID", dataGridView1.SelectedRows[0].Cells["ID_Examen"].Value);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Mise à jour avec succés");
            // affichage dans le Datagrid
            SqlCommand cmd2 = new SqlCommand("select * from Examens", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0I4VE1T;Initial Catalog=Auto Ecole;Persist Security Info=True;User ID=sa;Password=7355608k");
            SqlCommand cmd2 = new SqlCommand("SELECT *\r\nFROM Examens\r\nWHERE CAST(Date AS date) = CAST(GETDATE() AS date);\r\n", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 frm1 = new Form1();
            frm1.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0I4VE1T;Initial Catalog=Auto Ecole;Persist Security Info=True;User ID=sa;Password=7355608k");

            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE From Examens WHERE ID_Examen = @ID", con);

            cmd.Parameters.AddWithValue("@ID", dataGridView1.SelectedRows[0].Cells["ID_Examen"].Value);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Suppression avec succés");
            // affichage dans le Datagrid
            SqlCommand cmd2 = new SqlCommand("select * from Examens", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
