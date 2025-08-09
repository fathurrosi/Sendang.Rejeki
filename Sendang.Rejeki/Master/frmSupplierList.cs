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
using Newtonsoft.Json;

namespace Sendang.Rejeki.Master
{
    public partial class frmSupplierList : Form, IMasterHeader, IMasterFooter
    {
        public frmSupplierList()
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
            List<Supplier> list = SupplierItem.GetPaging(text, pageIndex, pageSize, out totalRecord);
            grid.DataSource = list;
            ctlFooter1.TotalRows = totalRecord;
        }

        public void Add()
        {
            frmSupplier f = new frmSupplier();
            //f.Username = Utilities.Username;
            f.Tag = this.Tag;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }

        public void Edit()
        {
            if (grid.CurrentRow == null) return;
            frmSupplier f = new frmSupplier();
            int Row = grid.CurrentRow.Index;
            f.Code = string.Format("{0}", grid[0, Row].Value);
            //f.Username = Utilities.Username;
            f.Tag = this.Tag;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }


        public void Delete()
        {
            if (grid.CurrentRow == null) return;
            int Row = grid.CurrentRow.Index;
            DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                Supplier item = SupplierItem.GetByCode(grid[0, Row].Value.ToString());
                int result = SupplierItem.Delete(grid[0, Row].Value.ToString());
                if (result > 0)
                {
                    Log.Delete(JsonConvert.SerializeObject(item));
                    Search();
                }
            }
        }


        private void frmSupplierList_Load(object sender, EventArgs e)
        {
            Search();
        }

        private void grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView dv = (DataGridView)sender;
            DataGridViewRow row = dv.Rows[e.RowIndex];
            row.MinimumHeight = 200;

        }


        static frmSupplierList _instance;
        public static frmSupplierList GetInstance()
        {
            if (_instance == null)
            {
                _instance = new frmSupplierList();
            }
            return _instance;
        }

        private void frmSupplierList_FormClosed(object sender, FormClosedEventArgs e)
        {
            _instance = null;
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
    }
}
