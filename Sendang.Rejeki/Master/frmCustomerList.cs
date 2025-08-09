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
    public partial class frmCustomerList : Form, IMasterHeader, IMasterFooter
    {


        public frmCustomerList()
        {
            InitializeComponent();
        }

        //public void Search()
        //{
        //    string textToSearch = ctlHeader1.TextToSearch;
        //    LoadData(textToSearch, ctlFooter1.Offset, ctlFooter1.PageSize);
        //}

        //void LoadData(string text, int offset, int pageSize)
        //{

        //    List<Customer> list = CustomerItem.GetPaging(text, offset, pageSize);
        //    grid.DataSource = list;
        //    ctlFooter1.TotalRows = CustomerItem.GetRecordCount(text);
        //}

        public void Search()
        {
            string textToSearch = ctlHeader1.TextToSearch;
            LoadData(textToSearch, ctlFooter1.PageIndex, ctlFooter1.PageSize);
        }

        void LoadData(string text, int pageIndex, int pageSize)
        {
            int totalRecord = 0;
            List<Customer> list = CustomerItem.GetPaging(text, pageIndex, pageSize, out totalRecord);
            grid.DataSource = list;
            ctlFooter1.TotalRows = totalRecord;
        }



        public void Add()
        {
            frmCustomer f = new frmCustomer();
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
            frmCustomer f = new frmCustomer();
            int rowIndex = grid.CurrentRow.Index;
            int ID = 0;
            int.TryParse(string.Format("{0}", grid.Rows[rowIndex].Cells["colID"].Value), out ID);

            f.ID = ID;
            f.Tag = this.Tag;
            //f.Username = Utilities.Username;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }


        public void Delete()
        {
            if (grid.CurrentRow == null) return;
            int rowIndex = grid.CurrentRow.Index;
            DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                int ID = 0;
                int.TryParse(string.Format("{0}", grid.Rows[rowIndex].Cells["colID"].Value), out ID);
                Customer item = CustomerItem.GetByID(ID);
                int result = CustomerItem.Delete(ID);
                if (result > 0)
                {
                    Log.Delete(JsonConvert.SerializeObject(item));
                    Search();
                }
            }
        }



        private void frmCustomerList_Load(object sender, EventArgs e)
        {
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
    }
}
