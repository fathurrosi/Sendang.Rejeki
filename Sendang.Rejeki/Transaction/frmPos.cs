using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicLayer;
using DataLayer;
using Sendang.Rejeki.Lookup;
using DataObject;
using System.IO;
using Microsoft.Reporting.WinForms;
using Newtonsoft.Json;

using System.Drawing.Printing;

namespace Sendang.Rejeki.Transaction
{
    //public partial class frmPos : Form
    public partial class frmPos : SendangRejekiForm
    {
        public class PosHelper
        {
            public int Index { get; set; }
            public string CatalogName { get; set; }
            public int CatalogID { get; set; }
            public string Qty { get; set; }

            public string Unit { get; set; }
            public string Price { get; set; }
            public string SubTotal { get; set; }
            public string Coli { get; set; }


        }

        #region Funtions

        private void Recalculate()
        {
            grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            int rowIndex = grid.CurrentCell.RowIndex;
            object qty = grid.Rows[rowIndex].Cells[3].Value;
            object price = grid.Rows[rowIndex].Cells[6].Value;

            decimal tempQty = 0;
            decimal tempPrice = 0;
            decimal.TryParse(string.Format("{0}", qty), out tempQty);
            decimal.TryParse(string.Format("{0}", price), out tempPrice);


            decimal total = tempQty * tempPrice;
            grid.Rows[rowIndex].Cells[7].Value = Utilities.ToString(total);

            decimal totalPrice = 0;
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                tempQty = 0;
                tempPrice = 0;
                total = 0;
                qty = grid.Rows[i].Cells[3].Value;
                price = grid.Rows[i].Cells[6].Value;

                decimal.TryParse(string.Format("{0}", qty), out tempQty);
                decimal.TryParse(string.Format("{0}", price), out tempPrice);
                total = tempQty * tempPrice;
                totalPrice += total;
            }

            txtTotalPrice.Text = Utilities.ToString(totalPrice);
            if (!string.IsNullOrEmpty(TransactionID))
            {
                RecalculatePayment();
            }

        }

        private void GetCatalog(Catalog catalog)
        {
            List<PosHelper> helperList = (List<PosHelper>)grid.DataSource;
            int qty = 1;
            int rowIndex = 1;
            DataGridViewCell cell = grid.CurrentCell;
            CatalogPrice pPrice = CatalogPriceItem.GetByCatalogID(catalog.ID);
            //decimal discount = GetDiscount(catalog.ProductCode, qty);
            if (cell != null) rowIndex = cell.RowIndex + 1;

            for (int i = 0; i < helperList.Count; i++)
            {
                if (helperList[i].Index == rowIndex)
                {

                    helperList[i].CatalogID = catalog.ID;
                    helperList[i].Price = string.Format("{0}", pPrice == null ? 0 : pPrice.SellPrice);
                    helperList[i].CatalogName = catalog.Name;
                    helperList[i].Qty = qty.ToString();
                    break;
                }
            }
            grid.DataSource = null;
            grid.DataSource = helperList.OrderBy(t => t.Index).ToList();
        }

        #endregion

        public frmPos()
        {
            InitializeComponent();
        }

        List<Catalog> catalogList = new List<Catalog>();
        Sale sale = null;
        string _AutoAID = "Auto Generate";
        private void frmPos_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnCancel.Text = "Close";
            //#if (debug)
            if (AllowCreate || AllowUpdate)
            {
                btnSave.Text = "Save";
                btnCancel.Text = "Cancel";
                btnSave.Enabled = true;
            }

            btnAdd.Enabled = AllowCreate;
            btnDelete.Enabled = AllowDelete;
            //#endif
            DateTime NOW = DateTime.Now;
            txtTransDate.CustomFormat = Utilities.FORMAT_DateTime;
            txtTransDate.Value = NOW;
            //txtTransDate.Enabled = Utilities.IsSuperAdmin();
            //int saleIndex = SaleItem.GetNewIndex(NOW);
            //txtTransNo.Text = (string.IsNullOrEmpty(TransactionID)) ? string.Format((saleIndex <= 1000) ? "{0}{1:000}" : "{0}{1:0000}", NOW.ToString(Utilities.FORMAT_Date_Flat), saleIndex) : TransactionID;
            txtTransNo.Text = (string.IsNullOrEmpty(TransactionID)) ? _AutoAID : TransactionID;
            List<Customer> list = CustomerItem.GetAll();
            list.Insert(0, new Customer(0, string.Empty));
            cboCustomer.DisplayMember = "FullName";
            cboCustomer.ValueMember = "ID";
            cboCustomer.DataSource = list.OrderBy(t => t.FullName).ToList();

            catalogList = CatalogItem.GetAll();

            List<Options> paymentList = OptionItem.GetOptionsByName("Payment");

            //List<object> paymentList = new List<object>();
            //paymentList.Add(new { ID = 1, Name = "Cash" });
            //paymentList.Add(new { ID = 2, Name = "Bank Transfer" });
            //paymentList.Add(new { ID = 3, Name = "Credit" });

            cboPaymentType.DataSource = paymentList;
            cboPaymentType.ValueMember = "ValueMember";
            cboPaymentType.DisplayMember = "DisplayMember";

            if (!string.IsNullOrEmpty(TransactionID))
            {
                sale = SaleItem.GetByTransID(TransactionID);
                if (sale == null)
                {
                    Utilities.ShowInformation("No transaction found!");
                    this.Enabled = false;
                    return;
                }

                btnSave.Text = "Update";
                txtTransDate.Value = sale.TransactionDate;
                txtNotes.Text = sale.Notes;
                txtTotalPrice.Text = Utilities.ToString(sale.TotalPrice);
                txtReturn.Text = Utilities.ToString(sale.TotalPaymentReturn.Value);
                txtTotalPayed.Text = Utilities.ToString(sale.TotalPayment);
                txtPayment.Text = Utilities.ToString(sale.TotalPayment);
                cboPaymentType.SelectedValue = sale.PaymentType;

                Customer cust = CustomerItem.GetByID(sale.MemberID.Value);
                cboCustomer.Text = cust == null ? string.Empty : cust.FullName;
                txtAddress.Text = cust == null ? string.Empty : cust.Address;
                foreach (SaleDetail detailItem in sale.Details)
                {
                    int row = grid.Rows.Add();
                    grid.Rows[row].Cells[0].Value = detailItem.Sequence;
                    grid.Rows[row].Cells[0].ReadOnly = true;

                    (grid.Rows[row].Cells[1] as DataGridViewComboBoxCell).DisplayMember = "Name"; //Name column of contact datasource
                    (grid.Rows[row].Cells[1] as DataGridViewComboBoxCell).ValueMember = "ID";//Value column of contact datasource        
                    (grid.Rows[row].Cells[1] as DataGridViewComboBoxCell).DataSource = catalogList;
                    (grid.Rows[row].Cells[1] as DataGridViewComboBoxCell).Value = detailItem.CatalogID;
                    grid.Rows[row].Cells[1].ReadOnly = true;

                    grid.Rows[row].Cells[2].Value = detailItem.CatalogID;
                    grid.Rows[row].Cells[3].Value = detailItem.Quantity;
                    grid.Rows[row].Cells[4].Value = detailItem.Unit;
                    grid.Rows[row].Cells[4].ReadOnly = true;

                    grid.Rows[row].Cells[5].Value = detailItem.Coli;
                    grid.Rows[row].Cells[6].Value = detailItem.Price;
                    grid.Rows[row].Cells[7].Value = detailItem.TotalPrice;
                    grid.Rows[row].Cells[8].Value = detailItem.ID;
                }

                txtAddress.Enabled = false;
                grid.BeginEdit(false);
                btnPrint.Enabled = true;
                btnPrintToFile.Enabled = true;
                btnPrintSJ.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int row = grid.Rows.Add();
            int totalRow = grid.Rows.Count;
            int maxValue = 0;

            for (int i = 0; i < totalRow; i++)
            {
                int value = 0;
                Int32.TryParse(string.Format("{0}", grid.Rows[i].Cells[0].Value), out value);
                if (value > maxValue)
                {
                    maxValue = value;
                }
            }

            grid.Rows[row].Cells[0].Value = maxValue + 1;
            grid.Rows[row].Cells[0].ReadOnly = true;

            this.grid.CurrentCell = this.grid[1, row];
            grid.BeginEdit(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DataGridViewRow currentRow = grid.CurrentRow;
            if (currentRow != null)
            {
                int rowIndex = currentRow.Index;
                grid.Rows.RemoveAt(rowIndex);
                Recalculate();
            }
        }

        #region Grid Event

        #endregion

        //public string TransactionID { get; set; }
        private string _TransactionID;

        public string TransactionID
        {
            get { return string.Format("{0}", _TransactionID); }
            set { _TransactionID = value; }
        }


        private void grid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (grid.CurrentCell.ColumnIndex == 3 || grid.CurrentCell.ColumnIndex == 6)
            {
                TextBox tb = (TextBox)e.Control;
                tb.Name = grid.CurrentCell.OwningColumn.Name;
                tb.KeyPress += tb_KeyPress;
                tb.Leave += tb_Leave;
                tb.KeyUp += tb_KeyUp;
                tb.Enter += tb_Enter;
            }
            else if (grid.CurrentCell.ColumnIndex == 1)
            {
                ComboBox cbo = (ComboBox)e.Control;
                cbo.SelectedIndexChanged += cbo_SelectedIndexChanged;
            }
        }

        void tb_Enter(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                tb.Text = Utilities.RawNumberFormat(tb.Text);
            }
        }

        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Utilities.IsValidNumberWithComma(sender, e.KeyChar))
            {
                e.Handled = true;
            }
        }
        void tb_KeyUp(object sender, KeyEventArgs e)
        {
            Recalculate();
        }

        void tb_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                if (string.Format("{0}", tb.Name).ToLower() == "colqty")
                    tb.Text = Utilities.CorrectFormat(tb.Text, "N2");
                else if (string.Format("{0}", tb.Name).ToLower() == "colColi".ToLower())
                {
                    tb.Text = Utilities.CorrectFormat(tb.Text, "N0");
                }
                else tb.Text = Utilities.CorrectFormat(tb.Text);

                Recalculate();
            }
        }

        void cbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            if (cbo != null)
            {
                Catalog cat = (Catalog)cbo.SelectedItem;
                if (cat != null)
                {
                    int rowIndex = grid.CurrentCell.RowIndex;
                    grid.Rows[rowIndex].Cells[2].Value = cat.ID;
                    grid.Rows[rowIndex].Cells[4].Value = cat.Unit;
                    grid.Rows[rowIndex].Cells[4].ReadOnly = true;

                }
            }
        }

        private void grid_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if ((this.grid.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn) ||
            (this.grid.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn))
            {
                this.grid.BeginEdit(false);

                //if (e.ColumnIndex == 4)
                //{
                //    DataGridViewComboBoxCell cbo = (DataGridViewComboBoxCell)(grid.Rows[e.RowIndex].Cells[e.ColumnIndex]);
                //    cbo.Items.Clear();
                //    cbo.Items.AddRange(Config.GetCatalogUnit());
                //}
            }
        }

        private void grid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DataGridViewComboBoxCell cboCatalog = (DataGridViewComboBoxCell)(grid.Rows[e.RowIndex].Cells[e.ColumnIndex]);
                cboCatalog.DataSource = null;
                cboCatalog.DisplayMember = "Name"; //Name column of contact datasource
                cboCatalog.ValueMember = "ID";//Value column of contact datasource        
                cboCatalog.DataSource = catalogList;
            }
        }

        private void RecalculatePayment()
        {
            decimal payment = 0;
            decimal totalPrice = 0;
            decimal.TryParse(txtPayment.Text, out payment);
            decimal.TryParse(txtTotalPrice.Text, out totalPrice);

            if (payment > 0)
            {
                decimal result = payment - totalPrice;
                txtReturn.Text = Utilities.ToString(result);
            }
        }
        private void txtPayment_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void txtPayment_KeyUp(object sender, KeyEventArgs e)
        {
            decimal payment = 0;
            decimal.TryParse(txtPayment.Text, out payment);
            txtTotalPayed.Text = Utilities.ToString(payment);
            RecalculatePayment();
        }
        private void txtPayment_Leave(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            Customer cust = (Customer)cbo.SelectedItem;
            txtAddress.Text = cust != null ? cust.Address : string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int paymentTipe = 0;
            int customerID = 0;
            sale = null;
            Sale existingItem = SaleItem.GetByTransID(txtTransNo.Text.Trim());
            if (string.IsNullOrEmpty(TransactionID) && existingItem != null)
            {
                Utilities.ShowValidation("Please change transaction no. value! it's already exist.");
                return;
            }
            else if (grid.Rows.Count == 0)
            {
                Utilities.ShowValidation("Please add transaction detail.");
                return;
            }
            else
            {
                for (int i = 0; i < grid.Rows.Count; i++)
                {
                    decimal tempQty = 0;
                    decimal tempPrice = 0;
                    //decimal total = 0;
                    object qty = grid.Rows[i].Cells[3].Value;
                    object objColi = grid.Rows[i].Cells[5].Value;
                    object price = grid.Rows[i].Cells[6].Value;
                    int catalogID = 0;

                    if (!int.TryParse(string.Format("{0}", grid.Rows[i].Cells[2].Value), out catalogID))
                    {
                        Utilities.ShowValidation("Please fill catalog field!");
                        return;
                    }
                    else if (!decimal.TryParse(string.Format("{0}", qty), out tempQty))
                    {
                        Utilities.ShowValidation("Please fill quantity field!");
                        return;
                    }
                    else if (tempQty == 0)
                    {
                        Utilities.ShowValidation("Please fill quantity field must be more then 0");
                        return;
                    }
                    else if (!decimal.TryParse(string.Format("{0}", price), out tempPrice))
                    {
                        Utilities.ShowValidation("Please fill price field!");
                        return;
                    }
                    else if (tempPrice == 0)
                    {
                        Utilities.ShowValidation("Please fill price field must be more then 0");
                        return;
                    }
                    //else if (txtPayment.Text.Length == 0)
                    //{
                    //    Utilities.ShowValidation("Please fill payment field");
                    //    txtPayment.Focus();
                    //    return;
                    //}
                }
            }

            int.TryParse(string.Format("{0}", cboCustomer.SelectedValue), out customerID);
            if (customerID == 0 && cboCustomer.Text.Length > 0)
            {
                //simpan dulu
                Customer customer = CustomerItem.Insert(cboCustomer.Text, txtAddress.Text, Utilities.Username);
                customerID = customer.ID;
            }

            int.TryParse(string.Format("{0}", cboPaymentType.SelectedValue), out paymentTipe);
            Sale item = new Sale();
            item.TransactionID = (string.IsNullOrEmpty(TransactionID)) ? txtTransNo.Text : TransactionID;
            List<SaleDetail> details = new List<SaleDetail>();
            //decimal totalQty = 0;
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                int sequence = Convert.ToInt32(grid.Rows[i].Cells[0].Value);
                SaleDetail detail = new SaleDetail();
                decimal Coli = 0;
                decimal tempQty = 0;
                decimal tempPrice = 0;
                //decimal tempTotalPrice = 0;

                int catalogID = 0;
                int.TryParse(string.Format("{0}", grid.Rows[i].Cells[2].Value), out catalogID);

                object qty = grid.Rows[i].Cells[3].Value;
                decimal.TryParse(string.Format("{0}", qty), out tempQty);

                int ID = 0;
                int.TryParse(string.Format("{0}", grid.Rows[i].Cells[8].Value), out ID);

                decimal.TryParse(string.Format("{0}", grid.Rows[i].Cells[5].Value), out Coli);

                object price = grid.Rows[i].Cells[6].Value;
                decimal.TryParse(string.Format("{0}", price), out tempPrice);

                decimal tempTotalPrice = tempQty * tempPrice;
                //totalQty += tempQty;
                detail.Discount = 0;
                detail.Price = tempPrice;
                detail.Quantity = tempQty;
                detail.TotalPrice = tempTotalPrice;
                detail.TransactionID = item.TransactionID;
                detail.CatalogID = catalogID;
                detail.Sequence = sequence;
                detail.ID = ID;
                detail.Coli = Coli;
                detail.Unit = string.Format("{0}", grid.Rows[i].Cells[4].Value).Trim();
                details.Add(detail);
            }

            item.MemberID = customerID;
            item.Notes = txtNotes.Text;
            item.PaymentType = paymentTipe;
            item.Tax = 0;
            item.Terminal = Utilities.GetComputerName();

            item.Username = Utilities.Username;
            item.TransactionDate = txtTransDate.Value;

            decimal totalPayed = 0;
            decimal.TryParse(txtTotalPayed.Text, out totalPayed);

            item.TotalPrice = decimal.Round(details.Sum(t => t.TotalPrice));
            item.TotalPaymentReturn = totalPayed - item.TotalPrice;
            item.TotalPayment = totalPayed;

            List<string> unitList = details.Select(t => t.Unit).Distinct().ToList();
            foreach (string unit in unitList)
            {
                decimal? unitQuantity = details.Where(t => t.Unit == unit).Sum(t => t.Quantity);
                if (unit == unitList.Last())
                    item.TotalQty += unitQuantity.HasValue ? string.Format("{0:N2}({1})", unitQuantity.Value, unit) : string.Empty;
                else
                    item.TotalQty += unitQuantity.HasValue ? string.Format("{0:N2}({1}),", unitQuantity.Value, unit) : string.Empty;
            }
            item.Details = details;

            int result = -1;

            txtTotalPrice.Text = Utilities.ToString(item.TotalPrice);
            txtReturn.Text = Utilities.ToString(item.TotalPaymentReturn.HasValue ? item.TotalPaymentReturn.Value : 0);
            txtTotalPayed.Text = Utilities.ToString(item.TotalPayment);
            try
            {
                existingItem = SaleItem.GetByTransID(TransactionID);
                if (existingItem == null)
                {
                    sale = SaleItem.Insert(item);
                    if (sale != null)
                    {
                        Log.Insert(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(sale)));
                        txtTransNo.Text = sale.TransactionID;
                    }
                    //sale = SaleItem.GetByTransID(item.TransactionID);
                }
                else
                {
                    result = SaleItem.Delete(TransactionID);
                    if (result > 0)
                    {
                        item.TransactionID = txtTransNo.Text.Trim();
                        item.TransactionDate = txtTransDate.Value;
                        //item.CustomerName = existingItem.CustomerName;
                        item.ExpiredDate = existingItem.ExpiredDate;
                        //item.MemberID = existingItem.MemberID;
                        sale = SaleItem.Insert(item);
                        if (sale != null)
                        {
                            txtTransNo.Text = sale.TransactionID;
                            SaleItem.Update(sale.TransactionID, DateTime.Now, item.Username);
                            Log.Update(string.Format("{0}-{1}", this.Text, string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(sale))));
                            //sale = SaleItem.GetByTransID(item.TransactionID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }

            if (sale != null)
            {
                if (Utilities.ShowInformation("Data sudah berhasil disimpan") == System.Windows.Forms.DialogResult.OK)
                {
                    DisableControl();
                    //if (Utilities.IsSuperAdmin() && !string.IsNullOrEmpty(TransactionID))
                    if (!string.IsNullOrEmpty(TransactionID))
                    {
                        btnCancel.Text = "Close";
                    }
                    //else
                    //{
                    //    DialogResult = System.Windows.Forms.DialogResult.OK;
                    //}
                }
            }
            else
            {
                Utilities.ShowInformation("Ups..! Mohon maaf data tidak berhasil disimpan");
            }
        }




        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }

        private void Export(LocalReport report)
        {
            string deviceInfo =
              @"<DeviceInfo>
                <OutputFormat>EMF</OutputFormat>
                <PageWidth>8.5in</PageWidth>
                <PageHeight>11in</PageHeight>
                <MarginTop>0.25in</MarginTop>
                <MarginLeft>0.25in</MarginLeft>
                <MarginRight>0.25in</MarginRight>
                <MarginBottom>0.25in</MarginBottom>

    <PageHeight>21cm</PageHeight>
    <PageWidth>14.8cm</PageWidth>
    <LeftMargin>0.54cm</LeftMargin>
    <RightMargin>0.54cm</RightMargin>
    <TopMargin>0.54cm</TopMargin>
    <BottomMargin>0.54cm</BottomMargin>
    <ColumnSpacing>1.27cm</ColumnSpacing>

            </DeviceInfo>";
            Warning[] warnings;
            List<Stream> streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream, out warnings);
            foreach (Stream stream in streams)
                stream.Position = 0;
        }

        void DisableControl()
        {
            btnPrint.Enabled = true;
            btnPrintToFile.Enabled = true;
            btnPrintSJ.Enabled = true;
            toolStrip.Enabled = false;
            btnCancel.Text = "Close";
            btnSave.Enabled = false;
            cboCustomer.Enabled = false;
            cboPaymentType.Enabled = false;
            txtNotes.ReadOnly = true;
            txtAddress.ReadOnly = true;
            txtPayment.ReadOnly = true;
            txtTransNo.ReadOnly = true;
            txtTransDate.Enabled = false;
            grid.Enabled = false;
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {

                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings.DefaultPageSettings.Landscape = false;
                pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                pd.Print();
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            Font printFont = new Font("Calibri", 10);
            int maxRecord = 11;
            List<SaleDetail> details = new List<SaleDetail>();
            details = sale.Details;
            if (details.Count < maxRecord)
            {
                int max = details.Select(t => t.Sequence).Max();
                int loop = maxRecord - details.Count;
                for (int i = 0; i < loop; i++)
                {
                    max++;
                    SaleDetail sd = new SaleDetail();
                    sd.Sequence = max;
                    details.Add(sd);
                }
            }

            /*****************************************************************
             * HEADER
             * **************************************************************/
            float topMargin = Config.TopMargin();

            #region HEADER
            float y = 8 + topMargin;
            float w = 575;
            ev.Graphics.DrawString(sale.TransactionDate.ToString(Utilities.FORMAT_DateTime), printFont, Brushes.Black, w, y);
            y += printFont.GetHeight(ev.Graphics);
            ev.Graphics.DrawString(sale.TransactionID, printFont, Brushes.Black, w, y);
            y += printFont.GetHeight(ev.Graphics);
            Customer cust = CustomerItem.GetByID(sale.MemberID.Value);
            string customer = cust == null ? string.Empty : cust.FullName;
            string address = cust == null ? string.Empty : cust.Address;
            int custLength = address.Length >= 35 ? address.Length / 35 : 1;
            if (custLength > 3) custLength = 3;
            ev.Graphics.DrawString(customer, printFont, Brushes.Black, w, y);
            for (int i = 0; i < custLength; i++)
            {
                y += printFont.GetHeight(ev.Graphics);
                int start = i == 0 ? 0 : (i * 35);
                int end = address.Length > 35 ? i == 0 ? 35 : (i + 1) * 35 : address.Length;
                int sisa = address.Length - end;
                if (sisa < 35)
                {
                    sisa = address.Length;
                }
                string s = address.Substring(start, (sisa < 35) ? sisa : 35);
                ev.Graphics.DrawString(s, printFont, Brushes.Black, w, y);

            }
            #endregion

            y = 152.1107845F + topMargin;
            float yTotal = 355.561371F + topMargin;
            float xNo = Config.GetX_No();
            float xCatalogName = Config.GetX_Item();
            float xColly = Config.GetX_Colly();
            float xQTY = Config.GetX_Qty();
            float xPrice = Config.GetX_Harga();
            //float xQTY = 570;            
            //float xPrice = 670;
            float xTotalPrice = Config.GetX_TotalHarga();
            List<SaleDetail> detals = sale.Details.OrderBy(t => t.Sequence).ToList();
            decimal totalPrice = 0;
            foreach (SaleDetail detail in details)
            {
                if (string.IsNullOrEmpty(detail.CatalogName)) continue;
                ev.Graphics.DrawString(string.Format("{0}", detail.Sequence), printFont, Brushes.Black, xNo, y);
                //ev.Graphics.DrawString(string.Format("{0}  /  {1:N0} Colly", detail.CatalogName, detail.Coli), printFont, Brushes.Black, xCatalogName, y);
                ev.Graphics.DrawString(string.Format("{0}", detail.CatalogName), printFont, Brushes.Black, xCatalogName, y);

                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Far;

                ev.Graphics.DrawString(Utilities.ToString(detail.Coli, "N2"), printFont, Brushes.Black, xColly, y, stringFormat);
                ev.Graphics.DrawString(Utilities.ToString(detail.Quantity, "N2"), printFont, Brushes.Black, xQTY, y, stringFormat);
                ev.Graphics.DrawString(Utilities.ToString(detail.Price), printFont, Brushes.Black, xPrice, y, stringFormat);
                ev.Graphics.DrawString(Utilities.ToString(detail.Quantity * detail.Price), printFont, Brushes.Black, xTotalPrice, y, stringFormat);

                totalPrice += detail.Quantity * detail.Price;
                y += printFont.GetHeight(ev.Graphics);

            }
            ev.Graphics.DrawString(Utilities.ToString(totalPrice), printFont, Brushes.Black, xTotalPrice, yTotal, new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Far });
            // If more lines exist, print another page.
            //if (line != null)
            //ev.HasMorePages = true;
            //else
            ev.HasMorePages = false;
        }

        private void btnPrintToFile_Click(object sender, EventArgs e)
        {
            string reportPath = Directory.GetCurrentDirectory() + "\\Report\\Receipt.rdlc";
            //string reportPath = Directory.GetCurrentDirectory() + "\\Report\\Print.rdlc";
            Report.frmReportViewer f = new Report.frmReportViewer();

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("TransDate", txtTransDate.Text, true));
            parameters.Add(new ReportParameter("TransNo", txtTransNo.Text, true));
            parameters.Add(new ReportParameter("CustomerName", cboCustomer.Text, true));
            parameters.Add(new ReportParameter("CustomerAddress", txtAddress.Text, true));
            parameters.Add(new ReportParameter("Note", txtNotes.Text, true));
            parameters.Add(new ReportParameter("TotalPrice", txtTotalPrice.Text, true));
            parameters.Add(new ReportParameter("TotalPayed", txtTotalPayed.Text, true));
            parameters.Add(new ReportParameter("TotalReturn", txtReturn.Text, true));


            f.ReportName = "Sale";
            f.ReportPath = reportPath;

            //List<SaleDetail>  details = new List<SaleDetail>();
            //details = sale.Details;
            //if (details.Count < 8)
            //{
            //    int max = details.Select(t => t.Sequence).Max();
            //    int loop = 8 - details.Count;
            //    for (int i = 0; i < loop; i++)
            //    {
            //        max++;
            //        SaleDetail sd = new SaleDetail();
            //        sd.Sequence = max;
            //        details.Add(sd);
            //    }
            //}
            
            //f.DataSource = details.OrderBy( t => t.Sequence).ToList();
            f.DataSource = sale.Details;
            f.Params = parameters;
            f.Tag = this.Tag;
            f.ShowDialog();
        }


        private void btnPrintSJ_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            //string reportPath = Directory.GetCurrentDirectory() + "\\Report\\Receipt.rdlc";
            string reportPath = Directory.GetCurrentDirectory() + "\\Report\\PrintSJ.rdlc";
            Report.frmReportViewer f = new Report.frmReportViewer();

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("TransDate", txtTransDate.Text, true));
            parameters.Add(new ReportParameter("TransNo", txtTransNo.Text, true));
            parameters.Add(new ReportParameter("CustomerName", cboCustomer.Text, true));
            parameters.Add(new ReportParameter("CustomerAddress", txtAddress.Text, true));
            parameters.Add(new ReportParameter("Note", txtNotes.Text, true));
            parameters.Add(new ReportParameter("TotalPrice", txtTotalPrice.Text, true));
            parameters.Add(new ReportParameter("TotalPayed", txtTotalPayed.Text, true));
            parameters.Add(new ReportParameter("TotalReturn", txtReturn.Text, true));
            parameters.Add(new ReportParameter("tglCetak", today.ToString("dd MMM yyyy"), true));

            f.ReportName = "Sale";
            f.ReportPath = reportPath;
            f.DataSource = sale.Details;
            f.Params = parameters;
            f.Tag = this.Tag;

            int result = SaleItem.PrintSJ(sale.TransactionID, today);
            if (result > 0)
            {
                f.ShowDialog();
            }
        }


        private void txtTransDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime NOW = txtTransDate.Value;
            //int saleIndex = SaleItem.GetNewIndex(NOW);
            if (string.IsNullOrEmpty(TransactionID))
            {
                //txtTransNo.Text = string.Format((saleIndex <= 1000) ? "{0}{1:000}" : "{0}{1:0000}", NOW.ToString(Utilities.FORMAT_Date_Flat), saleIndex);
                txtTransNo.Text = _AutoAID;
            }

        }
    }
}
