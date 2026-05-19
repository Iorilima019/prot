using MySql.Data.MySqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

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
            string id = txtIdade.Text;
            string nome = txtNome.Text;
            string idade = txtIdade.Text;
            string raca = cbRaca.Text;
            string gene = rabGenero.Text;
            string cast= cbCastro.Text;
            

            MessageBox.Show($"O nome do gato é {nome}, da raça {raca}, foi salvo com {idade} anos deidade. O gato é {gene} e {cast} castrado.");



            int controleLinhasAfetadas = 0;


            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO))
            {
                conn.Open();
                string scriptInsert = "INSERT INTO tb_gatito (nome) VALUE (@nome)";

                using (MySqlCommand comando = new MySqlCommand(scriptInsert, conn))
                {
                    comando.Parameters.AddWithValue("@nome", nome);
                    comando.Parameters.AddWithValue("@idade", idade);
                    comando.Parameters.AddWithValue("@raca", raca);
                    comando.Parameters.AddWithValue("@gen", gene);
                    comando.Parameters.AddWithValue("@cast",cast);
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
                        lbGeneResultado.Text = dadosResultado["gene"].ToString();
                        lbPelageResultado.Text = dadosResultado["cast"].ToString();
                        
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string campoId = TextId.txt;
            int controleLinhasAfetadas = 0;
            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO))
            {
                conn.Open();
                string scriptDelete = "DELETE FROM tb_gatito" +
                    "WHERE id = @id";

                using (MySqlCommand comando = new MySqlCommand(scriptDelete, conn))
                {
                    comando.Parameters.AddWithValue("@id", campoId);

                    controleLinhasAfetadas = comando.ExecuteNonQuery();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string campoId = TextId.txt;
            string campoNome = campoNome.text;
            string campoRaca = campoRaca.text;
            string campoIdade = campoIdade.text;
            string campoPelag = campoPelag.text;
            int controleLinhasAfetadas = 0;
            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO))
            {
                conn.Open();
                string scriptDelete = "UPDATE tb_cadastro SET" + "nome = $nome, raca = $raca, idade = $idade, pelagem = $pelag";

                using (MySqlCommand comando = new MySqlCommand(scriptDelete, conn))
                {
                    comando.Parameters.AddWithValue("@id", campoId);
                    comando.Parameters.AddWithValue("@nome", campoNome);
                    comando.Parameters.AddWithValue("@raca", campoRaca);
                    comando.Parameters.AddWithValue("@idade", campoIdade);
                    comando.Parameters.AddWithValue("@pelag", campoPelag);


                    controleLinhasAfetadas = comando.ExecuteNonQuery();
                }
            }
        }
    }
}