using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataObject;
using DataLayer;
using DataObject.Cstm;
using LogicLayer;
using Newtonsoft.Json;
using Sendang.Rejeki.Report;
using System.IO;
using Microsoft.Reporting.WinForms;

namespace Sendang.Rejeki.Transaction
{
    public partial class frmAccountReceivable : Form
    {
        public string InvoceNo { get; set; }
        public List<CstmInvoiceDetail> NewInvoiceDetails { get; set; }
        public frmAccountReceivable()
        {
            InitializeComponent();
            NewInvoiceDetails = new List<CstmInvoiceDetail>();
        }

        private List<Options> paymentList;
        private List<Options> tradeTermsList;
        private List<Options> shipmentList;

        const string InvoiceStatus_BelumDibayar = "IS001";//	Belum Dibayar	InvoiceStatus
        const string InvoiceStatus_LUNAS = "IS002";//	Sudah Dibayar/LUNAS	InvoiceStatus
        private void frmAccountReceivable_Load(object sender, EventArgs e)
        {
            List<Customer> list = CustomerItem.GetAll();
            list.Insert(0, new Customer(0, "", "--Pilih Buyer--"));
            cboBuyer.DataSource = list;
            cboBuyer.DisplayMember = "Code";
            cboBuyer.ValueMember = "ID";

            paymentList = OptionItem.GetOptionsByName("Payment");
            paymentList.Insert(0, new Options() { ValueMember = "", DisplayMember = "--Pilih Payment--", Name = "Payment" });

            tradeTermsList = OptionItem.GetOptionsByName("TradeTerms");
            tradeTermsList.Insert(0, new Options() { ValueMember = "", DisplayMember = "--Pilih Trade Terms--", Name = "TradeTerms" });

            shipmentList = OptionItem.GetOptionsByName("Shipment");
            shipmentList.Insert(0, new Options() { ValueMember = "", DisplayMember = "--Pilih Shipment--", Name = "Shipment" });

            cboPayment.DataSource = paymentList;
            cboPayment.ValueMember = "ValueMember";
            cboPayment.DisplayMember = "DisplayMember";

            cboTrade.DataSource = tradeTermsList;
            cboTrade.ValueMember = "ValueMember";
            cboTrade.DisplayMember = "DisplayMember";

            cboShipment.DataSource = shipmentList;
            cboShipment.ValueMember = "ValueMember";
            cboShipment.DisplayMember = "DisplayMember";


            cboBuyer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboBuyer.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cboTrade.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboTrade.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cboShipment.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboShipment.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cboPayment.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboPayment.AutoCompleteSource = AutoCompleteSource.CustomSource;

            txtNoInvoice.Text = "Auto Generate";
            dtTanggal.Value = DateTime.Now;
            dtTanggal.Enabled = false;
            dtDueDate.Value = dtTanggal.Value.AddDays(30);
            txtNoInvoice.Enabled = false;

            if (string.Format("{0}", InvoceNo).Length > 0)
            {
                LoadData(string.Format("{0}", InvoceNo));
            }
        }

        void LoadData(string invoiceNo)
        {
            grid.AutoGenerateColumns = false;
            Invoice item = InvoiceItem.GetOptionsByKey(invoiceNo);
            if (item != null)
            {
                cboBuyer.SelectedValue = item.CustomerID;
                cboBuyer.Enabled = false;
                txtNoInvoice.Text = item.InvoiceNo;
                DateTime? dueDate = item.DueDate;
                if (dueDate.HasValue) dtDueDate.Value = dueDate.Value;
                txtTotalPayment.Text = string.Format("Rp. {0:N0}", item.Paid);
                txtTotal.Text = string.Format("Rp. {0:N0}", item.Total);

                if (shipmentList.Where(t => t.ValueMember == item.Shipment).Count() == 0)
                    shipmentList.Add(new Options() { ValueMember = item.Shipment, DisplayMember = item.Shipment, Name = "Shipment" });

                if (paymentList.Where(t => t.ValueMember == item.Payment).Count() == 0)
                    paymentList.Add(new Options() { ValueMember = item.Payment, DisplayMember = item.Payment, Name = "Payment" });

                if (tradeTermsList.Where(t => t.ValueMember == item.Tradeterm).Count() == 0)
                    tradeTermsList.Add(new Options() { ValueMember = item.Tradeterm, DisplayMember = item.Tradeterm, Name = "TradeTerms" });

                cboPayment.DataSource = null;
                cboTrade.DataSource = null;
                cboShipment.DataSource = null;

                cboPayment.DataSource = paymentList;
                cboPayment.ValueMember = "ValueMember";
                cboPayment.DisplayMember = "DisplayMember";

                cboTrade.DataSource = tradeTermsList;
                cboTrade.ValueMember = "ValueMember";
                cboTrade.DisplayMember = "DisplayMember";

                cboShipment.DataSource = shipmentList;
                cboShipment.ValueMember = "ValueMember";
                cboShipment.DisplayMember = "DisplayMember";


                cboTrade.SelectedValue = item.Tradeterm;
                cboShipment.SelectedValue = item.Shipment;
                cboPayment.SelectedValue = item.Payment;
                dtTanggal.Value = item.InvoiceDate;
                txtTo.Text = item.To;
                txtRemarks.Text = item.Remark;
                txtDelivery.Text = item.Delivery;
                txtAttn.Text = item.Attn;

                txtTotalPayment.Text = string.Format("Rp. {0:N0}", item.Paid);
                txtTotal.Text = string.Format("Rp. {0:N0}", item.Total);

                if (item.Details != null && item.Details.Count > 0)
                {
                    InvoiceDetail detail = new InvoiceDetail();
                    detail.CatalogName = "Total";
                    detail.TotalAmount = string.Format("Rp. {0:N0}", item.TotalDetail);
                    item.Details.Add(detail);
                }
                grid.DataSource = item.Details;

                if (item.Status == InvoiceStatus_LUNAS)
                {
                    btnPrint.Enabled = false;
                    btnCancel.Text = "Close";
                }

            }
        }

        private void cboBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            grid.AutoGenerateColumns = false;
            ComboBox cbo = (ComboBox)sender;
            Customer cust = (Customer)cbo.SelectedItem;
            txtAddress.Text = cust != null ? cust.Address : string.Empty;
            txtCompany.Text = cust.FullName;
            txtTel.Text = cust.Phone;
            if (string.Format("{0}", InvoceNo).Length > 0)
            {
            }
            else
            {
                NewInvoiceDetails = SaleItem.GetDetailInvoice(cust.ID);
                if (NewInvoiceDetails.Count > 0)
                {
                    DateTime? dueDate = NewInvoiceDetails.Select(t => t.DueDate).Max();
                    if (dueDate.HasValue) dtDueDate.Value = dueDate.Value;

                    var totalPayment = (from item in NewInvoiceDetails
                                        select new { item.TotalPayment, item.TransactionID }
                                       ).Where(t => t.TotalPayment > 0).Distinct().ToList();

                    if (totalPayment.Count > 0)
                    {
                        txtTotalPayment.Text = string.Format("Rp. {0:N0}", totalPayment.Sum(t => t.TotalPayment));
                    }
                    else
                    {
                        txtTotalPayment.Text = string.Format("Rp. {0:N0}", 0);
                    }

                    txtTotal.Text = string.Format("Rp. {0:N0}", NewInvoiceDetails.Sum(t => t.Amount) - totalPayment.Sum(t => t.TotalPayment));
                    btnPrint.Enabled = true;
                    //btnSave.Enabled = true;

                    CstmInvoiceDetail detail = new CstmInvoiceDetail();
                    detail.CatalogName = "Total";
                    detail.TotalAmount = string.Format("Rp. {0:N0}", NewInvoiceDetails.Sum(t => t.Amount));
                    NewInvoiceDetails.Add(detail);


                    grid.DataSource = NewInvoiceDetails;
                }
                else
                {
                    //txtTotal.Top = (this.Height - txtTotal.Height) / 2;
                    txtTotalPayment.Text = string.Format("Rp. {0:N0}", 0);
                    txtTotal.Text = string.Format("Rp. {0:N0}", 0);
                    btnPrint.Enabled = false;
                    //btnSave.Enabled = false;
                    grid.DataSource = NewInvoiceDetails;
                }
            }

        }



        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                return;
            }

            DataObject.Menu menuItem = DataLayer.MenuItem.GetMenuByCode("cst_invoice_list");
            DialogResult dialog = Utilities.Confirmation(string.Format("Are you sure you want to print and save this invoice to your \"{0}\"?", menuItem == null ? "Invoice List" : menuItem.Name));
            if (dialog != System.Windows.Forms.DialogResult.Yes)
                return;

            Customer cust = (Customer)cboBuyer.SelectedItem;
            Invoice item = new Invoice();
            item.Attn = txtAttn.Text;
            item.InvoiceDate = dtTanggal.Value;
            item.DueDate = dtDueDate.Value;
            item.CustomerID = cust.ID;
            item.Delivery = txtDelivery.Text;
            item.InvoiceNo = InvoceNo;
            item.Remark = txtRemarks.Text;
            item.Payment = string.Format("{0}", cboPayment.SelectedValue != null ? cboPayment.SelectedValue : cboPayment.Text);
            item.Shipment = string.Format("{0}", cboShipment.SelectedValue != null ? cboShipment.SelectedValue : cboShipment.Text);
            item.Tradeterm = string.Format("{0}", cboTrade.SelectedValue != null ? cboTrade.SelectedValue : cboTrade.Text);
            item.To = txtTo.Text;
            item.CreatedBy = Utilities.Username;
            btnPrint.Enabled = false;

            Invoice result = null;
            if (string.Format("{0}", InvoceNo).Length > 0)
            {
                result = InvoiceItem.Update(item);
            }
            else
            {
                if (NewInvoiceDetails.Count > 0)
                {
                    item.TotalDetail = NewInvoiceDetails.Sum(t => t.Amount);
                }

                var totalPayment = (from t in NewInvoiceDetails
                                    select new { t.TotalPayment, t.TransactionID }
                                      ).Where(t => t.TotalPayment > 0).Distinct().ToList();

                if (totalPayment.Count > 0)
                    item.Paid = totalPayment.Sum(t => t.TotalPayment);
                else item.Paid = 0;

                item.Total = NewInvoiceDetails.Sum(t => t.Amount) - totalPayment.Sum(t => t.TotalPayment);
                item.Status = "IS001"; // belom dibayar

                List<InvoiceDetail> details = new List<InvoiceDetail>();
                foreach (var newDetail in NewInvoiceDetails)
                {
                    if (newDetail.CatalogName.ToLower() == "Total".ToLower()) continue;
                    InvoiceDetail detailItem = new InvoiceDetail();
                    detailItem.InvoiceID = item.InvoiceID;
                    detailItem.CatalogID = newDetail.CatalogId;
                    detailItem.Price = newDetail.Price;
                    detailItem.Quantity = newDetail.Quantity;
                    detailItem.RowIndex = newDetail.RowIndex;
                    detailItem.TotalPrice = newDetail.Amount;
                    detailItem.TransactionID = newDetail.TransactionID;
                    detailItem.PrintDate = newDetail.PrintDate;
                    detailItem.NoNota = newDetail.NoNota;
                    detailItem.Delivery = newDetail.Delivery;
                    details.Add(detailItem);
                }
                result = InvoiceItem.Insert(item, details);
            }
            if (result != null)
            {
                if (string.Format("{0}", InvoceNo).Length > 0)
                    Log.Update(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(result)));
                else Log.Insert(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(result)));
                this.Hide();
                frmReportViewer rptViewer = new frmReportViewer();
                rptViewer.ReportName = "Invoice";
                rptViewer.ReportPath = string.Format("{0}\\Report\\Invoice.rdlc", Directory.GetCurrentDirectory());
                rptViewer.Icon = Properties.Resources.sendangrejeki32x32;
                List<ReportParameter> parameters = new List<ReportParameter>();
                parameters.Add(new ReportParameter("paramBuyerCode", string.Format("{0} ", result.CustomerCode), true));
                parameters.Add(new ReportParameter("paramCompany", string.Format("{0} ", result.CustomerName), true));
                parameters.Add(new ReportParameter("paramAddress", string.Format("{0} ", cust.Address), true));
                parameters.Add(new ReportParameter("paramTel", string.Format("{0} ", cust.Phone), true));
                parameters.Add(new ReportParameter("paramAttn", string.Format("{0} ", result.Attn), true));

                parameters.Add(new ReportParameter("paramTo", string.Format("{0} ", result.To), true));

                string Shipment = "";
                string Tradeterm = "";
                string Payment = "";
                if (shipmentList.Where(t => t.ValueMember == item.Shipment && t.Name == "Shipment").Count() > 0)
                    Shipment = shipmentList.Where(t => t.ValueMember == item.Shipment && t.Name == "Shipment").Select(t => t.DisplayMember).FirstOrDefault();
                else Shipment = item.Shipment;
                if (paymentList.Where(t => t.ValueMember == item.Payment && t.Name == "Payment").Count() > 0)
                    Payment = paymentList.Where(t => t.ValueMember == item.Payment && t.Name == "Payment").Select(t => t.DisplayMember).FirstOrDefault();
                else Payment = item.Payment;
                if (tradeTermsList.Where(t => t.ValueMember == item.Tradeterm && t.Name == "TradeTerms").Count() > 0)
                    Tradeterm = tradeTermsList.Where(t => t.ValueMember == item.Tradeterm && t.Name == "TradeTerms").Select(t => t.DisplayMember).FirstOrDefault();
                else Tradeterm = item.Tradeterm;

                parameters.Add(new ReportParameter("paramShipmentMethod", string.Format("{0} ", Shipment), true));
                parameters.Add(new ReportParameter("paramTradeTerms", string.Format("{0} ", Tradeterm), true));
                parameters.Add(new ReportParameter("paramPayment", string.Format("{0} ", Payment), true));

                parameters.Add(new ReportParameter("paramInvoiceNo", string.Format("{0} ", result.InvoiceNo), true));
                parameters.Add(new ReportParameter("paramInvoiceDate", string.Format("{0:dd-MMM-yyyy}", result.InvoiceDate), true));
                parameters.Add(new ReportParameter("paramDelivery", string.Format("{0} ", result.Delivery), true));
                parameters.Add(new ReportParameter("paramRemark", string.Format("{0} ", result.Remark), true));
                parameters.Add(new ReportParameter("paramDueDate", string.Format("{0:dd-MMM-yyyy}", result.DueDate), true));
                parameters.Add(new ReportParameter("paramTotalDetail", string.Format("Rp. {0:N0}", result.TotalDetail), true));
                rptViewer.Params = parameters;
                rptViewer.DataSource = result.Details;
                DialogResult reportDialog = rptViewer.ShowDialog();
                if (reportDialog == System.Windows.Forms.DialogResult.Cancel)
                {
                    this.Close(); ;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }

        }



        public bool IsValid()
        {
            if (cboBuyer.SelectedIndex == -1)
            {
                Utilities.ShowValidation("Buyer tidak boleh kosong");
                cboBuyer.Focus();
                return false;
            }
            if (dtDueDate.Value <= new DateTime(1900, 1, 1))
            {

                Utilities.ShowValidation("Due Date harus valid");
                dtDueDate.Focus();
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.Modal)
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            else
                this.Close();
        }
    }
}
