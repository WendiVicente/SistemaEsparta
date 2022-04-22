namespace Sistema.Forms.modulo_traslados
{
    partial class VerTraslados
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtbuscarSolictudes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvSolicitudesTraslados = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtbuscarpeticiones = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvPeticionestras = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitudesTraslados)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeticionestras)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 425);
            this.panel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 425);
            this.tabControl1.TabIndex = 22;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtbuscarSolictudes);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.dgvSolicitudesTraslados);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 399);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Solicitudes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtbuscarSolictudes
            // 
            this.txtbuscarSolictudes.Location = new System.Drawing.Point(59, 18);
            this.txtbuscarSolictudes.Name = "txtbuscarSolictudes";
            this.txtbuscarSolictudes.Size = new System.Drawing.Size(611, 20);
            this.txtbuscarSolictudes.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Buscar";
            // 
            // dgvSolicitudesTraslados
            // 
            this.dgvSolicitudesTraslados.AllowUserToAddRows = false;
            this.dgvSolicitudesTraslados.AllowUserToDeleteRows = false;
            this.dgvSolicitudesTraslados.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSolicitudesTraslados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSolicitudesTraslados.Location = new System.Drawing.Point(7, 42);
            this.dgvSolicitudesTraslados.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSolicitudesTraslados.Name = "dgvSolicitudesTraslados";
            this.dgvSolicitudesTraslados.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.dgvSolicitudesTraslados.ReadOnly = true;
            this.dgvSolicitudesTraslados.RowHeadersWidth = 51;
            this.dgvSolicitudesTraslados.RowTemplate.Height = 24;
            this.dgvSolicitudesTraslados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSolicitudesTraslados.Size = new System.Drawing.Size(768, 340);
            this.dgvSolicitudesTraslados.TabIndex = 21;
            this.dgvSolicitudesTraslados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSolicitudesTraslados_CellClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtbuscarpeticiones);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.dgvPeticionestras);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 399);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Peticiones";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtbuscarpeticiones
            // 
            this.txtbuscarpeticiones.Location = new System.Drawing.Point(53, 13);
            this.txtbuscarpeticiones.Name = "txtbuscarpeticiones";
            this.txtbuscarpeticiones.Size = new System.Drawing.Size(611, 20);
            this.txtbuscarpeticiones.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Buscar";
            // 
            // dgvPeticionestras
            // 
            this.dgvPeticionestras.AllowUserToAddRows = false;
            this.dgvPeticionestras.AllowUserToDeleteRows = false;
            this.dgvPeticionestras.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPeticionestras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPeticionestras.Location = new System.Drawing.Point(1, 37);
            this.dgvPeticionestras.Margin = new System.Windows.Forms.Padding(2);
            this.dgvPeticionestras.Name = "dgvPeticionestras";
            this.dgvPeticionestras.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.dgvPeticionestras.ReadOnly = true;
            this.dgvPeticionestras.RowHeadersWidth = 51;
            this.dgvPeticionestras.RowTemplate.Height = 24;
            this.dgvPeticionestras.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPeticionestras.Size = new System.Drawing.Size(778, 340);
            this.dgvPeticionestras.TabIndex = 24;
            // 
            // VerTraslados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "VerTraslados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VerTraslados";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.VerTraslados_Load);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolicitudesTraslados)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeticionestras)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panel1;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvSolicitudesTraslados;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtbuscarSolictudes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtbuscarpeticiones;
        private System.Windows.Forms.Label label2;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvPeticionestras;
    }
}