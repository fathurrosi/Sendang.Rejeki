namespace Sendang.Rejeki.Transaction
{
    partial class frmProdRecon
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPricePerUnit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboCatalog = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProductQty = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProductPricePerUnit = new System.Windows.Forms.TextBox();
            this.txtTransDate = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.cboUnit = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboProduct = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnProccess = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.cboBiayaPenyusutan = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtBiayaProduksi = new System.Windows.Forms.TextBox();
            this.txtLastHPP = new System.Windows.Forms.TextBox();
            this.txtBiayaPenyusutan = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtStock);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtQty);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtDate);
            this.groupBox1.Controls.Add(this.txtUnit);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPricePerUnit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboCatalog);
            this.groupBox1.Location = new System.Drawing.Point(13, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 163);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Catalog";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(45, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 35;
            this.label10.Text = "Quantity :";
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.White;
            this.txtQty.Location = new System.Drawing.Point(107, 76);
            this.txtQty.MaxLength = 50;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(176, 20);
            this.txtQty.TabIndex = 34;
            this.txtQty.Enter += new System.EventHandler(this.tb_Enter);
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress);
            this.txtQty.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_KeyUp);
            this.txtQty.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(34, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 33;
            this.label9.Text = "Price Date :";
            // 
            // txtDate
            // 
            this.txtDate.BackColor = System.Drawing.Color.White;
            this.txtDate.Location = new System.Drawing.Point(107, 99);
            this.txtDate.MaxLength = 50;
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(243, 20);
            this.txtDate.TabIndex = 32;
            // 
            // txtUnit
            // 
            this.txtUnit.BackColor = System.Drawing.Color.White;
            this.txtUnit.Enabled = false;
            this.txtUnit.Location = new System.Drawing.Point(286, 76);
            this.txtUnit.MaxLength = 50;
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.ReadOnly = true;
            this.txtUnit.Size = new System.Drawing.Size(64, 20);
            this.txtUnit.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "Price/Unit :";
            // 
            // txtPricePerUnit
            // 
            this.txtPricePerUnit.BackColor = System.Drawing.Color.White;
            this.txtPricePerUnit.Enabled = false;
            this.txtPricePerUnit.Location = new System.Drawing.Point(107, 54);
            this.txtPricePerUnit.MaxLength = 50;
            this.txtPricePerUnit.Name = "txtPricePerUnit";
            this.txtPricePerUnit.ReadOnly = true;
            this.txtPricePerUnit.Size = new System.Drawing.Size(243, 20);
            this.txtPricePerUnit.TabIndex = 28;
            this.txtPricePerUnit.Enter += new System.EventHandler(this.tb_Enter);
            this.txtPricePerUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress);
            this.txtPricePerUnit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_KeyUp);
            this.txtPricePerUnit.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Catalog :";
            // 
            // cboCatalog
            // 
            this.cboCatalog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCatalog.FormattingEnabled = true;
            this.cboCatalog.Location = new System.Drawing.Point(107, 30);
            this.cboCatalog.Name = "cboCatalog";
            this.cboCatalog.Size = new System.Drawing.Size(243, 21);
            this.cboCatalog.TabIndex = 26;
            this.cboCatalog.SelectedIndexChanged += new System.EventHandler(this.cboCatalog_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(92, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Quantity :";
            // 
            // txtProductQty
            // 
            this.txtProductQty.BackColor = System.Drawing.Color.White;
            this.txtProductQty.Location = new System.Drawing.Point(150, 73);
            this.txtProductQty.MaxLength = 50;
            this.txtProductQty.Name = "txtProductQty";
            this.txtProductQty.Size = new System.Drawing.Size(150, 20);
            this.txtProductQty.TabIndex = 32;
            this.txtProductQty.Enter += new System.EventHandler(this.tb_Enter);
            this.txtProductQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress);
            this.txtProductQty.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_KeyUp);
            this.txtProductQty.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtBiayaPenyusutan);
            this.groupBox2.Controls.Add(this.txtLastHPP);
            this.groupBox2.Controls.Add(this.txtBiayaProduksi);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cboBiayaPenyusutan);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtProductPricePerUnit);
            this.groupBox2.Controls.Add(this.txtTransDate);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.cboUnit);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtProductQty);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cboProduct);
            this.groupBox2.Location = new System.Drawing.Point(470, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(411, 212);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Catalog";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(83, 170);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Price/Unit :";
            // 
            // txtProductPricePerUnit
            // 
            this.txtProductPricePerUnit.BackColor = System.Drawing.Color.White;
            this.txtProductPricePerUnit.Enabled = false;
            this.txtProductPricePerUnit.Location = new System.Drawing.Point(150, 167);
            this.txtProductPricePerUnit.MaxLength = 50;
            this.txtProductPricePerUnit.Name = "txtProductPricePerUnit";
            this.txtProductPricePerUnit.ReadOnly = true;
            this.txtProductPricePerUnit.Size = new System.Drawing.Size(245, 20);
            this.txtProductPricePerUnit.TabIndex = 37;
            this.txtProductPricePerUnit.Enter += new System.EventHandler(this.tb_Enter);
            this.txtProductPricePerUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress);
            this.txtProductPricePerUnit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_KeyUp);
            this.txtProductPricePerUnit.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // txtTransDate
            // 
            this.txtTransDate.CustomFormat = "dd-MMM-yyyy HH:mm:ss";
            this.txtTransDate.Enabled = false;
            this.txtTransDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtTransDate.Location = new System.Drawing.Point(150, 26);
            this.txtTransDate.Name = "txtTransDate";
            this.txtTransDate.Size = new System.Drawing.Size(150, 20);
            this.txtTransDate.TabIndex = 36;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(49, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 13);
            this.label12.TabIndex = 35;
            this.label12.Text = "Transaction Date :";
            // 
            // cboUnit
            // 
            this.cboUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnit.FormattingEnabled = true;
            this.cboUnit.Location = new System.Drawing.Point(305, 73);
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.Size = new System.Drawing.Size(90, 21);
            this.cboUnit.TabIndex = 34;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(111, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Item :";
            // 
            // cboProduct
            // 
            this.cboProduct.FormattingEnabled = true;
            this.cboProduct.Location = new System.Drawing.Point(150, 49);
            this.cboProduct.Name = "cboProduct";
            this.cboProduct.Size = new System.Drawing.Size(245, 21);
            this.cboProduct.TabIndex = 26;
            this.cboProduct.SelectedIndexChanged += new System.EventHandler(this.cboProduct_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(979, 29);
            this.panel1.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(979, 29);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Rekonsiliasi Produk (Item To Item)";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(421, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 20);
            this.label4.TabIndex = 28;
            this.label4.Text = "To";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(765, 237);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 45);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnProccess
            // 
            this.btnProccess.Location = new System.Drawing.Point(655, 237);
            this.btnProccess.Name = "btnProccess";
            this.btnProccess.Size = new System.Drawing.Size(96, 45);
            this.btnProccess.TabIndex = 30;
            this.btnProccess.Text = "Proccess";
            this.btnProccess.UseVisualStyleBackColor = true;
            this.btnProccess.Click += new System.EventHandler(this.btnProccess_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 180);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 31;
            this.label11.Text = "Keterangan :";
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(9, 199);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(398, 83);
            this.label13.TabIndex = 33;
            this.label13.Text = "Create On Time";
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnProccess);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Location = new System.Drawing.Point(30, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(913, 304);
            this.panel2.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "Biaya Penysutan :";
            // 
            // cboBiayaPenyusutan
            // 
            this.cboBiayaPenyusutan.FormattingEnabled = true;
            this.cboBiayaPenyusutan.Location = new System.Drawing.Point(150, 119);
            this.cboBiayaPenyusutan.Name = "cboBiayaPenyusutan";
            this.cboBiayaPenyusutan.Size = new System.Drawing.Size(63, 21);
            this.cboBiayaPenyusutan.TabIndex = 39;
            this.cboBiayaPenyusutan.SelectedIndexChanged += new System.EventHandler(this.cboBiayaPenyusutan_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(67, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 42;
            this.label7.Text = "HPP Terakhir :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(61, 146);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(83, 13);
            this.label14.TabIndex = 44;
            this.label14.Text = "Biaya Produksi :";
            // 
            // txtBiayaProduksi
            // 
            this.txtBiayaProduksi.BackColor = System.Drawing.Color.White;
            this.txtBiayaProduksi.Location = new System.Drawing.Point(150, 143);
            this.txtBiayaProduksi.MaxLength = 50;
            this.txtBiayaProduksi.Name = "txtBiayaProduksi";
            this.txtBiayaProduksi.Size = new System.Drawing.Size(245, 20);
            this.txtBiayaProduksi.TabIndex = 45;
            this.txtBiayaProduksi.Enter += new System.EventHandler(this.tb_Enter);
            this.txtBiayaProduksi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_KeyPress);
            this.txtBiayaProduksi.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_KeyUp);
            this.txtBiayaProduksi.Leave += new System.EventHandler(this.tb_Leave);
            // 
            // txtLastHPP
            // 
            this.txtLastHPP.BackColor = System.Drawing.Color.White;
            this.txtLastHPP.Location = new System.Drawing.Point(150, 96);
            this.txtLastHPP.MaxLength = 50;
            this.txtLastHPP.Name = "txtLastHPP";
            this.txtLastHPP.ReadOnly = true;
            this.txtLastHPP.Size = new System.Drawing.Size(245, 20);
            this.txtLastHPP.TabIndex = 46;
            // 
            // txtBiayaPenyusutan
            // 
            this.txtBiayaPenyusutan.BackColor = System.Drawing.Color.White;
            this.txtBiayaPenyusutan.Location = new System.Drawing.Point(219, 119);
            this.txtBiayaPenyusutan.MaxLength = 50;
            this.txtBiayaPenyusutan.Name = "txtBiayaPenyusutan";
            this.txtBiayaPenyusutan.ReadOnly = true;
            this.txtBiayaPenyusutan.Size = new System.Drawing.Size(176, 20);
            this.txtBiayaPenyusutan.TabIndex = 47;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 125);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(87, 13);
            this.label15.TabIndex = 37;
            this.label15.Text = "Stock Available :";
            // 
            // txtStock
            // 
            this.txtStock.BackColor = System.Drawing.Color.White;
            this.txtStock.Location = new System.Drawing.Point(107, 122);
            this.txtStock.MaxLength = 50;
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(176, 20);
            this.txtStock.TabIndex = 36;
            // 
            // frmProdRecon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(979, 389);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmProdRecon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Catalog To Catalog";
            this.Load += new System.EventHandler(this.frmProdRecon_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboCatalog;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.TextBox txtPricePerUnit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProductQty;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboProduct;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnProccess;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.ComboBox cboUnit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker txtTransDate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProductPricePerUnit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboBiayaPenyusutan;
        private System.Windows.Forms.TextBox txtBiayaProduksi;
        private System.Windows.Forms.TextBox txtLastHPP;
        private System.Windows.Forms.TextBox txtBiayaPenyusutan;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtStock;
    }
}