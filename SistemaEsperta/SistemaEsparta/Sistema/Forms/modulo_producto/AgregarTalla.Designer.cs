namespace Sistema.Forms.modulo_producto
{
    partial class AgregarTalla
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
            this.kryptonPanel5 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.rbtallapersonaliz = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numericTalla = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.rbTallaNumero = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rbcomboTallas = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.comboTallas = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.lbMedida = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtTalla = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.btnagregar = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonPanel4 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonPanel3 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.dgvtallaslist = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel5)).BeginInit();
            this.kryptonPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboTallas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel4)).BeginInit();
            this.kryptonPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvtallaslist)).BeginInit();
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
            this.toolStrip1.Size = new System.Drawing.Size(324, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
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
            // kryptonPanel5
            // 
            this.kryptonPanel5.Controls.Add(this.toolStrip1);
            this.kryptonPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel5.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel5.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel5.Name = "kryptonPanel5";
            this.kryptonPanel5.Size = new System.Drawing.Size(324, 24);
            this.kryptonPanel5.TabIndex = 4;
            // 
            // rbtallapersonaliz
            // 
            this.rbtallapersonaliz.Location = new System.Drawing.Point(15, 129);
            this.rbtallapersonaliz.Margin = new System.Windows.Forms.Padding(2);
            this.rbtallapersonaliz.Name = "rbtallapersonaliz";
            this.rbtallapersonaliz.Size = new System.Drawing.Size(117, 20);
            this.rbtallapersonaliz.TabIndex = 27;
            this.rbtallapersonaliz.Values.Text = "Personalizar Talla";
            this.rbtallapersonaliz.CheckedChanged += new System.EventHandler(this.rbcolorespersonal_CheckedChanged);
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.panel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel2.Location = new System.Drawing.Point(2, 2);
            this.kryptonPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(320, 176);
            this.kryptonPanel2.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.numericTalla);
            this.panel1.Controls.Add(this.rbTallaNumero);
            this.panel1.Controls.Add(this.rbtallapersonaliz);
            this.panel1.Controls.Add(this.rbcomboTallas);
            this.panel1.Controls.Add(this.comboTallas);
            this.panel1.Controls.Add(this.lbMedida);
            this.panel1.Controls.Add(this.txtTalla);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(320, 176);
            this.panel1.TabIndex = 5;
            // 
            // numericTalla
            // 
            this.numericTalla.Location = new System.Drawing.Point(157, 72);
            this.numericTalla.Name = "numericTalla";
            this.numericTalla.Size = new System.Drawing.Size(143, 22);
            this.numericTalla.TabIndex = 30;
            this.numericTalla.Visible = false;
            // 
            // rbTallaNumero
            // 
            this.rbTallaNumero.Location = new System.Drawing.Point(15, 74);
            this.rbTallaNumero.Margin = new System.Windows.Forms.Padding(2);
            this.rbTallaNumero.Name = "rbTallaNumero";
            this.rbTallaNumero.Size = new System.Drawing.Size(113, 20);
            this.rbTallaNumero.TabIndex = 28;
            this.rbTallaNumero.Values.Text = "Talla en Numero";
            this.rbTallaNumero.CheckedChanged += new System.EventHandler(this.rbTallaNumero_CheckedChanged);
            // 
            // rbcomboTallas
            // 
            this.rbcomboTallas.Location = new System.Drawing.Point(15, 29);
            this.rbcomboTallas.Margin = new System.Windows.Forms.Padding(2);
            this.rbcomboTallas.Name = "rbcomboTallas";
            this.rbcomboTallas.Size = new System.Drawing.Size(97, 20);
            this.rbcomboTallas.TabIndex = 26;
            this.rbcomboTallas.Values.Text = "Talla en Letra:";
            this.rbcomboTallas.CheckedChanged += new System.EventHandler(this.rblistacolores_CheckedChanged);
            // 
            // comboTallas
            // 
            this.comboTallas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTallas.DropDownWidth = 139;
            this.comboTallas.Location = new System.Drawing.Point(157, 28);
            this.comboTallas.Margin = new System.Windows.Forms.Padding(2);
            this.comboTallas.Name = "comboTallas";
            this.comboTallas.Size = new System.Drawing.Size(143, 21);
            this.comboTallas.TabIndex = 25;
            this.comboTallas.Visible = false;
            this.comboTallas.SelectedIndexChanged += new System.EventHandler(this.comboTallas_SelectedIndexChanged);
            // 
            // lbMedida
            // 
            this.lbMedida.Location = new System.Drawing.Point(144, 129);
            this.lbMedida.Name = "lbMedida";
            this.lbMedida.Size = new System.Drawing.Size(52, 20);
            this.lbMedida.TabIndex = 24;
            this.lbMedida.Values.Text = "Medida";
            this.lbMedida.Visible = false;
            // 
            // txtTalla
            // 
            this.txtTalla.Location = new System.Drawing.Point(202, 129);
            this.txtTalla.Name = "txtTalla";
            this.txtTalla.Size = new System.Drawing.Size(98, 23);
            this.txtTalla.TabIndex = 18;
            this.txtTalla.Visible = false;
            // 
            // btnagregar
            // 
            this.btnagregar.Location = new System.Drawing.Point(104, 2);
            this.btnagregar.Margin = new System.Windows.Forms.Padding(2);
            this.btnagregar.Name = "btnagregar";
            this.btnagregar.Size = new System.Drawing.Size(130, 35);
            this.btnagregar.TabIndex = 5;
            this.btnagregar.Values.Text = "Agregar";
            this.btnagregar.Click += new System.EventHandler(this.btnagregar_Click);
            // 
            // kryptonPanel4
            // 
            this.kryptonPanel4.Controls.Add(this.btnagregar);
            this.kryptonPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel4.Location = new System.Drawing.Point(2, 367);
            this.kryptonPanel4.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel4.Name = "kryptonPanel4";
            this.kryptonPanel4.Size = new System.Drawing.Size(320, 47);
            this.kryptonPanel4.TabIndex = 8;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel1.Controls.Add(this.kryptonPanel5);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(324, 440);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kryptonPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kryptonPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.kryptonPanel4, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.47826F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.71154F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.01923F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(324, 416);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.dgvtallaslist);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel3.Location = new System.Drawing.Point(2, 182);
            this.kryptonPanel3.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(320, 181);
            this.kryptonPanel3.TabIndex = 7;
            // 
            // dgvtallaslist
            // 
            this.dgvtallaslist.AllowUserToAddRows = false;
            this.dgvtallaslist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvtallaslist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvtallaslist.Location = new System.Drawing.Point(0, 0);
            this.dgvtallaslist.Margin = new System.Windows.Forms.Padding(2);
            this.dgvtallaslist.Name = "dgvtallaslist";
            this.dgvtallaslist.ReadOnly = true;
            this.dgvtallaslist.RowHeadersWidth = 51;
            this.dgvtallaslist.RowTemplate.Height = 24;
            this.dgvtallaslist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvtallaslist.Size = new System.Drawing.Size(320, 181);
            this.dgvtallaslist.TabIndex = 0;
            this.dgvtallaslist.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvtallaslist_UserDeletingRow);
            // 
            // AgregarTalla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 440);
            this.Controls.Add(this.kryptonPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AgregarTalla";
            this.Text = "Agregar Talla";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AgregarTalla_FormClosing);
            this.Load += new System.EventHandler(this.AgregarTalla_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel5)).EndInit();
            this.kryptonPanel5.ResumeLayout(false);
            this.kryptonPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboTallas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel4)).EndInit();
            this.kryptonPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvtallaslist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripButton btnAgregarlista;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel5;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbtallapersonaliz;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbcomboTallas;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox comboTallas;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbMedida;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtTalla;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnagregar;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbTallaNumero;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown numericTalla;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel3;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvtallaslist;
    }
}