namespace Sistema.Forms.modulo_traslados
{
    partial class VistaPrincipalTraslado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VistaPrincipalTraslado));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAgregar = new System.Windows.Forms.ToolStripButton();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.lblestado = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtfechaEmision = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lbnoVale = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel8 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnSolicitarProductos = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dgvListaAPedir = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.btnAgregartoProducto = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dgvproductosEncontrados = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.btnbuscar = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbsucursales = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel16 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.combotallas = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonLabel15 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dgvProductos = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.comboColores = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.dgvtallascolores = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtbuscarp = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lbcorrelativo = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaAPedir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvproductosEncontrados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbsucursales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.combotallas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboColores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvtallascolores)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.btnAgregar});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1476, 27);
            this.toolStrip.TabIndex = 3;
            this.toolStrip.Text = "ToolStrip";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Image = global::Sistema.Properties.Resources.Add_8x_16x;
            this.btnAgregar.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(73, 24);
            this.btnAgregar.Text = "Solicitar";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.lbcorrelativo);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel4);
            this.kryptonPanel1.Controls.Add(this.lblestado);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel5);
            this.kryptonPanel1.Controls.Add(this.txtfechaEmision);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel3);
            this.kryptonPanel1.Controls.Add(this.lbnoVale);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel8);
            this.kryptonPanel1.Controls.Add(this.btnSolicitarProductos);
            this.kryptonPanel1.Controls.Add(this.dgvListaAPedir);
            this.kryptonPanel1.Controls.Add(this.btnAgregartoProducto);
            this.kryptonPanel1.Controls.Add(this.dgvproductosEncontrados);
            this.kryptonPanel1.Controls.Add(this.btnbuscar);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel2);
            this.kryptonPanel1.Controls.Add(this.cbsucursales);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel16);
            this.kryptonPanel1.Controls.Add(this.combotallas);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel15);
            this.kryptonPanel1.Controls.Add(this.dgvProductos);
            this.kryptonPanel1.Controls.Add(this.comboColores);
            this.kryptonPanel1.Controls.Add(this.dgvtallascolores);
            this.kryptonPanel1.Controls.Add(this.kryptonLabel1);
            this.kryptonPanel1.Controls.Add(this.txtbuscarp);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 27);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalOffice2003;
            this.kryptonPanel1.Size = new System.Drawing.Size(1476, 722);
            this.kryptonPanel1.TabIndex = 4;
            // 
            // lblestado
            // 
            this.lblestado.Location = new System.Drawing.Point(1079, 85);
            this.lblestado.Margin = new System.Windows.Forms.Padding(2);
            this.lblestado.Name = "lblestado";
            this.lblestado.Size = new System.Drawing.Size(57, 20);
            this.lblestado.TabIndex = 31;
            this.lblestado.Values.Text = "Solicitud";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(958, 84);
            this.kryptonLabel5.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(47, 20);
            this.kryptonLabel5.TabIndex = 30;
            this.kryptonLabel5.Values.Text = "Estado";
            // 
            // txtfechaEmision
            // 
            this.txtfechaEmision.Location = new System.Drawing.Point(1079, 57);
            this.txtfechaEmision.Name = "txtfechaEmision";
            this.txtfechaEmision.ReadOnly = true;
            this.txtfechaEmision.Size = new System.Drawing.Size(267, 23);
            this.txtfechaEmision.TabIndex = 27;
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(958, 60);
            this.kryptonLabel3.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(92, 20);
            this.kryptonLabel3.TabIndex = 26;
            this.kryptonLabel3.Values.Text = "Fecha Solicitud";
            // 
            // lbnoVale
            // 
            this.lbnoVale.AutoSize = false;
            this.lbnoVale.Location = new System.Drawing.Point(1079, 7);
            this.lbnoVale.Margin = new System.Windows.Forms.Padding(2);
            this.lbnoVale.Name = "lbnoVale";
            this.lbnoVale.Size = new System.Drawing.Size(279, 21);
            this.lbnoVale.TabIndex = 25;
            this.lbnoVale.Values.Text = "";
            // 
            // kryptonLabel8
            // 
            this.kryptonLabel8.Location = new System.Drawing.Point(958, 12);
            this.kryptonLabel8.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel8.Name = "kryptonLabel8";
            this.kryptonLabel8.Size = new System.Drawing.Size(80, 20);
            this.kryptonLabel8.TabIndex = 24;
            this.kryptonLabel8.Values.Text = "No. Solicitud";
            // 
            // btnSolicitarProductos
            // 
            this.btnSolicitarProductos.Location = new System.Drawing.Point(1243, 446);
            this.btnSolicitarProductos.Name = "btnSolicitarProductos";
            this.btnSolicitarProductos.Size = new System.Drawing.Size(191, 46);
            this.btnSolicitarProductos.TabIndex = 23;
            this.btnSolicitarProductos.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnSolicitarProductos.Values.Image")));
            this.btnSolicitarProductos.Values.Text = "Enviar solicitud a Sucursal";
            this.btnSolicitarProductos.Click += new System.EventHandler(this.btnSolicitarProductos_Click);
            // 
            // dgvListaAPedir
            // 
            this.dgvListaAPedir.AllowUserToAddRows = false;
            this.dgvListaAPedir.AllowUserToDeleteRows = false;
            this.dgvListaAPedir.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListaAPedir.Location = new System.Drawing.Point(956, 109);
            this.dgvListaAPedir.Margin = new System.Windows.Forms.Padding(2);
            this.dgvListaAPedir.Name = "dgvListaAPedir";
            this.dgvListaAPedir.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.dgvListaAPedir.RowHeadersWidth = 51;
            this.dgvListaAPedir.RowTemplate.Height = 24;
            this.dgvListaAPedir.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvListaAPedir.Size = new System.Drawing.Size(502, 323);
            this.dgvListaAPedir.TabIndex = 22;
            // 
            // btnAgregartoProducto
            // 
            this.btnAgregartoProducto.Location = new System.Drawing.Point(809, 437);
            this.btnAgregartoProducto.Name = "btnAgregartoProducto";
            this.btnAgregartoProducto.Size = new System.Drawing.Size(108, 55);
            this.btnAgregartoProducto.TabIndex = 21;
            this.btnAgregartoProducto.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnAgregartoProducto.Values.Image")));
            this.btnAgregartoProducto.Values.Text = "Agregar";
            this.btnAgregartoProducto.Click += new System.EventHandler(this.btnAgregartoProducto_Click);
            // 
            // dgvproductosEncontrados
            // 
            this.dgvproductosEncontrados.AllowUserToAddRows = false;
            this.dgvproductosEncontrados.AllowUserToDeleteRows = false;
            this.dgvproductosEncontrados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvproductosEncontrados.Location = new System.Drawing.Point(427, 109);
            this.dgvproductosEncontrados.Margin = new System.Windows.Forms.Padding(2);
            this.dgvproductosEncontrados.Name = "dgvproductosEncontrados";
            this.dgvproductosEncontrados.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.dgvproductosEncontrados.ReadOnly = true;
            this.dgvproductosEncontrados.RowHeadersWidth = 51;
            this.dgvproductosEncontrados.RowTemplate.Height = 24;
            this.dgvproductosEncontrados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvproductosEncontrados.Size = new System.Drawing.Size(523, 323);
            this.dgvproductosEncontrados.TabIndex = 20;
            this.dgvproductosEncontrados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvproductosEncontrados_CellClick);
            // 
            // btnbuscar
            // 
            this.btnbuscar.Location = new System.Drawing.Point(705, 35);
            this.btnbuscar.Name = "btnbuscar";
            this.btnbuscar.Size = new System.Drawing.Size(120, 27);
            this.btnbuscar.TabIndex = 19;
            this.btnbuscar.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnbuscar.Values.Image")));
            this.btnbuscar.Values.Text = "Buscar";
            this.btnbuscar.Click += new System.EventHandler(this.btnbuscar_Click);
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(459, 11);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(104, 20);
            this.kryptonLabel2.TabIndex = 18;
            this.kryptonLabel2.Values.Text = "Sucursal a buscar";
            // 
            // cbsucursales
            // 
            this.cbsucursales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbsucursales.DropDownWidth = 231;
            this.cbsucursales.Location = new System.Drawing.Point(459, 35);
            this.cbsucursales.Margin = new System.Windows.Forms.Padding(2);
            this.cbsucursales.Name = "cbsucursales";
            this.cbsucursales.Size = new System.Drawing.Size(241, 21);
            this.cbsucursales.TabIndex = 17;
            // 
            // kryptonLabel16
            // 
            this.kryptonLabel16.Location = new System.Drawing.Point(18, 654);
            this.kryptonLabel16.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel16.Name = "kryptonLabel16";
            this.kryptonLabel16.Size = new System.Drawing.Size(36, 20);
            this.kryptonLabel16.TabIndex = 0;
            this.kryptonLabel16.Values.Text = "Talla";
            // 
            // combotallas
            // 
            this.combotallas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combotallas.DropDownWidth = 234;
            this.combotallas.Location = new System.Drawing.Point(115, 653);
            this.combotallas.Margin = new System.Windows.Forms.Padding(2);
            this.combotallas.Name = "combotallas";
            this.combotallas.Size = new System.Drawing.Size(298, 21);
            this.combotallas.TabIndex = 1;
            // 
            // kryptonLabel15
            // 
            this.kryptonLabel15.Location = new System.Drawing.Point(14, 616);
            this.kryptonLabel15.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel15.Name = "kryptonLabel15";
            this.kryptonLabel15.Size = new System.Drawing.Size(40, 20);
            this.kryptonLabel15.TabIndex = 0;
            this.kryptonLabel15.Values.Text = "Color";
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(11, 109);
            this.dgvProductos.Margin = new System.Windows.Forms.Padding(2);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.RowHeadersWidth = 51;
            this.dgvProductos.RowTemplate.Height = 24;
            this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProductos.Size = new System.Drawing.Size(412, 323);
            this.dgvProductos.TabIndex = 0;
            // 
            // comboColores
            // 
            this.comboColores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboColores.DropDownWidth = 231;
            this.comboColores.Location = new System.Drawing.Point(115, 616);
            this.comboColores.Margin = new System.Windows.Forms.Padding(2);
            this.comboColores.Name = "comboColores";
            this.comboColores.Size = new System.Drawing.Size(298, 21);
            this.comboColores.TabIndex = 1;
            // 
            // dgvtallascolores
            // 
            this.dgvtallascolores.AllowUserToAddRows = false;
            this.dgvtallascolores.AllowUserToDeleteRows = false;
            this.dgvtallascolores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvtallascolores.Location = new System.Drawing.Point(10, 437);
            this.dgvtallascolores.Margin = new System.Windows.Forms.Padding(2);
            this.dgvtallascolores.Name = "dgvtallascolores";
            this.dgvtallascolores.ReadOnly = true;
            this.dgvtallascolores.RowHeadersWidth = 51;
            this.dgvtallascolores.RowTemplate.Height = 24;
            this.dgvtallascolores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvtallascolores.Size = new System.Drawing.Size(412, 155);
            this.dgvtallascolores.TabIndex = 11;
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(14, 39);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(46, 20);
            this.kryptonLabel1.TabIndex = 0;
            this.kryptonLabel1.Values.Text = "buscar";
            // 
            // txtbuscarp
            // 
            this.txtbuscarp.Location = new System.Drawing.Point(75, 39);
            this.txtbuscarp.Margin = new System.Windows.Forms.Padding(2);
            this.txtbuscarp.Name = "txtbuscarp";
            this.txtbuscarp.Size = new System.Drawing.Size(210, 23);
            this.txtbuscarp.TabIndex = 1;
            this.txtbuscarp.TextChanged += new System.EventHandler(this.txtbuscarp_TextChanged);
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(958, 34);
            this.kryptonLabel4.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(93, 20);
            this.kryptonLabel4.TabIndex = 32;
            this.kryptonLabel4.Values.Text = "No. Correlativo";
            // 
            // lbcorrelativo
            // 
            this.lbcorrelativo.Location = new System.Drawing.Point(1079, 33);
            this.lbcorrelativo.Margin = new System.Windows.Forms.Padding(2);
            this.lbcorrelativo.Name = "lbcorrelativo";
            this.lbcorrelativo.Size = new System.Drawing.Size(50, 20);
            this.lbcorrelativo.TabIndex = 33;
            this.lbcorrelativo.Values.Text = "xxxxxxx";
            // 
            // VistaPrincipalTraslado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1476, 749);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.toolStrip);
            this.Name = "VistaPrincipalTraslado";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.Load += new System.EventHandler(this.VistaPrincipalTraslado_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListaAPedir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvproductosEncontrados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbsucursales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.combotallas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboColores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvtallascolores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btnAgregar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel16;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox combotallas;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel15;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvProductos;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox comboColores;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvtallascolores;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtbuscarp;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnbuscar;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbsucursales;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAgregartoProducto;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvproductosEncontrados;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSolicitarProductos;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvListaAPedir;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbnoVale;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtfechaEmision;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblestado;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbcorrelativo;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
    }
}