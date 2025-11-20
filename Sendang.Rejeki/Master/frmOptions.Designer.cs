namespace Sendang.Rejeki.Master
{
    partial class frmOptions
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
            this.ctlHeader1 = new Sendang.Rejeki.Control.ctlHeader();
            this.ctlFooter1 = new Sendang.Rejeki.Control.ctlFooter();
            this.grid = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValueMember = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDisplayMember = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
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
            this.ctlHeader1.NewButtonText = "Add";
            this.ctlHeader1.Size = new System.Drawing.Size(588, 25);
            this.ctlHeader1.TabIndex = 4;
            this.ctlHeader1.TextToSearch = "";
            // 
            // ctlFooter1
            // 
            this.ctlFooter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlFooter1.Location = new System.Drawing.Point(0, 330);
            this.ctlFooter1.Name = "ctlFooter1";
            this.ctlFooter1.Offset = 0;
            this.ctlFooter1.PageIndex = 1;
            this.ctlFooter1.Size = new System.Drawing.Size(588, 23);
            this.ctlFooter1.TabIndex = 5;
            this.ctlFooter1.TotalRows = 0;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colName,
            this.colValueMember,
            this.colDisplayMember,
            this.colDescription,
            this.CreatedDate,
            this.CreatedBy,
            this.ModifiedDate,
            this.ModifiedBy});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 25);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(588, 305);
            this.grid.TabIndex = 7;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            this.colName.Width = 150;
            // 
            // colValueMember
            // 
            this.colValueMember.DataPropertyName = "ValueMember";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.colValueMember.DefaultCellStyle = dataGridViewCellStyle1;
            this.colValueMember.HeaderText = "ValueMember";
            this.colValueMember.Name = "colValueMember";
            this.colValueMember.ReadOnly = true;
            this.colValueMember.Width = 150;
            // 
            // colDisplayMember
            // 
            this.colDisplayMember.DataPropertyName = "DisplayMember";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            this.colDisplayMember.DefaultCellStyle = dataGridViewCellStyle2;
            this.colDisplayMember.HeaderText = "DisplayMember";
            this.colDisplayMember.Name = "colDisplayMember";
            this.colDisplayMember.ReadOnly = true;
            this.colDisplayMember.Width = 150;
            // 
            // colDescription
            // 
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            this.colDescription.Width = 200;
            // 
            // CreatedDate
            // 
            this.CreatedDate.DataPropertyName = "CreatedDate";
            this.CreatedDate.HeaderText = "CreatedDate";
            this.CreatedDate.Name = "CreatedDate";
            this.CreatedDate.ReadOnly = true;
            this.CreatedDate.Visible = false;
            // 
            // CreatedBy
            // 
            this.CreatedBy.DataPropertyName = "CreatedBy";
            this.CreatedBy.HeaderText = "CreatedBy";
            this.CreatedBy.Name = "CreatedBy";
            this.CreatedBy.ReadOnly = true;
            this.CreatedBy.Visible = false;
            // 
            // ModifiedDate
            // 
            this.ModifiedDate.DataPropertyName = "ModifiedDate";
            this.ModifiedDate.HeaderText = "Modified Date";
            this.ModifiedDate.Name = "ModifiedDate";
            this.ModifiedDate.ReadOnly = true;
            this.ModifiedDate.Visible = false;
            // 
            // ModifiedBy
            // 
            this.ModifiedBy.DataPropertyName = "ModifiedBy";
            this.ModifiedBy.HeaderText = "ModifiedBy";
            this.ModifiedBy.Name = "ModifiedBy";
            this.ModifiedBy.ReadOnly = true;
            this.ModifiedBy.Visible = false;
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 353);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.ctlHeader1);
            this.Controls.Add(this.ctlFooter1);
            this.Name = "frmOptions";
            this.Text = "frmOptions";
            this.Load += new System.EventHandler(this.frmOptionsList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Control.ctlHeader ctlHeader1;
        private Control.ctlFooter ctlFooter1;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValueMember;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisplayMember;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedBy;
    }
}