namespace Sistema.Forms.modulo_compras
{
    partial class EditarSolicitud
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditarSolicitud));
            this.label12 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ListaProductSelect = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.lbFechasolicitud = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbbaseimponible = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbtotal = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbimpuesto = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbestado = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel5 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dtpEntrega = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.cbproveedor = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.txtcomprobante = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonLabel4 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel3 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnguardar = new System.Windows.Forms.ToolStripButton();
            this.btnvalidar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnagregarproducto = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnaddmore = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBorrar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListaProductSelect)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbproveedor)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.RoyalBlue;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Location = new System.Drawing.Point(896, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(230, 3);
            this.label12.TabIndex = 16;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.ListaProductSelect);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 279);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1158, 394);
            this.panel2.TabIndex = 4;
            // 
            // ListaProductSelect
            // 
            this.ListaProductSelect.AllowUserToAddRows = false;
            this.ListaProductSelect.AllowUserToDeleteRows = false;
            this.ListaProductSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListaProductSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListaProductSelect.Location = new System.Drawing.Point(0, 0);
            this.ListaProductSelect.Name = "ListaProductSelect";
            this.ListaProductSelect.ReadOnly = true;
            this.ListaProductSelect.RowHeadersWidth = 51;
            this.ListaProductSelect.RowTemplate.Height = 24;
            this.ListaProductSelect.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ListaProductSelect.Size = new System.Drawing.Size(1158, 394);
            this.ListaProductSelect.TabIndex = 1;
            this.ListaProductSelect.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.ListaProductSelect_RowsAdded);
            this.ListaProductSelect.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.ListaProductSelect_RowsRemoved);
            this.ListaProductSelect.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.ListaProductSelect_UserDeletedRow);
            // 
            // lbFechasolicitud
            // 
            this.lbFechasolicitud.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFechasolicitud.Location = new System.Drawing.Point(793, 37);
            this.lbFechasolicitud.Name = "lbFechasolicitud";
            this.lbFechasolicitud.Size = new System.Drawing.Size(328, 25);
            this.lbFechasolicitud.TabIndex = 8;
            this.lbFechasolicitud.Text = "dd/mm/yyyy";
            this.lbFechasolicitud.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.RoyalBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(796, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(326, 3);
            this.label2.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label11.Location = new System.Drawing.Point(917, 15);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(146, 18);
            this.label11.TabIndex = 18;
            this.label11.Text = "Base imponible:Q.";
            // 
            // lbbaseimponible
            // 
            this.lbbaseimponible.AutoSize = true;
            this.lbbaseimponible.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbbaseimponible.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lbbaseimponible.Location = new System.Drawing.Point(1073, 13);
            this.lbbaseimponible.Name = "lbbaseimponible";
            this.lbbaseimponible.Size = new System.Drawing.Size(49, 24);
            this.lbbaseimponible.TabIndex = 22;
            this.lbbaseimponible.Text = "0.00";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.lbtotal);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.lbimpuesto);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.lbbaseimponible);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 569);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1158, 104);
            this.panel3.TabIndex = 5;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // lbtotal
            // 
            this.lbtotal.AutoSize = true;
            this.lbtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtotal.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lbtotal.Location = new System.Drawing.Point(1050, 62);
            this.lbtotal.Name = "lbtotal";
            this.lbtotal.Size = new System.Drawing.Size(54, 25);
            this.lbtotal.TabIndex = 27;
            this.lbtotal.Text = "0.00";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label6.Location = new System.Drawing.Point(948, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 24);
            this.label6.TabIndex = 26;
            this.label6.Text = "Total:  Q.";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.RoyalBlue;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(896, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 3);
            this.label4.TabIndex = 25;
            // 
            // lbimpuesto
            // 
            this.lbimpuesto.AutoSize = true;
            this.lbimpuesto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbimpuesto.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lbimpuesto.Location = new System.Drawing.Point(1073, 37);
            this.lbimpuesto.Name = "lbimpuesto";
            this.lbimpuesto.Size = new System.Drawing.Size(44, 20);
            this.lbimpuesto.TabIndex = 24;
            this.lbimpuesto.Text = "0.00";
            this.lbimpuesto.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label3.Location = new System.Drawing.Point(961, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 18);
            this.label3.TabIndex = 23;
            this.label3.Text = "Impuesto:Q.";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.panel3);
            this.kryptonPanel1.Controls.Add(this.panel2);
            this.kryptonPanel1.Controls.Add(this.panel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 27);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(1158, 673);
            this.kryptonPanel1.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lbestado);
            this.panel1.Controls.Add(this.kryptonLabel5);
            this.panel1.Controls.Add(this.dtpEntrega);
            this.panel1.Controls.Add(this.cbproveedor);
            this.panel1.Controls.Add(this.txtcomprobante);
            this.panel1.Controls.Add(this.kryptonLabel4);
            this.panel1.Controls.Add(this.kryptonLabel3);
            this.panel1.Controls.Add(this.kryptonLabel2);
            this.panel1.Controls.Add(this.kryptonLabel1);
            this.panel1.Controls.Add(this.lbFechasolicitud);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1158, 279);
            this.panel1.TabIndex = 3;
            // 
            // lbestado
            // 
            this.lbestado.Location = new System.Drawing.Point(797, 199);
            this.lbestado.Name = "lbestado";
            this.lbestado.Size = new System.Drawing.Size(160, 24);
            this.lbestado.TabIndex = 26;
            this.lbestado.Values.Text = "Estado Actual compra";
            // 
            // kryptonLabel5
            // 
            this.kryptonLabel5.Location = new System.Drawing.Point(689, 199);
            this.kryptonLabel5.Name = "kryptonLabel5";
            this.kryptonLabel5.Size = new System.Drawing.Size(60, 24);
            this.kryptonLabel5.TabIndex = 25;
            this.kryptonLabel5.Values.Text = "Estado:";
            // 
            // dtpEntrega
            // 
            this.dtpEntrega.Location = new System.Drawing.Point(796, 117);
            this.dtpEntrega.Name = "dtpEntrega";
            this.dtpEntrega.Size = new System.Drawing.Size(325, 25);
            this.dtpEntrega.TabIndex = 24;
            // 
            // cbproveedor
            // 
            this.cbproveedor.DropDownWidth = 326;
            this.cbproveedor.Location = new System.Drawing.Point(189, 122);
            this.cbproveedor.Name = "cbproveedor";
            this.cbproveedor.Size = new System.Drawing.Size(326, 25);
            this.cbproveedor.TabIndex = 23;
            // 
            // txtcomprobante
            // 
            this.txtcomprobante.Location = new System.Drawing.Point(189, 47);
            this.txtcomprobante.Name = "txtcomprobante";
            this.txtcomprobante.Size = new System.Drawing.Size(326, 27);
            this.txtcomprobante.TabIndex = 22;
            // 
            // kryptonLabel4
            // 
            this.kryptonLabel4.Location = new System.Drawing.Point(614, 117);
            this.kryptonLabel4.Name = "kryptonLabel4";
            this.kryptonLabel4.Size = new System.Drawing.Size(117, 24);
            this.kryptonLabel4.TabIndex = 21;
            this.kryptonLabel4.Values.Text = "Fecha Estimada";
            // 
            // kryptonLabel3
            // 
            this.kryptonLabel3.Location = new System.Drawing.Point(614, 44);
            this.kryptonLabel3.Name = "kryptonLabel3";
            this.kryptonLabel3.Size = new System.Drawing.Size(135, 24);
            this.kryptonLabel3.TabIndex = 20;
            this.kryptonLabel3.Values.Text = "Fecha de Solicitud";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(30, 123);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(82, 24);
            this.kryptonLabel2.TabIndex = 19;
            this.kryptonLabel2.Values.Text = "Proveedor";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(30, 50);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(134, 24);
            this.kryptonLabel1.TabIndex = 18;
            this.kryptonLabel1.Values.Text = "No. Comprobante";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnguardar,
            this.toolStripSeparator1,
            this.btnvalidar,
            this.toolStripSeparator2,
            this.btnagregarproducto,
            this.toolStripSeparator3,
            this.btnaddmore,
            this.toolStripSeparator4,
            this.btnBorrar,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1158, 27);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnguardar
            // 
            this.btnguardar.Image = global::Sistema.Properties.Resources.SaveStatusBar1_16x;
            this.btnguardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnguardar.Name = "btnguardar";
            this.btnguardar.Size = new System.Drawing.Size(158, 24);
            this.btnguardar.Text = "Actualizar Registro";
            this.btnguardar.Click += new System.EventHandler(this.btnguardar_Click);
            // 
            // btnvalidar
            // 
            this.btnvalidar.Image = global::Sistema.Properties.Resources.Checkmark_green_12x_16x;
            this.btnvalidar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnvalidar.Name = "btnvalidar";
            this.btnvalidar.Size = new System.Drawing.Size(136, 24);
            this.btnvalidar.Text = "Validar Compra";
            this.btnvalidar.Click += new System.EventHandler(this.btnvalidar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // btnagregarproducto
            // 
            this.btnagregarproducto.Image = global::Sistema.Properties.Resources.Add_8x_16x;
            this.btnagregarproducto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnagregarproducto.Name = "btnagregarproducto";
            this.btnagregarproducto.Size = new System.Drawing.Size(151, 24);
            this.btnagregarproducto.Text = "Agregar Producto";
            this.btnagregarproducto.Click += new System.EventHandler(this.btnagregarproducto_Click_1);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // btnaddmore
            // 
            this.btnaddmore.Image = global::Sistema.Properties.Resources.Checkmark_green_12x_16x;
            this.btnaddmore.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnaddmore.Name = "btnaddmore";
            this.btnaddmore.Size = new System.Drawing.Size(153, 24);
            this.btnaddmore.Text = "Cambiar Cantidad";
            this.btnaddmore.Click += new System.EventHandler(this.btnaddmore_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // btnBorrar
            // 
            this.btnBorrar.Image = global::Sistema.Properties.Resources._0405_20_delete;
            this.btnBorrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(138, 24);
            this.btnBorrar.Text = "Borrar Producto";
            this.btnBorrar.Click += new System.EventHandler(this.btnBorrar_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // EditarSolicitud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 700);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditarSolicitud";
            this.Text = "EditarSolicitud";
            this.Load += new System.EventHandler(this.EditarSolicitud_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ListaProductSelect)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbproveedor)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbFechasolicitud;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbbaseimponible;
        private System.Windows.Forms.Panel panel3;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnguardar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        //private System.Windows.Forms.DataGridViewTextBoxColumn idCompra;
        public ComponentFactory.Krypton.Toolkit.KryptonDataGridView ListaProductSelect;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker dtpEntrega;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbproveedor;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtcomprobante;
        private System.Windows.Forms.ToolStripButton btnagregarproducto;
        private System.Windows.Forms.ToolStripButton btnvalidar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbestado;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel5;
        private System.Windows.Forms.Label lbimpuesto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbtotal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripButton btnaddmore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnBorrar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}