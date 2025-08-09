﻿using DataLayer;
using DataObject;
using LogicLayer;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Sendang.Rejeki.Master
{
    public partial class frmCatalogList : Form, IMasterHeader, IMasterFooter
    {
        public frmCatalogList()
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
            List<Catalog> list = CatalogItem.GetPaging(text, pageIndex, pageSize, out totalRecord);
            grid.DataSource = list;
            ctlFooter1.TotalRows = totalRecord;
        }

        public void Add()
        {
            frmCatalog f = new frmCatalog();
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
            frmCatalog f = new frmCatalog();
            int Row = grid.CurrentRow.Index;
            int ID = 0;
            int.TryParse(grid["Code", Row].Value.ToString(), out ID);
            f.Tag = this.Tag;
            f.ID = ID;
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
                int ID = 0;
                int.TryParse(grid["Code", Row].Value.ToString(), out ID);
                Catalog item = CatalogItem.GetByID(ID);
                int result = CatalogItem.Delete(ID);
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

        private void frmCatalogList_Load(object sender, EventArgs e)
        {
            Search();
        }

        public void Print()
        {
            string reportPath = Directory.GetCurrentDirectory() + "\\Report\\Catalog.rdlc";
            LogicLayer.Helper.Report rpt = new LogicLayer.Helper.Report();
            rpt.Print(reportPath, "Catalog", CatalogItem.GetAll());
        }

    }
}
