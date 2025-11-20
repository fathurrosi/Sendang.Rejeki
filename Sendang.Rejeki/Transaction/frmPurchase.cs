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
using System.IO;
using Microsoft.Reporting.WinForms;
using Newtonsoft.Json;

namespace Sendang.Rejeki.Transaction
{
    //public partial class frmPurchase : Form
    public partial class frmPurchase : SendangRejekiForm
    {

        public frmPurchase()
        {
            InitializeComponent();
        }
        public string PurchaseNo { get; set; }

        private void frmPurchase_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnCancel.Text = "Close";

            if (AllowCreate || AllowUpdate)
            {
                btnSave.Text = "Save";
                btnCancel.Text = "Cancel";
                btnSave.Enabled = true;
            }

            btnAdd.Enabled = AllowCreate;
            btnDelete.Enabled = AllowDelete;
            btnEdit.Enabled = AllowRead || AllowCreate || AllowUpdate;
            if (!AllowUpdate && AllowRead)
                btnEdit.Text = "View";


            grid.AutoGenerateColumns = false;
            List<Supplier> list = SupplierItem.GetAll();
            list.Insert(0, new Supplier());
            cboSupplier.DataSource = list.OrderBy(t => t.Name).ToList();
            cboSupplier.DisplayMember = "Name";
            cboSupplier.ValueMember = "Code";
          
            Purchase item = PurchaseItem.GetByCode(PurchaseNo);
            if (item != null)
            {
                txtTransDate.Value = item.PurchaseDate;
                cboSupplier.SelectedValue = item.SupplierCode;
                LoadDetail(item.Details);
            }
        }

        void LoadDetail(List<PurchaseDetail> list)
        {
            decimal total = 0;
            if (list != null)
            {
                list.ForEach(t =>
                {
                    total += Convert.ToDecimal(t.Qty) * (t.PricePerUnit);
                });
            }

            //txtTotalPrice.Text = total.ToString(Utilities.FORMAT_Money);
            grid.DataSource = null;
            grid.DataSource = list;

        }

        public bool IsValid()
        {
            try
            {
                //if (txtPurchaseNo.Text.Length == 0)
                //{
                //    Utilities.ShowValidation("Puchase No. tidak boleh kosong");
                //    txtPurchaseNo.Focus();
                //    return false;
                //}
                //else if (dtPurchaseDate.Value == null)
                //{
                //    Utilities.ShowValidation("Purchase date tidak boleh kosong");
                //    dtPurchaseDate.Focus();
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                LogicLayer.Log.Error(ex.ToString());
                return false;
            }

            return true;
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        public void Add()
        {
            List<PurchaseDetail> list = (List<PurchaseDetail>)grid.DataSource;
            frmPurchaseDetail f = new frmPurchaseDetail();
            f.Tag = this.Tag;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (list == null) list = new List<PurchaseDetail>();
                list.Add(f.Detail);
            }

            LoadDetail(list);
        }

        public void Edit()
        {
            if (grid.CurrentRow == null) return;
            int Row = grid.CurrentRow.Index;

            string code = string.Format("{0}", grid[0, Row].Value);
            List<PurchaseDetail> list = (List<PurchaseDetail>)grid.DataSource;
            PurchaseDetail selected = list.Where(t => t.UniqueID.ToString() == code).FirstOrDefault();
            frmPurchaseDetail f = new frmPurchaseDetail();
            f.Detail = selected;
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
                            list[i].PricePerUnit = f.Detail.PricePerUnit;
                            list[i].Qty = f.Detail.Qty;
                            list[i].TotalPrice = f.Detail.TotalPrice;
                            list[i].Unit = f.Detail.Unit;
                            list[i].Coli = f.Detail.Coli;
                        }
                    }
                }
            }
            LoadDetail(list);
        }

        public void Delete()
        {
            if (grid.CurrentRow == null) return;
            int Row = grid.CurrentRow.Index;
            DialogResult dialogResult = MessageBox.Show("Are you sure want to delete this?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                string code = grid["colID", Row].Value.ToString();
                List<PurchaseDetail> list = (List<PurchaseDetail>)grid.DataSource;
                if (list == null) list = new List<PurchaseDetail>();
                else
                {
                    PurchaseDetail selected = list.Where(t => t.ID.ToString() == code).FirstOrDefault();
                    if (selected != null)
                    {
                        list.Remove(selected);
                    }
                }

                LoadDetail(list);
            }
        }

        void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Utilities.IsValidNumberWithComma(sender, e.KeyChar))
            {
                e.Handled = true;
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
        void tb_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                tb.Text = Utilities.CorrectFormat(tb.Text);
            }
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = (ToolStripButton)sender;
            if (btn.Text.ToLower() == "edit") Edit();
            else if (btn.Text.ToLower() == "add") Add();
            else if (btn.Text.ToLower() == "delete") Delete();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<PurchaseDetail> details = (List<PurchaseDetail>)grid.DataSource;
            if (cboSupplier.Text.Length == 0)
            {
                Utilities.ShowValidation("Maaf, Supplier harus diisi");
                cboSupplier.Focus();
                return;
            }
            else if (txtTransDate.Value.Year <= 1900)
            {
                Utilities.ShowValidation("Maaf, Tanggal tidak valid");
                txtTransDate.Focus();
                return;
            }
            else if (details == null || details.Count == 0)
            {
                Utilities.ShowValidation("Maaf, silahkan input catalog yang akan diisikan ke dalam stock");
                return;
            }

            string supplierCode = string.Format("{0}", cboSupplier.SelectedValue);
            if (string.Format("{0}", cboSupplier.SelectedValue) == string.Empty)
            {
                //simpan dulu
                string name = cboSupplier.Text;
                string code = Utilities.Crop(name.Trim().Replace(" ", ""), 5);
                Supplier customer = SupplierItem.Insert(string.Format("{0}{1}", code, DateTime.Now.ToString(Utilities.FORMAT_DateTime_Flat)), name, Utilities.Username);
                Log.Insert(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(customer)));
                supplierCode = customer.Code;
            }

            decimal totalQty = 0;
            decimal totalPrice = 0;

            Purchase item = PurchaseItem.GetByCode(PurchaseNo);
            if (item == null)
            {
                item = new Purchase();
                item.CreatedBy = Utilities.Username;
                item.CreatedDate = DateTime.Now;
                item.PurchaseDate = txtTransDate.Value;
                int index = PurchaseItem.GetLastID();
                string purchaseNo = string.Format("{0}-{1}", item.PurchaseDate.ToString(Utilities.FORMAT_DateTime_Flat), index);
                item.PurchaseNo = purchaseNo;
                item.SupplierCode = supplierCode;
            }
            else
            {
                item.PurchaseDate = txtTransDate.Value;
                item.SupplierCode = supplierCode;
            }
            item.Details = details;
            foreach (PurchaseDetail detail in item.Details)
            {
                totalPrice += detail.TotalPrice;
                totalQty += detail.Qty;
            }
            item.TotalPrice = totalPrice;
            item.TotalQty = totalQty;

            List<string> unitList = details.Select(t => t.Unit).Distinct().ToList();
            item.Notes = string.Empty;
            foreach (string unit in unitList)
            {
                decimal? unitQuantity = details.Where(t => t.Unit == unit).Sum(t => t.Qty);
                if (unit == unitList.Last())
                    item.Notes += unitQuantity.HasValue ? string.Format("{0:N2}({1})", unitQuantity.Value, unit) : string.Empty;
                else
                    item.Notes += unitQuantity.HasValue ? string.Format("{0:N2}({1}),", unitQuantity.Value, unit) : string.Empty;
            }

            int result = -1;
            if (string.IsNullOrEmpty(PurchaseNo))
            {
                result = PurchaseItem.SaveStock(item);
                Log.Insert(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(item)));
            }
            else
            {
                result = PurchaseItem.EditStock(item);
                Log.Update(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(item)));
            }
            if (result > 0)
            {
                if (Utilities.ShowInformation("Data sudah berhasil disimpan") == System.Windows.Forms.DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(PurchaseNo))
                    {
                        btnSave.Enabled = false;
                        btnCancel.Text = "Close";
                        toolStrip.Enabled = btnSave.Enabled;
                        btnCancel.Text = !btnSave.Enabled ? "Close" : "Cancel";
                    }
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            else
            {
                Utilities.ShowInformation("Ups..! Mohon maaf data tidak berhasil disimpan");
            }
        }
    }
}
