using DataLayer;
using DataObject;
using LogicLayer;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sendang.Rejeki.Transaction
{
    public partial class frmProdRecon : Form
    {
        public frmProdRecon()
        {
            InitializeComponent();
        }

        private void frmProdRecon_Load(object sender, EventArgs e)
        {
            label13.Text = "Price/Unit = HPP + (HPP x Biaya Penyusutan(1-4)%) + Biaya Produksi" + Environment.NewLine + "Tekan tombol Procces jika anda sudah merasa yakin.";
            //List<Catalog> productList = new List<Catalog>();
            List<Catalog> itemList = CatalogItem.GetItems();
            List<Catalog> productList = CatalogItem.GetItems();

            itemList.Insert(0, new Catalog() { ID = 0, Name = "", Type = "Item" });
            cboCatalog.DataSource = null;
            cboCatalog.DisplayMember = "Name"; //Name column of contact datasource
            cboCatalog.ValueMember = "ID";//Value column of contact datasource        
            cboCatalog.DataSource = itemList;

            productList.Insert(0, new Catalog() { ID = 0, Name = "", Type = "Item" });
            cboProduct.DataSource = null;
            cboProduct.DisplayMember = "Name"; //Name column of contact datasource
            cboProduct.ValueMember = "ID";//Value column of contact datasource        
            cboProduct.DataSource = productList;

            List<Options> BiayaPenyusutanList = OptionItem.GetOptionsByName("BiayaPenyusutan");
            BiayaPenyusutanList.Insert(0, new Options() { ValueMember = "0", DisplayMember = "--Pilih--", Name = "BiayaPenyusutan" });

            cboBiayaPenyusutan.DataSource = BiayaPenyusutanList;
            cboBiayaPenyusutan.ValueMember = "ValueMember";
            cboBiayaPenyusutan.DisplayMember = "DisplayMember";


            cboUnit.Items.Clear();
            cboUnit.Items.AddRange(Config.GetCatalogUnit());

            if (!string.IsNullOrEmpty(ReconcileID))
            {
                List<ReconcileDetail> itemDetails = ReconcileItem.GetDetail(Convert.ToInt32(ReconcileID));
                Reconcile item = ReconcileItem.GetByID(Convert.ToInt32(ReconcileID));
                if (item != null)
                {
                    txtTransDate.Text = item.ProccessDate.ToString(Utilities.FORMAT_DateTime);
                }
                //btnProccess.Enabled = false;
                if (itemDetails.Count > 0)
                {
                    ReconcileDetail itemDetail = itemDetails.FirstOrDefault();
                    cboCatalog.SelectedValue = itemDetail.CatalogID;
                    txtDate.Text = itemDetail.CatalogPriceDate.HasValue ? itemDetail.CatalogPriceDate.Value.ToString(Utilities.FORMAT_Date) : string.Empty;

                    txtQty.Text = itemDetail.CatalogQty.ToString();
                    txtPricePerUnit.Text = Utilities.FormatToMoney(itemDetail.CatalogPrice);
                    Catalog cat = CatalogItem.GetByID(itemDetail.CatalogID);
                    if (cat != null)
                    {
                        txtUnit.Text = cat.Unit;
                    }

                    cboProduct.SelectedValue = itemDetail.ProductID;
                    txtProductPricePerUnit.Text = Utilities.FormatToMoney(itemDetail.ProductPrice);
                    txtProductQty.Text = itemDetail.ProductQty.ToString();

                    cat = CatalogItem.GetByID(itemDetail.ProductID);
                    if (cat != null)
                    {
                        cboUnit.SelectedValue = cat.Unit;
                    }

                    cboBiayaPenyusutan.SelectedValue = itemDetail.penyusutan;
                    txtBiayaPenyusutan.Text = Utilities.ToString(itemDetail.biayapenyusutan, "N2");
                    txtBiayaProduksi.Text = Utilities.ToString(itemDetail.biayaproduksi, "N2");
                    txtLastHPP.Text = Utilities.ToString(itemDetail.hpp, "N2");

                }
            }
        }

        private void cboCatalog_SelectedIndexChanged(object sender, EventArgs e)
        {
            catalogStock = 0;
            ComboBox cbo = (ComboBox)sender;
            Catalog c = (Catalog)cbo.SelectedItem;
            if (c != null)
            {
                CstmPurchaseDetail detailItem = PurchaseItem.GetCatalogReconcilePrice(c.ID);
                if (detailItem != null)
                {
                    List<CstmPurchasePriceRate> list = PurchaseItem.GetHargaBeliRata(c.ID.ToString(), DateTime.Now);
                    decimal price = list.Sum(t => t.Price) / list.Count;
                    txtPricePerUnit.Text = string.Format("{0:N2}", price);
                    txtDate.Text = string.Format("{0:dd MMM yyyy}", detailItem.PurchaseDate);
                    //txtProductPricePerUnit.Text = string.Format("{0:N2}", price);
                }
                else
                {
                    txtPricePerUnit.Text = string.Format("{0:N2}", 0);
                    txtDate.Text = string.Format("{0:dd MMM yyyy}", DateTime.Now);
                    //txtProductPricePerUnit.Text = string.Format("{0:N2}", 0);
                }
                txtUnit.Text = c.Unit;

                CatalogStock cs = CatalogStockItem.GetActiveByCatalogID(c.ID);
                if (cs != null)
                {
                    catalogStock = cs.Stock;
                }
                txtStock.Text = catalogStock.ToString();
            }
        }

        private void btnProccess_Click(object sender, EventArgs e)
        {
            decimal catQty = 0;
            decimal prodQty = 0;
            decimal catalogPricePerUnit = 0;
            decimal productPricePerUnit = 0;

            if (string.Format("{0}", cboCatalog.Text).Length == 0)
            {
                Utilities.ShowValidation("Catalog tidak boleh kosong!");
                cboCatalog.Focus();
                return;
            }
           
            if (!decimal.TryParse(txtQty.Text, out catQty))
            {
                Utilities.ShowValidation(string.Format("Tentukan Quantity {0}!", cboCatalog.SelectedItem.ToString()));
                txtQty.Focus();
                return;
            }
            if (catQty <= 0)
            {
                Utilities.ShowValidation(string.Format("Tentukan Quantity {0}!", cboCatalog.SelectedItem.ToString()));
                txtQty.Focus();
                return;
            }

            if (catalogStock < catQty)
            {
                Utilities.ShowValidation("Stok Catalog tidak mencukupi!");
                txtQty.Focus();
                return;
            }

            if (string.Format("{0}", cboProduct.Text).Length == 0)
            {
                Utilities.ShowValidation("Product tidak boleh kosong!");
                cboProduct.Focus();
                return;
            }

            if (txtTransDate.Value == null)
            {
                Utilities.ShowValidation(string.Format("Pilih Tanggal transaksi {0}!", cboProduct.SelectedItem.ToString()));
                txtTransDate.Focus();
                return;
            }
            if (cboUnit.Text.Trim().Length == 0)
            {
                Utilities.ShowValidation(string.Format("Tentukan satuan {0}!", cboProduct.SelectedItem.ToString()));
                cboUnit.Focus();
                return;
            }
            if (!decimal.TryParse(txtProductPricePerUnit.Text, out catalogPricePerUnit))
            {
                Utilities.ShowValidation(string.Format("Tentukan harga {0}!", cboCatalog.Text));
                txtProductPricePerUnit.Focus();
                return;
            }

            if (!decimal.TryParse(txtProductQty.Text, out prodQty))
            {
                Utilities.ShowValidation(string.Format("Tentukan jumlah {0}!", cboProduct.Text));
                txtProductQty.Focus();
                return;
            }
            if (prodQty <= 0)
            {
                Utilities.ShowValidation(string.Format("Tentukan jumlah {0}!", cboProduct.Text));
                txtProductQty.Focus();
                return;
            }
            if (!decimal.TryParse(txtProductPricePerUnit.Text, out productPricePerUnit))
            {
                Utilities.ShowValidation(string.Format("Tentukan harga {0}!", cboProduct.Text));
                txtProductPricePerUnit.Focus();
                return;
            }
            if (productPricePerUnit <= 0)
            {
                Utilities.ShowValidation(string.Format("Tentukan harga {0}!", cboProduct.Text));
                txtProductPricePerUnit.Focus();
                return;
            }

            if (cboCatalog.SelectedValue == null || (int)cboCatalog.SelectedValue == 0)
            {
                Utilities.ShowValidation("Pilih Item terlebih dahulu!");
                cboProduct.SelectedValue = 0;
                cboProduct.Focus();
                return;
            }

            if (cboCatalog.SelectedValue != null && (int)cboCatalog.SelectedValue > 0)
            {
                Catalog catSelected = (Catalog)cboCatalog.SelectedItem;
                Catalog c = (Catalog)cboProduct.SelectedItem;
                if (c != null && c.ID == catSelected.ID)
                {
                    Utilities.ShowValidation("Catalog Tujuan tidak boleh sama dengan Catalog Sumber!");
                    cboProduct.SelectedValue = 0;
                    cboProduct.Focus();
                    return;
                }
            }

            if (HPP <= 0)
            {
                Utilities.ShowValidation(string.Format("Tentukan HPP {0}!", cboProduct.Text));
                txtProductPricePerUnit.Focus();
                return;
            }



            Catalog product = null;
            if (cboProduct.SelectedValue == null &&
                string.Format("{0}", cboProduct.Text).Length > 0)
            {
                product = CatalogItem.GetCatalog(cboProduct.Text.Trim(), cboUnit.Text.Trim());
                if (product == null)
                {
                    product = CatalogItem.Insert(cboProduct.Text, cboUnit.Text, string.Empty, string.Empty, null, Utilities.Username, "Item");
                    Log.Insert(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(product)));
                    List<Catalog> productList = (List<Catalog>)cboProduct.DataSource;
                    productList.Add(product);
                    cboProduct.DataSource = null;
                    cboProduct.DataSource = productList.OrderBy(t => t.Name).ToList();
                    cboProduct.SelectedValue = product.ID;
                }
            }
            else
            {
                product = (Catalog)cboProduct.SelectedItem;
            }


            ReconcileDetail detail = new ReconcileDetail();
            detail.CatalogID = (int)cboCatalog.SelectedValue;
            Catalog cat = (Catalog)cboCatalog.SelectedItem;
            detail.CatalogPrice = catalogPricePerUnit;
            detail.CatalogQty = catQty;
            detail.CreatedDate = DateTime.Now;
            detail.CreatedBy = Utilities.Username;
            detail.CatalogUnit = cat.Unit;
            try
            {
                detail.CatalogPriceDate = Convert.ToDateTime(txtDate.Text);
            }
            catch (Exception)
            {
                detail.CatalogPriceDate = (DateTime?)null;
            }
            detail.ProductID = product.ID;
            detail.ProductPrice = productPricePerUnit;
            detail.ProductQty = prodQty;
            detail.ProductUnit = product.Unit;
            detail.penyusutan = string.Format("{0}", cboBiayaPenyusutan.SelectedValue);
            detail.hpp = HPP;
            detail.biayapenyusutan = HPP * biayaPenyusutan;
            detail.biayaproduksi = biayaProduksi;

            string desc = string.Format("{0:N2}{1} {2} ----> {3:N2}{4} {5}.", catQty, cat.Unit, cat.ToString(), prodQty, product.Unit, product.ToString());
            DialogResult dialogResult = MessageBox.Show("Are you sure want to procces this?\nProccessing this would update current stock", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
            {
                if (!string.IsNullOrEmpty(ReconcileID))
                {
                    int deleteResult = ReconcileItem.Delete(ReconcileID);
                    if (deleteResult > 0)
                    {
                        Reconcile result = ReconcileItem.Insert(txtTransDate.Value, desc, detail, 1);
                        if (result != null)
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                            Utilities.ShowValidation("Done!");
                        }
                        else
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                            Utilities.ShowValidation("Failed!");
                        }
                    }
                }
                else
                {

                    Reconcile result = ReconcileItem.Insert(txtTransDate.Value, desc, detail, 1);
                    if (result != null)
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        Utilities.ShowValidation("Done!");
                    }
                    else
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                        Utilities.ShowValidation("Failed!");
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        decimal itemPricePerUnit = 0;
        decimal HPP = 0;
        decimal biayaPenyusutan = 0;
        decimal biayaProduksi = 0;
        decimal catalogStock = 0;
        void calculatePrice()
        {
            /*
              RUMUS
              harga price dihitung  = (HPP 0.9(terakhir) * Biaya penyusutan(memilih antar 1 - 4 %)) +Biaya Produksi(bisa ditulis manual))
              */

            decimal.TryParse(Utilities.RawNumberFormat(txtProductPricePerUnit.Text), out itemPricePerUnit);
            decimal.TryParse(Utilities.RawNumberFormat(txtLastHPP.Text), out HPP);
            if (cboBiayaPenyusutan.SelectedValue != null)
                decimal.TryParse(cboBiayaPenyusutan.SelectedValue.ToString(), out biayaPenyusutan);
            decimal.TryParse(Utilities.RawNumberFormat(txtBiayaProduksi.Text), out biayaProduksi);

            itemPricePerUnit = HPP + (HPP * biayaPenyusutan) + biayaProduksi;

            txtBiayaPenyusutan.Text = Utilities.ToString(HPP * biayaPenyusutan, "N2");

            txtProductPricePerUnit.Text = Utilities.ToString(itemPricePerUnit);
        }

        private void cboProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = (ComboBox)sender;
            Catalog c = (Catalog)cbo.SelectedItem;



            if (c != null && c.ID > 0)
            {
                cstmHPP lastHPP = HPPItem.GetLastHpp(c.ID, DateTime.Now);
                if (lastHPP == null)
                {
                    lastHPP = new cstmHPP() { HPP = 0 };
                }
                txtLastHPP.Text = string.Format("{0:N0}", lastHPP.HPP);
                cboUnit.Text = c.Unit;
                cboUnit.Enabled = false;

            }
            else
            {
                txtLastHPP.Text = "0";
                cboUnit.Text = "";
                cboUnit.Enabled = true;
            }
            calculatePrice();
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
            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                calculatePrice();
            }
        }


        void tb_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb != null)
            {
                calculatePrice();
            }
        }
        public string ReconcileID { get; set; }

        private void cboBiayaPenyusutan_SelectedIndexChanged(object sender, EventArgs e)
        {
            calculatePrice();
        }
    }
}
