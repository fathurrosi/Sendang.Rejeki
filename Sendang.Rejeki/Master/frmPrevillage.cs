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
using DataObject;


namespace Sendang.Rejeki.Master
{
    public partial class frmPrevillage : Form, ITransButton
    {
        public DataObject.Menu CurrentSelectedMenu { get; set; }

        //List<Previllage> PrevillageList = new List<Previllage>();
        private List<Previllage> _PrevillageList;

        public List<Previllage> PrevillageList
        {
            get
            {
                if (_PrevillageList == null)
                {
                    //PrevillageList = PrevillageItem.GetByRoleID(SelectedRoleID);
                    _PrevillageList = PrevillageItem.GetAll();
                }
                else if (_PrevillageList.Count == 0)
                {
                    //PrevillageList = PrevillageItem.GetByRoleID(SelectedRoleID);
                    _PrevillageList = PrevillageItem.GetAll();
                }
                return _PrevillageList;
            }
            set { _PrevillageList = value; }
        }

        public enum RoleAccess
        {
            AllowNone = 0,
            AllowRead = 1,
            AllowCreate = 2,
            AllowUpdate = 3,
            AllowDelete = 4,
            AllowPrint = 5
        }

        public class ItemHelper
        {
            public ItemHelper(RoleAccess roleAccess, int roleID, int menuID, bool check)
            {
                RoleAccess = roleAccess;
                RoleID = roleID;
                MenuID = menuID;
                Check = check;
            }
            public int RoleID { get; set; }
            public int MenuID { get; set; }
            public RoleAccess RoleAccess { get; set; }
            public bool Check { get; set; }
            public override string ToString()
            {
                return RoleAccess.ToString();
            }
        }

        public frmPrevillage()
        {
            InitializeComponent();
        }

        public int SelectedRoleID
        {
            get
            {
                int roleID = 0;
                int.TryParse(cboRole.SelectedValue.ToString(), out roleID);
                return roleID;
            }
        }

        private void frmPrevillage_Load(object sender, EventArgs e)
        {
            cboRole.ValueMember = "ID";
            cboRole.DisplayMember = "Name";
            cboRole.DataSource = RoleItem.GetRoles();
            cboRole.SelectedIndex = 0;

            //int roleId = 0;
            //int.TryParse(cboRole.SelectedValue.ToString(), out roleId);

            //LoadData(roleId);
        }

        void LoadData(int roleId)
        {
            PrevillageList = null;
            tvPrevillage.Nodes.Clear();
            List<DataObject.Menu> list = DataLayer.MenuItem.GetMenus();
            List<DataObject.Menu> parentList = list.Where(t => t.ParentID == 0).OrderBy(t => t.Sequence).ToList();
            TreeNode root = new TreeNode();
            root.Text = "Root";
            root.Name = "Root";

            parentList.ForEach(t =>
            {
                TreeNode parent = new TreeNode();
                parent.Name = t.ID.ToString();
                parent.Text = t.Name;
                parent.Tag = t;
                parent.ToolTipText = t.Description;
                Previllage item = PrevillageList.Where(p => p.MenuID == t.ID && p.RoleID == roleId).FirstOrDefault();
                parent.Checked = (item != null) ? item.AllowRead : false;

                if (list.Where(t1 => t1.ParentID == t.ID).Count() > 0)
                {
                    LoadMenu(roleId, parent, list, t.ID, PrevillageList);
                }

                root.Nodes.Add(parent);
            });

            tvPrevillage.Nodes.Add(root);
            tvPrevillage.ExpandAll();
        }

        void LoadMenu(int roleId, TreeNode tMenuItem, List<DataObject.Menu> list, int parentID, List<Previllage> prevList)
        {
            list.Where(t => t.ParentID == parentID).ToList().ForEach(t =>
            {
                if (t.ID == 50)
                {
                    string test = t.ID.ToString();
                }
                TreeNode child = new TreeNode();
                child.Name = t.Code;
                child.Text = t.Name;
                child.Tag = t;
                child.ToolTipText = t.Description;

                Previllage item = prevList.Where(p => p.MenuID == t.ID && p.RoleID == roleId).FirstOrDefault();
                child.Checked = (item != null) ? item.AllowRead : false;

                if (list.Where(t1 => t1.ParentID == t.ID).Count() > 0)
                    LoadMenu(roleId, child, list, t.ID, prevList);
                tMenuItem.Nodes.Add(child);
            });
        }

        void SetRoleAccess(int roleID
            , int menuID
            , bool readAccess
            , bool createAccess
            , bool updateAccess
            , bool deleteAccess
            )
        {
            Previllage item = null;
            if (PrevillageList.Where(t => t.RoleID == roleID && t.MenuID == menuID).Count() == 0)
            {
                item = new Previllage();
                item.MenuID = menuID;
                item.RoleID = roleID;
                //if (readAccess == RoleAccess.AllowRead) item.AllowRead = isChecked;
                //else if (readAccess == RoleAccess.AllowCreate) item.AllowCreate = isChecked;
                //else if (readAccess == RoleAccess.AllowDelete) item.AllowDelete = isChecked;
                //else if (readAccess == RoleAccess.AllowPrint) item.AllowPrint = isChecked;
                //else if (readAccess == RoleAccess.AllowUpdate) item.AllowUpdate = isChecked;

                item.AllowRead = readAccess;
                item.AllowCreate = createAccess;
                item.AllowDelete = deleteAccess;
                item.AllowUpdate = updateAccess;
                item.AllowPrint = true;

                PrevillageList.Add(item);

                //checkBoxRead.Checked = readAccess;
                //checkBoxCreate.Checked = createAccess;
                //checkBoxUpdate.Checked = updateAccess;
                //checkBoxDelete.Checked = deleteAccess;
            }
            else
            {
                for (int i = 0; i < PrevillageList.Count; i++)
                {
                    item = PrevillageList[i];
                    if (item.MenuID == menuID && item.RoleID == roleID)
                    {

                        PrevillageList[i].AllowRead = readAccess;
                        PrevillageList[i].AllowCreate = createAccess;
                        PrevillageList[i].AllowDelete = deleteAccess;
                        PrevillageList[i].AllowUpdate = updateAccess;
                        PrevillageList[i].AllowPrint = true;

                        //if (readAccess == RoleAccess.AllowRead) PrevillageList[i].AllowRead = isChecked;
                        //else if (readAccess == RoleAccess.AllowCreate) PrevillageList[i].AllowCreate = isChecked;
                        //else if (readAccess == RoleAccess.AllowDelete) PrevillageList[i].AllowDelete = isChecked;
                        //else if (readAccess == RoleAccess.AllowPrint) PrevillageList[i].AllowPrint = isChecked;
                        //else if (readAccess == RoleAccess.AllowUpdate) PrevillageList[i].AllowUpdate = isChecked;

                        //checkBoxRead.Checked = readAccess;
                        //checkBoxCreate.Checked = createAccess;
                        //checkBoxUpdate.Checked = updateAccess;
                        //checkBoxDelete.Checked = deleteAccess;

                        break;
                    }
                }
            }
        }

        void CheckAllChildNodes(TreeNode tNode, bool nodeChecked)
        {
            foreach (TreeNode node in tNode.Nodes)
            {
                node.Checked = nodeChecked;
                DataObject.Menu menu = (DataObject.Menu)node.Tag;
                if (menu != null)
                {
                    SetRoleAccess(SelectedRoleID
                        , menu.ID
                        , node.Checked
                        , checkBoxCreate.Checked
                        , checkBoxUpdate.Checked
                        , checkBoxDelete.Checked);
                }

                if (node.Nodes.Count > 0)
                    CheckAllChildNodes(node, nodeChecked);
            }
        }

        public bool IsValid()
        {
            if (cboRole.SelectedIndex == -1)
            {
                cboRole.Focus();
                return false;
            }
            return true;
        }

        public void Save()
        {
            try
            {
                PrevillageItem.Update(PrevillageList, SelectedRoleID);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                Utilities.ShowInformation("Data saved!");
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                Utilities.ShowInformation(ex.Message);
                this.DialogResult = System.Windows.Forms.DialogResult.Retry;
            }
        }

        List<Previllage> IterateTreeNode(TreeNode node)
        {
            List<Previllage> pList = new List<Previllage>();
            foreach (TreeNode childNode in node.Nodes)
            {
                if (childNode.Checked)
                {
                    DataObject.Menu menu = (DataObject.Menu)childNode.Tag;
                    Previllage p = new Previllage();
                    p.MenuID = menu.ID;
                    p.RoleID = SelectedRoleID;
                    pList.Add(p);
                }

                if (childNode.Nodes.Count > 0)
                {
                    List<Previllage> result = IterateTreeNode(childNode);
                    pList.AddRange(result);
                }
            }
            return pList;
        }

        public void Cancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void tv_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                CurrentSelectedMenu = (DataObject.Menu)e.Node.Tag;
                if (CurrentSelectedMenu != null)
                {
                    int roleID = 0;
                    int.TryParse(string.Format("{0}", cboRole.SelectedValue), out roleID);
                    textBoxMenu.Text = e.Node.Checked ? CurrentSelectedMenu.Name : "";
                    textBoxMenuChecked.Text = e.Node.Checked.ToString().ToLower();
                    textBoxMenuID.Text = e.Node.Checked ? CurrentSelectedMenu.ID.ToString() : "";

                    if (e.Node.Nodes.Count > 0)
                    {
                        checkBoxRead.Checked = e.Node.Checked;
                        checkBoxCreate.Checked = false;
                        checkBoxUpdate.Checked = false;
                        checkBoxDelete.Checked = false;
                    }
                    else
                    {
                        checkBoxRead.Checked = e.Node.Checked;
                        checkBoxCreate.Checked = e.Node.Checked;
                        checkBoxUpdate.Checked = e.Node.Checked;
                        checkBoxDelete.Checked = e.Node.Checked;
                    }
                    SetRoleAccess(SelectedRoleID
                     , CurrentSelectedMenu.ID
                     , e.Node.Checked
                     , checkBoxCreate.Checked
                     , checkBoxUpdate.Checked
                     , checkBoxDelete.Checked);
                }

                if (e.Node.Nodes.Count > 0)
                {
                    CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
        }

        private void tvPrevillage_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var temp = e.Action;
            TreeNode node = e.Node;
            CurrentSelectedMenu = (DataObject.Menu)node.Tag;
            if (CurrentSelectedMenu == null) return;

            int roleID = 0;
            int.TryParse(string.Format("{0}", cboRole.SelectedValue), out roleID);
            textBoxMenu.Text = CurrentSelectedMenu.Name;
            textBoxMenuChecked.Text = node.Checked.ToString().ToLower();
            textBoxMenuID.Text = CurrentSelectedMenu.ID.ToString();
            //List<Previllage> list =PrevillageList.Where(t=> t.RoleID == roleID && t.MenuID == CurrentSelectedMenu.ID);
            Previllage item = PrevillageList.Where(t => t.RoleID == roleID && t.MenuID == CurrentSelectedMenu.ID).FirstOrDefault();

            bool allowRead = (item == null) ? false : item.AllowRead;
            bool allowCreate = (item == null) ? false : item.AllowCreate;
            bool allowUpdate = (item == null) ? false : item.AllowUpdate;
            bool allowDelete = (item == null) ? false : item.AllowDelete;
            bool allowPrint = (item == null) ? false : item.AllowPrint;

            checkBoxRead.Checked = allowRead;
            checkBoxCreate.Checked = allowCreate;
            checkBoxUpdate.Checked = allowUpdate;
            checkBoxDelete.Checked = allowDelete;

            if (e.Node.Nodes.Count > 0)
            {
                checkBoxRead.Checked = allowRead;
                checkBoxCreate.Checked = false;
                checkBoxUpdate.Checked = false;
                checkBoxDelete.Checked = false;
            }
            else
            {
                checkBoxRead.Checked = allowRead;
                checkBoxCreate.Checked = allowCreate;
                checkBoxUpdate.Checked = allowUpdate;
                checkBoxDelete.Checked = allowDelete;
            }

            //clbPrevillage.Items.Add(new ItemHelper(RoleAccess.AllowCreate, roleID, m.ID, allowCreate), allowCreate);
            //clbPrevillage.Items.Add(new ItemHelper(RoleAccess.AllowUpdate, roleID, m.ID, allowUpdate), allowUpdate);
            //clbPrevillage.Items.Add(new ItemHelper(RoleAccess.AllowDelete, roleID, m.ID, allowDelete), allowDelete);
            //clbPrevillage.Items.Add(new ItemHelper(RoleAccess.AllowPrint, roleID, m.ID, allowPrint), allowPrint);
        }

        private void clbPrevillage_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //ItemHelper itemHelper = (ItemHelper)clbPrevillage.SelectedItem;
            //if (itemHelper != null)
            //{
            //    bool check = e.NewValue.ToString().ToLower() == "checked" ? true : false;
            //    SetRoleAccess(itemHelper.RoleID, itemHelper.MenuID, check, itemHelper.RoleAccess);
            //}
        }

        private void cboRole_SelectedIndexChanged(object sender, EventArgs e)
        {

            textBoxMenu.Text = "";
            textBoxMenuChecked.Text = "";
            textBoxMenuID.Text = "";


            checkBoxRead.Checked = false;
            checkBoxCreate.Checked = false;
            checkBoxUpdate.Checked = false;
            checkBoxDelete.Checked = false;

            int roleId = 0;
            ComboBox cbo = sender as ComboBox;
            int.TryParse(cbo.SelectedValue.ToString(), out roleId);
            LoadData(roleId);
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            //MessageBox.Show(checkBox.Name + '_' + CurrentSelectedMenu.Name);
            int menuId = 0;
            bool menuChecked = false;
            menuChecked = textBoxMenuChecked.Text == "true";
            //textBoxMenu.Text ;
            int.TryParse(textBoxMenuID.Text, out menuId);

            SetRoleAccess(SelectedRoleID
                      , menuId
                      , menuChecked
                      , checkBoxCreate.Checked
                      , checkBoxUpdate.Checked
                      , checkBoxDelete.Checked);
        }


    }
}
