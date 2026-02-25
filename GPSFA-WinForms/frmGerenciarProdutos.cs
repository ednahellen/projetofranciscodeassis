using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GPSFA_WinForms
{
    public partial class frmGerenciarProdutos : Form
    {
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);

        // Variáveis globais
        int codUsuLogado;
        int codList;
        int codOri;
        string nomeOrigem;
        int codOrigem;
        int diaDeArrecadacao = DateTime.Now.Day;
        DateTime diaDeDistribuicao = DateTime.Now.AddDays(7);

        // CONSTRUTORES
        public frmGerenciarProdutos()
        {
            InitializeComponent();
            carregarOrigemCbb();
            carregarUnidadesCbb();
            carregarProdutosCbb();
            ConfigurarCampos();
        }

        public frmGerenciarProdutos(int codUsu)
        {
            codUsuLogado = codUsu;
            InitializeComponent();
            carregarOrigemCbb();
            carregarUnidadesCbb();
            carregarProdutosCbb();
            ConfigurarCampos();
            dtpDiaDistribuicao.Value = diaDeDistribuicao;
        }

        public frmGerenciarProdutos(int codUsu, string origemSelecionada)
        {
            nomeOrigem = origemSelecionada;
            codUsuLogado = codUsu;
            InitializeComponent();
            carregarOrigemCbb();
            carregarUnidadesCbb();
            carregarProdutosCbb();
            ConfigurarCampos();

            if (!string.IsNullOrEmpty(nomeOrigem))
                cbbOrigemDoacao.Text = nomeOrigem;
        }

        // Configurações iniciais dos campos
        private void ConfigurarCampos()
        {
            dtpDataEntrada.Value = DateTime.Now;
            dtpDataEntrada.Enabled = false; //data de entrada sempre será a data atual
            dtpDataValidade.MinDate = DateTime.Now.AddDays(1);
            dtpDataValidade.MaxDate = DateTime.Now.AddYears(2);
            txtQuantidade.Text = "1";
        }

        //desativando botão fechar da janela

        private void frmCadastrarAlimentos_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);
        }

        // CARREGAR COMBOBOXES
        private void carregarOrigemCbb()
        {
            try
            {
                using (var conn = DataBaseConnection.OpenConnection())
                {
                    string sql = "SELECT nome FROM tbOrigemDoacao ORDER BY nome ASC;";
                    
                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var reader = cmd. ExecuteReader())
                        
                    {
                        cbbOrigemDoacao.Items.Clear();
                        while (reader.Read())
                        {
                            cbbOrigemDoacao.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar origens: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregarUnidadesCbb()
        {
            try
            {
                using (var conn = DataBaseConnection.OpenConnection())
                {
                    string sql = "SELECT descricao FROM tbUnidades ORDER BY descricao ASC;";
                    
                    using (var cmd = new MySqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        cbbUnidadeMedida.Items.Clear();
                        while (reader.Read())
                        {
                            cbbUnidadeMedida.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar unidades: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void carregarProdutosCbb()
        {
            try
            {
                using (var conn = DataBaseConnection.OpenConnection())
                {
                    string sql = "SELECT descricao FROM tbLista ORDER BY descricao ASC;";
                    
                    using (var cmd = new MySqlCommand (sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        cbbDescricao.Items.Clear();
                        while (reader.Read())
                        {
                            cbbDescricao.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar produtos: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // VALIDAÇÕES
        private bool VerificarCampos()
        {
            if (string.IsNullOrWhiteSpace(cbbDescricao.Text))
            {
                MessageBox.Show("Selecione a descrição do produto.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbDescricao.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cbbOrigemDoacao.Text))
            {
                MessageBox.Show("Selecione a origem da doação!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbOrigemDoacao.Focus();
                return false;
            }

            // Código de barras não é mais obrigatório
            // if (string.IsNullOrWhiteSpace(txtCodBarras.Text))
            // {
            //     MessageBox.Show("Informe o código de barras!", "Atenção",
            //         MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     txtCodBarras.Focus();
            //     return false;
            // }

            if (Regex.IsMatch(txtQuantidade.Text, @"[^0-9]"))
            {
                MessageBox.Show("Quantidade deve conter apenas números!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantidade.Focus();
                return false;
            }

            int quantidade = Convert.ToInt32(txtQuantidade.Text);
            if (quantidade <= 0)
            {
                MessageBox.Show("Quantidade deve ser maior que zero!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantidade.Focus();
                return false;
            }

            if (dtpDataValidade.Value.Date <= DateTime.Today)
            {
                MessageBox.Show("Data de validade deve ser futura!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDataValidade.Focus();
                return false;
            }

            return true;
        }


        // REGISTRAR ENTRADA DE PRODUTO
        private bool RegistrarEntradaProduto()
        {
            try
            {
                using (var conn = DataBaseConnection.OpenConnection())
                {
                    int quantidade = Convert.ToInt32(txtQuantidade.Text);
                    decimal peso = Convert.ToDecimal(txtPeso.Text);
                    string codBar = string.IsNullOrWhiteSpace(txtCodBarras.Text) ? null : txtCodBarras.Text.Trim();

                    // 1. Se tem código de barras, verifica se produto já existe
                    if (!string.IsNullOrEmpty(codBar))
                    {
                        string sqlVerifica = "SELECT codProd, estoqueAtual, descricao FROM tbProdutos WHERE codBar = @codBar";
                        using (var cmdVerifica = new MySqlCommand(sqlVerifica, conn))
                        {
                            cmdVerifica.Parameters.AddWithValue("@codBar", codBar);
                            using (var reader = cmdVerifica.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // PRODUTO EXISTE: Atualizar estoque
                                    int codProd = reader.GetInt32("codProd");
                                    int estoqueAtual = reader.GetInt32("estoqueAtual");
                                    string nomeProduto = reader.GetString("descricao");
                                    reader.Close();

                                    string sqlUpdate = @"UPDATE tbProdutos SET 
                                        estoqueAtual = @novoEstoque,
                                        dataDeEntrada = @dataEntrada,
                                        dataDeValidade = @dataValidade,
                                        codUsu = @codUsu,
                                        codOri = @codOri
                                        WHERE codProd = @codProd";

                                    using (var cmdUpdate = new MySqlCommand(sqlUpdate, conn))
                                    {
                                        cmdUpdate.Parameters.AddWithValue("@novoEstoque", estoqueAtual + quantidade);
                                        cmdUpdate.Parameters.AddWithValue("@dataEntrada", DateTime.Now);
                                        cmdUpdate.Parameters.AddWithValue("@dataValidade", dtpDataValidade.Value);
                                        cmdUpdate.Parameters.AddWithValue("@codUsu", codUsuLogado);
                                        cmdUpdate.Parameters.AddWithValue("@codOri", codOri);
                                        cmdUpdate.Parameters.AddWithValue("@codProd", codProd);
                                        cmdUpdate.ExecuteNonQuery();
                                    }

                                    MessageBox.Show($"Estoque atualizado!\nProduto: {nomeProduto}\nQuantidade anterior: {estoqueAtual}\nNova quantidade: {estoqueAtual + quantidade}",
                                        "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return true;
                                }
                            }
                        }
                    }

                    // 2. PRODUTO NOVO: Inserir novo registro
                    string sqlInsert = @"INSERT INTO tbProdutos 
                        (descricao, peso, unidade, codBar, dataDeEntrada, dataDeValidade, 
                         dataLimiteDeSaida, estoqueAtual, estoqueMinimo, localizacao, 
                         codUsu, codOri, codList)
                        VALUES 
                        (@descricao, @peso, @unidade, @codBar, @dataEntrada, @dataValidade,
                         @dataLimite, @estoqueAtual, 5, @localizacao,
                         @codUsu, @codOri, @codList);
                        SELECT LAST_INSERT_ID();";

                    using (var cmdInsert = new MySqlCommand(sqlInsert, conn))
                    {
                        DateTime dataLimite = dtpDataValidade.Value.AddDays(-30);

                        cmdInsert.Parameters.AddWithValue("@descricao", cbbDescricao.Text);
                        cmdInsert.Parameters.AddWithValue("@peso", peso);
                        cmdInsert.Parameters.AddWithValue("@unidade", cbbUnidadeMedida.Text);
                        cmdInsert.Parameters.AddWithValue("@codBar", string.IsNullOrEmpty(codBar) ? DBNull.Value : (object)codBar);
                        cmdInsert.Parameters.AddWithValue("@dataEntrada", DateTime.Now);
                        cmdInsert.Parameters.AddWithValue("@dataValidade", dtpDataValidade.Value);
                        cmdInsert.Parameters.AddWithValue("@dataLimite", dataLimite);
                        cmdInsert.Parameters.AddWithValue("@estoqueAtual", quantidade);
                        cmdInsert.Parameters.AddWithValue("@localizacao", "A definir");
                        cmdInsert.Parameters.AddWithValue("@codUsu", codUsuLogado);
                        cmdInsert.Parameters.AddWithValue("@codOri", codOri);
                        cmdInsert.Parameters.AddWithValue("@codList", codList);

                        int novoCodProd = Convert.ToInt32(cmdInsert.ExecuteScalar());

                        string msgCodBar = string.IsNullOrEmpty(codBar) ? "sem código de barras" : $"código: {codBar}";
                        MessageBox.Show($"Novo produto cadastrado com sucesso!\nCódigo interno: {novoCodProd}\nProduto: {cbbDescricao.Text}\nQuantidade: {quantidade}\n{msgCodBar}",
                            "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return true;
                    }
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    MessageBox.Show("Este código de barras já está cadastrado para outro produto!",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Erro no banco de dados: {ex.Message}",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao registrar entrada: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        // EVENTOS DOS BOTÕES
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (!VerificarCampos())
                return;

            if (RegistrarEntradaProduto())
            {
                LimparCampos();
            }
        }

        private void LimparCampos()
        {
            cbbDescricao.SelectedIndex = -1;
            txtQuantidade.Text = "1";
            txtCodBarras.Clear();
            dtpDataValidade.Value = DateTime.Now.AddMonths(6);
            cbbUnidadeMedida.Enabled = true;
            txtPeso.Enabled = true;
            txtPeso.Clear();
            cbbOrigemDoacao.SelectedIndex = -1;
            codList = 0;
            codOri = 0;
        }

        // EVENTOS DAS COMBOBOXES
        private void cbbDescricao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbDescricao.SelectedItem == null) return;

            try
            {
                string nomeSelecionado = cbbDescricao.SelectedItem.ToString();

                using (var conn = DataBaseConnection.OpenConnection())
                {
                    string sql = "SELECT codList, peso, unidade FROM tbLista WHERE descricao = @descricao;";
                    using (MySqlCommand comm = new MySqlCommand(sql, conn))
                    {
                        comm.Parameters.AddWithValue("@descricao", nomeSelecionado);
                        using (MySqlDataReader DR = comm.ExecuteReader())
                        {
                            if (DR.Read())
                            {
                                codList = DR.GetInt32(0);
                                txtPeso.Text = DR.GetDecimal(1).ToString("0.###");
                                cbbUnidadeMedida.Text = DR.GetString(2);
                                cbbUnidadeMedida.Enabled = false;
                                txtPeso.Enabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados do produto: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbbOrigemDoacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbOrigemDoacao.SelectedItem == null) return;

            try
            {
                string origemSelecionada = cbbOrigemDoacao.SelectedItem.ToString();
                using (var conn = DataBaseConnection.OpenConnection())
                {
                    string sql = "SELECT codOri FROM tborigemdoacao WHERE nome = @nome;";
                    using (MySqlCommand comm = new MySqlCommand(sql, conn))
                    {
                        comm.Parameters.AddWithValue("@nome", origemSelecionada);
                        using (MySqlDataReader DR = comm.ExecuteReader())
                        {
                            if (DR.Read())
                            {
                                codOri = DR.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar código da origem: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // EVENTOS DE TECLADO
        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // BOTÕES DE NAVEGAÇÃO
        private void btnDoacao_Click(object sender, EventArgs e)
        {
            frmOrigemDoacao abrir = new frmOrigemDoacao(codUsuLogado);
            abrir.Show();
            this.Hide();
        }

        private void btnLista_Click(object sender, EventArgs e)
        {
            frmListaProdutos abrir = new frmListaProdutos(codUsuLogado);
            abrir.Show();
            this.Hide();
        }

        private void btnMedida_Click(object sender, EventArgs e)
        {
            frmUnidadeMedida abrir = new frmUnidadeMedida(codUsuLogado);
            abrir.Show();
            this.Hide();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal abrir = new frmMenuPrincipal(codUsuLogado);
            abrir.Show();
            this.Close();
        }
    }
}