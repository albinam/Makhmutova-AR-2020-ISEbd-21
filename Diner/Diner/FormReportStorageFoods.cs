using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.BusinessLogics;
using Unity;
using DinerBusinessLogic.Interfaces;

namespace Diner
{
    public partial class FormReportStorageFoods : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        private readonly IStorageLogic storageLogic;
        public FormReportStorageFoods(ReportLogic logic, IStorageLogic storageLogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.storageLogic = storageLogic;
        }
        private void ButtonMake_Click(object sender, EventArgs e)
        {
            try
            {
                var dict = storageLogic.Read(null);
                if (dict != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var Storage in dict)
                    {
                        int foodsSum = 0;
                        dataGridView.Rows.Add(new object[] { Storage.StorageName, "", "" });
                        foreach (var food in Storage.StorageFoods)
                        {
                            dataGridView.Rows.Add(new object[] { "", food.Value.Item1, food.Value.Item2 });
                            foodsSum += food.Value.Item2;
                        }
                        dataGridView.Rows.Add(new object[] { "Итого", "", foodsSum });
                        dataGridView.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ButtonSaveToExcel_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveStorageFoodsToExcelFile(new ReportBindingModel { FileName = dialog.FileName });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
