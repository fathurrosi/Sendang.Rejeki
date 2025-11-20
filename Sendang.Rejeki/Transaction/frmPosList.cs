using DataLayer;
using DataObject;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sendang.Rejeki.Transaction
{
    public partial class frmPosList : Form, IMasterHeader, IMasterFooter
    {
        public frmPosList()
        {
            InitializeComponent();
        }

        public void Enter()
        {
            //throw new NotImplementedException();
        }
        //public void Search()
        //{
        //    string textToSearch = ctlHeader1.TextToSearch;
        //    LoadData(textToSearch, ctlFooter1.Offset, ctlFooter1.PageSize);
        //}

        //void LoadData(string text, int offset, int pageSize)
        //{
        //    List<Sale> list = SaleItem.GetPagingItemToProduct(text, offset, pageSize);
        //    grid.AutoGenerateColumns = false;
        //    grid.DataSource = list.OrderByDescending( t => t.TransactionDate).ToList();
        //    ctlFooter1.TotalRows = SaleItem.GetRecordCount(text);
        //}


        public void Search()
        {
            string textToSearch = ctlHeader1.TextToSearch;
            LoadData(textToSearch, ctlFooter1.PageIndex, ctlFooter1.PageSize);
        }

        void LoadData(string text, int pageIndex, int pageSize)
        {
            int totalRecord = 0;
            List<Sale> list = SaleItem.GetPaging(text, pageIndex, pageSize, out totalRecord);
            grid.DataSource = list;
            ctlFooter1.TotalRows = totalRecord;
        }



        public void Add()
        {
            frmPos f = new frmPos();
            //f.Username = Utilities.Username;
            f.TransactionID = string.Empty;
            f.Tag = this.Tag;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }

        public void Edit()
        {
            if (grid.CurrentRow == null) return;
            int Row = grid.CurrentRow.Index;
            Form f = null;
            //if (!Utilities.IsSuperAdmin())
            //    f = new frmPosView() { TransactionID = string.Format("{0}", grid[2, Row].Value) };
            //else f = new frmPos() { TransactionID = string.Format("{0}", grid[2, Row].Value) };
            f = new frmPos() { TransactionID = string.Format("{0}", grid["colTransactionID", Row].Value) };
            f.Tag = this.Tag;
            if (f != null && f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }


        public void Delete()
        {
            if (grid.CurrentRow == null) return;
            int rowIndex = grid.CurrentRow.Index;
            DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this?\nDeleting this would update current stock", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string transactionID = string.Format("{0}", grid.Rows[rowIndex].Cells["colTransactionID"].Value);
                int result = SaleItem.Delete(transactionID);
                if (result > 0) Search();
            }
        }


        private void frmPosList_Load(object sender, EventArgs e)
        {
            //ctlHeader1.NewButtonEnabled = false;
            //ctlHeader1.NewButtonEnabled = Utilities.IsSuperAdmin();
            //ctlHeader1.EditButtonText = Utilities.IsSuperAdmin() ? "Edit" : "View";
            //ctlHeader1.EditButtonEnabled = true;
            //ctlHeader1.DeleteButtonEnabled = Utilities.IsSuperAdmin();

            grid.AutoGenerateColumns = false;
            Search();
        }

        public void Print()
        {
            //string reportPath = Directory.GetCurrentDirectory() + "\\Report\\Catalog.rdlc";
            //Report.frmReportViewer f = new Report.frmReportViewer();
            //f.ReportName = "Catalog";
            //f.ReportPath = reportPath;
            //f.DataSource = CatalogItem.GetAll();
            //f.ShowDialog();
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 7)
            {
                frmUpdateExpiredDate f = new frmUpdateExpiredDate();
                f.Tag = this.Tag;
                if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //MessageBox.Show(string.Format("{0}", grid.Rows[e.RowIndex].Cells[2].Value));
                    DateTime expDate = f.ExpiredDate;
                    SaleItem.Update(string.Format("{0}", grid.Rows[e.RowIndex].Cells["colTransactionID"].Value), expDate);
                    Search();
                }
            }
            else
            {
                Edit();
            }
        }

        private void grid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
