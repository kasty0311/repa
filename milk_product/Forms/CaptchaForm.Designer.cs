namespace milk_product.Forms
{
    partial class CaptchaForm
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
            this.btnCheck1 = new System.Windows.Forms.Button();
            this.pb1 = new System.Windows.Forms.PictureBox();
            this.pb2 = new System.Windows.Forms.PictureBox();
            this.pb3 = new System.Windows.Forms.PictureBox();
            this.pb4 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb4)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCheck1
            // 
            this.btnCheck1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCheck1.Location = new System.Drawing.Point(92, 400);
            this.btnCheck1.Name = "btnCheck1";
            this.btnCheck1.Size = new System.Drawing.Size(279, 23);
            this.btnCheck1.TabIndex = 2;
            this.btnCheck1.Text = "Пройти проверку";
            this.btnCheck1.UseVisualStyleBackColor = true;
            this.btnCheck1.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // pb1
            // 
            this.pb1.InitialImage = null;
            this.pb1.Location = new System.Drawing.Point(51, 40);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(189, 176);
            this.pb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb1.TabIndex = 3;
            this.pb1.TabStop = false;
            // 
            // pb2
            // 
            this.pb2.InitialImage = null;
            this.pb2.Location = new System.Drawing.Point(240, 40);
            this.pb2.Name = "pb2";
            this.pb2.Size = new System.Drawing.Size(189, 176);
            this.pb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb2.TabIndex = 4;
            this.pb2.TabStop = false;
            // 
            // pb3
            // 
            this.pb3.InitialImage = null;
            this.pb3.Location = new System.Drawing.Point(51, 216);
            this.pb3.Name = "pb3";
            this.pb3.Size = new System.Drawing.Size(189, 176);
            this.pb3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb3.TabIndex = 5;
            this.pb3.TabStop = false;
            // 
            // pb4
            // 
            this.pb4.InitialImage = null;
            this.pb4.Location = new System.Drawing.Point(240, 216);
            this.pb4.Name = "pb4";
            this.pb4.Size = new System.Drawing.Size(189, 176);
            this.pb4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb4.TabIndex = 6;
            this.pb4.TabStop = false;
            // 
            // CaptchaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 457);
            this.Controls.Add(this.pb4);
            this.Controls.Add(this.pb3);
            this.Controls.Add(this.pb2);
            this.Controls.Add(this.pb1);
            this.Controls.Add(this.btnCheck1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(484, 504);
            this.MinimumSize = new System.Drawing.Size(484, 504);
            this.Name = "CaptchaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Верификация CAPTCHA";
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCheck1;
        private System.Windows.Forms.PictureBox pb1;
        private System.Windows.Forms.PictureBox pb2;
        private System.Windows.Forms.PictureBox pb3;
        private System.Windows.Forms.PictureBox pb4;
    }
}