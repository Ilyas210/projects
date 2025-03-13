using CodeGeneratorProject.BusinessLayerFun;
using CodeGeneratorProject.DataAcessFun;
using GeneratorBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGeneratorProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTable ColumnsTable;
        void FillComboBox1()
        {
            DataTable dataTable = ClsGeneratorBusiness.GetDataBases();
            foreach (DataRow Row in dataTable.Rows)
            {
                cbxDataBase.Items.Add(Row[0].ToString());
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            FillComboBox1();
            cbxDataBase.SelectedIndex = 0;
            btnGenerateDataLayer.Enabled = false;
            btnGenerateBusinessLayer.Enabled = false;
            btnCopy.Enabled = false;



        }

        void FillCombox2()
        {
            DataTable dataTable = ClsGeneratorBusiness.GetTables(cbxDataBase.Text.ToString());
            foreach (DataRow Row in dataTable.Rows)
            {
                cbxTable.Items.Add(Row[0].ToString());
            }
            
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

            cbxTable.Items.Clear();
            if (cbxDataBase.Text != "")
            {

                FillCombox2();
                cbxTable.SelectedIndex = 0;
            }
        }

        void fillComboBox4()
        {
            ColumnsTable = ClsGeneratorBusiness.GetColumns(cbxDataBase.Text, cbxTable.Text);
            foreach (DataRow Row in ColumnsTable.Rows)
            {
                cbxFindBy.Items.Add(Row[0].ToString());
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxFindBy.Items.Clear();
            if (cbxTable.Text != "")
            {

                fillComboBox4();
                cbxFindBy.SelectedIndex = 0;
            }

        }

        private void btnListColumns_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTableSingularName.Text))
            {
                txtTableSingularName.Focus();
                errorProvider1.SetError(txtTableSingularName, "Shouls have a value!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtTableSingularName, "");
                btnGenerateDataLayer.Enabled = true;

            }
            dataGridView1.DataSource = ClsGeneratorBusiness.GetTableProperty(cbxDataBase.Text, cbxTable.Text);
        }

        private void btnGenerateDataLayer_Click(object sender, EventArgs e)
        {
            string Text = "";

            Text += "public class clsData" + txtTableSingularName.Text + "\n{";
            Text += clsFind.GenerateFunFind(cbxTable.Text, txtTableSingularName.Text, cbxFindBy.Text,
                 dataGridView1.DataSource);

            Text += clsGetList.GenerateGetListFun(cbxTable.Text);

            Text += clsAddNew.GenerateFun(cbxTable.Text, txtTableSingularName.Text, dataGridView1.DataSource);

            Text += clsUpdate.GenerateUpdateFun(cbxTable.Text, txtTableSingularName.Text, dataGridView1.DataSource, cbxFindBy.Text);

            Text += clsDelete.GenerateDeleteFun(cbxTable.Text, txtTableSingularName.Text, dataGridView1.DataSource);

            Text += clsIsExist.GenerateFun(cbxTable.Text, txtTableSingularName.Text, dataGridView1.DataSource);
            Text += "}";
            btnGenerateBusinessLayer.Enabled = true;
            btnCopy.Enabled = true;

            rtbCode.Text = Text;
        }

        private void btnGenerateBusinessLayer_Click(object sender, EventArgs e)
        {
            rtbCode.Clear();

            rtbCode.Text += "public class cls" + txtTableSingularName.Text + "\n{";
            rtbCode.Text += clsMembers_cons.GenerateMembers_Constructors(txtTableSingularName.Text, dataGridView1.DataSource);
            rtbCode.Text += clsFindBus.GenerateFun(txtTableSingularName.Text, cbxFindBy.Text, dataGridView1.DataSource);
            rtbCode.Text += clsIsExistBus.GenerateFun(txtTableSingularName.Text, dataGridView1.DataSource);
            rtbCode.Text += clsGetListBus.GenerateFun(txtTableSingularName.Text, cbxTable.Text);
            rtbCode.Text += clsAddNewBus.GenerateFun(txtTableSingularName.Text, dataGridView1.DataSource);
            rtbCode.Text += clsUpdateBus.GenerateFun(txtTableSingularName.Text, dataGridView1.DataSource);
            rtbCode.Text += clsSave.GenerateFun(txtTableSingularName.Text);
            rtbCode.Text += clsDeleteBus.GenerateFun(txtTableSingularName.Text, dataGridView1.DataSource);
            rtbCode.Text += "\n}";
            btnCopy.Enabled = true;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rtbCode.Text);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            
            rtbCode.Clear();
            dataGridView1.Refresh();
            cbxDataBase.SelectedIndex = 0;
            cbxTable.SelectedIndex = 0;
            cbxFindBy.SelectedIndex = 0;
            btnGenerateDataLayer.Enabled = false;
            btnGenerateBusinessLayer.Enabled = false;
            btnCopy.Enabled = false;
            txtTableSingularName.Clear();
        }

        private void cbxFindBy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
