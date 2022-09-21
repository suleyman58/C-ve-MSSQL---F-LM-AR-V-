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


namespace SQL___FİLM_ARŞİVİ
{

    public partial class Form1 : Form
     
    {
      
        public Form1()


        {
            InitializeComponent();

        }
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-OBB930O;Initial Catalog=film;Integrated Security=True");
        DataTable tablo = new DataTable();
        int row_number = 0;

        private void filmler_insert(string id,string ad, string tur, string sure, string vizyon)
        {
            try
            {
                SqlCommand insert_komut = new SqlCommand("insert into filmler values (@ıd,@ad,@tür,@süre,@vizyon)", baglanti);
                insert_komut.Parameters.AddWithValue("@ıd", id);
                insert_komut.Parameters.AddWithValue("@ad", ad);
                insert_komut.Parameters.AddWithValue("@tür", tur);
                insert_komut.Parameters.AddWithValue("@süre", sure);
                insert_komut.Parameters.AddWithValue("@vizyon", vizyon);

                baglanti.Open();
                insert_komut.ExecuteNonQuery();
                baglanti.Close();

                MessageBox.Show("kayıt eklendi");
            }
            catch (Exception e)
            {

                MessageBox.Show("hata" + e.Message);

            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            filmler_insert(textBox1.Text,textBox2.Text,textBox3.Text,textBox4.Text,textBox5.Text);


        }
       
        private void Listeleme(int film_id)
        {
            tablo.Clear();
            baglanti.Open();
            SqlCommand select_komut = new SqlCommand();
            select_komut.CommandText = "select * from filmler where filmID=@film_id";
            select_komut.Parameters.AddWithValue("@film_id", film_id);
            select_komut.Connection = baglanti;
            SqlDataAdapter adap = new SqlDataAdapter(select_komut);
            adap.Fill(tablo);



            label13.Text = tablo.Rows[0]["filmID"].ToString();
            label14.Text = tablo.Rows[0]["filmadi"].ToString();
            label15.Text = tablo.Rows[0]["filmtür"].ToString();
            label16.Text = tablo.Rows[0]["filmsüre"].ToString();
            label17.Text = tablo.Rows[0]["filmvizyon"].ToString();


            baglanti.Close();
        }

        private int get_filmID(int row_num)
        {

            baglanti.Open();
            int sonuc = 0;
            SqlCommand select_komut = new SqlCommand();
            select_komut.Connection = baglanti;
            select_komut.CommandText = "select filmID from vw_filmler where row_num=@row_number";
            select_komut.Parameters.AddWithValue("@row_number", row_num);
           

            

            SqlDataReader rd = select_komut.ExecuteReader();

            if (rd.HasRows)
            {
                rd.Read(); // read first row
                sonuc = rd.GetInt32(0);
                rd.Close();
                baglanti.Close();
                return sonuc;


            }
            else
            {
                rd.Close();
                baglanti.Close();
                return 0;

            }
            
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            int min_film_id=0;

            SqlCommand select_komut = new SqlCommand();
            select_komut.Connection = baglanti;
            select_komut.CommandText = "select min(filmID) from filmler";



            

            SqlDataReader rd= select_komut.ExecuteReader();

            if (rd.HasRows)
            {
                rd.Read(); // read first row
                min_film_id = rd.GetInt32(0);

            }
            rd.Close();
            baglanti.Close();

            Listeleme(min_film_id);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            int max_film_id = 0;

            SqlCommand select_komut = new SqlCommand();
            select_komut.Connection = baglanti;
            select_komut.CommandText = "select max(filmID) from filmler";



           

            SqlDataReader rd = select_komut.ExecuteReader();

            if (rd.HasRows)
            {
                rd.Read(); // read first row
                max_film_id = rd.GetInt32(0);

            }
            rd.Close();
            baglanti.Close();

            Listeleme(max_film_id);
            
        }
        


       


        private void button5_Click(object sender, EventArgs e)
        {

            row_number++;


            if (get_filmID(row_number)!=0)
            {

             Listeleme(get_filmID(row_number));

            }
            else
            {
                row_number--;

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            row_number--;


            if (get_filmID(row_number) != 0)
            {

                Listeleme(get_filmID(row_number));

            }
            else
            {
                row_number++;

            }

            


        }
    }
}
