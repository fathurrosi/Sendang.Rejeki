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
using System.IO;

namespace Sendang.Rejeki.Control
{
    public partial class ctlHeader : UserControl
    {
        public string NewButtonText
        {
            get { return btnAdd.Text; }
            set { btnAdd.Text = string.Format("{0}", value).Length > 0 ? value : "Add"; }
        }

        public string DeleteButtonText
        {
            get { return btnDelete.Text; }
            set { btnDelete.Text = string.Format("{0}", value).Length > 0 ? value : "Delete"; }
        }

        public string EditButtonText
        {
            get { return btnEdit.Text; }
            set
            {
                btnEdit.Image = global::Sendang.Rejeki.Properties.Resources.pencil;
                btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
                if (string.Format("{0}", value).ToLower() == "view")
                {
                    btnEdit.Image = global::Sendang.Rejeki.Properties.Resources.application_view_list;
                }
                btnEdit.Text = string.Format("{0}", value).Length > 0 ? value : "Edit";
            }
        }

        public bool NewButtonEnabled
        {
            get { return btnAdd.Enabled; }
            set { btnAdd.Enabled = value; }
        }

        public bool DeleteButtonEnabled
        {
            get { return btnDelete.Enabled; }
            set { btnDelete.Enabled = value; }
        }

        public bool EditButtonEnabled
        {
            get { return btnEdit.Enabled; }
            set { btnEdit.Enabled = value; }
        }


        public Button DeleteButton { get; set; }
        public Button EditButton { get; set; }
        public Button NewButton { get; set; }
        private bool _IsLookup;

        public bool IsLookup
        {
            get { return _IsLookup; }
            set
            {
                _IsLookup = value;
                //btnAdd.Enabled = !value;
                //btnEdit.Enabled = !value;
                //btnDelete.Enabled = !value;
            }
        }

        public ctlHeader()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {

#if (!DEBUG)
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
                List<Previllage> selectedPrevillages = previllages.Where(t => t.MenuID == currentManu.ID && roles.Contains(t.RoleID)).ToList();
                allowRead = selectedPrevillages.Where(t => t.AllowRead).Count() > 0;
                allowCreate = selectedPrevillages.Where(t => t.AllowCreate).Count() > 0;
                allowUpdate = selectedPrevillages.Where(t => t.AllowUpdate).Count() > 0;
                allowDelete = selectedPrevillages.Where(t => t.AllowDelete).Count() > 0;
                allowPrint = selectedPrevillages.Where(t => t.AllowPrint).Count() > 0;

                btnAdd.Enabled = allowCreate;
                btnDelete.Enabled = allowDelete;
                btnPrint.Enabled = allowPrint;
                btnEdit.Enabled = allowUpdate || allowCreate;
                if (!btnEdit.Enabled && allowRead)
                {
                    EditButtonText = "View";
                    btnEdit.Enabled = true;
                }

                // permanently disable
                if (currentManu.Code == "stock")
                {
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
            else
            {
                btnAdd.Enabled = allowCreate;
                btnDelete.Enabled = allowDelete;
                btnPrint.Enabled = allowPrint;
                btnEdit.Enabled = allowUpdate || allowCreate;
            }
#endif

            base.OnLoad(e);
        }

        public string TextToSearch
        {
            get { return txtSearch.Text; }
            set { value = txtSearch.Text; }
        }

        private Form GetActiveForm(System.Windows.Forms.Control ctl)
        {
            if (ctl is Form)
                return (Form)ctl;
            else
                return GetActiveForm(ctl.Parent);
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ToolStripButton btn = (ToolStripButton)sender;
            Execute(btn.Name);
        }

        void Execute(string name)
        {
            Form form = GetActiveForm(this.Parent);
            IMasterHeader header = (IMasterHeader)form;
            if (header != null)
            {
                switch (name)
                {
                    case "btnAdd":
                        header.Add();
                        break;
                    case "btnEdit":
                        header.Edit();
                        break;
                    case "btnSearch":
                        header.Search();
                        break;
                    case "btnDelete":
                        header.Delete();
                        break;
                    case "btnPrint":
                        header.Print();
                        break;
                    default:
                        header.Search();
                        break;
                }
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            Execute(string.Empty);
            //}
        }
    }
}
