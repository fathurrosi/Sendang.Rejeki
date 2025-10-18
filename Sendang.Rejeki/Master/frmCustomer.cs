using DataLayer;
using DataObject;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Sendang.Rejeki.Master
{
    public partial class frmCustomer : Form, ITransButton
    {
        public frmCustomer()
        {
            InitializeComponent();
        }
        public int ID { get; set; }


        public bool IsValid()
        {
            if (txtCode.Text.Length == 0)
            {
                Utilities.ShowValidation("Kode tidak boleh kosong");
                txtName.Focus();
                return false;
            }
            else if (txtName.Text.Length == 0)
            {
                Utilities.ShowValidation("Nama tidak boleh kosong");
                txtName.Focus();
                return false;
            }
            else
            {
                List<Customer> custList = CustomerItem.GetByCode(txtCode.Text.Trim());
                if (ID > 0)
                {
                    //Customer item = CustomerItem.GetByID(ID);
                    Customer itemOther = custList.Where(t => t.ID != ID).FirstOrDefault();
                    if (itemOther != null)
                    {
                        Utilities.ShowValidation(string.Format("Maaf, Kode \"{0}\" sudah dipakai oleh customer {1}.\nSilahkan input dengan Kode yang berbeda", txtCode.Text.Trim(), itemOther.FullName));
                        txtCode.Focus();
                        return false;
                    }
                }
                else if (custList.Count > 0)
                {
                    Utilities.ShowValidation(string.Format("Maaf, Kode \"{0}\" sudah ada dalam database.\nSilahkan input dengan Kode yang berbeda", txtCode.Text.Trim()));
                    txtCode.Focus();
                    return false;
                }
            }
            //else if (txtDesc.Text.Length == 0) return false;
            //else if (txtNote.Text..cou == 0) return false;
            return true;
        }

        public void Save()
        {
            int result = -1;
            if (ID > 0)
            {
                result = CustomerItem.Update(ID, txtCode.Text, txtName.Text, txtAddress.Text, txtPhone.Text, Utilities.Username);
            }
            else
            {
                result = CustomerItem.Insert(txtName.Text, txtAddress.Text, txtPhone.Text, Utilities.Username);
            }
            if (result > 0)
            {

                if (ID > 0)
                    Log.Update(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(new Customer() { ID = ID, FullName = txtName.Text, Address = txtAddress.Text, Phone = txtPhone.Text })));
                else Log.Insert(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(new Customer() { ID = ID, FullName = txtName.Text, Address = txtAddress.Text, Phone = txtPhone.Text })));

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void frmCustomer_Load(object sender, EventArgs e)
        {
            Customer prd = CustomerItem.GetByID(ID);
            if (prd != null)
            {
                txtCode.Enabled = true;
                txtCode.Text = prd.Code;
                txtAddress.Text = prd.Address;
                txtName.Text = prd.FullName;
                txtPhone.Text = prd.Phone;
            }
            else
            {
                txtCode.Text = "Auto Generate";
                txtCode.Enabled = false;
            }
        }

    }
}
