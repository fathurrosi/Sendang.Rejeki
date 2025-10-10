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
using Newtonsoft.Json;
using System.IO;

namespace Sendang.Rejeki.Master
{
    public partial class frmOptions : Form, IMasterHeader, IMasterFooter
    {
        public frmOptions()
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
            List<Options> list = OptionItem.GetPaging(text, pageIndex, pageSize, out totalRecord);
            grid.DataSource = list;
            ctlFooter1.TotalRows = totalRecord;
        }

        public void Add()
        {
            frmOption f = new frmOption();
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
            frmOption f = new frmOption();
            int Row = grid.CurrentRow.Index;
            string name = grid["colName", Row].Value.ToString();
            string valueMember = grid["colValueMember", Row].Value.ToString();
            f.Tag = this.Tag;
            f.KeyName = name;
            f.KeyValueMember = valueMember;
            //f.Username = Utilities.Username;
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
                string name = grid["colName", Row].Value.ToString();
                string valueMember = grid["colValueMember", Row].Value.ToString();
                Options item = OptionItem.GetOptionsByKey(name, valueMember);
                int result = OptionItem.Delete(name, valueMember);
                if (result > 0)
                {
                    Log.Delete(JsonConvert.SerializeObject(item));
                    Search();
                }
            }
        }



        private void grid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView dv = (DataGridView)sender;
            DataGridViewRow row = dv.Rows[e.RowIndex];
            //row.MinimumHeight = 200;

        }

        private void frmOptionsList_Load(object sender, EventArgs e)
        {
            Search();
        }

        public void Print()
        {
            //string reportPath = Directory.GetCurrentDirectory() + "\\Report\\Options.rdlc";
            //LogicLayer.Helper.Report rpt = new LogicLayer.Helper.Report();
            //rpt.Print(reportPath, "Options", OptionItem.GetAll());
        }

    }
}
