using DinerBusinessLogic.BindingModels;
using DinerBusinessLogic.Interfaces;
using DinerBusinessLogic.ViewModels;
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
    public partial class FormStorage : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IStorageLogic logic;
        private int? id;
        private Dictionary<int, (string, int)> storageFoods;
        public FormStorage(IStorageLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }
        private void FormStorage_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    StorageViewModel view = logic.Read(new StorageBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.StorageName;
                        storageFoods = view.StorageFoods;
                    }
                    if (storageFoods != null)
                    {
                        dataGridView.Rows.Clear();
                        dataGridView.ColumnCount = 3;
                        dataGridView.Columns[0].Visible = false;
                        dataGridView.Columns[1].HeaderText = "Продукт";
                        dataGridView.Columns[2].HeaderText = "Количество";
                        dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        foreach (var sf in storageFoods)
                        {
                            dataGridView.Rows.Add(new object[] { sf.Key, sf.Value.Item1, sf.Value.Item2 });
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    logic.CreateOrUpdate(new StorageBindingModel
                    {
                        Id = id,
                        StorageName = textBoxName.Text
                    });
                }           
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}