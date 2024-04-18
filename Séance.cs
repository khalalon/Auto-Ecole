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
    public partial class Séance : Form
    {
        public Séance()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0I4VE1T;Initial Catalog=Auto Ecole;Persist Security Info=True;User ID=sa;Password=7355608k");
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Séances values(@Date, @Type, @Durée, @Prix, @Payée, @PrénomClient, @Moniteur, @CIN )", con);

            cmd.Parameters.AddWithValue("@Durée", textBox1.Text);
            cmd.Parameters.AddWithValue("@Prix", int.Parse(textBox2.Text));
            cmd.Parameters.AddWithValue("@PrénomClient", textBox4.Text);
            cmd.Parameters.AddWithValue("@Date", dateTimePicker1.Value.Date);
            cmd.Parameters.AddWithValue("@Type", comboBox1.Text);
            cmd.Parameters.AddWithValue("@Moniteur", comboBox2.Text);
            cmd.Parameters.AddWithValue("@Payée", comboBox3.Text);
            cmd.Parameters.AddWithValue("@CIN", textBox3.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Une Séance Ajouté !");
            // affichage dans le Datagrid
            SqlCommand cmd2 = new SqlCommand("select * from Séances", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Séance_Load(object sender, EventArgs e)
        {
            textBox1.Text = "1 Heure";
            comboBox3.SelectedItem = "Non";
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0I4VE1T;Initial Catalog=Auto Ecole;Persist Security Info=True;User ID=sa;Password=7355608k");
            SqlCommand cmd2 = new SqlCommand("select * from Séances", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                if (comboBox1.SelectedItem.ToString() == "Code")
                {
                    comboBox2.SelectedItem = "Chema";
                }
                else
                {
                    comboBox2.SelectedItem = "Mahdi";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 frm1 = new Form1();
            frm1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0I4VE1T;Initial Catalog=Auto Ecole;Persist Security Info=True;User ID=sa;Password=7355608k");

            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE Séances SET Payée = @Payée WHERE ID_Seance = @ID", con);

            cmd.Parameters.AddWithValue("@Payée", comboBox3.SelectedItem);
            cmd.Parameters.AddWithValue("@ID", dataGridView1.SelectedRows[0].Cells["ID_Seance"].Value);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Mise à jour avec succés");
            // affichage dans le Datagrid
            SqlCommand cmd2 = new SqlCommand("select * from Séances", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0I4VE1T;Initial Catalog=Auto Ecole;Persist Security Info=True;User ID=sa;Password=7355608k");
            SqlCommand cmd2 = new SqlCommand("SELECT *\r\nFROM Séances\r\nWHERE CAST(Date AS date) = CAST(GETDATE() AS date);\r\n", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0I4VE1T;Initial Catalog=Auto Ecole;Persist Security Info=True;User ID=sa;Password=7355608k");

            con.Open();
            SqlCommand cmd = new SqlCommand("DELETE From Séances WHERE ID_Seance = @ID", con);

            cmd.Parameters.AddWithValue("@ID", dataGridView1.SelectedRows[0].Cells["ID_Seance"].Value);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Suppression avec succés");
            // affichage dans le Datagrid
            SqlCommand cmd2 = new SqlCommand("select * from Séances", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
