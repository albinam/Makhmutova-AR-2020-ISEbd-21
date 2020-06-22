using DinerBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DinerClientView
{
    public partial class FormMessages : Form
    {
        public FormMessages()
        {
            InitializeComponent();
        }
        private void FormMessages_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var list = APIClient.GetRequest<List<MessageInfoViewModel>>($"api/client/getmessages?clientId={Program.Client.Id}");
                if (list != null)
                {
                    Program.ConfigGrid(list, dataGridView);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}