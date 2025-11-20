using DataLayer;
using LogicLayer;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sendang.Rejeki.Report
{
    public partial class frmReportViewer : Form
    {
        public frmReportViewer()
        {
            InitializeComponent();
            Params = new List<ReportParameter>();
        }

        public object DataSource { get; set; }
        public string ReportPath { get; set; }
        public string ReportName { get; set; }
        public List<ReportParameter> Params { get; set; }

        public string ReportText { get; set; }
        private void frmReportViewer_Load(object sender, EventArgs e)
        {
            try
            {

                this.Text = string.Format("{0}", ReportText).Length > 0 ? ReportText : ReportName;
                this.ClientSize = new System.Drawing.Size(950, 600);

                // Set Processing Mode.
                reportViewer.ProcessingMode = ProcessingMode.Local;




                //ReportDataSource rptSource = new ReportDataSource("DataSet1", bindingSource);
                //rptViewer.LocalReport.DataSources.Add(rptSource);

                // Set RDL file.
                reportViewer.LocalReport.ReportPath = ReportPath;
                BindingSource bindingSource = new BindingSource();
                bindingSource.DataSource = DataSource;
                ReportDataSource rptSource = new ReportDataSource(ReportName, bindingSource);
                if (Params != null)
                    reportViewer.LocalReport.SetParameters(Params);
                reportViewer.LocalReport.DataSources.Add(rptSource);

                this.reportViewer.RefreshReport();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
