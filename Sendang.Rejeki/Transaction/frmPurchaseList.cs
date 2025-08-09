﻿using DataLayer;
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
    public partial class frmPurchaseList : Form, IMasterHeader, IMasterFooter
    {
        public frmPurchaseList()
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
            List<CstmPurchase> list = PurchaseItem.GetCstmPaging(text, pageIndex, pageSize, out totalRecord);
            grid.AutoGenerateColumns = false;
            grid.DataSource = list;
            ctlFooter1.TotalRows = totalRecord;// PurchaseItem.GetCstmRecordCount(text);
        }

        public void Add()
        {
            frmPurchase f = new frmPurchase();
            //f.Username = Utilities.Username;
            f.PurchaseNo = string.Empty;
            f.Tag = this.Tag;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }

        public void Edit()
        {
            if (grid.CurrentRow == null) return;
            frmPurchase f = new frmPurchase();
            int Row = grid.CurrentRow.Index;
            f.PurchaseNo = string.Format("{0}", grid[0, Row].Value);
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
            int rowIndex = grid.CurrentRow.Index;
            DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this?\nDeleting this would update current stock", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string PurchaseNo = string.Format("{0}", grid.Rows[rowIndex].Cells["Code"].Value);
                int result = PurchaseItem.DeleteStock(PurchaseNo);
                if (result > 0) Search();
            }
        }

        private void frmPurchaseList_Load(object sender, EventArgs e)
        {
            //ctlHeader1.EditButtonText = Utilities.IsSuperAdmin() ? "Edit" : "View";
            //ctlHeader1.DeleteButtonEnabled = Utilities.IsSuperAdmin();
            Search();
        }

        public void Print()
        {
            throw new NotImplementedException();
        }
    }
}
