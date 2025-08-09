using Sendang.Rejeki.Control;
namespace Sendang.Rejeki.Master
{
    partial class frmPrevillage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Node3");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Node2", new System.Windows.Forms.TreeNode[] {
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Node0", new System.Windows.Forms.TreeNode[] {
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Node12");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Node4", new System.Windows.Forms.TreeNode[] {
            treeNode17});
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Node8");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Node5", new System.Windows.Forms.TreeNode[] {
            treeNode19});
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Node9");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Node10");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Node6", new System.Windows.Forms.TreeNode[] {
            treeNode21,
            treeNode22});
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Node11");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Node7", new System.Windows.Forms.TreeNode[] {
            treeNode24});
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Node1", new System.Windows.Forms.TreeNode[] {
            treeNode18,
            treeNode20,
            treeNode23,
            treeNode25});
            this.label4 = new System.Windows.Forms.Label();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvPrevillage = new System.Windows.Forms.TreeView();
            this.checkBoxDelete = new System.Windows.Forms.CheckBox();
            this.checkBoxUpdate = new System.Windows.Forms.CheckBox();
            this.checkBoxCreate = new System.Windows.Forms.CheckBox();
            this.checkBoxRead = new System.Windows.Forms.CheckBox();
            this.ctlTransButton1 = new Sendang.Rejeki.Control.ctlTransButton();
            this.textBoxMenu = new System.Windows.Forms.TextBox();
            this.textBoxMenuID = new System.Windows.Forms.TextBox();
            this.textBoxMenuChecked = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Role Name :";
            // 
            // cboRole
            // 
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.FormattingEnabled = true;
            this.cboRole.Location = new System.Drawing.Point(79, 8);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(165, 21);
            this.cboRole.TabIndex = 8;
            this.cboRole.SelectedIndexChanged += new System.EventHandler(this.cboRole_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cboRole);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(523, 38);
            this.panel2.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(523, 257);
            this.panel1.TabIndex = 13;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvPrevillage);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBoxMenuChecked);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxMenuID);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxMenu);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxDelete);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxUpdate);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxCreate);
            this.splitContainer1.Panel2.Controls.Add(this.checkBoxRead);
            this.splitContainer1.Size = new System.Drawing.Size(523, 257);
            this.splitContainer1.SplitterDistance = 174;
            this.splitContainer1.TabIndex = 3;
            // 
            // tvPrevillage
            // 
            this.tvPrevillage.CheckBoxes = true;
            this.tvPrevillage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvPrevillage.FullRowSelect = true;
            this.tvPrevillage.HotTracking = true;
            this.tvPrevillage.Location = new System.Drawing.Point(0, 0);
            this.tvPrevillage.Name = "tvPrevillage";
            treeNode14.Name = "Node3";
            treeNode14.Text = "Node3";
            treeNode15.Name = "Node2";
            treeNode15.Text = "Node2";
            treeNode16.Name = "Node0";
            treeNode16.Text = "Node0";
            treeNode17.Name = "Node12";
            treeNode17.Text = "Node12";
            treeNode18.Name = "Node4";
            treeNode18.Text = "Node4";
            treeNode19.Name = "Node8";
            treeNode19.Text = "Node8";
            treeNode20.Name = "Node5";
            treeNode20.Text = "Node5";
            treeNode21.Name = "Node9";
            treeNode21.Text = "Node9";
            treeNode22.Name = "Node10";
            treeNode22.Text = "Node10";
            treeNode23.Name = "Node6";
            treeNode23.Text = "Node6";
            treeNode24.Name = "Node11";
            treeNode24.Text = "Node11";
            treeNode25.Name = "Node7";
            treeNode25.Text = "Node7";
            treeNode26.Name = "Node1";
            treeNode26.Text = "Node1";
            this.tvPrevillage.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode26});
            this.tvPrevillage.ShowNodeToolTips = true;
            this.tvPrevillage.Size = new System.Drawing.Size(174, 257);
            this.tvPrevillage.TabIndex = 2;
            this.tvPrevillage.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tv_AfterCheck);
            this.tvPrevillage.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPrevillage_AfterSelect);
            // 
            // checkBoxDelete
            // 
            this.checkBoxDelete.AutoSize = true;
            this.checkBoxDelete.Location = new System.Drawing.Point(18, 101);
            this.checkBoxDelete.Name = "checkBoxDelete";
            this.checkBoxDelete.Size = new System.Drawing.Size(85, 17);
            this.checkBoxDelete.TabIndex = 3;
            this.checkBoxDelete.Text = "Allow Delete";
            this.checkBoxDelete.UseVisualStyleBackColor = true;
            this.checkBoxDelete.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBoxUpdate
            // 
            this.checkBoxUpdate.AutoSize = true;
            this.checkBoxUpdate.Location = new System.Drawing.Point(18, 78);
            this.checkBoxUpdate.Name = "checkBoxUpdate";
            this.checkBoxUpdate.Size = new System.Drawing.Size(89, 17);
            this.checkBoxUpdate.TabIndex = 2;
            this.checkBoxUpdate.Text = "Allow Update";
            this.checkBoxUpdate.UseVisualStyleBackColor = true;
            this.checkBoxUpdate.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBoxCreate
            // 
            this.checkBoxCreate.AutoSize = true;
            this.checkBoxCreate.Location = new System.Drawing.Point(18, 55);
            this.checkBoxCreate.Name = "checkBoxCreate";
            this.checkBoxCreate.Size = new System.Drawing.Size(85, 17);
            this.checkBoxCreate.TabIndex = 1;
            this.checkBoxCreate.Text = "Allow Create";
            this.checkBoxCreate.UseVisualStyleBackColor = true;
            this.checkBoxCreate.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // checkBoxRead
            // 
            this.checkBoxRead.AutoSize = true;
            this.checkBoxRead.Enabled = false;
            this.checkBoxRead.Location = new System.Drawing.Point(18, 32);
            this.checkBoxRead.Name = "checkBoxRead";
            this.checkBoxRead.Size = new System.Drawing.Size(80, 17);
            this.checkBoxRead.TabIndex = 0;
            this.checkBoxRead.Text = "Allow Read";
            this.checkBoxRead.UseVisualStyleBackColor = true;
            this.checkBoxRead.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // ctlTransButton1
            // 
            this.ctlTransButton1.CancelButtonEnabled = true;
            this.ctlTransButton1.CancelButtonText = null;
            this.ctlTransButton1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlTransButton1.IsLookup = false;
            this.ctlTransButton1.Location = new System.Drawing.Point(0, 295);
            this.ctlTransButton1.Name = "ctlTransButton1";
            this.ctlTransButton1.SaveButtonEnabled = true;
            this.ctlTransButton1.SaveButtonText = "";
            this.ctlTransButton1.Size = new System.Drawing.Size(523, 45);
            this.ctlTransButton1.TabIndex = 11;
            // 
            // textBoxMenu
            // 
            this.textBoxMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMenu.BackColor = System.Drawing.SystemColors.HighlightText;
            this.textBoxMenu.Location = new System.Drawing.Point(18, 6);
            this.textBoxMenu.Name = "textBoxMenu";
            this.textBoxMenu.ReadOnly = true;
            this.textBoxMenu.Size = new System.Drawing.Size(315, 20);
            this.textBoxMenu.TabIndex = 4;
            // 
            // textBoxMenuID
            // 
            this.textBoxMenuID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMenuID.BackColor = System.Drawing.SystemColors.HighlightText;
            this.textBoxMenuID.Location = new System.Drawing.Point(18, 222);
            this.textBoxMenuID.Name = "textBoxMenuID";
            this.textBoxMenuID.ReadOnly = true;
            this.textBoxMenuID.Size = new System.Drawing.Size(315, 20);
            this.textBoxMenuID.TabIndex = 5;
            this.textBoxMenuID.Visible = false;
            // 
            // textBoxMenuChecked
            // 
            this.textBoxMenuChecked.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMenuChecked.BackColor = System.Drawing.SystemColors.HighlightText;
            this.textBoxMenuChecked.Location = new System.Drawing.Point(18, 196);
            this.textBoxMenuChecked.Name = "textBoxMenuChecked";
            this.textBoxMenuChecked.ReadOnly = true;
            this.textBoxMenuChecked.Size = new System.Drawing.Size(315, 20);
            this.textBoxMenuChecked.TabIndex = 6;
            this.textBoxMenuChecked.Visible = false;
            // 
            // frmPrevillage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 340);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ctlTransButton1);
            this.Name = "frmPrevillage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Previllage";
            this.Load += new System.EventHandler(this.frmPrevillage_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboRole;
        private ctlTransButton ctlTransButton1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView tvPrevillage;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox checkBoxDelete;
        private System.Windows.Forms.CheckBox checkBoxUpdate;
        private System.Windows.Forms.CheckBox checkBoxCreate;
        private System.Windows.Forms.CheckBox checkBoxRead;
        private System.Windows.Forms.TextBox textBoxMenu;
        private System.Windows.Forms.TextBox textBoxMenuID;
        private System.Windows.Forms.TextBox textBoxMenuChecked;
    }
}