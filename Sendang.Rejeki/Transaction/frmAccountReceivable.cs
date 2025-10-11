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
        }

        private void cboBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            Customer cust = (Customer)cbo.SelectedItem;
            txtAddress.Text = cust != null ? cust.Address : string.Empty;
            txtCompany.Text = cust.FullName;
            txtTel.Text = cust.Phone;

            NewInvoiceDetails = SaleItem.GetDetailInvoice(cust.ID);
            grid.DataSource = NewInvoiceDetails;
            if (NewInvoiceDetails.Count > 0)
            {
                //txtTotal.Top = (this.Height - txtTotal.Height) / 2;
                txtTotal.Text = string.Format("Rp. {0:N0}", NewInvoiceDetails.Sum(t => t.Amount));
                btnPrint.Enabled = true;
                btnSave.Enabled = true;
            }
            else
            {
                //txtTotal.Top = (this.Height - txtTotal.Height) / 2;
                txtTotal.Text = string.Format("Rp. {0:N0}", 0);
                btnPrint.Enabled = false;
                btnSave.Enabled = false;
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
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                return;
            }

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
            item.Status = "";
            item.To = txtTo.Text;
            if (NewInvoiceDetails.Count > 0)
            {
                item.Total = NewInvoiceDetails.Sum(t => t.Amount);
            }
            item.Tradeterm = string.Format("{0}", cboTrade.SelectedValue);

            List<InvoiceDetail> details = new List<InvoiceDetail>();
            foreach (var newDetail in NewInvoiceDetails)
            {
                InvoiceDetail detail = new InvoiceDetail();
                detail.InvoiceID = item.InvoiceID;
                detail.CatalogID = newDetail.CatalogId;

                detail.Price = newDetail.Price;
                detail.Quantity = newDetail.Quantity;
                detail.Sequence = newDetail.RowIndex;
                detail.TotalPrice = newDetail.Amount;
                detail.TransactionID = newDetail.TransactionID;


                details.Add(detail);
            }

            InvoiceItem.Insert(item, details);
        }

    }
}
