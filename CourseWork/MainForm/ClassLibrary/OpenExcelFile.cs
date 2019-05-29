//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using MainForm;
//namespace ClassLibrary
//{
//    public class OpenExcelFile
//    {

//        public static void getReader()
//        {
//            MainForm form1 = new MainForm();
//            form1 = (MainForm)form1;
//            FileStream fs = File.Open(MainForm.getOfd1.FileName, FileMode.Open, FileAccess.Read);
//            IExcelDataReader reader;
//            if (ofd1.FilterIndex == 2)
//            {
//                reader = ExcelReaderFactory.CreateBinaryReader(fs);
//            }
//            else
//            {
//                reader = ExcelReaderFactory.CreateOpenXmlReader(fs);
//            }
//            reader.IsFirstRowAsColumnNames = true;

//        }
//    }
//}
