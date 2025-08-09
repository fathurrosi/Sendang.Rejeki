using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LogicLayer;
using DataObject;
using DataLayer;

namespace Sendang.Rejeki.Control
{
    public partial class ctlTransButton : UserControl
    {
        private Form GetActiveForm(System.Windows.Forms.Control ctl)
        {
            if (ctl is Form)
                return (Form)ctl;
            else
                return GetActiveForm(ctl.Parent);
        }

        private bool _IsLookup = false;


        public bool IsLookup
        {
            get { return _IsLookup; }
            set { _IsLookup = value; }
        }

        private bool _SaveButtonEnabled = true;

        public bool SaveButtonEnabled
        {
            get { return _SaveButtonEnabled; }
            set
            {
                _SaveButtonEnabled = value;
                btnSave.Enabled = _SaveButtonEnabled;
            }
        }

        private bool _CancelButtonEnabled = true;

        public bool CancelButtonEnabled
        {
            get { return _CancelButtonEnabled; }
            set
            {
                _CancelButtonEnabled = value;
                btnCancel.Enabled = _CancelButtonEnabled;
            }
        }


        public ctlTransButton()
        {

            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ITransButton master = (ITransButton)this.Parent;
            if (master != null)
            {
                Button btn = (Button)sender;
                switch (btn.Name)
                {
                    case "btnSave":
                        if (master.IsValid())
                        {
                            master.Save();
                        }
                        break;
                    case "btnCancel":
                        master.Cancel();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ctlTransButton_Load(object sender, EventArgs e)
        {
            User user = Utilities.CurrentUser;
            List<int> roles = user.Roles.Select(t => t.ID).ToList();
            List<Previllage> previllages = PrevillageItem.GetAll();

            bool allowRead = false;
            bool allowCreate = false;
            bool allowUpdate = false;
            bool allowDelete = false;
            bool allowPrint = false;

            Form form = GetActiveForm(this.Parent);
            if (form != null)
            {
                DataObject.Menu currentManu = form.Tag as DataObject.Menu;
                if (currentManu != null)
                {
                    List<Previllage> selectedPrevillages = previllages.Where(t => t.MenuID == currentManu.ID && roles.Contains(t.RoleID)).ToList();
                    allowRead = selectedPrevillages.Where(t => t.AllowRead).Count() > 0;
                    allowCreate = selectedPrevillages.Where(t => t.AllowCreate).Count() > 0;
                    allowUpdate = selectedPrevillages.Where(t => t.AllowUpdate).Count() > 0;
                    allowDelete = selectedPrevillages.Where(t => t.AllowDelete).Count() > 0;
                    allowPrint = selectedPrevillages.Where(t => t.AllowPrint).Count() > 0;
                }
            }

            if (IsLookup)
            {
                btnSave.Text = "OK";
                btnSave.Enabled = true;
            }
            else if (allowCreate || allowUpdate)
            {
                btnSave.Text = "Save";
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Text = "Save";
                btnSave.Enabled = false;
                btnCancel.Text = "Close";
            }
            //if (!SaveButtonEnabled)
            //{
            //    btnSave.Enabled = false;
            //}
            //if (!CancelButtonEnabled)
            //{
            //    btnCancel.Enabled = false;
            //}
        }

        public string SaveButtonText { get; set; }
        public string CancelButtonText { get; set; }

    }
}
