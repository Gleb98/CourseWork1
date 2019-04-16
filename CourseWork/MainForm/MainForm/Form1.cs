
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
using ClassLibrary;

namespace MainForm
{
    public partial class Form1 : Form
    {
        progressForm newProgForm = new progressForm();
        public Form1()
        {
            InitializeComponent();
            ofd1.Filter = "Excel Workbook 97-2003| *. xls| Excel Workbook| *. xlsx";
            sfd1.Filter = "Excel Workbook 97-2003| *. xls| Excel Workbook| *. xlsx";
            ofd1.ValidateNames = true;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        DataSet result= new DataSet();

        private void btnOpen_Click(object sender, EventArgs e)
        {
            
                if (ofd1.ShowDialog()==DialogResult.OK)
                {
                    FileStream fs = File.Open(ofd1.FileName, FileMode.Open, FileAccess.Read);
                    IExcelDataReader reader;
                    if (ofd1.FilterIndex == 1)
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(fs);
                    }
                    else
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                    }
                    reader.IsFirstRowAsColumnNames = true;
                    result = reader.AsDataSet();
                    
                    cboSheet.Items.Clear();
                    foreach (SD.DataTable dt in result.Tables)
                        cboSheet.Items.Add(dt.TableName);
                    reader.Close();

                }
            
        }

        private void cboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView.DataSource = result.Tables[cboSheet.SelectedIndex];
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
            redactWS.redactFirstRow(ref ws, dataGridView);
            redactWS.redactOthers(ref ws, dataGridView);
            ws.SaveAs(sfd1.FileName);

        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
           // newProgForm.progressBar.Value = e.ProgressPercentage;
           // newProgForm.progressLabel.Text = string.Format("Загружка данных.Ожидайте... {0}", e.ProgressPercentage);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        
            if(e.Error == null)
            {
                MessageBox.Show("Поток остановился успешно", "Успех!");
                Thread.Sleep(100);
                
               // newProgForm.Close();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
