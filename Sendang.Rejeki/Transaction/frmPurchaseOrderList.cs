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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sendang.Rejeki.Transaction
{
    /// <summary>
    /// 1. Create PO (Draft)
    /// 2. Submit PO for approval(Pending)
    /// 3. Approve PO(Approved)
    /// 4. Receive items(Partially Received or Fully Received)
    /// 5. Receive invoice(Invoiced)
    /// 6. Make payment(Paid)
    /// 7. Close PO(Closed)
    /// </summary>
    public partial class frmPurchaseOrderList : Form, IMasterHeader, IMasterFooter
    {
        public frmPurchaseOrderList()
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
            List<PurchaseOrder> list = PurchaseOrderItem.GetPaging(text, pageIndex, pageSize, out totalRecord);
            grid.AutoGenerateColumns = false;
            grid.DataSource = list;
            ctlFooter1.TotalRows = totalRecord;
        }

        public void Add()
        {
            frmPurchaseOrder f = new frmPurchaseOrder();
            f.PurchaseOrderCode = string.Empty;
            f.Tag = this.Tag;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }

        public void Edit()
        {
            if (grid.CurrentRow == null) return;
            frmPurchaseOrder f = new frmPurchaseOrder();
            int Row = grid.CurrentRow.Index;
            f.PurchaseOrderCode = string.Format("{0}", grid[0, Row].Value);
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
                string PurchaseOrderCode = string.Format("{0}", grid.Rows[rowIndex].Cells["Code"].Value);
                int result = PurchaseOrderItem.Delete(PurchaseOrderCode);
                if (result > 0) Search();
            }
        }

        private void frmPurchaseList_Load(object sender, EventArgs e)
        {
            Search();
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        public void Enter()
        {
            //throw new NotImplementedException();
        }
    }

}
