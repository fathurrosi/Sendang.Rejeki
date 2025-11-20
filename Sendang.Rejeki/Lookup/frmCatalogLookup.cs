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

namespace Sendang.Rejeki.Lookup
{
    public partial class frmCatalogLookup : Form, ITransButton, IMasterHeader, IMasterFooter
    {
        public Catalog Selected { get; set; }
        public frmCatalogLookup()
        {
            InitializeComponent();
            Search();

        }

        public void Search()
        {
            string textToSearch = ctlHeader1.TextToSearch;
            LoadData(textToSearch, ctlFooter1.PageIndex, ctlFooter1.PageSize);
        }

        void LoadData(string text, int pageIndex, int pageSize)
        {
            int totalRecord = 0;
            List<Catalog> list = CatalogItem.GetPaging(text, pageIndex, pageSize, out totalRecord);
            grid.AutoGenerateColumns = false;
            grid.DataSource = list;
            ctlFooter1.TotalRows = totalRecord;
        }

        public void Add()
        {
            return;
        }

        public void Edit()
        {
            return;
        }

        public void Delete()
        {
            return;
        }

        public bool IsValid()
        {
            return true;
        }

        public void Save()
        {
            try
            {
                if (grid.CurrentRow == null) return;
                int Row = grid.CurrentRow.Index;
                int id = 0;
                int.TryParse(string.Format("{0}", grid[0, Row].Value), out id);
                Selected = CatalogItem.GetByID(id);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                this.DialogResult = System.Windows.Forms.DialogResult.Retry;
            }
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Save();
        }

        public string TextToSearch { get; set; }


        public void Print()
        {  //string reportPath = Directory.GetCurrentDirectory() + "\\Report\\Catalog.rdlc";
            //Report.frmReportViewer f = new Report.frmReportViewer();
            //f.ReportName = "Catalog";
            //f.ReportPath = reportPath;
            //f.DataSource = CatalogItem.GetAll();
            //f.ShowDialog();
        }

        public void Enter()
        {
            //throw new NotImplementedException();
        }
    }
}
