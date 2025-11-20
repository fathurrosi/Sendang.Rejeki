namespace Sendang.Rejeki.Lookup
{
    partial class frmSupplierLookup
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
            this.ctlMaster1 = new Sendang.Rejeki.Control.ctlTransButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grid = new System.Windows.Forms.DataGridView();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctlFooter1 = new Sendang.Rejeki.Control.ctlFooter();
            this.ctlHeader1 = new Sendang.Rejeki.Control.ctlHeader();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlMaster1
            // 
            this.ctlMaster1.CancelButtonEnabled = true;
            this.ctlMaster1.CancelButtonText = null;
            this.ctlMaster1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlMaster1.IsLookup = true;
            this.ctlMaster1.Location = new System.Drawing.Point(0, 400);
            this.ctlMaster1.Name = "ctlMaster1";
            this.ctlMaster1.SaveButtonEnabled = true;
            this.ctlMaster1.SaveButtonText = null;
            this.ctlMaster1.Size = new System.Drawing.Size(828, 33);
            this.ctlMaster1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(828, 400);
            this.panel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.grid);
            this.panel1.Controls.Add(this.ctlFooter1);
            this.panel1.Controls.Add(this.ctlHeader1);
            this.panel1.Location = new System.Drawing.Point(17, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(796, 391);
            this.panel1.TabIndex = 14;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToOrderColumns = true;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.colName,
            this.CompanyAddress});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 24);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(796, 341);
            this.grid.TabIndex = 1;
            this.grid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grid_CellDoubleClick);
            //this.grid.Enter += new System.EventHandler(this.grid_Enter);
            // 
            // Code
            // 
            this.Code.DataPropertyName = "Code";
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Supplier Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 150;
            // 
            // CompanyAddress
            // 
            this.CompanyAddress.DataPropertyName = "Address";
            this.CompanyAddress.HeaderText = "Address";
            this.CompanyAddress.Name = "CompanyAddress";
            this.CompanyAddress.ReadOnly = true;
            this.CompanyAddress.Width = 300;
            // 
            // ctlFooter1
            // 
            this.ctlFooter1.BackColor = System.Drawing.SystemColors.Control;
            this.ctlFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlFooter1.Location = new System.Drawing.Point(0, 365);
            this.ctlFooter1.Name = "ctlFooter1";
            this.ctlFooter1.Offset = 0;
            this.ctlFooter1.PageIndex = 1;
            this.ctlFooter1.Size = new System.Drawing.Size(796, 26);
            this.ctlFooter1.TabIndex = 2;
            this.ctlFooter1.TotalRows = 0;
            // 
            // ctlHeader1
            // 
            this.ctlHeader1.DeleteButton = null;
            this.ctlHeader1.DeleteButtonEnabled = false;
            this.ctlHeader1.DeleteButtonText = "Delete";
            this.ctlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlHeader1.EditButton = null;
            this.ctlHeader1.EditButtonEnabled = false;
            this.ctlHeader1.EditButtonText = "Edit";
            this.ctlHeader1.IsLookup = true;
            this.ctlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctlHeader1.Name = "ctlHeader1";
            this.ctlHeader1.NewButton = null;
            this.ctlHeader1.NewButtonEnabled = false;
            this.ctlHeader1.NewButtonText = "Add";
            this.ctlHeader1.Size = new System.Drawing.Size(796, 24);
            this.ctlHeader1.TabIndex = 0;
            this.ctlHeader1.TextToSearch = "";
            // 
            // frmSupplierLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 433);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ctlMaster1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmSupplierLookup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Supplier Lookup";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmSupplierLookup_Paint);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Control.ctlTransButton ctlMaster1;
        private System.Windows.Forms.Panel panel2;
        private Control.ctlHeader ctlHeader1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyAddress;
        private Control.ctlFooter ctlFooter1;
    }
}