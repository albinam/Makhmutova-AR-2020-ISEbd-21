using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DinerStorageView
{
    public partial class FormFillStorage : Form
    {
        private int id;

        public FormFillStorage(int id)
        {
            InitializeComponent();
            this.id = id;
        }
        private void FormFillStorage_Load(object sender, System.EventArgs e)
        {
            try
            {
                List<FoodViewModel> list = APIStorage.GetRequest<List<FoodViewModel>>($"api/Storage/getfoodslist");
                if (list != null)
                {
                    comboBoxFood.DisplayMember = "FoodName";
                    comboBoxFood.ValueMember = "Id";
                    comboBoxFood.DataSource = list;
                    comboBoxFood.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonSave_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxFood.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                APIStorage.PostRequest("api/Storage/fillstorage", new StorageFoodBindingModel
                {
                    Id = 0,
                    StorageId = id,
                    FoodId = Convert.ToInt32(comboBoxFood.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}