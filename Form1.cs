namespace Auto_Ecole
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Hide the current form (assuming this code is in a form class)
            this.Hide();

            // Create an instance of the "Clients" form
            Clients clientsForm = new Clients();

            // Show the "Clients" form
            clientsForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Séance séance = new Séance();
            séance.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Examen ex = new Examen();
            ex.Show();
        }
    }
}