using DinerBusinessLogic;
using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace Diner
{
    public partial class FormFillStorage : Form
    {
        [Dependency]
        public new Unity.IUnityContainer Container { get; set; }
        private readonly IFoodLogic logicF;
        private readonly MainLogic logicM;
        private readonly IStorageLogic logicS;
        public FormFillStorage(IFoodLogic logicF, MainLogic logicM, IStorageLogic logicS)
        {
            InitializeComponent();
            this.logicF = logicF;
            this.logicM = logicM;
            this.logicS = logicS;
        }
        private void FormFillStorage_Load(object sender, EventArgs e)
        {
            try
            {
                var storageList = logicS.Read(null);
                comboBoxStorage.DataSource = storageList;
                comboBoxStorage.DisplayMember = "StorageName";
                comboBoxStorage.ValueMember = "Id";
                var FoodList = logicF.Read(null);
                comboBoxFood.DataSource = FoodList;
                comboBoxFood.DisplayMember = "FoodName";
                comboBoxFood.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните количество", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStorage.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
MessageBoxIcon.Error);
                return;
            }
            if (comboBoxFood.SelectedValue == null)
            {
                MessageBox.Show("Выберите продукт", "Ошибка", MessageBoxButtons.OK,
MessageBoxIcon.Error);
                return;
            }
            try
            {
                int storageId = Convert.ToInt32(comboBoxStorage.SelectedValue);
                int foodId = Convert.ToInt32(comboBoxFood.SelectedValue);
                int count = Convert.ToInt32(textBoxCount.Text);
                this.logicM.FillStorage(new StorageFoodBindingModel
                {
                    StorageId = storageId,
                    FoodId = foodId,
                    Count = count
                });
                MessageBox.Show("Склад успешно пополнен", "Сообщение",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
MessageBoxIcon.Error);
            }
            Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}