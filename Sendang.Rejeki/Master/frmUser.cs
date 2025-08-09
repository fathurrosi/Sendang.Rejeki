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
using Newtonsoft.Json;


namespace Sendang.Rejeki.Master
{
    public partial class frmUser : Form, ITransButton
    {
        public string SelectedUsername { get; set; }
        public frmUser()
        {
            InitializeComponent();
        }

        public bool IsValid()
        {
            if (txtUsername.Text.Length == 0)
            {
                Utilities.ShowValidation("Unknown Username!");
                txtUsername.Focus();
                return false;
            }
            else if (txtPassword.Text.Length == 0)
            {
                txtPassword.Focus();
                Utilities.ShowValidation("Password is empty!");
                return false;
            }
            else if (txtPassword.Text != txtConfirmPassword.Text)
            {
                txtConfirmPassword.Focus();
                Utilities.ShowValidation("Password does not match!");
                return false;
            }
            else if (checkRole.CheckedItems.Count == 0)
            {
                Utilities.ShowValidation("Please select at leat one role!");
                checkRole.Focus();
                return false;
            }
            else
            {
                //User user = Utilities.CurrentUser;
                //if (user != null)
                //{
                //User currentUser = UserItem.GetUser(user.Username);
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    Utilities.ShowValidation("Password does not match!");
                    txtConfirmPassword.Focus();
                    return false;
                }
                //}
            }
            return true;
        }

        public void Save()
        {
            try
            {
                int result = -1;
                List<Role> list = new List<Role>();
                foreach (object item in checkRole.CheckedItems)
                {
                    list.Add((Role)item);
                }
                if (string.Format("{0}", SelectedUsername).Length == 0)
                {
                    result = UserItem.Insert(txtUsername.Text, Security.Encrypt(txtPassword.Text.Trim()), list);
                }
                else result = UserItem.Update(SelectedUsername, Security.Encrypt(txtPassword.Text.Trim()), list);
                if (result > 0)
                {
                    if (string.Format("{0}", SelectedUsername).Length == 0)
                        Log.Insert(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(new User() { Username = txtUsername.Text })));
                    else Log.Update(string.Format("{0}-{1}", this.Text, JsonConvert.SerializeObject(new User() { Username = txtUsername.Text })));


                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else this.DialogResult = System.Windows.Forms.DialogResult.Retry;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Log.Error(ex.ToString());
                this.DialogResult = System.Windows.Forms.DialogResult.Retry;
            }
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            List<Role> roleList = RoleItem.GetRoles();
            roleList.ForEach(t =>
            {
                checkRole.Items.Add(t);
            });

            if (string.Format("{0}", SelectedUsername).Length > 0)
            {
                User user = UserItem.GetUser(SelectedUsername);
                if (user != null)
                {
                    txtUsername.Enabled = false;
                    txtUsername.Text = user.Username;
                    txtPassword.Text = Security.Decrypt(user.Password);
                    int count = checkRole.Items.Count;
                    for (int i = 0; i < count; i++)
                    {
                        Role role = (Role)checkRole.Items[i];
                        if (user.Roles != null &&
                            user.Roles.Where(t => t.ID == role.ID).Count() > 0)
                        {
                            checkRole.SetItemChecked(i, true);
                        }
                    }
                }
            }
        }
    }
}
