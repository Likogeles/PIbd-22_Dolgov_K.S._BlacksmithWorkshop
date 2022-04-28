using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.BusinessLogicsContracts;
using AbstractForgeContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace AbstractForgeView
{
    public partial class FormCreateOrder : Form
    {
        private readonly IManufactureLogic _logicM;
        private readonly IOrderLogic _logicO;
        private readonly IClientLogic _logicC;
        public FormCreateOrder(IManufactureLogic logicM, IOrderLogic logicO, IClientLogic logicC)
        {
            InitializeComponent();
            _logicM = logicM;
            _logicO = logicO;
            _logicC = logicC;
        }
        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            List<ManufactureViewModel> list = _logicM.Read(null);
            if (list != null)
            {
                comboBoxManufacture.DisplayMember = "ManufactureName";
                comboBoxManufacture.ValueMember = "Id";
                comboBoxManufacture.DataSource = list;
                comboBoxManufacture.SelectedItem = null;
            }
            List<ClientViewModel> listC = _logicC.Read(null);
            if (listC != null)
            {
                comboBoxClient.DisplayMember = "ClientFIO";
                comboBoxClient.ValueMember = "Id";
                comboBoxClient.DataSource = listC;
                comboBoxClient.SelectedItem = null;
            }
        }
        private void CalcSum()
        {
            if (comboBoxManufacture.SelectedValue != null &&
           !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxManufacture.SelectedValue);
                    ManufactureViewModel manufacture = _logicM.Read(new ManufactureBindingModel
                    {
                        Id = id
                    })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * manufacture?.Price ?? 0).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void ComboBoxManufacture_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxManufacture.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logicO.CreateOrder(new CreateOrderBindingModel
                {
                    ManufactureId = Convert.ToInt32(comboBoxManufacture.SelectedValue),
                    ClientId = Convert.ToInt32(comboBoxClient.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSum.Text)
                });
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
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
