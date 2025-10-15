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

        private void frmAccountReceivable_Load(object sender, EventArgs e)
        {
            List<Options> paymentList = OptionItem.GetOptionsByName("Payment");
            paymentList.Insert(0, new Options() { ValueMember = "", DisplayMember = "--Pilih Payment--", Name = "Payment" });
            cboPayment.DataSource = paymentList;
            cboPayment.ValueMember = "ValueMember";
            cboPayment.DisplayMember = "DisplayMember";

            List<Options> tradeTermsList = OptionItem.GetOptionsByName("TradeTerms");
            tradeTermsList.Insert(0, new Options() { ValueMember = "", DisplayMember = "--Pilih Trade Terms--", Name = "TradeTerms" });
            cboTrade.DataSource = tradeTermsList;
            cboTrade.ValueMember = "ValueMember";
            cboTrade.DisplayMember = "DisplayMember";

            List<Options> shipmentList = OptionItem.GetOptionsByName("Shipment");
            shipmentList.Insert(0, new Options() { ValueMember = "", DisplayMember = "--Pilih Shipment--", Name = "Shipment" });
            cboShipment.DataSource = shipmentList;
            cboShipment.ValueMember = "ValueMember";
            cboShipment.DisplayMember = "DisplayMember";

            List<Customer> list = CustomerItem.GetAll();
            list.Insert(0, new Customer(0, "", "--Pilih Buyer--"));
            cboBuyer.DataSource = list;
            cboBuyer.DisplayMember = "Code";
            cboBuyer.ValueMember = "ID";

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

                cboTrade.SelectedValue = item.Tradeterm;
                cboShipment.SelectedValue = item.Shipment;
                cboPayment.SelectedValue = item.Payment;
                dtTanggal.Value = item.InvoiceDate;
                txtTo.Text = item.To;
                txtRemarks.Text = item.Remark;
                txtDelivery.Text = item.Delivery;
                txtAttn.Text = item.Attn;

                if (item.Details != null && item.Details.Count > 0)
                {
                    InvoiceDetail detail = new InvoiceDetail();
                    detail.CatalogName = "Total";
                    detail.TotalAmount = string.Format("Rp. {0:N0}", item.TotalDetail);
                    item.Details.Add(detail);
                }
                grid.DataSource = item.Details;
            }
        }

        private void cboBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            DialogResult dialog = Utilities.Confirmation(string.Format("Are you sure you want to print and save this invoice to your menu \"{0}\"?", menuItem == null ? "Invoice List" : menuItem.Name));
            if (dialog != System.Windows.Forms.DialogResult.Yes)
                return;

            Customer cust = (Customer)cboBuyer.SelectedItem;
            Invoice item = new Invoice();
            item.Payment = string.Format("{0}", cboPayment.SelectedValue);
            item.Attn = txtAttn.Text;
            item.InvoiceDate = dtTanggal.Value;
            item.DueDate = dtDueDate.Value;
            item.CustomerID = cust.ID;
            item.Delivery = txtDelivery.Text;
            item.InvoiceNo = InvoceNo;
            item.Remark = txtRemarks.Text;
            item.Shipment = string.Format("{0}", cboShipment.SelectedValue);
            item.Status = "IS001"; // belom dibayar
            item.To = txtTo.Text;
            item.CreatedBy = Utilities.Username;
            if (NewInvoiceDetails.Count > 0)
            {
                item.TotalDetail = NewInvoiceDetails.Sum(t => t.Amount);
            }
            item.Tradeterm = string.Format("{0}", cboTrade.SelectedValue);

            var totalPayment = (from t in NewInvoiceDetails
                                select new { t.TotalPayment, t.TransactionID }
                                  ).Where(t => t.TotalPayment > 0).Distinct().ToList();

            if (totalPayment.Count > 0)
                item.Paid = totalPayment.Sum(t => t.TotalPayment);
            else item.Paid = 0;

            item.Total = NewInvoiceDetails.Sum(t => t.Amount) - totalPayment.Sum(t => t.TotalPayment);
            btnPrint.Enabled = true;
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

            Invoice result = null;
            if (string.Format("{0}", InvoceNo).Length > 0)
            {
                result = InvoiceItem.Update(item);
            }
            else
            {
                result = InvoiceItem.Insert(item, details);
            }
            if (result != null)
            {
                btnCancel.Text = "Close";
                if (string.Format("{0}", InvoceNo).Length > 0)
                    Log.Update(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(result)));
                else Log.Insert(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(result)));
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
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
