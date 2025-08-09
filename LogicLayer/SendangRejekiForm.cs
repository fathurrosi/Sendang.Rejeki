using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataObject;
using DataLayer;

namespace LogicLayer
{
    public class SendangRejekiForm : System.Windows.Forms.Form
    {
        public bool AllowRead { get; set; }
        public bool AllowCreate { get; set; }
        public bool AllowUpdate { get; set; }
        public bool AllowDelete { get; set; }
        public bool AllowPrint { get; set; }

        //private Form GetActiveForm(System.Windows.Forms.Control ctl)
        //{
        //    if (ctl is Form)
        //        return (Form)ctl;
        //    else
        //        return GetActiveForm(ctl.Parent);
        //}

        protected override void OnLoad(EventArgs e)
        {

            User user = Utilities.CurrentUser;
            List<int> roles = user.Roles.Select(t => t.ID).ToList();
            List<Previllage> previllages = PrevillageItem.GetAll();

            System.Windows.Forms.Form form = this;// GetActiveForm(this.Parent);
            if (form != null)
            {
                DataObject.Menu currentManu = form.Tag as DataObject.Menu;
                List<Previllage> selectedPrevillages = previllages.Where(t => t.MenuID == currentManu.ID && roles.Contains(t.RoleID)).ToList();
                AllowRead = selectedPrevillages.Where(t => t.AllowRead).Count() > 0;
                AllowCreate = selectedPrevillages.Where(t => t.AllowCreate).Count() > 0;
                AllowUpdate = selectedPrevillages.Where(t => t.AllowUpdate).Count() > 0;
                AllowDelete = selectedPrevillages.Where(t => t.AllowDelete).Count() > 0;
                AllowPrint = selectedPrevillages.Where(t => t.AllowPrint).Count() > 0;
            }

            //btnAdd.Enabled = allowCreate;
            //btnDelete.Enabled = allowDelete;
            //btnPrint.Enabled = allowPrint;
            //btnEdit.Enabled = allowRead || allowCreate;
            //if (!allowCreate && allowRead)
            //    EditButtonText = "View";

            base.OnLoad(e);
        }
    }
}

