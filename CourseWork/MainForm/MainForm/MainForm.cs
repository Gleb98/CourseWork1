
using Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using SD = System.Data;
using WF = System.Windows.Forms;

using System.Collections;

namespace MainForm
{
    public partial class MainForm : Form
    {
        private progressForm newProgForm = new progressForm();
        private DataSet result1 = new DataSet();
        private DataSet result2 = new DataSet();
        private SD.DataTable buffTable = new SD.DataTable();
        private DataSet result3 = new DataSet();
        //int indexOfTableToClone = 0;
        private int indexOfFacultyColumn = 0;
        public MainForm()
        {
            InitializeComponent();
            ofd1.Filter = "Excel Workbook| *. xlsx| Excel Workbook 97-2003| *. xls";
            sfd1.Filter = "Excel Workbook 97-2003| *. xls| Excel Workbook| *. xlsx";
            ofd1.ValidateNames = true;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        private void btnOpen_Click(object sender, EventArgs e)
        {



        }

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {

            dataGridView.DataSource = Result2.Tables[cboSheet.SelectedIndex];
            SD.DataTable dt = Result2.Tables[cboSheet.SelectedIndex];
            if (dt.TableName.Contains("Рабочий учебный план"))
            {
                cbPulpit.Items.Clear();
                cbPulpit.Items.Add("Отобразить всё");
                for (var j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j][IndexOfFacultyColumn].ToString() != "")
                    {

                        cbPulpit.Items.Add(dt.Rows[j][IndexOfFacultyColumn].ToString().Trim());
                        cbPulpit.DropDownHeight += 20;

                    }


                }
            }
            else
            {
                cbPulpit.Items.Clear();
                cbPulpit.DropDownHeight = 15;
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (sfd1.ShowDialog() == DialogResult.OK)
            {

                // newProgForm.Show();
                // newProgForm.progressBar.Minimum = 0;
                // newProgForm.progressBar.Value = 0;
                backgroundWorker.RunWorkerAsync();
            }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = excel.Workbooks.Add();
            Worksheet ws = (Worksheet)excel.ActiveSheet;
            

        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error == null)
            {
                MessageBox.Show("Поток остановился успешно", "Успех!");
                Thread.Sleep(100);

               
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = File.Open(ofd1.FileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader;
                if (ofd1.FilterIndex == 2)
                {
                    reader = ExcelReaderFactory.CreateBinaryReader(fs);
                }
                else
                {
                    reader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                }
                reader.IsFirstRowAsColumnNames = true;
                Result1 = reader.AsDataSet();
                
                cbPulpit.Items.Clear();

                foreach (SD.DataTable dt in Result1.Tables)
                {
                    if (dt.TableName.Contains("Рабочий учебный план") || dt.TableName.Contains("РУП"))
                    {



                        for (var i = 0; i < dt.Columns.Count; i++)
                        {
                            if (dt.Columns[i].ColumnName.Contains("fuc"))
                            {

                                IndexOfFacultyColumn = i;
                                break;
                            }
                        }

                        Result2.Tables.Add(dt.Copy());
                        Result2.AcceptChanges();
                        cboSheet.Items.Add(dt.TableName);
                        cboSheet.DropDownHeight += 20;

                        
                    }
                    else
                    {
                        Result2.Tables.Add(dt.Copy());
                        Result3.Tables.Add(dt.Copy());
                        Result2.AcceptChanges();
                        Result3.AcceptChanges();
                        cboSheet.Items.Add(dt.TableName);
                        clbDiscip.Items.Add(dt.TableName);
                        cboSheet.DropDownHeight += 20;
                    }

                }

                reader.Close();

            }
        }

        private void cbPulpit_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (var j = 0; j < dataGridView.Rows.Count; j++)
            {
                CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dataGridView.DataSource];
                currencyManager1.SuspendBinding();
                dataGridView.Rows[j].Visible = true;
                currencyManager1.ResumeBinding();
            }

            for (var i = 0; i < dataGridView.Rows.Count; i++)
            {

                if (dataGridView[IndexOfFacultyColumn, i].Value.ToString() != cbPulpit.SelectedItem.ToString() && cbPulpit.SelectedItem.ToString() != "Отобразить всё")
                {

                    CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dataGridView.DataSource];

                    currencyManager1.SuspendBinding();
                    dataGridView.Rows[i].Visible = false;
                    currencyManager1.ResumeBinding();



                }
                else if (cbPulpit.SelectedItem.ToString() == "Отобразить всё")
                {
                    for (var j = 0; j < dataGridView.Rows.Count; j++)
                    {
                        CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dataGridView.DataSource];
                        currencyManager1.SuspendBinding();
                        dataGridView.Rows[i].Visible = true;
                        currencyManager1.ResumeBinding();
                    }

                }


            }




        }

        private void button1_Click(object sender, EventArgs e)
        {
            Result2.Tables.Clear();
            cboSheet.Items.Clear();
            cbPulpit.Items.Clear();
            dataGridView.DataSource = null;
            cboSheet.DropDownHeight = 15;
            cbPulpit.DropDownHeight = 15;
        }
        public WF.Label getDataLabel
        {
            get
            {
                return cDataLabel;
            }

        }
        public WF.Label getCafedraNameLabel
        {
            get
            {
                return cnLabel;
            }

        }
        public DataGridView getDGV
        {
            get
            {
                return dataGridView;
            }
        }
        public ComboBox getCBOSheet
        {
            get
            {
                return cboSheet;
            }
        }
        public ComboBox getCBPulpit
        {
            get
            {
                return cbPulpit;
            }
        }
        public OpenFileDialog getOfd1
        {
            get
            {
                return ofd1;
            }
        }

        public DataSet Result1 { get => result1; set => result1 = value; }
        public progressForm NewProgForm { get => newProgForm; set => newProgForm = value; }
        public DataSet Result2 { get => result2; set => result2 = value; }
        public SD.DataTable BuffTable { get => buffTable; set => buffTable = value; }
        public int IndexOfFacultyColumn { get => indexOfFacultyColumn; set => indexOfFacultyColumn = value; }
        public DataSet Result3 { get => result3; set => result3 = value; }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (nameOfColumnTB.Text != String.Empty)
            {
                for(int i = 0; i<dgvData.ColumnCount; i++)
                {
                    if (nameOfColumnTB.Text == dgvData.Columns[i].HeaderText)
                    {
                        dgvData.Columns.Remove(nameOfColumnTB.Text);
                        nameOfColumnTB.Clear();
                    }
                    //else
                    //{
                    //    MessageBox.Show("Название столбца введено не корректно!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //}
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите название столбца.", "Что-то произошло", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (nameOfColumnTB.Text != String.Empty)
            {
                dgvData.Columns.Add(nameOfColumnTB.Text, nameOfColumnTB.Text);
                nameOfColumnTB.Clear();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите название столбца.", "Что-то произошло", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void nameOfColumnTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAddSpec_Click(object sender, EventArgs e)
        {
            if(nameOfspecTB.Text != String.Empty)
            {
                int add = dgvData.Rows.Add(nameOfspecTB.Text.ToUpper());
                dgvData.Rows[add].Cells[0].Style.Font = new System.Drawing.Font(dgvData.DefaultCellStyle.Font, FontStyle.Bold);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddDiscip_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < clbDiscip.CheckedIndices.Count; i++)
            //{
            //    DataRow[] dr = Result2.Tables[i].Select();
            //    for(int j =0; j<dr.)
            //    dgvData.Rows.Add()
            //}
        }
    }
}
