namespace GPSFA_WinForms
{
    partial class frmGerenciarProdutos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGerenciarProdutos));
            this.btnNovo = new System.Windows.Forms.Button();
            this.lblCodBarras = new System.Windows.Forms.Label();
            this.txtCodBarras = new System.Windows.Forms.TextBox();
            this.lblUnidadeDeMedida = new System.Windows.Forms.Label();
            this.cbbUnidadeMedida = new System.Windows.Forms.ComboBox();
            this.txtPeso = new System.Windows.Forms.TextBox();
            this.lblPeso = new System.Windows.Forms.Label();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.lblDataValidade = new System.Windows.Forms.Label();
            this.dtpDataValidade = new System.Windows.Forms.DateTimePicker();
            this.lblDataEntrada = new System.Windows.Forms.Label();
            this.dtpDataEntrada = new System.Windows.Forms.DateTimePicker();
            this.lblDescricao = new System.Windows.Forms.Label();
            this.gpbCamposDoProduto = new System.Windows.Forms.GroupBox();
            this.btnDoacao = new System.Windows.Forms.Button();
            this.lblOrigemDoacao = new System.Windows.Forms.Label();
            this.cbbOrigemDoacao = new System.Windows.Forms.ComboBox();
            this.btnMedida = new System.Windows.Forms.Button();
            this.btnLista = new System.Windows.Forms.Button();
            this.cbbDescricao = new System.Windows.Forms.ComboBox();
            this.pnlCrud = new System.Windows.Forms.Panel();
            this.btnVoltar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.btnAlterar = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnCadastrar = new System.Windows.Forms.Button();
            this.gpbCamposDoProduto.SuspendLayout();
            this.pnlCrud.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNovo
            // 
            this.btnNovo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNovo.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNovo.Image = ((System.Drawing.Image)(resources.GetObject("btnNovo.Image")));
            this.btnNovo.Location = new System.Drawing.Point(4, 15);
            this.btnNovo.Margin = new System.Windows.Forms.Padding(4);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(221, 86);
            this.btnNovo.TabIndex = 11;
            this.btnNovo.Text = "&Novo";
            this.btnNovo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNovo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNovo.UseVisualStyleBackColor = true;
            // 
            // lblCodBarras
            // 
            this.lblCodBarras.AutoSize = true;
            this.lblCodBarras.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodBarras.ForeColor = System.Drawing.Color.Black;
            this.lblCodBarras.Location = new System.Drawing.Point(48, 98);
            this.lblCodBarras.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodBarras.Name = "lblCodBarras";
            this.lblCodBarras.Size = new System.Drawing.Size(260, 36);
            this.lblCodBarras.TabIndex = 1;
            this.lblCodBarras.Text = "Código de barras";
            // 
            // txtCodBarras
            // 
            this.txtCodBarras.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodBarras.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodBarras.Location = new System.Drawing.Point(48, 137);
            this.txtCodBarras.Margin = new System.Windows.Forms.Padding(4);
            this.txtCodBarras.MaxLength = 13;
            this.txtCodBarras.Name = "txtCodBarras";
            this.txtCodBarras.Size = new System.Drawing.Size(482, 47);
            this.txtCodBarras.TabIndex = 1;
            // 
            // lblUnidadeDeMedida
            // 
            this.lblUnidadeDeMedida.AutoSize = true;
            this.lblUnidadeDeMedida.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnidadeDeMedida.ForeColor = System.Drawing.Color.Black;
            this.lblUnidadeDeMedida.Location = new System.Drawing.Point(1193, 236);
            this.lblUnidadeDeMedida.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUnidadeDeMedida.Name = "lblUnidadeDeMedida";
            this.lblUnidadeDeMedida.Size = new System.Drawing.Size(289, 36);
            this.lblUnidadeDeMedida.TabIndex = 9;
            this.lblUnidadeDeMedida.Text = "Unidade de medida";
            // 
            // cbbUnidadeMedida
            // 
            this.cbbUnidadeMedida.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbUnidadeMedida.FormattingEnabled = true;
            this.cbbUnidadeMedida.Location = new System.Drawing.Point(1200, 274);
            this.cbbUnidadeMedida.Margin = new System.Windows.Forms.Padding(4);
            this.cbbUnidadeMedida.Name = "cbbUnidadeMedida";
            this.cbbUnidadeMedida.Size = new System.Drawing.Size(337, 47);
            this.cbbUnidadeMedida.TabIndex = 5;
            // 
            // txtPeso
            // 
            this.txtPeso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPeso.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPeso.Location = new System.Drawing.Point(1021, 274);
            this.txtPeso.Margin = new System.Windows.Forms.Padding(4);
            this.txtPeso.MaxLength = 10;
            this.txtPeso.Name = "txtPeso";
            this.txtPeso.Size = new System.Drawing.Size(130, 47);
            this.txtPeso.TabIndex = 4;
            // 
            // lblPeso
            // 
            this.lblPeso.AutoSize = true;
            this.lblPeso.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeso.ForeColor = System.Drawing.Color.Black;
            this.lblPeso.Location = new System.Drawing.Point(1015, 236);
            this.lblPeso.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(87, 36);
            this.lblPeso.TabIndex = 7;
            this.lblPeso.Text = "Peso";
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQuantidade.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantidade.Location = new System.Drawing.Point(791, 274);
            this.txtQuantidade.Margin = new System.Windows.Forms.Padding(4);
            this.txtQuantidade.MaxLength = 10;
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(182, 47);
            this.txtQuantidade.TabIndex = 3;
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQuantidade.ForeColor = System.Drawing.Color.Black;
            this.lblQuantidade.Location = new System.Drawing.Point(784, 236);
            this.lblQuantidade.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Size = new System.Drawing.Size(178, 36);
            this.lblQuantidade.TabIndex = 5;
            this.lblQuantidade.Text = "Quantidade";
            // 
            // lblDataValidade
            // 
            this.lblDataValidade.AutoSize = true;
            this.lblDataValidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataValidade.ForeColor = System.Drawing.Color.Black;
            this.lblDataValidade.Location = new System.Drawing.Point(361, 388);
            this.lblDataValidade.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDataValidade.Name = "lblDataValidade";
            this.lblDataValidade.Size = new System.Drawing.Size(140, 36);
            this.lblDataValidade.TabIndex = 57;
            this.lblDataValidade.Text = "Validade";
            // 
            // dtpDataValidade
            // 
            this.dtpDataValidade.CalendarFont = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataValidade.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataValidade.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataValidade.Location = new System.Drawing.Point(369, 427);
            this.dtpDataValidade.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDataValidade.Name = "dtpDataValidade";
            this.dtpDataValidade.Size = new System.Drawing.Size(212, 40);
            this.dtpDataValidade.TabIndex = 7;
            // 
            // lblDataEntrada
            // 
            this.lblDataEntrada.AutoSize = true;
            this.lblDataEntrada.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataEntrada.ForeColor = System.Drawing.Color.Black;
            this.lblDataEntrada.Location = new System.Drawing.Point(47, 388);
            this.lblDataEntrada.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDataEntrada.Name = "lblDataEntrada";
            this.lblDataEntrada.Size = new System.Drawing.Size(126, 36);
            this.lblDataEntrada.TabIndex = 54;
            this.lblDataEntrada.Text = "Entrada";
            // 
            // dtpDataEntrada
            // 
            this.dtpDataEntrada.CalendarFont = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataEntrada.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDataEntrada.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataEntrada.Location = new System.Drawing.Point(47, 427);
            this.dtpDataEntrada.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDataEntrada.Name = "dtpDataEntrada";
            this.dtpDataEntrada.Size = new System.Drawing.Size(212, 40);
            this.dtpDataEntrada.TabIndex = 6;
            // 
            // lblDescricao
            // 
            this.lblDescricao.AutoSize = true;
            this.lblDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescricao.ForeColor = System.Drawing.Color.Black;
            this.lblDescricao.Location = new System.Drawing.Point(47, 236);
            this.lblDescricao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescricao.Name = "lblDescricao";
            this.lblDescricao.Size = new System.Drawing.Size(156, 36);
            this.lblDescricao.TabIndex = 3;
            this.lblDescricao.Text = "Descrição";
            // 
            // gpbCamposDoProduto
            // 
            this.gpbCamposDoProduto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(237)))), ((int)(((byte)(228)))));
            this.gpbCamposDoProduto.Controls.Add(this.btnDoacao);
            this.gpbCamposDoProduto.Controls.Add(this.lblOrigemDoacao);
            this.gpbCamposDoProduto.Controls.Add(this.cbbOrigemDoacao);
            this.gpbCamposDoProduto.Controls.Add(this.btnMedida);
            this.gpbCamposDoProduto.Controls.Add(this.btnLista);
            this.gpbCamposDoProduto.Controls.Add(this.cbbDescricao);
            this.gpbCamposDoProduto.Controls.Add(this.lblCodBarras);
            this.gpbCamposDoProduto.Controls.Add(this.txtCodBarras);
            this.gpbCamposDoProduto.Controls.Add(this.lblUnidadeDeMedida);
            this.gpbCamposDoProduto.Controls.Add(this.cbbUnidadeMedida);
            this.gpbCamposDoProduto.Controls.Add(this.txtPeso);
            this.gpbCamposDoProduto.Controls.Add(this.lblPeso);
            this.gpbCamposDoProduto.Controls.Add(this.txtQuantidade);
            this.gpbCamposDoProduto.Controls.Add(this.lblQuantidade);
            this.gpbCamposDoProduto.Controls.Add(this.lblDataValidade);
            this.gpbCamposDoProduto.Controls.Add(this.dtpDataValidade);
            this.gpbCamposDoProduto.Controls.Add(this.lblDataEntrada);
            this.gpbCamposDoProduto.Controls.Add(this.dtpDataEntrada);
            this.gpbCamposDoProduto.Controls.Add(this.lblDescricao);
            this.gpbCamposDoProduto.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbCamposDoProduto.Location = new System.Drawing.Point(16, 15);
            this.gpbCamposDoProduto.Margin = new System.Windows.Forms.Padding(4);
            this.gpbCamposDoProduto.Name = "gpbCamposDoProduto";
            this.gpbCamposDoProduto.Padding = new System.Windows.Forms.Padding(4);
            this.gpbCamposDoProduto.Size = new System.Drawing.Size(1619, 538);
            this.gpbCamposDoProduto.TabIndex = 0;
            this.gpbCamposDoProduto.TabStop = false;
            this.gpbCamposDoProduto.Enter += new System.EventHandler(this.gpbCamposDoProduto_Enter);
            // 
            // btnDoacao
            // 
            this.btnDoacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDoacao.FlatAppearance.BorderSize = 0;
            this.btnDoacao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoacao.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDoacao.Image = ((System.Drawing.Image)(resources.GetObject("btnDoacao.Image")));
            this.btnDoacao.Location = new System.Drawing.Point(945, 137);
            this.btnDoacao.Margin = new System.Windows.Forms.Padding(4);
            this.btnDoacao.Name = "btnDoacao";
            this.btnDoacao.Size = new System.Drawing.Size(64, 44);
            this.btnDoacao.TabIndex = 63;
            this.btnDoacao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDoacao.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDoacao.UseVisualStyleBackColor = true;
            this.btnDoacao.Click += new System.EventHandler(this.btnDoacao_Click);
            // 
            // lblOrigemDoacao
            // 
            this.lblOrigemDoacao.AutoSize = true;
            this.lblOrigemDoacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrigemDoacao.ForeColor = System.Drawing.Color.Black;
            this.lblOrigemDoacao.Location = new System.Drawing.Point(616, 98);
            this.lblOrigemDoacao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOrigemDoacao.Name = "lblOrigemDoacao";
            this.lblOrigemDoacao.Size = new System.Drawing.Size(277, 36);
            this.lblOrigemDoacao.TabIndex = 62;
            this.lblOrigemDoacao.Text = "Origem da Doação";
            // 
            // cbbOrigemDoacao
            // 
            this.cbbOrigemDoacao.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbOrigemDoacao.FormattingEnabled = true;
            this.cbbOrigemDoacao.Location = new System.Drawing.Point(616, 137);
            this.cbbOrigemDoacao.Margin = new System.Windows.Forms.Padding(4);
            this.cbbOrigemDoacao.Name = "cbbOrigemDoacao";
            this.cbbOrigemDoacao.Size = new System.Drawing.Size(313, 47);
            this.cbbOrigemDoacao.TabIndex = 61;
            // 
            // btnMedida
            // 
            this.btnMedida.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMedida.FlatAppearance.BorderSize = 0;
            this.btnMedida.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMedida.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMedida.Image = ((System.Drawing.Image)(resources.GetObject("btnMedida.Image")));
            this.btnMedida.Location = new System.Drawing.Point(1547, 277);
            this.btnMedida.Margin = new System.Windows.Forms.Padding(4);
            this.btnMedida.Name = "btnMedida";
            this.btnMedida.Size = new System.Drawing.Size(64, 44);
            this.btnMedida.TabIndex = 60;
            this.btnMedida.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMedida.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMedida.UseVisualStyleBackColor = true;
            this.btnMedida.Click += new System.EventHandler(this.btnMedida_Click);
            // 
            // btnLista
            // 
            this.btnLista.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLista.FlatAppearance.BorderSize = 0;
            this.btnLista.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLista.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLista.Image = ((System.Drawing.Image)(resources.GetObject("btnLista.Image")));
            this.btnLista.Location = new System.Drawing.Point(691, 274);
            this.btnLista.Margin = new System.Windows.Forms.Padding(4);
            this.btnLista.Name = "btnLista";
            this.btnLista.Size = new System.Drawing.Size(64, 44);
            this.btnLista.TabIndex = 59;
            this.btnLista.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLista.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLista.UseVisualStyleBackColor = true;
            this.btnLista.Click += new System.EventHandler(this.btnLista_Click);
            // 
            // cbbDescricao
            // 
            this.cbbDescricao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDescricao.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbDescricao.FormattingEnabled = true;
            this.cbbDescricao.Location = new System.Drawing.Point(47, 274);
            this.cbbDescricao.Margin = new System.Windows.Forms.Padding(4);
            this.cbbDescricao.Name = "cbbDescricao";
            this.cbbDescricao.Size = new System.Drawing.Size(635, 47);
            this.cbbDescricao.TabIndex = 58;
            // 
            // pnlCrud
            // 
            this.pnlCrud.BackColor = System.Drawing.Color.White;
            this.pnlCrud.Controls.Add(this.btnVoltar);
            this.pnlCrud.Controls.Add(this.btnLimpar);
            this.pnlCrud.Controls.Add(this.btnPesquisar);
            this.pnlCrud.Controls.Add(this.btnAlterar);
            this.pnlCrud.Controls.Add(this.btnExcluir);
            this.pnlCrud.Controls.Add(this.btnCadastrar);
            this.pnlCrud.Controls.Add(this.btnNovo);
            this.pnlCrud.Location = new System.Drawing.Point(16, 608);
            this.pnlCrud.Margin = new System.Windows.Forms.Padding(4);
            this.pnlCrud.Name = "pnlCrud";
            this.pnlCrud.Size = new System.Drawing.Size(1619, 116);
            this.pnlCrud.TabIndex = 11;
            // 
            // btnVoltar
            // 
            this.btnVoltar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVoltar.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVoltar.Image = ((System.Drawing.Image)(resources.GetObject("btnVoltar.Image")));
            this.btnVoltar.Location = new System.Drawing.Point(1393, 15);
            this.btnVoltar.Margin = new System.Windows.Forms.Padding(4);
            this.btnVoltar.Name = "btnVoltar";
            this.btnVoltar.Size = new System.Drawing.Size(221, 86);
            this.btnVoltar.TabIndex = 17;
            this.btnVoltar.Text = "&Voltar";
            this.btnVoltar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnVoltar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnVoltar.UseVisualStyleBackColor = true;
            this.btnVoltar.Click += new System.EventHandler(this.btnVoltar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpar.Image = ((System.Drawing.Image)(resources.GetObject("btnLimpar.Image")));
            this.btnLimpar.Location = new System.Drawing.Point(692, 15);
            this.btnLimpar.Margin = new System.Windows.Forms.Padding(4);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(221, 86);
            this.btnLimpar.TabIndex = 14;
            this.btnLimpar.Text = "&Limpar";
            this.btnLimpar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLimpar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLimpar.UseVisualStyleBackColor = true;
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisar.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisar.Image = ((System.Drawing.Image)(resources.GetObject("btnPesquisar.Image")));
            this.btnPesquisar.Location = new System.Drawing.Point(1151, 15);
            this.btnPesquisar.Margin = new System.Windows.Forms.Padding(4);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(221, 86);
            this.btnPesquisar.TabIndex = 16;
            this.btnPesquisar.Text = "&Pesquisar";
            this.btnPesquisar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPesquisar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPesquisar.UseVisualStyleBackColor = true;
            // 
            // btnAlterar
            // 
            this.btnAlterar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAlterar.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlterar.Image = ((System.Drawing.Image)(resources.GetObject("btnAlterar.Image")));
            this.btnAlterar.Location = new System.Drawing.Point(463, 15);
            this.btnAlterar.Margin = new System.Windows.Forms.Padding(4);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(221, 86);
            this.btnAlterar.TabIndex = 13;
            this.btnAlterar.Text = "&Alterar";
            this.btnAlterar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAlterar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAlterar.UseVisualStyleBackColor = true;
            // 
            // btnExcluir
            // 
            this.btnExcluir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluir.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluir.Image = ((System.Drawing.Image)(resources.GetObject("btnExcluir.Image")));
            this.btnExcluir.Location = new System.Drawing.Point(921, 15);
            this.btnExcluir.Margin = new System.Windows.Forms.Padding(4);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(221, 86);
            this.btnExcluir.TabIndex = 15;
            this.btnExcluir.Text = "&Excluir";
            this.btnExcluir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcluir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExcluir.UseVisualStyleBackColor = true;
            // 
            // btnCadastrar
            // 
            this.btnCadastrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCadastrar.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadastrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCadastrar.Image")));
            this.btnCadastrar.Location = new System.Drawing.Point(233, 15);
            this.btnCadastrar.Margin = new System.Windows.Forms.Padding(4);
            this.btnCadastrar.Name = "btnCadastrar";
            this.btnCadastrar.Size = new System.Drawing.Size(221, 86);
            this.btnCadastrar.TabIndex = 12;
            this.btnCadastrar.Text = "&Cadastrar";
            this.btnCadastrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCadastrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCadastrar.UseVisualStyleBackColor = true;
            this.btnCadastrar.Click += new System.EventHandler(this.btnCadastrar_Click);
            // 
            // frmGerenciarProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(237)))), ((int)(((byte)(228)))));
            this.ClientSize = new System.Drawing.Size(1782, 770);
            this.Controls.Add(this.gpbCamposDoProduto);
            this.Controls.Add(this.pnlCrud);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "frmGerenciarProdutos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grupo Socorrista São Francisco de Assis - Gerenciar Produtos";
            this.Load += new System.EventHandler(this.frmCadastrarAlimentos_Load);
            this.gpbCamposDoProduto.ResumeLayout(false);
            this.gpbCamposDoProduto.PerformLayout();
            this.pnlCrud.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnNovo;
        private System.Windows.Forms.Label lblCodBarras;
        private System.Windows.Forms.TextBox txtCodBarras;
        private System.Windows.Forms.Label lblUnidadeDeMedida;
        private System.Windows.Forms.ComboBox cbbUnidadeMedida;
        private System.Windows.Forms.TextBox txtPeso;
        private System.Windows.Forms.Label lblPeso;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.Label lblDataValidade;
        private System.Windows.Forms.DateTimePicker dtpDataValidade;
        private System.Windows.Forms.Label lblDataEntrada;
        private System.Windows.Forms.DateTimePicker dtpDataEntrada;
        private System.Windows.Forms.Label lblDescricao;
        private System.Windows.Forms.GroupBox gpbCamposDoProduto;
        private System.Windows.Forms.Panel pnlCrud;
        private System.Windows.Forms.Button btnVoltar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnCadastrar;
        private System.Windows.Forms.ComboBox cbbDescricao;
        private System.Windows.Forms.Button btnLista;
        private System.Windows.Forms.Button btnMedida;
        private System.Windows.Forms.Button btnDoacao;
        private System.Windows.Forms.Label lblOrigemDoacao;
        private System.Windows.Forms.ComboBox cbbOrigemDoacao;
    }
}