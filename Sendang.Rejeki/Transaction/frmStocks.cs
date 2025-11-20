using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataLayer;
using DataObject;
using LogicLayer;
using System.IO;

namespace Sendang.Rejeki.Transaction
{
    public partial class frmStocks : Form, IMasterHeader, IMasterFooter
    {
        public frmStocks()
        {
            InitializeComponent();
        }
        public void Enter()
        {
            //throw new NotImplementedException();
        }
        private void frmStocks_Load(object sender, EventArgs e)
        {
            //ctlHeader1.EditButtonEnabled = Utilities.IsSuperAdmin();
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
            //List<ColiPerCatalog> ColiPurchaseList = CurrentStockItem.GetAllColiPurchase();
            //List<ColiPerCatalog> ColiSaleList = CurrentStockItem.GetAllColiSales();
            List<CurrentStock> list = CurrentStockItem.GetCurrentStockPaging(text, pageIndex, pageSize, out totalRecord);
            //ColiPerCatalog
            //for (int i = 0; i < list.Count; i++)
            //{
            //    //if (1 == list[i].CatalogID)
            //    //{
            //    //    string test = list[i].CatalogName;
            //    //}
            //    ColiPerCatalog ColiPurchase = ColiPurchaseList.Where(t => t.CatalogID == list[i].CatalogID).FirstOrDefault();
            //    ColiPerCatalog ColiSale = ColiSaleList.Where(t => t.CatalogID == list[i].CatalogID).FirstOrDefault();
            //    decimal ColiJual = (ColiSale == null) ? 0 : ColiSale.TotalColi;
            //    decimal ColiBeli = (ColiPurchase == null) ? 0 : ColiPurchase.TotalColi;
            //    list[i].Coli = ColiBeli - ColiJual;
            //}

            grid.DataSource = list;
            ctlFooter1.TotalRows = totalRecord;// CurrentStockItem.GetRecordCount(text);
        }

        public void Add()
        {
            //throw new NotImplementedException();
        }

        public void Edit()
        {
            if (grid.CurrentRow == null) return;
            frmCatalogStock f = new frmCatalogStock();
            int Row = grid.CurrentRow.Index;
            int ID = 0;
            int.TryParse(string.Format("{0}", grid["colCatalogID", Row].Value), out ID);
            decimal currentStock = 0;
            decimal.TryParse(string.Format("{0}", grid["colStock", Row].Value), out currentStock);


            decimal currentColly = 0;
            decimal.TryParse(string.Format("{0}", grid["colColi", Row].Value), out currentColly);

            f.CatalogID = ID;
            f.CatalogName = string.Format("{0}", grid["colCatalogName", Row].Value);
            f.CurrentStock = currentStock;
            f.CurrentColly = currentColly;
            f.CatalogUnit = string.Format("{0}", grid["colUnit", Row].Value);
            f.Tag = this.Tag;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Search();
            }
        }

        public void Delete()
        {
            return;
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
        }


    }
}
