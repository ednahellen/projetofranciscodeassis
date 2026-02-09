using CpfLibrary;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPSFA_WinForms
{
    public partial class frmVoluntarios : Form
    {
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);

        public frmVoluntarios()
        {
            InitializeComponent();
            desativarBotoes();
            desabilitarCampos();
        }

        public frmVoluntarios(string text)
        {
            InitializeComponent();
            buscarDadosDoVoluntario(text);
            desativarBotoesNovo();
            desabilitarBotaoCadastrar();
            habilitarCampos();
        }

        // Instância global do código do voluntário
        int codVol = 0;

        private void buscarDadosDoVoluntario(string codVoluntario)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = $"SELECT * FROM tbVoluntarios WHERE codVol = @codVol;";

            comm.CommandType = CommandType.Text;
            comm.Parameters.Clear();

            comm.Parameters.Add("@codVol", MySqlDbType.VarChar, 100).Value = codVoluntario;

            comm.Connection = DataBaseConnection.OpenConnection();

            MySqlDataReader DR;
            DR = comm.ExecuteReader();

            while (DR.Read())
            {
                codVol = DR.GetInt32(0);
                txtNomeVoluntario.Text = DR.GetString(1);
                mskTelefone.Text = DR.GetString(2);
                mskCpf.Text = DR.GetString(3);
                mskCep.Text = DR.GetString(4);
                txtRua.Text = DR.GetString(5);
                txtNumero.Text = DR.GetString(6);
                txtComplemento.Text = DR.GetString(7);
                txtBairro.Text = DR.GetString(8);
                txtUsuario.Text = DR.GetString(11);
                txtSenha.Text = DR.GetString(12);
            }

            DataBaseConnection.CloseConnection();
        }

        private int buscarVoluntario(string descricao)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = $"SELECT * FROM tbVoluntarios WHERE nome = @descricao;";

            comm.CommandType = CommandType.Text;

            comm.Parameters.Clear();
            comm.Parameters.Add("@descricao", MySqlDbType.VarChar, 20).Value = descricao;

            comm.Connection = DataBaseConnection.OpenConnection();

            int resp = comm.ExecuteNonQuery();

            DataBaseConnection.CloseConnection();

            return resp;
        }

        public int cadastrarVoluntario(string nome, string telCel, string cpf, string cep, string rua, string numero, string complemento, string bairro, string cidade, string estado, string usuario, string senha)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "INSERT INTO tbVoluntarios(nome,telCel,cpf,cep,rua,numero,complemento,bairro,cidade,estado,usuario,senha)VALUES(@nome,@telCel,@cpf,@cep,@rua,@numero,@complemento,@bairro,@cidade,@estado,@usuario,@senha);";
            comm.CommandType = CommandType.Text;

            comm.Parameters.Clear();
            comm.Parameters.Add("@nome", MySqlDbType.VarChar, 20).Value = nome;
            comm.Parameters.Add("@telCel", MySqlDbType.VarChar, 20).Value = telCel;
            comm.Parameters.Add("@cpf", MySqlDbType.VarChar, 20).Value = cpf;
            comm.Parameters.Add("@cep", MySqlDbType.VarChar, 20).Value = cep;
            comm.Parameters.Add("@rua", MySqlDbType.VarChar, 20).Value = rua;
            comm.Parameters.Add("@numero", MySqlDbType.VarChar, 20).Value = numero;
            comm.Parameters.Add("@complemento", MySqlDbType.VarChar, 20).Value = complemento;
            comm.Parameters.Add("@bairro", MySqlDbType.VarChar, 20).Value = bairro;
            comm.Parameters.Add("@cidade", MySqlDbType.VarChar, 20).Value = cidade;
            comm.Parameters.Add("@estado", MySqlDbType.VarChar, 20).Value = estado;
            comm.Parameters.Add("@usuario", MySqlDbType.VarChar, 20).Value = usuario;
            comm.Parameters.Add("@senha", MySqlDbType.VarChar, 20).Value = senha;

            comm.Connection = DataBaseConnection.OpenConnection();

            try
            {
                int resp = comm.ExecuteNonQuery();

                DataBaseConnection.CloseConnection();

                return resp;
            }
            catch (Exception)
            {
                MessageBox.Show("Este voluntario já existe!", "Mensagem do sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            return 0;
        }

        // Métodos para habilitar ou desabilitar campos da janela
        private void limparCampos()
        {
            txtNomeVoluntario.Clear();
            txtNomeVoluntario.Focus();
            mskTelefone.Clear();
            mskCpf.Clear(); 
            txtRua.Clear();
            txtNumero.Clear();
            txtComplemento.Clear();
            mskCep.Clear();
            txtBairro.Clear();
            txtUsuario.Clear();
            txtSenha.Clear();
        }
        
        private void desabilitarCampos()
        {
            txtNomeVoluntario.Enabled = false;
            txtNomeVoluntario.Enabled = false;
            txtRua.Enabled = false;
            txtNumero.Enabled = false;
            txtComplemento.Enabled = false;
            txtBairro.Enabled = false;
            txtUsuario.Enabled = false;
            txtSenha.Enabled = false;
            mskCpf.Enabled = false;
            mskTelefone.Enabled = false;
            mskCep.Enabled = false;
            cbbCidade.Enabled = false;
            cbbEstado.Enabled = false;
        }

        private void habilitarCampos()
        {
            txtNomeVoluntario.Enabled = true;
            txtNomeVoluntario.Enabled = true;
            txtRua.Enabled = true;
            txtNumero.Enabled = true;
            txtComplemento.Enabled = true;
            txtBairro.Enabled = true;
            txtUsuario.Enabled = true;
            txtSenha.Enabled = true;
            mskCpf.Enabled = true;
            mskTelefone.Enabled = true;
            mskCep.Enabled = true;
            cbbCidade.Enabled = true;
            cbbEstado.Enabled = true;
        }

        // Métodos para desabilitar ou habilitar recursos da janela
        private void desativarBotoes()
        {
            btnCadastrar.Enabled = false;
            btnAlterar.Enabled = false;
            btnLimpar.Enabled = false;
            btnExcluir.Enabled = false;
        }
        
        private void desabilitarBotaoCadastrar()
        {
            btnCadastrar.Enabled = false;
        }

        private void habilitarBotoesCadastrar()
        {
            btnCadastrar.Enabled = true;
        }

        private void desativarBotoesNovo()
        {
            btnNovo.Enabled = false;
        }

        // Conigurações dos botões e suas respectivas ações
        private void btnNovo_Click(object sender, EventArgs e)
        {
            habilitarCampos();
            habilitarBotoesCadastrar();
            desativarBotoesNovo();
            btnLimpar.Enabled = true;
            txtNomeVoluntario.Focus();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (txtNomeVoluntario.Text.Equals("")) // Falta adicionar mais validações
            {
                MessageBox.Show("Favor inserir valores!", "Mensagem do sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                txtNomeVoluntario.Focus();
            }
            else if (buscarVoluntario(txtNomeVoluntario.Text).Equals(1))
            {
                MessageBox.Show("Este registro já existe!", "Mensagem do sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                txtNomeVoluntario.Focus();
            }
            else
            {
                int resp = cadastrarVoluntario(txtNomeVoluntario.Text, txtRua.Text, txtNumero.Text, txtComplemento.Text, txtBairro.Text, txtUsuario.Text, txtSenha.Text, mskCpf.Text, mskTelefone.Text, mskCep.Text, txtUsuario.Text, txtSenha.Text);

                if (resp.Equals(1))
                {
                    MessageBox.Show("Cadastrado com sucesso!", "Mensagem do sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                    desativarBotoes();
                    desabilitarCampos();
                    desativarBotoes();
                    btnNovo.Enabled = true;
                    btnNovo.Focus();
                }
                else
                {
                    MessageBox.Show("Erro ao Cadastrar!", "Mensagem do sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);

                    limparCampos();
                    desabilitarCampos();
                    desativarBotoes();
                    desabilitarBotaoCadastrar();
                    btnNovo.Enabled = true;

                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
            desabilitarCampos();
            desativarBotoes();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmListaVoluntarios abrir = new frmListaVoluntarios();
            abrir.Show();
            this.Close();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal abrir = new frmMenuPrincipal();
            abrir.Show();
            this.Close();
        }

        private void frmVoluntarios_Load(object sender, EventArgs e)
        {

        }
    }
}
