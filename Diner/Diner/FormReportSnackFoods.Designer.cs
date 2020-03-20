namespace Diner
{
    partial class FormReportSnackFoods
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
            this.buttonMake = new System.Windows.Forms.Button();
            this.buttonToPdf = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // buttonMake
            // 
            this.buttonMake.Location = new System.Drawing.Point(13, 6);
            this.buttonMake.Name = "buttonMake";
            this.buttonMake.Size = new System.Drawing.Size(117, 20);
            this.buttonMake.TabIndex = 4;
            this.buttonMake.Text = "Сформировать";
            this.buttonMake.UseVisualStyleBackColor = true;
            this.buttonMake.Click += new System.EventHandler(this.ButtonMake_Click);
            // 
            // buttonToPdf
            // 
            this.buttonToPdf.Location = new System.Drawing.Point(152, 6);
            this.buttonToPdf.Name = "buttonToPdf";
            this.buttonToPdf.Size = new System.Drawing.Size(144, 20);
            this.buttonToPdf.TabIndex = 5;
            this.buttonToPdf.Text = "в Pdf";
            this.buttonToPdf.UseVisualStyleBackColor = true;
            this.buttonToPdf.Click += new System.EventHandler(this.ButtonToPdf_Click);
            // 
            // reportViewer
            // 
            this.reportViewer.LocalReport.ReportEmbeddedResource = "Diner.Report.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(13, 32);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(656, 305);
            this.reportViewer.TabIndex = 1;
            // 
            // FormReportSnackFoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 347);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.buttonToPdf);
            this.Controls.Add(this.buttonMake);
            this.Name = "FormReportSnackFoods";
            this.Text = "Отчеты";
            this.Load += new System.EventHandler(this.FormReportSnackFoods_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonMake;
        private System.Windows.Forms.Button buttonToPdf;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
    }
}