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
using MySqlConnector;

namespace Prot1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string DADOS_CONEXAO = "server=localhost;user=root;password=;database=;";
        private void btnSave_Click(object sender, EventArgs e)
        {
            string nome = txtNome.Text;
            string idade = txtIdade.Text;
            string raca = cbRaca.Text;
            string pelagem = cbPelagem.Text;
            string gene = cbGenero.Text;

            MessageBox.Show($"O nome do gato é {nome}, da raça {raca}, com a pelagem {pelagem} foi salvo com {idade} anos deidade. O gato é {gene}");



            int controleLinhasAfetadas = 0;


            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO))
            {
                conn.Open();
                string scriptInsert = "INSERT INTO tb_gatos (nome) VALUE (@nome)";

                using (MySqlCommand comando = new MySqlCommand(scriptInsert, conn))
                {
                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@idade", idade);
                    comando.Parameters.AddWithValue("@raca", raca);
                    comando.Parameters.AddWithValue("@pelagem", pelagem);
                    comando.Parameters.AddWithValue("@gene", gene);
                    controleLinhasAfetadas = comando.ExecuteNonQuery();

                    comando.ExecuteNonQuery();
                }
            }
            //MysqlConnection

            if (controleLinhasAfetadas > 0)
            {
                MessageBox.Show("Gato cadastrado com sucesso!");
            }
            else
            {
                MessageBox.Show("Ops. parece que os novelos estão bagunçados!");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnConsut_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO))
            {
                conn.Open();
                string scriptConsultaIndividual = "SELECT * FROM tb_gatos WHERE id = @id";
                using (MySqlCommand comando = new MySqlCommand(scriptConsultaIndividual, conn))
                {
                    MySqlDataReader reader = comando.ExecuteReader();
                    var dadosResultado = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        lbIdResultado.Tesxt = dadosResultado["id"].ToString();
                        lbNomeResultado.Text = dadosResultado["nome"].ToString();
                        lbRacaResultado.Text = dadosResultado["raca"].ToString();
                        lbPelageResultado.Text = dadosResultado["pelagem"].ToString();
                        lbGeneResultado.Text = dadosResultado["gene"].ToString();
                    }
                }







                conn.Close();
            }
        }

        private void btnConsutList_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO))
            {
                conn.Open();
                string campoPelagem = cbPelagem.Text;
                string scriptConsultaLista = "SELECT * FROM tb_gatos";
                    using (MySqlCommand comando = new MySqlCommand(scriptConsultaLista, conn))
                    {
                        MySqlDataAdapiter resultadoConsultaMySql = new MySqlDataAdapiter(comando);
                        
                        DataTable dt = new DataTable();
                        resultadoConsultaMySql.Fill(dt);
                        dgvListarTudo.DataSource = dt;
                        
                        
                    }
                conn.Close();
            }
        }
    }
}