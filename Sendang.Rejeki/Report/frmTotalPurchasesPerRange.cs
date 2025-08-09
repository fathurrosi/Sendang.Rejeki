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
using Microsoft.Reporting.WinForms;
namespace Sendang.Rejeki.Report
{
    public partial class frmTotalPurchasesPerRange : Form
    {
        public frmTotalPurchasesPerRange()
        {
            InitializeComponent();
        }

        private void frmTotalPurchasesPerRange_Load(object sender, EventArgs e)
        {
            this.Text = "Total Purchases Per Supplier";
            dateTimePickerFrom.Format = DateTimePickerFormat.Custom;
            dateTimePickerFrom.CustomFormat = "dd-MMM-yyyy";
            dateTimePickerFrom.ShowUpDown = false;
            dateTimePickerFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            dateTimePickerTo.Format = DateTimePickerFormat.Custom;
            dateTimePickerTo.CustomFormat = "dd-MMM-yyyy";
            dateTimePickerTo.ShowUpDown = false;
            dateTimePickerTo.Value = DateTime.Now;

            List<Supplier> suppliers = SupplierItem.GetAll().OrderBy(t => t.Name).ToList();

            suppliers.Insert(0, new Supplier() { Code = "", Name = "--Pilih Supplier--" });

            cboSupplier.DataSource = suppliers;
            cboSupplier.ValueMember = "Code";
            cboSupplier.DisplayMember = "Name";

            //cboBulan.DataSource = Utilities.GetAllMonth();
            //cboBulan.ValueMember = "ID";
            //cboBulan.DisplayMember = "Name";
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string supplierCode = string.Format("{0}", cboSupplier.SelectedValue);
            dataGridView1.DataSource = PurchaseDetailItem.GetTotalPurchasesPerSupplierDaily(supplierCode, dateTimePickerFrom.Value, dateTimePickerTo.Value);
        }

        private void dateTimePickerFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            string supplierCode = string.Format("{0}", cboSupplier.SelectedValue);
            DateTime from = dateTimePickerFrom.Value;
            DateTime to = dateTimePickerTo.Value;
            List<CstmPurchasesPerSupplierDaily> list = PurchaseDetailItem.GetTotalPurchasesPerSupplierDaily(supplierCode, from, to);

            if (list.Count == 0)
            {
                Utilities.ShowInformation("No record found!");
                return;
            }
            string reportPath = Directory.GetCurrentDirectory();
            frmReportViewer rptViewer = new Report.frmReportViewer();
            string title = string.Format("Total Purchases of ({0:dd MMM yyyy} - {1:dd MMM yyyy})", from, to);

            rptViewer.Params.Add(new ReportParameter("title", title, false));
            rptViewer.ReportName = "DataSet1";
            rptViewer.ReportText = "Total Sales Per Customer Daily";
            rptViewer.ReportPath = string.Format("{0}\\Report\\TotalPurchasesPerSupplierDaily.rdlc", reportPath);
            rptViewer.DataSource = list;
            rptViewer.WindowState = FormWindowState.Maximized;
            rptViewer.Show();
        }
    }
}
