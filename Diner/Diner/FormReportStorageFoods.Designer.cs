namespace Diner
{
    partial class FormReportStorageFoods
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Storage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FoodName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ButtonSaveToExcel = new System.Windows.Forms.Button();
            this.buttonMake = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Storage,
            this.FoodName,
            this.Count});
            this.dataGridView.Location = new System.Drawing.Point(11, 35);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 28;
            this.dataGridView.Size = new System.Drawing.Size(343, 299);
            this.dataGridView.TabIndex = 3;
            // 
            // Storage
            // 
            this.Storage.HeaderText = "Склад";
            this.Storage.Name = "Storage";
            // 
            // FoodName
            // 
            this.FoodName.HeaderText = "Продукт";
            this.FoodName.Name = "FoodName";
            // 
            // Count
            // 
            this.Count.HeaderText = "Количество";
            this.Count.Name = "Count";
            // 
            // ButtonSaveToExcel
            // 
            this.ButtonSaveToExcel.Location = new System.Drawing.Point(374, 35);
            this.ButtonSaveToExcel.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonSaveToExcel.Name = "ButtonSaveToExcel";
            this.ButtonSaveToExcel.Size = new System.Drawing.Size(113, 23);
            this.ButtonSaveToExcel.TabIndex = 2;
            this.ButtonSaveToExcel.Text = "Сохранить в Excel";
            this.ButtonSaveToExcel.UseVisualStyleBackColor = true;
            this.ButtonSaveToExcel.Click += new System.EventHandler(this.ButtonSaveToExcel_Click);
            // 
            // buttonMake
            // 
            this.buttonMake.Location = new System.Drawing.Point(374, 72);
            this.buttonMake.Name = "buttonMake";
            this.buttonMake.Size = new System.Drawing.Size(113, 23);
            this.buttonMake.TabIndex = 4;
            this.buttonMake.Text = "Сформировать";
            this.buttonMake.UseVisualStyleBackColor = true;
            this.buttonMake.Click += new System.EventHandler(this.ButtonMake_Click);
            // 
            // FormReportStorageFoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 345);
            this.Controls.Add(this.buttonMake);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.ButtonSaveToExcel);
            this.Name = "FormReportStorageFoods";
            this.Text = "Отчет по продуктам на складе";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button ButtonSaveToExcel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Storage;
        private System.Windows.Forms.DataGridViewTextBoxColumn FoodName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.Button buttonMake;
    }
}