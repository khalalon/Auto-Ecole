using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Auto_Ecole
{
    public partial class Clients : Form
    {
        public Clients()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0I4VE1T;Initial Catalog=Auto Ecole;Persist Security Info=True;User ID=sa;Password=7355608k");
            con.Open();
            SqlCommand cmd = new SqlCommand("Insert into Clients values(@CIN, @NomClient, @PrenomClient, @Sexe, @Telephone)", con);

            cmd.Parameters.AddWithValue("@CIN", int.Parse(textBox1.Text));
            cmd.Parameters.AddWithValue("@NomClient", textBox2.Text);
            cmd.Parameters.AddWithValue("@PrenomClient", textBox3.Text);

            // Check which radio button is checked and set the value accordingly
            string sexe = radioButton1.Checked ? "Homme" : "Femme";
            cmd.Parameters.AddWithValue("@Sexe", sexe);

            cmd.Parameters.AddWithValue("@Telephone", textBox5.Text);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Un Client Ajouté !");
            // affichage dans le Datagrid
            SqlCommand cmd2 = new SqlCommand("select * from Clients", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            radioButton1.Checked = false; // Reset radio buttons
            radioButton2.Checked = false;
            textBox5.Text = "";
        }



        private void Clients_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0I4VE1T;Initial Catalog=Auto Ecole;Persist Security Info=True;User ID=sa;Password=7355608k");
            // affichage dans le Datagrid
            SqlCommand cmd2 = new SqlCommand("select * from Clients", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Fiche detailsForm = new Fiche();


                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string prenom = selectedRow.Cells["PRENOM"].Value.ToString();
                string nom = selectedRow.Cells["NOM"].Value.ToString();
                string cin = selectedRow.Cells["CIN"].Value.ToString();
                string sexe = selectedRow.Cells["Sexe"].Value.ToString();
                string telephone = selectedRow.Cells["Telephone"].Value.ToString();



                // Repeat this for all the columns you need

                // Pass these values to the fiche form
                detailsForm.SetLabel10Text(prenom);
                detailsForm.SetLabel9Text(nom);
                detailsForm.SetLabel11Text(cin);
                detailsForm.SetLabel12Text(sexe);
                detailsForm.SetLabel13Text(telephone);

                // Repeat this for all the values you need

                // Toggle the visibility of the DetailsForm
                detailsForm.Show();
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form = new Form1();
            form.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;

                Fiche detailsForm = new Fiche();

                DataGridViewRow selectedRow = dataGridView1.Rows[selectedRowIndex];
                string prenom = selectedRow.Cells["PRENOM"].Value.ToString();
                string nom = selectedRow.Cells["NOM"].Value.ToString();
                string cin = selectedRow.Cells["CIN"].Value.ToString();
                string sexe = selectedRow.Cells["Sexe"].Value.ToString();
                string telephone = selectedRow.Cells["Telephone"].Value.ToString();

                using (SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0I4VE1T;Initial Catalog=Auto Ecole;Persist Security Info=True;User ID=sa;Password=7355608k"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT SUM(Prix) FROM Séances WHERE Payée = 'Non' AND CIN = @CIN", con);
                    SqlCommand cmd1 = new SqlCommand("SELECT count(ID_Seance) FROM Séances WHERE CIN = @CIN", con);
                    SqlCommand cmd2 = new SqlCommand("SELECT count(ID_Seance) FROM Séances WHERE Payée = 'Non' AND CIN = @CIN", con);

                    cmd.Parameters.AddWithValue("@CIN", cin);
                    cmd1.Parameters.AddWithValue("@CIN", cin);
                    cmd2.Parameters.AddWithValue("@CIN", cin);



                    object result = cmd.ExecuteScalar();
                    object count = cmd1.ExecuteScalar();
                    object res = cmd2.ExecuteScalar();


                    if (result != null && result != DBNull.Value)
                    {
                        decimal sumPrix = Convert.ToDecimal(result);
                        detailsForm.SetLabel16Text(sumPrix.ToString() + " DT");
                        decimal counts = Convert.ToDecimal(count);
                        detailsForm.SetLabel14Text(counts.ToString() + " Séances");
                        decimal count1 = Convert.ToDecimal(res);
                        detailsForm.SetLabel15Text(count1.ToString() + " Séances");
                    }
                    else
                    {
                        MessageBox.Show("No unpaid sessions found for the selected client.");
                    }
                }

                detailsForm.SetLabel10Text(prenom);
                detailsForm.SetLabel9Text(nom);
                detailsForm.SetLabel11Text(cin);
                detailsForm.SetLabel12Text(sexe);
                detailsForm.SetLabel13Text(telephone);

                detailsForm.Show();

            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client à afficher.");
            }
        }

    }
}
