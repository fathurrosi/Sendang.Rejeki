using DataLayer;
using DataObject;
using LogicLayer;
using Microsoft.Reporting.WinForms;
using Newtonsoft.Json;
using Sendang.Rejeki.Lookup;
using Sendang.Rejeki.Report;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
    public partial class frmPurchaseOrder : Form, ITransButton
    {
        public frmPurchaseOrder()
        {
            InitializeComponent();
        }

        public string PurchaseOrderCode { get; internal set; }

        public void Cancel()
        {
            if (this.Modal)
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            else
                this.Close();
        }

        public bool IsValid()
        {
            if (txtCompanyKode.Text.Length == 0)
            {
                Utilities.ShowValidation("Maaf, Kode Vendor harus diisi");
                btnLookup.Focus();
                return false;
            }

            if (dtTanggal.Value.Year <= 1900)
            {
                Utilities.ShowValidation("Maaf, PO Date tidak valid");
                dtTanggal.Focus();
                return false;
            }

            if (dtDeliveryDate.Value.Year <= 1900)
            {
                Utilities.ShowValidation("Maaf, Delivery Date tidak valid");
                dtTanggal.Focus();
                return false;
            }


            List<PurchaseOrderDetail> details = (List<PurchaseOrderDetail>)grid.DataSource;
            if (details == null || details.Count == 0)
            {
                Utilities.ShowValidation("Maaf, silahkan input catalog ke dalam Daftar PO");
                return false;
            }

            return true;
        }
        public string FormatText(string text)
        {
            string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Replace(".", ". ");
            }
            return string.Join(Environment.NewLine, lines);
        }

       

        public void Save()
        {
            DataObject.Menu menuItem = DataLayer.MenuItem.GetMenuByCode("po_list");
            DialogResult dialog = Utilities.Confirmation(string.Format("Are you sure you want to print and save this invoice to your \"{0}\"?", menuItem == null ? "Invoice List" : menuItem.Name));
            if (dialog != System.Windows.Forms.DialogResult.Yes)
                return;

            PurchaseOrder item = new PurchaseOrder();
            item.Attn = txtAttn.Text;
            item.OrderDate = dtTanggal.Value;
            item.DeliveryDate = dtDeliveryDate.Value;
            item.SupplierCode = txtCompanyKode.Text;
            item.Delivery = txtDelivery.Text;
            item.PurchaseOrderCode = PurchaseOrderCode;
            item.Remark = txtRemarks.Text;
            item.Payment = string.Format("{0}", cboPayment.SelectedValue != null ? cboPayment.SelectedValue : cboPayment.Text);
            item.Shipment = string.Format("{0}", cboShipment.SelectedValue != null ? cboShipment.SelectedValue : cboShipment.Text);
            item.Tradeterm = string.Format("{0}", cboTrade.SelectedValue != null ? cboTrade.SelectedValue : cboTrade.Text);
            item.To = txtTo.Text;
            item.CreatedBy = Utilities.Username;
            ctlTransButton1.SaveButtonEnabled = false;

            PurchaseOrder result = null;
            if (string.Format("{0}", PurchaseOrderCode).Length > 0)
            {
                result = PurchaseOrderItem.Update(item);
            }
            else
            {
                item.Status = "POSTS01";
                item.Details = (List<PurchaseOrderDetail>)grid.DataSource;
                if (item.Details.Count > 0)
                {
                    item.TotalAmount = item.Details.Sum(t => t.TotalPrice);
                }
                result = PurchaseOrderItem.Insert(item, item.Details);
            }

            if (result != null)
            {
                Supplier supplier = SupplierItem.GetByCode(result.SupplierCode);
                List<PurchaseOrderNotes> noteList = PurchaseOrderNotesItem.GetByCode(result.PurchaseOrderCode);
                if (string.Format("{0}", PurchaseOrderCode).Length > 0)
                    Log.Update(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(result)));
                else Log.Insert(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(result)));
                this.Hide();
                frmReportViewer rptViewer = new frmReportViewer();
                rptViewer.ReportName = "DataSet1";
                rptViewer.ReportPath = string.Format("{0}\\Report\\PurchaseOrder.rdlc", Directory.GetCurrentDirectory());
                rptViewer.Icon = Properties.Resources.sendangrejeki32x32;
                List<ReportParameter> parameters = new List<ReportParameter>();

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

                parameters.Add(new ReportParameter("paramSupplierCode", string.Format("{0} ", result.SupplierCode), true));
                parameters.Add(new ReportParameter("paramCompany", string.Format("{0} ", result.SupplierName), true));
                parameters.Add(new ReportParameter("paramAddress", string.Format("{0} ", supplier.Address), true));
                parameters.Add(new ReportParameter("paramTel", string.Format("{0} ", supplier.Phone), true));
                parameters.Add(new ReportParameter("paramAttn", string.Format("{0} ", result.Attn), true));
                parameters.Add(new ReportParameter("paramTo", string.Format("{0} ", result.To), true));
                parameters.Add(new ReportParameter("paramShipmentMethod", string.Format("{0} ", Shipment), true));
                parameters.Add(new ReportParameter("paramTradeTerms", string.Format("{0} ", Tradeterm), true));
                parameters.Add(new ReportParameter("paramPayment", string.Format("{0} ", Payment), true));
                parameters.Add(new ReportParameter("paramPurchaseOrderCode", string.Format("{0} ", result.PurchaseOrderCode), true));
                parameters.Add(new ReportParameter("paramOrderDate", string.Format("{0:dd-MMM-yyyy}", result.OrderDate), true));
                parameters.Add(new ReportParameter("paramDelivery", string.Format("{0} ", result.Delivery), true));
                parameters.Add(new ReportParameter("paramRemark", string.Format("{0} ", result.Remark), true));
                parameters.Add(new ReportParameter("paramTotalDetail", string.Format("Rp. {0:N0}", result.TotalAmount), true));

                string totalQty = string.Format("{0:N0} {1}", result.Details.Sum(t => t.Quantity), result.Details.Select(t => t.Unit).FirstOrDefault());
                parameters.Add(new ReportParameter("paramTotalQty", totalQty, true));

                StringBuilder sb = new StringBuilder();
                foreach (var note in noteList)
                {
                    sb.Append(note.Note);
                    sb.Append(Environment.NewLine);
                }

                string freeText = FormatText(sb.ToString());

                parameters.Add(new ReportParameter("paramNotes", freeText, true));


                LogicLayer.Barcode barcode = new LogicLayer.Barcode();
                System.Drawing.Image imageBarcode = barcode.GenerateBarcode("https://www.sendangrejeki.com/");
                string base64Image = "";
                using (MemoryStream ms = new MemoryStream())
                {
                    imageBarcode.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();
                    base64Image = Convert.ToBase64String(imageBytes);
                }
                parameters.Add(new ReportParameter("paramImage", base64Image));
                parameters.Add(new ReportParameter("paramMIMEtype", "image/png"));

                rptViewer.Params = parameters;
                rptViewer.DataSource = result.Details;
                rptViewer.WindowState = FormWindowState.Maximized;
                DialogResult reportDialog = rptViewer.ShowDialog();
                if (reportDialog == System.Windows.Forms.DialogResult.Cancel)
                {
                    this.Close(); ;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        private List<Options> paymentList;
        private List<Options> tradeTermsList;
        private List<Options> shipmentList;
        private void frmPurchaseOrder_Load(object sender, EventArgs e)
        {
            grid.AutoGenerateColumns = false;
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

            cboTrade.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboTrade.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cboShipment.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboShipment.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cboPayment.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboPayment.AutoCompleteSource = AutoCompleteSource.CustomSource;

            DateTime today = DateTime.Now;
            txtPurchaseOrderNo.Text = "Auto Generate";
            dtTanggal.Value = today;
            dtTanggal.Enabled = false;
            //dtDueDate.Value = dtTanggal.Value.AddDays(30);


            if (!string.IsNullOrEmpty(PurchaseOrderCode))
            {
                LoadData();
            }
        }


        void LoadData()
        {
            grid.AutoGenerateColumns = false;
            PurchaseOrder item = PurchaseOrderItem.GetByCode(PurchaseOrderCode);
            if (item != null)
            {
                Supplier supp = SupplierItem.GetByCode(item.SupplierCode);
                btnLookup.Enabled = false;
                txtCompanyKode.Text = item.SupplierCode;
                txtCompany.Text = supp != null ? supp.Name : "";
                txtCompanyAddress.Text = supp != null ? supp.Address : "";
                txtCompanyTel.Text = supp != null ? supp.Phone : "";

                txtPurchaseOrderNo.Text = item.PurchaseOrderCode;
                if (item.DeliveryDate.HasValue)
                    dtDeliveryDate.Value = item.DeliveryDate.Value;


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
                dtTanggal.Value = item.OrderDate;
                txtTo.Text = item.To;
                txtRemarks.Text = item.Remark;
                txtDelivery.Text = item.Delivery;
                txtAttn.Text = item.Attn;

                LoadDetail(item.Details);
                //if (item.Status == InvoiceStatus_LUNAS)
                //{
                //    ctlTransButton1.SaveButtonEnabled = false;
                //    ctlTransButton1.CancelButtonText = "Close";
                //}

            }
        }


        void LoadDetail(List<PurchaseOrderDetail> list)
        {
            List<PurchaseOrderDetail> orderedList = list
                .OrderBy(p => p.CreatedDate)
                .Select((p, i) => { p.Sequence = i + 1; return p; })
                .ToList();
            grid.DataSource = null;
            grid.DataSource = orderedList;
        }

        private void btnLookup_Click(object sender, EventArgs e)
        {
            frmSupplierLookup f = new frmSupplierLookup();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Supplier sup = f.SelectedSupplier;
                if (sup != null)
                {
                    txtCompanyKode.Text = sup.Code;
                    txtCompany.Text = sup.Name;
                    txtCompanyAddress.Text = sup.Address;
                    txtCompanyTel.Text = sup.Phone;
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = (ToolStripButton)sender;
            if (btn.Text.ToLower() == "edit") Edit();
            else if (btn.Text.ToLower() == "add") Add();
            else if (btn.Text.ToLower() == "delete") Delete();
        }

        public void Add()
        {
            List<PurchaseOrderDetail> list = (List<PurchaseOrderDetail>)grid.DataSource;
            frmPurchaseDetail f = new frmPurchaseDetail();
            f.Tag = this.Tag;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (f.Detail != null)
                {
                    if (list == null) list = new List<PurchaseOrderDetail>();

                    if (list.Where(t => t.CatalogID == f.Detail.CatalogID).Count() > 0)
                    {
                        foreach (var detail in list.Where(p => p.CatalogID == f.Detail.CatalogID))
                        {
                            detail.Quantity += f.Detail.Qty;
                            detail.UnitPrice = f.Detail.PricePerUnit;
                            detail.TotalPrice = detail.Quantity * detail.UnitPrice;
                        }
                    }
                    else
                    {

                        PurchaseOrderDetail detailItem = new PurchaseOrderDetail();
                        detailItem.UniqueID = f.Detail.UniqueID;
                        detailItem.CreatedBy = f.Detail.CreatedBy;
                        detailItem.CreatedDate = f.Detail.CreatedDate;
                        detailItem.CatalogID = f.Detail.CatalogID;
                        detailItem.CatalogName = f.Detail.CatalogName;
                        detailItem.UnitPrice = f.Detail.PricePerUnit;
                        detailItem.Quantity = f.Detail.Qty;
                        detailItem.TotalPrice = f.Detail.TotalPrice;
                        detailItem.Unit = f.Detail.Unit;
                        detailItem.Coli = f.Detail.Coli;
                        list.Add(detailItem);
                    }
                }
            }

            LoadDetail(list);
        }

        public void Edit()
        {
            if (grid.CurrentRow == null) return;
            int Row = grid.CurrentRow.Index;

            string code = string.Format("{0}", grid["colUniqueID", Row].Value);
            List<PurchaseOrderDetail> list = (List<PurchaseOrderDetail>)grid.DataSource;
            PurchaseOrderDetail selected = list.Where(t => t.UniqueID.ToString() == code).FirstOrDefault();
            if (selected != null)
            {
                frmPurchaseDetail f = new frmPurchaseDetail();
                f.Detail = new PurchaseDetail()
                {
                    UniqueID = selected.UniqueID,
                    CreatedBy = selected.CreatedBy,
                    CreatedDate = selected.CreatedDate,
                    CatalogID = selected.CatalogID,
                    CatalogName = selected.CatalogName,
                    PricePerUnit = selected.UnitPrice,
                    Qty = selected.Quantity,
                    TotalPrice = selected.TotalPrice,
                    Unit = selected.Unit,
                    Coli = selected.Coli

                };

                f.Tag = this.Tag;
                if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (list != null)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list[i].UniqueID.ToString() == code)
                            {
                                list[i].CatalogID = f.Detail.CatalogID;
                                list[i].CatalogName = f.Detail.CatalogName;
                                list[i].Unit = f.Detail.Unit;

                                list[i].UnitPrice = f.Detail.PricePerUnit;
                                list[i].Quantity = f.Detail.Qty;
                                list[i].TotalPrice = f.Detail.TotalPrice;
                                list[i].Coli = f.Detail.Coli;

                                list[i].CreatedBy = f.Detail.CreatedBy;
                                list[i].CreatedDate = f.Detail.CreatedDate;
                            }
                        }
                    }
                }
                LoadDetail(list);


            }
        }

        public void Delete()
        {
            if (grid.CurrentRow == null) return;
            int Row = grid.CurrentRow.Index;
            DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string code = grid["colID", Row].Value.ToString();
                List<PurchaseOrderDetail> list = (List<PurchaseOrderDetail>)grid.DataSource;
                if (list == null) list = new List<PurchaseOrderDetail>();
                else
                {
                    PurchaseOrderDetail selected = list.Where(t => t.PurchaseOrderItemID.ToString() == code).FirstOrDefault();
                    if (selected != null)
                    {
                        list.Remove(selected);
                    }
                }

                LoadDetail(list);
            }
        }


    }
}
