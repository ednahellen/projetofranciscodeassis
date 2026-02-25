using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GPSFA_WinForms
{
    public partial class frmRelatorios : Form
    {
        int codUsuLogado;

        public frmRelatorios()
        {
            InitializeComponent();
        }

        public frmRelatorios(int codUsu)
        {
            InitializeComponent();
            codUsuLogado = codUsu;
        }

        #region LOAD

        private void frmRelatorios_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            CarregarUsuarios();
            CarregarTodosProdutos();

            dtpDataInicialPeriodo.Value = DateTime.Now.AddMonths(-1);
            dtpDataFinalPeriodo.Value = DateTime.Today;

            btnExportarExcel.Enabled = false;
        }

        #endregion

        #region CONFIGURAÇÕES

        private void ConfigurarGrid()
        {
            dgvProdutos.AutoGenerateColumns = true;
            dgvProdutos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProdutos.MultiSelect = false;
            dgvProdutos.ReadOnly = true;
            dgvProdutos.RowHeadersVisible = false;

            // Adicionar handler para o evento DataError
            dgvProdutos.DataError += new DataGridViewDataErrorEventHandler(dgvProdutos_DataError);
        }

        // Tratar erros de dados para evitar a caixa de diálogo
        private void dgvProdutos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Apenas ignorar o erro
            e.Cancel = true;
        }

        #endregion

        #region CONSULTAS

        private void CarregarTodosProdutos()
        {
            string sql = BaseQuery() + " ORDER BY prod.dataDeValidade ASC;";

            try
            {
                using (var conn = DataBaseConnection.OpenConnection())
                using (var cmd = new MySqlCommand(sql, conn))
                using (var da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    LimparColunasAntigas();
                    dgvProdutos.DataSource = dt;

                    // Configurar as colunas após carregar os dados
                    ConfigurarColunasVisiveis();
                    PreencherColunaStatus();
                }

                btnExportarExcel.Enabled = dgvProdutos.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarRelatorio(DateTime dataInicial, DateTime dataFinal, string usuario)
        {
            StringBuilder sql = new StringBuilder(BaseQuery());
            sql.Append(" WHERE prod.dataDeEntrada BETWEEN @dataInicial AND @dataFinal ");

            if (!string.IsNullOrWhiteSpace(usuario) && usuario != "Selecione...")
                sql.Append(" AND vol.nome = @usuario ");

            sql.Append(" ORDER BY prod.dataDeValidade ASC;");

            try
            {
                using (var conn = DataBaseConnection.OpenConnection())
                using (var cmd = new MySqlCommand(sql.ToString(), conn))
                {
                    cmd.Parameters.AddWithValue("@dataInicial", dataInicial.Date);
                    cmd.Parameters.AddWithValue("@dataFinal", dataFinal.Date.AddDays(1).AddSeconds(-1));

                    if (!string.IsNullOrWhiteSpace(usuario) && usuario != "Selecione...")
                        cmd.Parameters.AddWithValue("@usuario", usuario);

                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        LimparColunasAntigas();
                        dgvProdutos.DataSource = dt;

                        // Configurar as colunas após carregar os dados
                        ConfigurarColunasVisiveis();
                        PreencherColunaStatus();
                    }
                }

                btnExportarExcel.Enabled = dgvProdutos.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar relatório: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparColunasAntigas()
        {
            dgvProdutos.DataSource = null;
            dgvProdutos.Columns.Clear();
        }

        private string BaseQuery()
        {
            return @"SELECT 
                        DATE_FORMAT(prod.dataDeEntrada, '%d/%m/%Y') AS 'Data Entrada',
                        DATE_FORMAT(prod.dataDeValidade, '%d/%m/%Y') AS 'Data Validade',
                        prod.descricao AS 'Produto',
                        prod.estoqueAtual AS 'Quantidade',
                        prod.unidade AS 'Unidade',
                        prod.peso AS 'Peso',
                        DATEDIFF(prod.dataDeValidade, CURDATE()) AS 'DiasRestantes_Num',
                        vol.nome AS 'Cadastrado por'
                     FROM tbprodutos AS prod
                     INNER JOIN tbUsuarios AS usr ON prod.codUsu = usr.codUsu
                     INNER JOIN tbVoluntarios AS vol ON usr.codVol = vol.codVol";
        }

        private void ConfigurarColunasVisiveis()
        {
            if (dgvProdutos.Columns.Count == 0) return;

            // Ocultar a coluna numérica de dias restantes
            if (dgvProdutos.Columns["DiasRestantes_Num"] != null)
            {
                dgvProdutos.Columns["DiasRestantes_Num"].Visible = false;
            }

            // Adicionar coluna de status se não existir
            if (!dgvProdutos.Columns.Contains("Status Validade"))
            {
                DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn();
                colStatus.Name = "Status Validade";
                colStatus.HeaderText = "Status Validade";
                colStatus.Width = 120;
                dgvProdutos.Columns.Add(colStatus);
            }

            // Definir ordem das colunas
            int index = 0;
            if (dgvProdutos.Columns["Data Entrada"] != null)
                dgvProdutos.Columns["Data Entrada"].DisplayIndex = index++;

            if (dgvProdutos.Columns["Data Validade"] != null)
                dgvProdutos.Columns["Data Validade"].DisplayIndex = index++;

            if (dgvProdutos.Columns["Status Validade"] != null)
                dgvProdutos.Columns["Status Validade"].DisplayIndex = index++;

            if (dgvProdutos.Columns["Produto"] != null)
                dgvProdutos.Columns["Produto"].DisplayIndex = index++;

            if (dgvProdutos.Columns["Quantidade"] != null)
                dgvProdutos.Columns["Quantidade"].DisplayIndex = index++;

            if (dgvProdutos.Columns["Unidade"] != null)
                dgvProdutos.Columns["Unidade"].DisplayIndex = index++;

            if (dgvProdutos.Columns["Peso"] != null)
                dgvProdutos.Columns["Peso"].DisplayIndex = index++;

            if (dgvProdutos.Columns["Cadastrado por"] != null)
                dgvProdutos.Columns["Cadastrado por"].DisplayIndex = index++;

            // Definir larguras
            if (dgvProdutos.Columns["Data Entrada"] != null)
                dgvProdutos.Columns["Data Entrada"].Width = 100;

            if (dgvProdutos.Columns["Data Validade"] != null)
                dgvProdutos.Columns["Data Validade"].Width = 100;

            if (dgvProdutos.Columns["Status Validade"] != null)
            {
                dgvProdutos.Columns["Status Validade"].Width = 120;
                dgvProdutos.Columns["Status Validade"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvProdutos.Columns["Produto"] != null)
                dgvProdutos.Columns["Produto"].Width = 200;

            if (dgvProdutos.Columns["Quantidade"] != null)
            {
                dgvProdutos.Columns["Quantidade"].Width = 80;
                dgvProdutos.Columns["Quantidade"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvProdutos.Columns["Unidade"] != null)
                dgvProdutos.Columns["Unidade"].Width = 70;

            if (dgvProdutos.Columns["Peso"] != null)
            {
                dgvProdutos.Columns["Peso"].Width = 80;
                dgvProdutos.Columns["Peso"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvProdutos.Columns["Cadastrado por"] != null)
                dgvProdutos.Columns["Cadastrado por"].Width = 150;
        }

        private void PreencherColunaStatus()
        {
            foreach (DataGridViewRow row in dgvProdutos.Rows)
            {
                if (row.Cells["DiasRestantes_Num"].Value != null &&
                    row.Cells["DiasRestantes_Num"].Value != DBNull.Value)
                {
                    int dias = Convert.ToInt32(row.Cells["DiasRestantes_Num"].Value);
                    string statusTexto = "";

                    if (dias < 0)
                    {
                        // Produto vencido - vermelho
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        row.DefaultCellStyle.ForeColor = Color.White;
                        statusTexto = Math.Abs(dias) + " dias (VENCIDO)";
                    }
                    else if (dias <= 7)
                    {
                        // Vence em até 7 dias - laranja
                        row.DefaultCellStyle.BackColor = Color.Orange;
                        statusTexto = dias + " dias (URGENTE)";
                    }
                    else if (dias <= 30)
                    {
                        // Vence em até 30 dias - amarelo
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        statusTexto = dias + " dias (ATENÇÃO)";
                    }
                    else
                    {
                        statusTexto = dias + " dias";
                    }

                    // Preencher a coluna de status
                    if (row.Cells["Status Validade"] != null)
                    {
                        row.Cells["Status Validade"].Value = statusTexto;

                        // Ajustar cor do texto
                        if (dias < 0)
                            row.Cells["Status Validade"].Style.ForeColor = Color.White;
                        else if (dias <= 7)
                            row.Cells["Status Validade"].Style.ForeColor = Color.Black;
                    }
                }
            }
        }

        #endregion

        #region FILTROS

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (dtpDataInicialPeriodo.Value.Date > dtpDataFinalPeriodo.Value.Date)
            {
                MessageBox.Show("Data inicial não pode ser maior que data final!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string usuario = cbbUsuario.SelectedItem?.ToString() ?? "";
            if (usuario == "Selecione...")
                usuario = "";

            BuscarRelatorio(
                dtpDataInicialPeriodo.Value.Date,
                dtpDataFinalPeriodo.Value.Date,
                usuario
            );
        }

        private void btnLimparFiltros_Click(object sender, EventArgs e)
        {
            dtpDataInicialPeriodo.Value = DateTime.Now.AddMonths(-1);
            dtpDataFinalPeriodo.Value = DateTime.Today;
            cbbUsuario.SelectedIndex = 0; // "Selecione..."

            CarregarTodosProdutos();
        }

        #endregion

        #region USUÁRIOS

        private void CarregarUsuarios()
        {
            try
            {
                using (var conn = DataBaseConnection.OpenConnection())
                using (var cmd = new MySqlCommand("SELECT nome FROM tbVoluntarios ORDER BY nome;", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    cbbUsuario.Items.Clear();
                    cbbUsuario.Items.Add("Selecione...");

                    while (reader.Read())
                        cbbUsuario.Items.Add(reader.GetString("nome"));
                }

                cbbUsuario.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar usuários: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region EXPORTAÇÃO

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            if (dgvProdutos.Rows.Count == 0)
            {
                MessageBox.Show("Não há dados para exportar.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Arquivo Excel (*.xlsx)|*.xlsx";
                sfd.FileName = $"Relatorio_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("Relatório");

                        // Cabeçalhos
                        int colIndex = 1;
                        for (int i = 0; i < dgvProdutos.Columns.Count; i++)
                        {
                            if (dgvProdutos.Columns[i].Visible)
                            {
                                ws.Cell(1, colIndex).Value = dgvProdutos.Columns[i].HeaderText;
                                colIndex++;
                            }
                        }

                        // Dados
                        for (int i = 0; i < dgvProdutos.Rows.Count; i++)
                        {
                            colIndex = 1;
                            for (int j = 0; j < dgvProdutos.Columns.Count; j++)
                            {
                                if (dgvProdutos.Columns[j].Visible)
                                {
                                    ws.Cell(i + 2, colIndex).Value =
                                        dgvProdutos.Rows[i].Cells[j].Value?.ToString() ?? "";
                                    colIndex++;
                                }
                            }
                        }

                        ws.Columns().AdjustToContents();
                        wb.SaveAs(sfd.FileName);
                    }

                    MessageBox.Show("Relatório exportado com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao exportar: {ex.Message}", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region NAVEGAÇÃO

        private void btnMenu_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal abrir = new frmMenuPrincipal(codUsuLogado);
            abrir.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmGerenciarProdutos abrir = new frmGerenciarProdutos(codUsuLogado);
            abrir.Show();
            this.Hide();
        }

        #endregion
    }
}