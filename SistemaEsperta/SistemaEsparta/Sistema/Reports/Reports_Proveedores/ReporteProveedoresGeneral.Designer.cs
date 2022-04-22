namespace Sistema.Reports.Reports_Proveedores
{
    partial class ReporteProveedoresGeneral
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteProveedoresGeneral));
            this.ListarProveedoresBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rvProveedores = new Microsoft.Reporting.WinForms.ReportViewer();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.ListarProveedoresBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListarProveedoresBindingSource
            // 
            this.ListarProveedoresBindingSource.DataSource = typeof(CapaDatos.ListasPersonalizadas.ListarProveedores);
            // 
            // rvProveedores
            // 
            this.rvProveedores.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "listageneralproveedores";
            reportDataSource1.Value = this.ListarProveedoresBindingSource;
            this.rvProveedores.LocalReport.DataSources.Add(reportDataSource1);
            this.rvProveedores.LocalReport.ReportEmbeddedResource = "Sistema.Reports.Reports_Proveedores.ReporteProveedoresGeneral.rdlc";
            this.rvProveedores.Location = new System.Drawing.Point(0, 0);
            this.rvProveedores.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rvProveedores.Name = "rvProveedores";
            this.rvProveedores.ServerReport.BearerToken = null;
            this.rvProveedores.Size = new System.Drawing.Size(600, 366);
            this.rvProveedores.TabIndex = 0;
            this.rvProveedores.Load += new System.EventHandler(this.rvProveedores_Load);
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.rvProveedores);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(600, 366);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // ReporteProveedoresGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.kryptonPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ReporteProveedoresGeneral";
            this.Text = "ReporteProveedoresGeneral";
            this.Load += new System.EventHandler(this.ReporteProveedoresGeneral_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ListarProveedoresBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvProveedores;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private System.Windows.Forms.BindingSource ListarProveedoresBindingSource;
    }
}