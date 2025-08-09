namespace Sendang.Rejeki.Transaction
{
    partial class frmStockList
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
            this.grid = new System.Windows.Forms.DataGridView();
            this.ctlFooter1 = new Sendang.Rejeki.Control.ctlFooter();
            this.ctlHeader1 = new Sendang.Rejeki.Control.ctlHeader();
            this.colStockDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCatalogID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCatalogName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreatedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStockDate,
            this.colCatalogID,
            this.colCatalogName,
            this.colStock,
            this.colUnit,
            this.colCreatedBy});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 25);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(735, 221);
            this.grid.TabIndex = 6;
            // 
            // ctlFooter1
            // 
            this.ctlFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlFooter1.Location = new System.Drawing.Point(0, 246);
            this.ctlFooter1.Name = "ctlFooter1";
            this.ctlFooter1.Offset = 0;
            this.ctlFooter1.PageIndex = 1;
            this.ctlFooter1.Size = new System.Drawing.Size(735, 23);
            this.ctlFooter1.TabIndex = 5;
            this.ctlFooter1.TotalRows = 0;
            // 
            // ctlHeader1
            // 
            this.ctlHeader1.DeleteButton = null;
            this.ctlHeader1.DeleteButtonEnabled = false;
            this.ctlHeader1.DeleteButtonText = "Delete";
            this.ctlHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctlHeader1.EditButton = null;
            this.ctlHeader1.EditButtonEnabled = true;
            this.ctlHeader1.EditButtonText = "Edit";
            this.ctlHeader1.IsLookup = false;
            this.ctlHeader1.Location = new System.Drawing.Point(0, 0);
            this.ctlHeader1.Name = "ctlHeader1";
            this.ctlHeader1.NewButton = null;
            this.ctlHeader1.NewButtonEnabled = false;
            this.ctlHeader1.NewButtonText = "Add";
            this.ctlHeader1.Size = new System.Drawing.Size(735, 25);
            this.ctlHeader1.TabIndex = 4;
            this.ctlHeader1.TextToSearch = "";
            // 
            // colStockDate
            // 
            this.colStockDate.DataPropertyName = "StockDate";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Format = "dd-MMM-yyyy";
            this.colStockDate.DefaultCellStyle = dataGridViewCellStyle1;
            this.colStockDate.HeaderText = "Tanggal";
            this.colStockDate.Name = "colStockDate";
            this.colStockDate.ReadOnly = true;
            // 
            // colCatalogID
            // 
            this.colCatalogID.DataPropertyName = "CatalogID";
            this.colCatalogID.HeaderText = "CatalogID";
            this.colCatalogID.Name = "colCatalogID";
            this.colCatalogID.ReadOnly = true;
            this.colCatalogID.Visible = false;
            // 
            // colCatalogName
            // 
            this.colCatalogName.DataPropertyName = "CatalogName";
            this.colCatalogName.HeaderText = "Item";
            this.colCatalogName.Name = "colCatalogName";
            this.colCatalogName.ReadOnly = true;
            this.colCatalogName.Width = 250;
            // 
            // colStock
            // 
            this.colStock.DataPropertyName = "Stock";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            this.colStock.DefaultCellStyle = dataGridViewCellStyle2;
            this.colStock.HeaderText = "Stock";
            this.colStock.Name = "colStock";
            this.colStock.ReadOnly = true;
            this.colStock.Width = 150;
            // 
            // colUnit
            // 
            this.colUnit.DataPropertyName = "Unit";
            this.colUnit.HeaderText = "Satuan";
            this.colUnit.Name = "colUnit";
            this.colUnit.ReadOnly = true;
            this.colUnit.Width = 70;
            // 
            // colCreatedBy
            // 
            this.colCreatedBy.DataPropertyName = "CreatedBy";
            this.colCreatedBy.HeaderText = "Di Update Oleh";
            this.colCreatedBy.Name = "colCreatedBy";
            this.colCreatedBy.ReadOnly = true;
            this.colCreatedBy.Visible = false;
            this.colCreatedBy.Width = 150;
            // 
            // frmStockList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 269);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.ctlFooter1);
            this.Controls.Add(this.ctlHeader1);
            this.Name = "frmStockList";
            this.Text = "frmStock";
            this.Load += new System.EventHandler(this.frmStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private Control.ctlFooter ctlFooter1;
        private Control.ctlHeader ctlHeader1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStockDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCatalogID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCatalogName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreatedBy;
    }
}