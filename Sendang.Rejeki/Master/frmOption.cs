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
using Newtonsoft.Json;
using DataLayer;

namespace Sendang.Rejeki.Master
{
    public partial class frmOption : Form, ITransButton
    {
        public frmOption()
        {
            InitializeComponent();
        }

        public bool IsValid()
        {
            if (txtName.Text.Length == 0)
            {
                Utilities.ShowValidation("Name tidak boleh kosong");
                txtName.Focus();
                return false;
            }
            else if (txtCode.Text.Length == 0)
            {
                Utilities.ShowValidation("Kode tidak boleh kosong");
                txtCode.Focus();
                return false;
            }
            else if (txtDesc.Text.Length == 0)
            {
                Utilities.ShowValidation("Deskripsi tidak boleh kosong");
                txtDesc.Focus();
                return false;
            }
            return true;
        }

        public void Save()
        {
            if (IsValid())
            {
                Options item = new Options()
                {
                    Name = txtName.Text.Trim(),
                    ValueMember = txtCode.Text.Trim(),
                    DisplayMember = txtDesc.Text.Trim(),
                    CreatedBy = Utilities.Username

                };
                var result = OptionItem.Save(item);
                if (result != null)
                {
                    Log.Update(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(result)));
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            Options prd = OptionItem.GetOptionsByKey(KeyName, KeyValueMember);
            if (prd != null)
            {
                txtName.Text = prd.Name;
                txtCode.Text = prd.ValueMember;
                txtDesc.Text = prd.DisplayMember;
                txtName.Enabled = false;
                txtCode.Enabled = false;
            }
        }

        public string KeyName { get; set; }

        public string KeyValueMember { get; set; }
    }
}
