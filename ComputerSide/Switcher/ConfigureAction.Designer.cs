namespace Switcher
{
  partial class ConfigureAction
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.imageFileDialog = new System.Windows.Forms.OpenFileDialog();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOk = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.txtName = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtDescription = new System.Windows.Forms.TextBox();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.openProcessDialog = new System.Windows.Forms.OpenFileDialog();
      this.txtProcessParams = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.txtProcess = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.btnSelectProcess = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.Location = new System.Drawing.Point(6, 19);
      this.pictureBox1.MinimumSize = new System.Drawing.Size(150, 150);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(150, 150);
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.pictureBox1);
      this.groupBox1.Location = new System.Drawing.Point(12, 16);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(163, 181);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Click to select an image...";
      // 
      // imageFileDialog
      // 
      this.imageFileDialog.FileName = "openFileDialog1";
      this.imageFileDialog.Filter = "(*.bmp, *.jpg, *.png, *.gif, *.ico)|*.bmp;*.jpg;*.png;*.gif;*.ico";
      this.imageFileDialog.SupportMultiDottedExtensions = true;
      this.imageFileDialog.Title = "Choose Image For Action...";
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
      this.btnCancel.Location = new System.Drawing.Point(18, 316);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 2;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = false;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnOk
      // 
      this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOk.BackColor = System.Drawing.SystemColors.Control;
      this.btnOk.Location = new System.Drawing.Point(471, 316);
      this.btnOk.Name = "btnOk";
      this.btnOk.Size = new System.Drawing.Size(75, 23);
      this.btnOk.TabIndex = 3;
      this.btnOk.Text = "Ok";
      this.btnOk.UseVisualStyleBackColor = false;
      this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(204, 55);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(38, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Name:";
      // 
      // txtName
      // 
      this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtName.Location = new System.Drawing.Point(274, 52);
      this.txtName.Name = "txtName";
      this.txtName.Size = new System.Drawing.Size(272, 20);
      this.txtName.TabIndex = 5;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(204, 97);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(63, 13);
      this.label2.TabIndex = 6;
      this.label2.Text = "Description:";
      // 
      // txtDescription
      // 
      this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtDescription.Location = new System.Drawing.Point(274, 97);
      this.txtDescription.Multiline = true;
      this.txtDescription.Name = "txtDescription";
      this.txtDescription.Size = new System.Drawing.Size(272, 88);
      this.txtDescription.TabIndex = 7;
      // 
      // comboBox1
      // 
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[] {
            "Launch Program..."});
      this.comboBox1.Location = new System.Drawing.Point(18, 203);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(157, 21);
      this.comboBox1.TabIndex = 8;
      this.comboBox1.Text = "(None)";
      this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
      // 
      // openProcessDialog
      // 
      this.openProcessDialog.Filter = "(*.exe)|*.exe";
      // 
      // txtProcessParams
      // 
      this.txtProcessParams.Location = new System.Drawing.Point(274, 261);
      this.txtProcessParams.Name = "txtProcessParams";
      this.txtProcessParams.Size = new System.Drawing.Size(272, 20);
      this.txtProcessParams.TabIndex = 9;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(207, 261);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(63, 13);
      this.label3.TabIndex = 10;
      this.label3.Text = "Parameters:";
      // 
      // txtProcess
      // 
      this.txtProcess.Location = new System.Drawing.Point(274, 228);
      this.txtProcess.Name = "txtProcess";
      this.txtProcess.Size = new System.Drawing.Size(235, 20);
      this.txtProcess.TabIndex = 11;
      this.txtProcess.Click += new System.EventHandler(this.txtProcess_Click);
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(207, 231);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(48, 13);
      this.label4.TabIndex = 12;
      this.label4.Text = "Process:";
      // 
      // btnSelectProcess
      // 
      this.btnSelectProcess.Location = new System.Drawing.Point(515, 226);
      this.btnSelectProcess.Name = "btnSelectProcess";
      this.btnSelectProcess.Size = new System.Drawing.Size(31, 23);
      this.btnSelectProcess.TabIndex = 13;
      this.btnSelectProcess.Text = "...";
      this.btnSelectProcess.UseVisualStyleBackColor = true;
      this.btnSelectProcess.Click += new System.EventHandler(this.btnSelectProcess_Click);
      // 
      // ConfigureAction
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(558, 351);
      this.Controls.Add(this.btnSelectProcess);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.txtProcess);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.txtProcessParams);
      this.Controls.Add(this.comboBox1);
      this.Controls.Add(this.txtDescription);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.txtName);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnOk);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.groupBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.MinimumSize = new System.Drawing.Size(390, 275);
      this.Name = "ConfigureAction";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "ConfigureAction";
      this.Load += new System.EventHandler(this.ConfigureAction_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.OpenFileDialog imageFileDialog;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtDescription;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.OpenFileDialog openProcessDialog;
    private System.Windows.Forms.TextBox txtProcessParams;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox txtProcess;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button btnSelectProcess;
  }
}