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

namespace ZoranKovacevizZadatak
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            DataRow dr;
            Konekcija kon = new Konekcija();
            kon.OtvoriKonekciju();

            SqlCommand cmd = new SqlCommand("Select * from Person", kon.GetConnection());

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dr = dt.NewRow();
            dr.ItemArray = new object[] { 0, "--Izaberite personu--" };
            dt.Rows.InsertAt(dr, 0);

            comboBox1.ValueMember = "IDPerson";
            comboBox1.DisplayMember = "FirstName";

            comboBox1.DataSource = dt; //kljucno vezivanje combobox-a i Data Table
            kon.ZatvoriKonekciju();

        }

        public void PopuniFriendsOfFriends(int ID) {

            Konekcija kon = new Konekcija();

            SqlCommand command = new SqlCommand();
            command.Connection = kon.GetConnection();
            command.CommandText = "FriendsOfFriends";
            command.CommandType = CommandType.StoredProcedure;


            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@IDperson";
            parameter.SqlDbType = SqlDbType.Int;
            parameter.Direction = ParameterDirection.Input;
            parameter.Value = ID;

            command.Parameters.Add(parameter);

            kon.OtvoriKonekciju();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    listBox2.Items.Add(reader[1].ToString() + " " + reader[2].ToString());
                }
            }
            else
            {
                Console.WriteLine("Nema redova");
            }
            reader.Close();

            kon.ZatvoriKonekciju();

        
        }

        public void PopuniPrijatelje(int ID) {

            Konekcija kon = new Konekcija();
           
        SqlCommand command = new SqlCommand();
        command.Connection = kon.GetConnection();

        command.CommandText = "Sel_FriendsOfPerson";
        command.CommandType = CommandType.StoredProcedure;


        SqlParameter parameter = new SqlParameter();
        parameter.ParameterName = "@IDperson";
        parameter.SqlDbType = SqlDbType.Int;
        parameter.Direction = ParameterDirection.Input;
        parameter.Value = ID;

        command.Parameters.Add(parameter);

          kon.OtvoriKonekciju();
        SqlDataReader reader = command.ExecuteReader();

        if (reader.HasRows)
        {
            while (reader.Read())
            {
                  listBox1.Items.Add(reader[1].ToString() + " "+reader[2].ToString());
            }
        }
        else
        {
            Console.WriteLine("Nema redova");
        }
        reader.Close();

        kon.ZatvoriKonekciju();
    }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            PopuniPrijatelje(Convert.ToInt32(comboBox1.SelectedValue));
            PopuniFriendsOfFriends(Convert.ToInt32(comboBox1.SelectedValue));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    
}
}


