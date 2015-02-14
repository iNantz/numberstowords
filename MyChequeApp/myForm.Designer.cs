namespace MyChequeApp
{
    partial class frmMyCheque
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
            this.lblInput = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.lblOutput = new System.Windows.Forms.Label();
            this.chkCurrency = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblInput
            // 
            this.lblInput.AutoSize = true;
            this.lblInput.Location = new System.Drawing.Point(13, 22);
            this.lblInput.Name = "lblInput";
            this.lblInput.Size = new System.Drawing.Size(31, 13);
            this.lblInput.TabIndex = 0;
            this.lblInput.Text = "Input";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(55, 22);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(273, 20);
            this.txtInput.TabIndex = 1;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(334, 22);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 2;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // lblOutput
            // 
            this.lblOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOutput.Location = new System.Drawing.Point(55, 68);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(354, 123);
            this.lblOutput.TabIndex = 3;
            // 
            // chkCurrency
            // 
            this.chkCurrency.AutoSize = true;
            this.chkCurrency.Checked = true;
            this.chkCurrency.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCurrency.Location = new System.Drawing.Point(55, 48);
            this.chkCurrency.Name = "chkCurrency";
            this.chkCurrency.Size = new System.Drawing.Size(68, 17);
            this.chkCurrency.TabIndex = 4;
            this.chkCurrency.Text = "Currency";
            this.chkCurrency.UseVisualStyleBackColor = true;
            this.chkCurrency.Visible = false;
            this.chkCurrency.CheckedChanged += new System.EventHandler(this.chkCurrency_CheckedChanged);
            // 
            // frmMyCheque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 200);
            this.Controls.Add(this.chkCurrency);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.lblInput);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(434, 239);
            this.MinimizeBox = false;
            this.Name = "frmMyCheque";
            this.Text = "Cheque Writing";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.CheckBox chkCurrency;
    }
}

