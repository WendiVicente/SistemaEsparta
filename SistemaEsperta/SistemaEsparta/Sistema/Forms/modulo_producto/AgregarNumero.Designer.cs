namespace Sistema.Forms.modulo_producto
{
    partial class AgregarNumero
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAgregarlista = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnAgregar = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvTomo = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.pTomo = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.kryptonLabel1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbTomos = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.pEdicion = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.kryptonLabel2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.cbEdiciones = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.dgvEdicion = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTomo)).BeginInit();
            this.pTomo.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbTomos)).BeginInit();
            this.pEdicion.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbEdiciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdicion)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAgregarlista,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(387, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnAgregarlista
            // 
            this.btnAgregarlista.Image = global::Sistema.Properties.Resources.Checkmark_green_12x_16x;
            this.btnAgregarlista.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAgregarlista.Name = "btnAgregarlista";
            this.btnAgregarlista.Size = new System.Drawing.Size(73, 24);
            this.btnAgregarlista.Text = "Guardar";
            this.btnAgregarlista.Click += new System.EventHandler(this.btnAgregarlista_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.btnAgregar);
            this.kryptonPanel1.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 27);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(387, 503);
            this.kryptonPanel1.TabIndex = 2;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(117, 443);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(138, 46);
            this.btnAgregar.TabIndex = 1;
            this.btnAgregar.Values.Text = "Agregar";
            this.btnAgregar.Click += new System.EventHandler(this.btnagregar_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvTomo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pTomo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pEdicion, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dgvEdicion, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.85068F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.81448F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24.66063F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.1267F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(387, 438);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvTomo
            // 
            this.dgvTomo.AllowUserToAddRows = false;
            this.dgvTomo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTomo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTomo.Location = new System.Drawing.Point(2, 101);
            this.dgvTomo.Margin = new System.Windows.Forms.Padding(2);
            this.dgvTomo.Name = "dgvTomo";
            this.dgvTomo.ReadOnly = true;
            this.dgvTomo.RowHeadersWidth = 51;
            this.dgvTomo.RowTemplate.Height = 24;
            this.dgvTomo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTomo.Size = new System.Drawing.Size(383, 86);
            this.dgvTomo.TabIndex = 9;
            // 
            // pTomo
            // 
            this.pTomo.BackColor = System.Drawing.Color.White;
            this.pTomo.Controls.Add(this.groupBox2);
            this.pTomo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pTomo.Location = new System.Drawing.Point(2, 2);
            this.pTomo.Margin = new System.Windows.Forms.Padding(2);
            this.pTomo.Name = "pTomo";
            this.pTomo.Size = new System.Drawing.Size(383, 95);
            this.pTomo.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.kryptonLabel1);
            this.groupBox2.Controls.Add(this.cbTomos);
            this.groupBox2.Location = new System.Drawing.Point(9, 13);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(375, 76);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tomo";
            // 
            // kryptonLabel1
            // 
            this.kryptonLabel1.Location = new System.Drawing.Point(42, 23);
            this.kryptonLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel1.Name = "kryptonLabel1";
            this.kryptonLabel1.Size = new System.Drawing.Size(111, 20);
            this.kryptonLabel1.TabIndex = 3;
            this.kryptonLabel1.Values.Text = "Personalizar Tomo";
            // 
            // cbTomos
            // 
            this.cbTomos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTomos.DropDownWidth = 139;
            this.cbTomos.Location = new System.Drawing.Point(175, 23);
            this.cbTomos.Margin = new System.Windows.Forms.Padding(2);
            this.cbTomos.Name = "cbTomos";
            this.cbTomos.Size = new System.Drawing.Size(145, 21);
            this.cbTomos.TabIndex = 25;
            this.cbTomos.SelectedIndexChanged += new System.EventHandler(this.cbTomos_SelectedIndexChanged);
            // 
            // pEdicion
            // 
            this.pEdicion.BackColor = System.Drawing.Color.White;
            this.pEdicion.Controls.Add(this.groupBox1);
            this.pEdicion.Dock = System.Windows.Forms.DockStyle.Top;
            this.pEdicion.Location = new System.Drawing.Point(2, 191);
            this.pEdicion.Margin = new System.Windows.Forms.Padding(2);
            this.pEdicion.Name = "pEdicion";
            this.pEdicion.Size = new System.Drawing.Size(383, 101);
            this.pEdicion.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.kryptonLabel2);
            this.groupBox1.Controls.Add(this.cbEdiciones);
            this.groupBox1.Location = new System.Drawing.Point(9, 11);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(375, 82);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Edición";
            // 
            // kryptonLabel2
            // 
            this.kryptonLabel2.Location = new System.Drawing.Point(34, 22);
            this.kryptonLabel2.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonLabel2.Name = "kryptonLabel2";
            this.kryptonLabel2.Size = new System.Drawing.Size(119, 20);
            this.kryptonLabel2.TabIndex = 3;
            this.kryptonLabel2.Values.Text = "Personalizar Edición";
            // 
            // cbEdiciones
            // 
            this.cbEdiciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEdiciones.DropDownWidth = 139;
            this.cbEdiciones.Location = new System.Drawing.Point(175, 21);
            this.cbEdiciones.Margin = new System.Windows.Forms.Padding(2);
            this.cbEdiciones.Name = "cbEdiciones";
            this.cbEdiciones.Size = new System.Drawing.Size(145, 21);
            this.cbEdiciones.TabIndex = 25;
            this.cbEdiciones.SelectedIndexChanged += new System.EventHandler(this.cbEdiciones_SelectedIndexChanged);
            // 
            // dgvEdicion
            // 
            this.dgvEdicion.AllowUserToAddRows = false;
            this.dgvEdicion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEdicion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEdicion.Location = new System.Drawing.Point(2, 298);
            this.dgvEdicion.Margin = new System.Windows.Forms.Padding(2);
            this.dgvEdicion.Name = "dgvEdicion";
            this.dgvEdicion.ReadOnly = true;
            this.dgvEdicion.RowHeadersWidth = 51;
            this.dgvEdicion.RowTemplate.Height = 24;
            this.dgvEdicion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEdicion.Size = new System.Drawing.Size(383, 138);
            this.dgvEdicion.TabIndex = 7;
            // 
            // AgregarNumero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 530);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AgregarNumero";
            this.Text = "Agregar Numero";
            this.Load += new System.EventHandler(this.AgregarColorTalla_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTomo)).EndInit();
            this.pTomo.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbTomos)).EndInit();
            this.pEdicion.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbEdiciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEdicion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAgregarlista;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pEdicion;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbEdiciones;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvEdicion;
        private System.Windows.Forms.GroupBox groupBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvTomo;
        private System.Windows.Forms.Panel pTomo;
        private System.Windows.Forms.GroupBox groupBox2;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbTomos;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAgregar;
    }
}