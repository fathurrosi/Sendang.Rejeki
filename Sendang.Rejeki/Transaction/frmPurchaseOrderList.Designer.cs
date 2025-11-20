namespace Sendang.Rejeki.Transaction
{
    partial class frmPurchaseOrderList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ctlFooter1 = new Sendang.Rejeki.Control.ctlFooter();
            this.ctlHeader1 = new Sendang.Rejeki.Control.ctlHeader();
            this.grid = new System.Windows.Forms.DataGridView();
            this.colInvoiceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInvoiceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSupplierName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // ctlFooter1
            // 
            this.ctlFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlFooter1.Location = new System.Drawing.Point(0, 427);
            this.ctlFooter1.Name = "ctlFooter1";
            this.ctlFooter1.Offset = 0;
            this.ctlFooter1.PageIndex = 1;
            this.ctlFooter1.Size = new System.Drawing.Size(800, 23);
            this.ctlFooter1.TabIndex = 6;
            this.ctlFooter1.TotalRows = 0;
            // 
            // ctlHeader1
            // 
            this.ctlHeader1.DeleteButton = null;
            this.ctlHeader1.DeleteButtonEnabled = true;
            this.ctlHeader1.DeleteButtonText = "Delete";
            this.ctlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlHeader1.EditButton = null;
            this.ctlHeader1.EditButtonEnabled = true;
            this.ctlHeader1.EditButtonText = "Edit";
            this.ctlHeader1.IsLookup = false;
            this.ctlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctlHeader1.Name = "ctlHeader1";
            this.ctlHeader1.NewButton = null;
            this.ctlHeader1.NewButtonEnabled = true;
            this.ctlHeader1.NewButtonText = "Input Stock";
            this.ctlHeader1.Size = new System.Drawing.Size(800, 25);
            this.ctlHeader1.TabIndex = 7;
            this.ctlHeader1.TextToSearch = "";
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colInvoiceNo,
            this.colInvoiceDate,
            this.colSupplierName,
            this.colCustomerCode,
            this.colTotal,
            this.colStatus,
            this.colAction});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 25);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(800, 402);
            this.grid.TabIndex = 8;
            // 
            // colInvoiceNo
            // 
            this.colInvoiceNo.DataPropertyName = "PurchaseOrderCode";
            this.colInvoiceNo.HeaderText = "No. PO";
            this.colInvoiceNo.Name = "colInvoiceNo";
            this.colInvoiceNo.ReadOnly = true;
            this.colInvoiceNo.Width = 120;
            // 
            // colInvoiceDate
            // 
            this.colInvoiceDate.DataPropertyName = "OrderDate";
            dataGridViewCellStyle1.Format = "dd MMM yyyy";
            this.colInvoiceDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.colInvoiceDate.HeaderText = "Order Date";
            this.colInvoiceDate.Name = "colInvoiceDate";
            this.colInvoiceDate.ReadOnly = true;
            // 
            // colSupplierName
            // 
            this.colSupplierName.DataPropertyName = "SupplierName";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colSupplierName.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSupplierName.HeaderText = "Nama Vendor";
            this.colSupplierName.Name = "colSupplierName";
            this.colSupplierName.ReadOnly = true;
            this.colSupplierName.Width = 500;
            // 
            // colCustomerCode
            // 
            this.colCustomerCode.DataPropertyName = "CustomerCode";
            this.colCustomerCode.HeaderText = "Kode Vendor";
            this.colCustomerCode.Name = "colCustomerCode";
            this.colCustomerCode.ReadOnly = true;
            this.colCustomerCode.Visible = false;
            // 
            // colTotal
            // 
            this.colTotal.DataPropertyName = "TotalAmount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            this.colTotal.DefaultCellStyle = dataGridViewCellStyle3;
            this.colTotal.HeaderText = "Total Amount";
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "StatusDesc";
            this.colStatus.HeaderText = "Status";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colAction
            // 
            this.colAction.HeaderText = "Action";
            this.colAction.Name = "colAction";
            this.colAction.ReadOnly = true;
            this.colAction.Text = "";
            this.colAction.Width = 150;
            // 
            // frmPurchaseOrderList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.ctlHeader1);
            this.Controls.Add(this.ctlFooter1);
            this.Name = "frmPurchaseOrderList";
            this.Text = "frmPurchaseOrderList";
            this.Load += new System.EventHandler(this.frmPurchaseList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Control.ctlFooter ctlFooter1;
        private Control.ctlHeader ctlHeader1;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvoiceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSupplierName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewButtonColumn colAction;
    }
}