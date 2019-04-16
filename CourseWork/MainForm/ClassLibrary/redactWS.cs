using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace ClassLibrary
{
    public class redactWS
    {
        public static void redactFirstRow(ref Worksheet ws, DataGridView dgv)
        {
            for (int i = 0; i<dgv.Rows[0].Cells.Count; i++)
            {
                ws.Cells[0, i] = dgv[0, i].Value;
            }
        }
        public static void redactOthers(ref Worksheet ws, DataGridView dgv)
        {
            for (int i = 1; i < dgv.Rows.Count; i++)
            {
                for (int j = 0; j<dgv.Rows[0].Cells.Count; j++)
                {
                    ws.Cells[i, j] = dgv[i, j].Value;
                }
            }
        }
    }
}
