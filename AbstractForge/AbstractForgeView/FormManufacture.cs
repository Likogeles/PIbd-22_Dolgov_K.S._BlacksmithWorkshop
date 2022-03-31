using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using AbstractForgeContracts.BindingModels;
using AbstractForgeContracts.BusinessLogicsContracts;
using AbstractForgeContracts.ViewModels;
using System.Windows.Forms;
using Unity;

namespace AbstractForgeView
{
    public partial class FormManufacture : Form
    {
        public int Id { set { id = value; } }
        private readonly IManufactureLogic _logic;
        private int? id;
        private Dictionary<int, (string, int)> manufactureComponents;
        public FormManufacture(IManufactureLogic logic)
        {
            InitializeComponent();
            _logic = logic;
            dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void FormProduct_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ManufactureViewModel view = _logic.Read(new ManufactureBindingModel
                    {
                        Id = id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.ManufactureName;
                        textBoxPrice.Text = view.Price.ToString();
                        manufactureComponents = view.ManufactureComponents;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                manufactureComponents = new Dictionary<int, (string, int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (manufactureComponents != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in manufactureComponents)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1, pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var form = Program.Container.Resolve<FormManufactureComponent>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (manufactureComponents.ContainsKey(Convert.ToInt32(form.Id)))
                {
                    manufactureComponents[form.Id] = (form.ComponentName, form.Count);
                }
                else
                {
                    manufactureComponents.Add(form.Id, (form.ComponentName, form.Count));
                }
                LoadData();
            }
        }
        private void ButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Program.Container.Resolve<FormManufactureComponent>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = manufactureComponents[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    manufactureComponents[form.Id] = (form.ComponentName, form.Count);
                    LoadData();
                }
            }
        }
        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        manufactureComponents.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (manufactureComponents == null || manufactureComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logic.CreateOrUpdate(new ManufactureBindingModel
                {
                    Id = id,
                    ManufactureName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    ManufactureComponents = manufactureComponents
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
