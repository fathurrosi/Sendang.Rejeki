namespace Sendang.Rejeki.Master
{
    partial class frmOption
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
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.ctlMaster1 = new Sendang.Rejeki.Control.ctlTransButton();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 45;
            this.label5.Text = "Name :";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(88, 11);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(262, 20);
            this.txtName.TabIndex = 44;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 48;
            this.label1.Text = "Value / Code :";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(88, 37);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(262, 20);
            this.txtCode.TabIndex = 47;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Display :";
            // 
            // txtDisplay
            // 
            this.txtDisplay.Location = new System.Drawing.Point(88, 63);
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.Size = new System.Drawing.Size(262, 20);
            this.txtDisplay.TabIndex = 49;
            // 
            // ctlMaster1
            // 
            this.ctlMaster1.CancelButtonEnabled = true;
            this.ctlMaster1.CancelButtonText = null;
            this.ctlMaster1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ctlMaster1.IsLookup = false;
            this.ctlMaster1.Location = new System.Drawing.Point(0, 191);
            this.ctlMaster1.Name = "ctlMaster1";
            this.ctlMaster1.SaveButtonEnabled = true;
            this.ctlMaster1.SaveButtonText = null;
            this.ctlMaster1.Size = new System.Drawing.Size(525, 45);
            this.ctlMaster1.TabIndex = 46;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Description :";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(88, 89);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(424, 102);
            this.txtDescription.TabIndex = 51;
            // 
            // frmOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 236);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.ctlMaster1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtName);
            this.Name = "frmOption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmOption";
            this.Load += new System.EventHandler(this.frmOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.OpenFileDialog fileDialog;
        private Control.ctlTransButton ctlMaster1;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtDescription;
    }
}