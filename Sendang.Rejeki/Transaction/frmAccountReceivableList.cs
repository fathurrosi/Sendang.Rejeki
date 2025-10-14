using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicLayer;
using DataObject;
using DataLayer;

namespace Sendang.Rejeki.Transaction
{
    public partial class frmAccountReceivableList : Form, IMasterHeader, IMasterFooter
    {
        public frmAccountReceivableList()
        {
            InitializeComponent();
        }

        public void Search()
        {
            string textToSearch = ctlHeader1.TextToSearch;
            LoadData(textToSearch, ctlFooter1.PageIndex, ctlFooter1.PageSize);
        }

        void LoadData(string text, int pageIndex, int pageSize)
        {
            int totalRecord = 0;
            List<Invoice> list = InvoiceItem.GetPaging(text, pageIndex, pageSize, out totalRecord);
            grid.AutoGenerateColumns = false;
            grid.DataSource = list;
            ctlFooter1.TotalRows = totalRecord;
        }

        public void Add()
        {
            frmAccountReceivable f = new frmAccountReceivable();
            f.Tag = this.Tag;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }

        public void Edit()
        {
            if (grid.CurrentRow == null) return;
            frmAccountReceivable f = new frmAccountReceivable();
            int Row = grid.CurrentRow.Index;
            f.Tag = this.Tag;
            f.InvoceNo = string.Format("{0}", grid["colInvoiceNo", Row].Value);
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }


        public void Delete()
        {
            //if (grid.CurrentRow == null) return;
            //int Row = grid.CurrentRow.Index;
            //DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            //{
            //    int ID = 0;
            //    int.TryParse(grid["Code", Row].Value.ToString(), out ID);
            //    Catalog item = CatalogItem.GetByID(ID);
            //    int result = CatalogItem.Delete(ID);
            //    if (result > 0)
            //    {
            //        Log.Delete(JsonConvert.SerializeObject(item));
            //        Search();
            //    }
            //}
        }



        private void grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView dv = (DataGridView)sender;
            DataGridViewRow row = dv.Rows[e.RowIndex];
            //row.MinimumHeight = 200;

        }

        private void frmAccountReceivableList_Load(object sender, EventArgs e)
        {
            Search();
        }

        public void Print()
        {
            //string reportPath = Directory.GetCurrentDirectory() + "\\Report\\Catalog.rdlc";
            //LogicLayer.Helper.Report rpt = new LogicLayer.Helper.Report();
            //rpt.Print(reportPath, "Catalog", CatalogItem.GetAll());
        }


    }
}
