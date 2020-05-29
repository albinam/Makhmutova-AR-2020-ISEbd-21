namespace DinerStorageView
{
    partial class FormStorage
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
            this.storageNameLabel = new System.Windows.Forms.Label();
            this.storageNameTextBox = new System.Windows.Forms.TextBox();
            this.foodsGroupBox = new System.Windows.Forms.GroupBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.foodsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // storageNameLabel
            // 
            this.storageNameLabel.AutoSize = true;
            this.storageNameLabel.Location = new System.Drawing.Point(21, 9);
            this.storageNameLabel.Name = "storageNameLabel";
            this.storageNameLabel.Size = new System.Drawing.Size(60, 13);
            this.storageNameLabel.TabIndex = 0;
            this.storageNameLabel.Text = "Название:";
            // 
            // storageNameTextBox
            // 
            this.storageNameTextBox.Location = new System.Drawing.Point(87, 6);
            this.storageNameTextBox.Name = "storageNameTextBox";
            this.storageNameTextBox.Size = new System.Drawing.Size(234, 20);
            this.storageNameTextBox.TabIndex = 1;
            // 
            // foodsGroupBox
            // 
            this.foodsGroupBox.Controls.Add(this.dataGridView);
            this.foodsGroupBox.Location = new System.Drawing.Point(11, 43);
            this.foodsGroupBox.Name = "foodsGroupBox";
            this.foodsGroupBox.Size = new System.Drawing.Size(405, 362);
            this.foodsGroupBox.TabIndex = 2;
            this.foodsGroupBox.TabStop = false;
            this.foodsGroupBox.Text = "Компоненты";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(0, 19);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(403, 342);
            this.dataGridView.TabIndex = 0;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(260, 411);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(341, 411);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormStorage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 443);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.foodsGroupBox);
            this.Controls.Add(this.storageNameTextBox);
            this.Controls.Add(this.storageNameLabel);
            this.Name = "FormStorage";
            this.Text = "Склад";
            this.Load += new System.EventHandler(this.FormStorage_Load);
            this.foodsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label storageNameLabel;
        private System.Windows.Forms.TextBox storageNameTextBox;
        private System.Windows.Forms.GroupBox foodsGroupBox;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
    }
}