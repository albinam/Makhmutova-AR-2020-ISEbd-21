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
    public partial class FormStorage : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        public FormStorage()
        {
            InitializeComponent();
        }
        private void FormStorage_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    StorageViewModel view = APIStorage.GetRequest<StorageViewModel>($"api/storage/getstorage?storageId={id}");
                    if (view != null)
                    {
                        textBoxName.Text = view.StorageName;
                    }
                    var storageList = APIStorage.GetRequest<List<StorageViewModel>>($"api/storage/getstorageslist");
                    var storageFoods = storageList[0].StorageFoods;
                    for (int i = 0; i < storageList.Count; ++i)
                    {
                        if (storageList[i].Id == id)
                        {
                            storageFoods = storageList[i].StorageFoods;
                        }
                    }
                    if (storageFoods != null)
                    {
                        dataGridView.DataSource = storageFoods;
                       // dataGridView.Columns[0].Visible = false;
                       // dataGridView.Columns[1].Visible = false;
                      //  dataGridView.Columns[2].Visible = false;
                      //  dataGridView.Columns[3].Visible = false;
                       // dataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните поле Название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                APIStorage.PostRequest("api/Storage/createorupdatestorage", new StorageBindingModel
                {
                    Id = id,
                    StorageName = textBoxName.Text
                });
                MessageBox.Show("Успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}