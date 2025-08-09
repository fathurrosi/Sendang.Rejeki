using DataLayer;
using DataObject;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sendang.Rejeki.Transaction
{
    public partial class frmStockList : Form, IMasterHeader, IMasterFooter
    {       
        public frmStockList()
        {
            InitializeComponent();
        }

        public void Search()
        {
            string textToSearch = ctlHeader1.TextToSearch;
            LoadData(textToSearch, ctlFooter1.Offset, ctlFooter1.PageSize);
        }

        void LoadData(string text, int offset, int pageSize)
        {

            List<CstmCatalogStock> list = CatalogStockItem.GetPaging(text, offset, pageSize);
            grid.DataSource = list;
            ctlFooter1.TotalRows = CatalogStockItem.GetRecordCount(text);
        }

        public void Add()
        {
            //frmCatalog f = new frmCatalog();
            //f.Username = Utilities.Username;
            //if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    Search();
            //}
        }

        public void Edit()
        {
            frmCatalogStock f = new frmCatalogStock();
            int Row = grid.CurrentRow.Index;
            int ID = 0;
            int.TryParse(string.Format("{0}", grid["colCatalogID", Row].Value), out ID);
            decimal currentStock =0;
            decimal.TryParse(string.Format("{0}",grid["colStock", Row].Value), out currentStock);
            f.CatalogID = ID;
            f.CatalogName =string.Format("{0}",grid["colCatalogName", Row].Value);
            f.CurrentStock= currentStock;
            f.CatalogUnit = string.Format("{0}", grid["colUnit", Row].Value);
            //f.Username = Utilities.Username;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }

        public void Delete()
        {
            return;
            //int Row = grid.CurrentRow.Index;
            //DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            //{
            //    int ID = 0;
            //    int.TryParse(grid[0, Row].Value.ToString(), out ID);

            //    int result = CatalogStcokItem.Delete(ID);
            //    if (result > 0) Search();
            //}
        }


        private void grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView dv = (DataGridView)sender;
            DataGridViewRow row = dv.Rows[e.RowIndex]; 
        }
        private void frmStock_Load(object sender, EventArgs e)
        {
            Search();
        }


        public void Print()
        {
            //string reportPath = Directory.GetCurrentDirectory() + "\\Report\\Stock.rdlc";
            //Report.frmReportViewer f = new Report.frmReportViewer();
            //f.ReportName = "Stock";
            //f.ReportPath = reportPath;
            //f.DataSource = CatalogStockItem.GetAll();
            //f.ShowDialog();
        }
    }
}
