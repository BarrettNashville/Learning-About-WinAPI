namespace MathApp
{
    partial class Form1
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
            this.txtNum1 = new System.Windows.Forms.TextBox();
            this.txtNum2 = new System.Windows.Forms.TextBox();
            this.cbxOperation = new System.Windows.Forms.ComboBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtNum1
            // 
            this.txtNum1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNum1.Location = new System.Drawing.Point(151, 35);
            this.txtNum1.Name = "txtNum1";
            this.txtNum1.Size = new System.Drawing.Size(121, 20);
            this.txtNum1.TabIndex = 0;
            this.txtNum1.Text = "Enter First Number";
            this.txtNum1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtNum1_MouseDown);
            // 
            // txtNum2
            // 
            this.txtNum2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNum2.Location = new System.Drawing.Point(151, 79);
            this.txtNum2.Name = "txtNum2";
            this.txtNum2.Size = new System.Drawing.Size(121, 20);
            this.txtNum2.TabIndex = 1;
            this.txtNum2.Text = "Enter Second Number";
            this.txtNum2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtNum2_MouseDown);
            // 
            // cbxOperation
            // 
            this.cbxOperation.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbxOperation.CausesValidation = false;
            this.cbxOperation.FormattingEnabled = true;
            this.cbxOperation.Items.AddRange(new object[] {
            "Plus",
            "Minus",
            "Divide",
            "Multiply"});
            this.cbxOperation.Location = new System.Drawing.Point(151, 123);
            this.cbxOperation.Name = "cbxOperation";
            this.cbxOperation.Size = new System.Drawing.Size(121, 21);
            this.cbxOperation.TabIndex = 2;
            // 
            // lblResult
            // 
            this.lblResult.CausesValidation = false;
            this.lblResult.Location = new System.Drawing.Point(12, 215);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(398, 23);
            this.lblResult.TabIndex = 3;
            this.lblResult.Text = "Answer";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(174, 168);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 4;
            this.btnCalculate.Text = "Equals";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 262);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.cbxOperation);
            this.Controls.Add(this.txtNum2);
            this.Controls.Add(this.txtNum1);
            this.Name = "Form1";
            this.Text = "Math App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNum1;
        private System.Windows.Forms.TextBox txtNum2;
        private System.Windows.Forms.ComboBox cbxOperation;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnCalculate;
    }
}

