using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicLayer;
using DataObject;
using DataLayer;

namespace Sendang.Rejeki.Transaction
{
    public partial class frmAccountReceivablePayment : Form, ITransButton
    {
        public frmAccountReceivablePayment()
        {
            InitializeComponent();
        }

        public string InvoceNo { get; set; }
        //public decimal TotalPrice { get; set; }

        private void frmAccountReceivablePayment_Load(object sender, EventArgs e)
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

            if (string.Format("{0}", InvoceNo).Length > 0)
            {
                LoadData(string.Format("{0}", InvoceNo));
            }
        }
        public void Save()
        {
            if (!IsValid()) return;

            DialogResult dialog = Utilities.Confirmation(string.Format("Are you sure you want to update this invoice payment?\nUpdating this will impact to sales payment"));
            if (dialog != System.Windows.Forms.DialogResult.Yes)
                return;

            decimal paymentAmount = 0;
            decimal totalPayment = 0;
            decimal totalReturn = 0;
            decimal.TryParse(txtPaymentAmount.Text, out paymentAmount);

            decimal.TryParse(txtTotalPaidInfo.Text, out totalPayment);
            decimal.TryParse(txtTotalReturnInfo.Text, out totalReturn);

            var result = InvoicePaymentItem.UpdateInvoicePayment(txtNoInvoice.Text, totalPayment, totalReturn, paymentAmount, Utilities.Username);
            if (result != null)
            {
                if (this.Modal)
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                else
                    this.Close();
            }
        }

        public void Cancel()
        {
            if (this.Modal)
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            else
                this.Close();
        }

        void LoadData(string invoiceNo)
        {
            Invoice item = InvoiceItem.GetOptionsByKey(invoiceNo);
            if (item != null)
            {
                cboBuyer.SelectedValue = item.CustomerID;
                cboBuyer.Enabled = false;
                txtNoInvoice.Text = item.InvoiceNo;
                DateTime? dueDate = item.DueDate;
                if (dueDate.HasValue) dtDueDate.Value = dueDate.Value;
                txtTotal.Text = Utilities.ToString(item.Total);
                //TotalPrice = item.Total;

                cboTrade.SelectedValue = item.Tradeterm;
                cboShipment.SelectedValue = item.Shipment;
                cboPayment.SelectedValue = item.Payment;
                dtTanggal.Value = item.InvoiceDate;
                txtTo.Text = item.To;
                txtRemarks.Text = item.Remark;
                txtDelivery.Text = item.Delivery;
                txtAttn.Text = item.Attn;

                InvoicePayment paymentItem = InvoicePaymentItem.GetByInvoiceNo(invoiceNo);
                if (paymentItem != null)
                {
                    txtTotalPaidInfo.Text = Utilities.ToString(paymentItem.TotalPayment);
                    txtTotalReturnInfo.Text = Utilities.ToString(paymentItem.TotalReturn);
                    txtPaymentAmount.Text = Utilities.ToString(paymentItem.PaymentAmount);
                }
            }
        }

        private void cboBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            Customer cust = (Customer)cbo.SelectedItem;
            txtAddress.Text = cust != null ? cust.Address : string.Empty;
            txtCompany.Text = cust.FullName;
            txtTel.Text = cust.Phone;
        }


        public bool IsValid()
        {
            if (cboBuyer.SelectedIndex == -1)
            {
                Utilities.ShowValidation("Buyer tidak boleh kosong");
                cboBuyer.Focus();
                return false;
            }
            if (txtPaymentAmount.Text.Trim().Length == 0)
            {
                Utilities.ShowValidation("Payment Amount tidak boleh kosong");
                txtPaymentAmount.Focus();
                return false;
            }

            decimal paymentAmount = 0;
            decimal totalToPay = 0;
            decimal.TryParse(txtPaymentAmount.Text, out paymentAmount);
            decimal.TryParse(txtTotal.Text, out totalToPay);

            if (paymentAmount < totalToPay)
            {
                Utilities.ShowValidation("Payment Amount harus sejumlah total invoice");
                txtPaymentAmount.Focus();
                return false;
            }
            return true;
        }

        //void tb_Enter(object sender, EventArgs e)
        //{
        //    TextBox tb = (TextBox)sender;
        //    if (tb != null)
        //    {
        //        tb.Text = Utilities.RawNumberFormat(tb.Text);
        //    }
        //}

        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Utilities.IsValidNumberWithComma(sender, e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //void tb_KeyUp(object sender, KeyEventArgs e)
        //{
        //    TextBox tb = (TextBox)sender;
        //    if (tb != null)
        //    {
        //        tb.Text = Utilities.RawNumberFormat(tb.Text);
        //        //RecalculatePayment();
        //    }

        //}

        void tb_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                tb.Text = Utilities.CorrectFormat(tb.Text);
                RecalculatePayment();
            }
        }


        private void txtPayment_KeyUp(object sender, KeyEventArgs e)
        {
            RecalculatePayment();
        }

        private void RecalculatePayment()
        {
            decimal payment = 0;
            decimal totalPrice = 0;
            decimal.TryParse(txtPaymentAmount.Text, out payment);
            decimal.TryParse(txtTotal.Text, out totalPrice);
            txtTotalPaidInfo.Text = Utilities.ToString(totalPrice >= payment ? payment : totalPrice);

            if (payment > 0)
            {
                decimal result = payment - totalPrice;
                if (result >= 0) txtTotalReturnInfo.Text = Utilities.ToString(result);
                else txtTotalReturnInfo.Text = Utilities.ToString(0);
            }
        }
    }
}
