﻿namespace Sistema.Forms.modulo_producto
{
    partial class AgregarColor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregarColor));
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel5 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnAgregarlista = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbvincular = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btPaleta = new System.Windows.Forms.Button();
            this.rbcolorespaleta = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rbcolorespersonal = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.rblistacolores = new ComponentFactory.Krypton.Toolkit.KryptonRadioButton();
            this.cbcoloresba = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.lbColor = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtColor = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.kryptonPanel3 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.dgvColoresadd = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.kryptonPanel4 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnagregar = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.Colorpaleta = new System.Windows.Forms.ColorDialog();
            this.txtVincularColor = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel5)).BeginInit();
            this.kryptonPanel5.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbcoloresba)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColoresadd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel4)).BeginInit();
            this.kryptonPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.kryptonPanel5);
            this.kryptonPanel1.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(379, 497);
            this.kryptonPanel1.TabIndex = 0;
            // 
            // kryptonPanel5
            // 
            this.kryptonPanel5.Controls.Add(this.toolStrip1);
            this.kryptonPanel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel5.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel5.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel5.Name = "kryptonPanel5";
            this.kryptonPanel5.Size = new System.Drawing.Size(379, 28);
            this.kryptonPanel5.TabIndex = 4;
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
            this.toolStrip1.Size = new System.Drawing.Size(379, 27);
            this.toolStrip1.TabIndex = 0;
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.kryptonPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.kryptonPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.kryptonPanel4, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(379, 497);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.Controls.Add(this.panel1);
            this.kryptonPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.kryptonPanel2.Location = new System.Drawing.Point(2, 2);
            this.kryptonPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.Size = new System.Drawing.Size(375, 219);
            this.kryptonPanel2.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txtVincularColor);
            this.panel1.Controls.Add(this.lbvincular);
            this.panel1.Controls.Add(this.btPaleta);
            this.panel1.Controls.Add(this.rbcolorespaleta);
            this.panel1.Controls.Add(this.rbcolorespersonal);
            this.panel1.Controls.Add(this.rblistacolores);
            this.panel1.Controls.Add(this.cbcoloresba);
            this.panel1.Controls.Add(this.lbColor);
            this.panel1.Controls.Add(this.txtColor);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(375, 219);
            this.panel1.TabIndex = 5;
            // 
            // lbvincular
            // 
            this.lbvincular.Location = new System.Drawing.Point(78, 183);
            this.lbvincular.Name = "lbvincular";
            this.lbvincular.Size = new System.Drawing.Size(100, 20);
            this.lbvincular.TabIndex = 40;
            this.lbvincular.Values.Text = "Vincular a Color:";
            // 
            // btPaleta
            // 
            this.btPaleta.BackColor = System.Drawing.Color.Transparent;
            this.btPaleta.Enabled = false;
            this.btPaleta.Location = new System.Drawing.Point(198, 86);
            this.btPaleta.Name = "btPaleta";
            this.btPaleta.Size = new System.Drawing.Size(52, 23);
            this.btPaleta.TabIndex = 39;
            this.btPaleta.UseVisualStyleBackColor = false;
            this.btPaleta.Visible = false;
            // 
            // rbcolorespaleta
            // 
            this.rbcolorespaleta.Location = new System.Drawing.Point(31, 86);
            this.rbcolorespaleta.Margin = new System.Windows.Forms.Padding(2);
            this.rbcolorespaleta.Name = "rbcolorespaleta";
            this.rbcolorespaleta.Size = new System.Drawing.Size(138, 20);
            this.rbcolorespaleta.TabIndex = 28;
            this.rbcolorespaleta.Values.Text = "Crear Desde la Paleta";
            this.rbcolorespaleta.CheckedChanged += new System.EventHandler(this.rbcolorespaleta_CheckedChanged);
            // 
            // rbcolorespersonal
            // 
            this.rbcolorespersonal.Location = new System.Drawing.Point(31, 124);
            this.rbcolorespersonal.Margin = new System.Windows.Forms.Padding(2);
            this.rbcolorespersonal.Name = "rbcolorespersonal";
            this.rbcolorespersonal.Size = new System.Drawing.Size(157, 20);
            this.rbcolorespersonal.TabIndex = 27;
            this.rbcolorespersonal.Values.Text = "Utilizar Codigos Alternos";
            this.rbcolorespersonal.CheckedChanged += new System.EventHandler(this.rbcolorespersonal_CheckedChanged);
            // 
            // rblistacolores
            // 
            this.rblistacolores.Location = new System.Drawing.Point(31, 40);
            this.rblistacolores.Margin = new System.Windows.Forms.Padding(2);
            this.rblistacolores.Name = "rblistacolores";
            this.rblistacolores.Size = new System.Drawing.Size(117, 20);
            this.rblistacolores.TabIndex = 26;
            this.rblistacolores.Values.Text = "Elegir desde Lista";
            this.rblistacolores.CheckedChanged += new System.EventHandler(this.rblistacolores_CheckedChanged);
            // 
            // cbcoloresba
            // 
            this.cbcoloresba.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbcoloresba.DropDownWidth = 139;
            this.cbcoloresba.Location = new System.Drawing.Point(198, 40);
            this.cbcoloresba.Margin = new System.Windows.Forms.Padding(2);
            this.cbcoloresba.Name = "cbcoloresba";
            this.cbcoloresba.Size = new System.Drawing.Size(128, 21);
            this.cbcoloresba.TabIndex = 25;
            this.cbcoloresba.SelectedIndexChanged += new System.EventHandler(this.cbcoloresba_SelectedIndexChanged);
            // 
            // lbColor
            // 
            this.lbColor.Location = new System.Drawing.Point(122, 149);
            this.lbColor.Name = "lbColor";
            this.lbColor.Size = new System.Drawing.Size(53, 20);
            this.lbColor.TabIndex = 24;
            this.lbColor.Values.Text = "Código:";
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(198, 146);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(128, 23);
            this.txtColor.TabIndex = 18;
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.dgvColoresadd);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel3.Location = new System.Drawing.Point(2, 225);
            this.kryptonPanel3.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.Size = new System.Drawing.Size(375, 219);
            this.kryptonPanel3.TabIndex = 7;
            // 
            // dgvColoresadd
            // 
            this.dgvColoresadd.AllowUserToAddRows = false;
            this.dgvColoresadd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColoresadd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColoresadd.Location = new System.Drawing.Point(0, 0);
            this.dgvColoresadd.Margin = new System.Windows.Forms.Padding(2);
            this.dgvColoresadd.Name = "dgvColoresadd";
            this.dgvColoresadd.ReadOnly = true;
            this.dgvColoresadd.RowHeadersWidth = 51;
            this.dgvColoresadd.RowTemplate.Height = 24;
            this.dgvColoresadd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvColoresadd.Size = new System.Drawing.Size(375, 219);
            this.dgvColoresadd.TabIndex = 0;
            this.dgvColoresadd.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dgvColoresadd_UserDeletingRow);
            // 
            // kryptonPanel4
            // 
            this.kryptonPanel4.Controls.Add(this.btnagregar);
            this.kryptonPanel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel4.Location = new System.Drawing.Point(2, 448);
            this.kryptonPanel4.Margin = new System.Windows.Forms.Padding(2);
            this.kryptonPanel4.Name = "kryptonPanel4";
            this.kryptonPanel4.Size = new System.Drawing.Size(375, 47);
            this.kryptonPanel4.TabIndex = 8;
            // 
            // btnagregar
            // 
            this.btnagregar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnagregar.Location = new System.Drawing.Point(120, 0);
            this.btnagregar.Margin = new System.Windows.Forms.Padding(2);
            this.btnagregar.Name = "btnagregar";
            this.btnagregar.Size = new System.Drawing.Size(130, 45);
            this.btnagregar.TabIndex = 5;
            this.btnagregar.Values.Text = "Agregar";
            this.btnagregar.Click += new System.EventHandler(this.btnagregar_Click);
            // 
            // txtVincularColor
            // 
            this.txtVincularColor.Location = new System.Drawing.Point(198, 183);
            this.txtVincularColor.Name = "txtVincularColor";
            this.txtVincularColor.Size = new System.Drawing.Size(128, 23);
            this.txtVincularColor.TabIndex = 41;
            // 
            // AgregarColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnagregar;
            this.ClientSize = new System.Drawing.Size(379, 497);
            this.Controls.Add(this.kryptonPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgregarColor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Color";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AgregarColor_FormClosing);
            this.Load += new System.EventHandler(this.AgregarColor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel5)).EndInit();
            this.kryptonPanel5.ResumeLayout(false);
            this.kryptonPanel5.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbcoloresba)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColoresadd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel4)).EndInit();
            this.kryptonPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel3;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvColoresadd;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel5;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnAgregarlista;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbcolorespaleta;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rbcolorespersonal;
        private ComponentFactory.Krypton.Toolkit.KryptonRadioButton rblistacolores;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbcoloresba;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbColor;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtColor;
        private System.Windows.Forms.ColorDialog Colorpaleta;
        private System.Windows.Forms.Button btPaleta;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lbvincular;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnagregar;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtVincularColor;
    }
}