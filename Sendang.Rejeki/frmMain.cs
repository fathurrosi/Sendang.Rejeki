using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicLayer;
using Sendang.Rejeki.Master;
using Sendang.Rejeki.Transaction;
using System.Globalization;
using System.Threading;
using System.IO;
using DataLayer;
using DataObject;
using Microsoft.Reporting.WinForms;
using Sendang.Rejeki.Report;
namespace Sendang.Rejeki
{
    public partial class frmMain : Form
    {
        //LogicLayer.Log log = LogicLayer.Log.CreateInstance();
        public frmMain()
        {
            InitializeComponent();
            this.Text = string.Format(" Sendang Rejeki {0}", DateTime.Now.Year);
        }

        Form GetInstance(string formName)
        {
            
            Form activeForm = null;            
            switch (formName.ToLower().Trim())
            {
                case "logout":
                    Log.Info("Logout");
                    Application.Restart();
                    break;
                case "exit": ;
                    Log.Info("Close");
                    Application.Exit();
                    break;
                case "po_detail":
                    activeForm = new frmPurchaseOrder(); break;
                case "po_list":
                    activeForm = new frmPurchaseOrderList(); break;

                case "option":
                    activeForm = new frmOptions(); break;
                case "cst_invoice":
                    activeForm = new frmAccountReceivable(); break;
                case "cst_invoice_list":
                    activeForm = new frmAccountReceivableList(); break;
                case "catalog":
                    activeForm = new frmCatalogList(); break;
                case "supplier":
                    activeForm = new frmSupplierList(); break;
                case "purchase":
                    activeForm = new frmPurchase(); break;
                case "menu":
                    activeForm = new frmMenuList(); break;
                case "user":
                    activeForm = new frmUserList(); break;
                case "role":
                    activeForm = new frmRoleList(); break;
                case "previllage":
                    activeForm = new frmPrevillage(); break;
                case "catalogprice":
                    activeForm = new frmCatalogPriceList(); break;
                case "salespoint":
                    activeForm = new frmPos(); break;
                case "salelist":
                    activeForm = new frmPosList(); break;
                case "polist":
                    activeForm = new frmPurchaseList(); break;
                case "customer":
                    activeForm = new frmCustomerList(); break;
                case "stock":
                    activeForm = new frmStocks(); break;
                case "recon":
                    activeForm = new frmReconcileList(); break;
                case "recon_item":
                    activeForm = new frmProdReconList(); break;
                case "hpp":
                    activeForm = new frmRptHPP(); break;
                case "genhpp":
                    activeForm = new frmHPP(); break;
                case "rppc":
                    activeForm = new frmRptItemPurchasedPerCustomer(); break;
                case "frmtscm":
                    activeForm = new frmTotalSalesPerCustomer(); break;
                case "ndgp":
                    activeForm = new frmRptDailyGrossProfit(); break;
                case "tppsmonthly":
                    activeForm = new frmTotalPurchasesPerSupplier(); break;
                case "tpmonthly":
                    activeForm = new frmTotalPurchasesMonthly(); break;
                case "monthlysales":
                    activeForm = new frmMonthlySales(); break;
                case "tpperday":
                    activeForm = new frmTotalPurchasesPerRange(); break;
                default:
                    break;
            }

            if (activeForm != null)
            {
                activeForm.Icon = Properties.Resources.sendangrejeki32x32;
                foreach (Form f in this.MdiChildren)
                {
                    if (f.Name == activeForm.Name)
                    {
                      
                        activeForm = f;
                        
                        break;
                    }
                }
            }

            return activeForm;
        }

        void OnClick(object sender, EventArgs e)
        {
            try
            {
                ToolStripDropDownItem item = (ToolStripDropDownItem)sender;
                Form form = GetInstance(item.Name);
                if (form == null)
                {
                    string reportPath = Directory.GetCurrentDirectory();
                    Report.frmReportViewer rptViewer = null;
                    Report.frmReportViewerWithRange rptViewerRange = null;
                    switch (item.Name.ToLower().Trim())
                    {
                        case "dailysalescatalog":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.DailySalesPerCatalog;
                            rptViewerRange.ReportName = "DailyCatalogSales";
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\DailyCatalogSales.rdlc", reportPath);
                            rptViewerRange.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewerRange.ShowDialog();
                            break;
                        case "dailysales":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.TotalDailySale;
                            rptViewerRange.ReportName = "DailySales";
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\DailySales.rdlc", reportPath);
                            rptViewerRange.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewerRange.ShowDialog();
                            break;
                        case "dailysalesdetail":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.TotalDailySaleDetail;
                            rptViewerRange.ReportName = "DailySales";
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\DailySalesDetail.rdlc", reportPath);
                            rptViewerRange.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewerRange.ShowDialog();
                            break;
                        case "ar":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.Piutang;
                            rptViewerRange.ReportName = "Piutang";
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\Piutang.rdlc", reportPath);
                            rptViewerRange.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewerRange.ShowDialog();
                            break;
                        case "dailygrossprofit":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.DailyGrossProfit;
                            rptViewerRange.ReportName = "DailyGrossProfit";
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\DailyGrossProfit.rdlc", reportPath);
                            rptViewerRange.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewerRange.ShowDialog();
                            break;
                        case "monthlygrossprofit":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.MonthlyGrossProfit;
                            rptViewerRange.ReportName = "MonthlyGrossProfit";                            
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\MonthlyGrossProfit.rdlc", reportPath);
                            rptViewerRange.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewerRange.ShowDialog();
                            break;
                        case "salespermonth":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.TotalSalesPerCatalog;
                            rptViewerRange.ReportName = "SalesPerMonth";
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\SalesPerMonth.rdlc", reportPath);
                            rptViewerRange.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewerRange.ShowDialog();
                            break;
                        case "totalsalespermonth":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.TotalSalesPerItemPerMonth;
                            rptViewerRange.ReportName = "SalesPerMonth";
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\TotalSalesPerItemPerMonth.rdlc", reportPath);
                            rptViewerRange.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewerRange.ShowDialog();
                            break;
                        case "salespercustomer":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.TotalSalesPerCustomer;
                            rptViewerRange.ReportName = "SalesPerCustomer";
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\SalesPerCustomer.rdlc", reportPath);
                            rptViewerRange.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewerRange.ShowDialog();
                            break;
                        case "salesperformancepermonth":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.SalesPerformancePerMonth;
                            rptViewerRange.ReportName = "SalesPerformancePerMonth";
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\SalesPerformancePerMonth.rdlc", reportPath);
                            rptViewerRange.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewerRange.ShowDialog();
                            break;
                        case "dailypurchasesdetail":
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.DailyPurchaseDetail;
                            rptViewerRange.ReportName = "dailypurchasesdetail";
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\dailypurchasesdetail.rdlc", reportPath);
                            rptViewerRange.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewerRange.ShowDialog();
                            break;
                        case "lapstockdetail":                            
                            rptViewerRange = new Report.frmReportViewerWithRange();
                            rptViewerRange.RptType = ReportType.StockDetail;
                            rptViewerRange.ReportName = "CstmCatalogStock";                            
                            rptViewerRange.ReportPath = string.Format("{0}\\Report\\StockDetails.rdlc", reportPath);
                            rptViewerRange.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewerRange.ShowDialog();
                            break;
                        case "lapstock":
                            rptViewer = new Report.frmReportViewer();
                            rptViewer.ReportName = "Stock";
                            rptViewer.DataSource = CatalogStockItem.GetStockReport();
                            rptViewer.ReportPath = string.Format("{0}\\Report\\Stock.rdlc", reportPath);
                            rptViewer.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewer.ShowDialog();
                            break;

                        case "lapsupplier":
                            rptViewer = new Report.frmReportViewer();
                            rptViewer.ReportName = "Supplier";
                            rptViewer.DataSource = SupplierItem.GetAll();
                            rptViewer.ReportPath = string.Format("{0}\\Report\\Supplier.rdlc", reportPath);
                            rptViewer.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewer.ShowDialog();
                            break;
                        case "lapcustomer":
                            rptViewer = new Report.frmReportViewer();
                            rptViewer.ReportName = "Customer";
                            rptViewer.DataSource = CustomerItem.GetAll();
                            rptViewer.ReportPath = string.Format("{0}\\Report\\Customer.rdlc", reportPath);
                            rptViewer.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewer.ShowDialog();
                            break;
                        case "rptcatalog":
                            rptViewer = new Report.frmReportViewer();
                            rptViewer.ReportName = "Catalog";
                            rptViewer.ReportPath = string.Format("{0}\\Report\\Catalog.rdlc", reportPath);
                            rptViewer.DataSource = CatalogItem.GetAll();
                            rptViewer.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewer.ShowDialog();
                            break;
                        case "graphofsales":
                            rptViewer = new Report.frmReportViewer();
                            rptViewer.ReportName = "graphofsales";
                            rptViewer.ReportPath = string.Format("{0}\\Report\\graphofsales.rdlc", reportPath);
                            rptViewer.Icon = Properties.Resources.sendangrejeki32x32;
                            rptViewer.ShowDialog();
                            break;
                        default:
                            rptViewer = null;
                            rptViewerRange = null;
                            break;
                    }

                    if (rptViewer != null)
                    {
                        Log.Info(string.Format("{0} opened {1} report", Utilities.Username, rptViewer.ReportName));
                    }
                    else if (rptViewerRange != null)
                    {
                        Log.Info(string.Format("{0} opened {1} report", Utilities.Username, rptViewerRange.ReportName));
                    }
                }
                else
                {
                    Log.Info(string.Format("{0} opened {1} form", Utilities.Username, item.Text));
                    form.Tag = item.Tag;
                    form.Text = item.Text;
                    form.MdiParent = this;
                    form.WindowState = FormWindowState.Maximized;
                    form.Show();
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                Utilities.ShowInformation(ex.ToString());
            }
        }

        void LoadMenu(ToolStripMenuItem tMenuItem, List<DataObject.Menu> list, int parentID)
        {
            list.Where(t => t.ParentID == parentID).OrderBy(t => t.Sequence).ToList().ForEach(t =>
            {
                ToolStripMenuItem child = new ToolStripMenuItem();
                child.Tag = t;
                child.Name = t.Code;
                child.Text = t.Name;
                child.ToolTipText = t.Description;
                child.Image = GetImage(t.Name);
                child.Enabled = t.Enabled;
                if (list.Where(t1 => t1.ParentID == t.ID).Count() > 0)
                    LoadMenu(child, list, t.ID);
                else child.Click += new EventHandler(OnClick);
                tMenuItem.DropDownItems.Add(child);
            });
        }

        private Image GetImage(string name)
        {
            Image image = null;
            switch (name.ToLower())
            {
                case "report": image = global::Sendang.Rejeki.Properties.Resources.rpt; break;
                case "user management": image = global::Sendang.Rejeki.Properties.Resources.users1; break;
                case "neraca": image = global::Sendang.Rejeki.Properties.Resources.neraca; break;
                case "user": image = global::Sendang.Rejeki.Properties.Resources.singleuser; break;
                case "menu": image = global::Sendang.Rejeki.Properties.Resources.menu; break;
                case "role": image = global::Sendang.Rejeki.Properties.Resources.role; break;
                case "previllage": image = global::Sendang.Rejeki.Properties.Resources.previllage; break;
                case "transaction": image = global::Sendang.Rejeki.Properties.Resources.transaction; break;
                case "generate hpp": image = global::Sendang.Rejeki.Properties.Resources.hpp; break;
                case "sale": image = global::Sendang.Rejeki.Properties.Resources.sale; break;
                case "list of sale": image = global::Sendang.Rejeki.Properties.Resources.salelist; break;
                case "product reconcile": image = global::Sendang.Rejeki.Properties.Resources.reconcile; break;
                case "stock": image = global::Sendang.Rejeki.Properties.Resources.stock; break;
                case "stock list per supplier": image = global::Sendang.Rejeki.Properties.Resources.supplierlist; break;
                case "input stock": image = global::Sendang.Rejeki.Properties.Resources.input; break;
                case "stock detail/update": image = global::Sendang.Rejeki.Properties.Resources.edit; break;
                case "catalog": image = global::Sendang.Rejeki.Properties.Resources.catalog; break;
                case "customer": image = global::Sendang.Rejeki.Properties.Resources.customers; break;
                case "supplier": image = global::Sendang.Rejeki.Properties.Resources.supplier; break;
                case "master": image = global::Sendang.Rejeki.Properties.Resources.master; break;
                case "logout": image = global::Sendang.Rejeki.Properties.Resources.logout; break;
                case "exit": image = global::Sendang.Rejeki.Properties.Resources.exit; break;
                case "file": image = global::Sendang.Rejeki.Properties.Resources.file; break;
                default: image = global::Sendang.Rejeki.Properties.Resources.graphic_report__2_; break;


            }
            return image;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            menuStrip.Items.Clear();
            List<DataObject.Menu> list = DataLayer.MenuItem.GetMenus();
            List<Previllage> prevList = PrevillageItem.GetByUsername(Utilities.Username);
            for (int i = 0; i < list.Count; i++)
            {
                Previllage prev = prevList.Where(t => t.MenuID == list[i].ID).FirstOrDefault();
                list[i].Enabled = prev == null ? false : prev.AllowRead;
            }

            List<DataObject.Menu> parentList = list.Where(t => t.ParentID == 0).OrderBy(t => t.Sequence).ToList();
            parentList.ForEach(t =>
            {
                ToolStripMenuItem parent = new ToolStripMenuItem();
                parent.Name = t.Code;
                parent.Text = t.Name;
                parent.Image = GetImage(t.Name);
                parent.Enabled = t.Enabled;
                parent.ToolTipText = t.Description;
                if (list.Where(t1 => t1.ParentID == t.ID).Count() > 0)
                    LoadMenu(parent, list, t.ID);
                else parent.Click += new EventHandler(OnClick);
                menuStrip.Items.Add(parent);
            });
        }
    }
}
