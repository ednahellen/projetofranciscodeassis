using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPSFA_WinForms
{
    public partial class frmLogin : Form
    {
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);

        //Desativando botão fechar
        private void frmLogin_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);
        }

        public frmLogin()
        {
            InitializeComponent();
        }

        //Criando método de limpar campos
        public void limparCampos()
        {
            txtUsuario.Clear();
            txtSenha.Clear();
            txtUsuario.Focus();
        }

        int codUsuLogado;
        int codVolLogado;
        bool usuarioAtivo;
        string tipoAcesso;

        //Criando método para acesso do Usúario 
        bool resp = false;

        public bool acessaUsuario(string usuario, string senha)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "SELECT codUsu, codVol, ativo, tipo FROM tbUsuarios where usuario=@usuario and senha=@senha;";
            comm.CommandType = CommandType.Text;
            comm.Parameters.Clear();
            comm.Parameters.Add("@usuario", MySqlDbType.VarChar, 100).Value = usuario;
            comm.Parameters.Add("@senha", MySqlDbType.VarChar, 100).Value = senha;

            comm.Connection = DataBaseConnection.OpenConnection();

            using (MySqlDataReader DR = comm.ExecuteReader())
            {
                if (DR.Read())
                {
                    try
                    {
                        resp = DR.HasRows;

                        codUsuLogado = DR.GetInt32(0);
                        codVolLogado = DR.GetInt32(1);
                        usuarioAtivo = DR.GetBoolean(2);
                        tipoAcesso = DR.GetString(3);

                        DataBaseConnection.CloseConnection();
                        return resp;
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show($"Banco de dados não conectado. Erro:\n\n{error}", "Mensagem do sistema",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                        DataBaseConnection.CloseConnection();
                    }
                }
            }
            return resp;
        }                

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string usuario, senha;

            usuario = txtUsuario.Text;
            senha = txtSenha.Text;

            if (acessaUsuario(usuario, senha))
            {
                if (usuarioAtivo)
                {
                    frmMenuPrincipal abrir = new frmMenuPrincipal();
                    abrir.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Acesso negado! O usuário informado se encontra desativado.", "Mensagem do sistema",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    limparCampos();
                }
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos.", "Mensagem do sistema",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Error,
                   MessageBoxDefaultButton.Button1);
                limparCampos();
                    
            }
           
        }

      
    }
}
