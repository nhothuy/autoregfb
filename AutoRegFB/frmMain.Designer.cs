namespace AutoRegFB
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.geckoWebBrowser = new Gecko.GeckoWebBrowser();
            this.btnRegFB = new System.Windows.Forms.Button();
            this.btnLoginFB = new System.Windows.Forms.Button();
            this.btnResetAll = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.chkDecaptcha = new System.Windows.Forms.CheckBox();
            this.chkPlayPK = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // geckoWebBrowser
            // 
            this.geckoWebBrowser.Location = new System.Drawing.Point(-1, 2);
            this.geckoWebBrowser.Name = "geckoWebBrowser";
            this.geckoWebBrowser.Size = new System.Drawing.Size(573, 459);
            this.geckoWebBrowser.TabIndex = 0;
            this.geckoWebBrowser.UseHttpActivityObserver = false;
            this.geckoWebBrowser.DocumentCompleted += new System.EventHandler<Gecko.Events.GeckoDocumentCompletedEventArgs>(this.geckoWebBrowser_DocumentCompleted);
            // 
            // btnRegFB
            // 
            this.btnRegFB.Location = new System.Drawing.Point(575, 2);
            this.btnRegFB.Name = "btnRegFB";
            this.btnRegFB.Size = new System.Drawing.Size(99, 23);
            this.btnRegFB.TabIndex = 1;
            this.btnRegFB.Text = "Reg FB";
            this.btnRegFB.UseVisualStyleBackColor = true;
            this.btnRegFB.Click += new System.EventHandler(this.btnRegFB_Click);
            // 
            // btnLoginFB
            // 
            this.btnLoginFB.Location = new System.Drawing.Point(575, 29);
            this.btnLoginFB.Name = "btnLoginFB";
            this.btnLoginFB.Size = new System.Drawing.Size(99, 23);
            this.btnLoginFB.TabIndex = 4;
            this.btnLoginFB.Text = "Login FB";
            this.btnLoginFB.UseVisualStyleBackColor = true;
            this.btnLoginFB.Click += new System.EventHandler(this.btnLoginFB_Click);
            // 
            // btnResetAll
            // 
            this.btnResetAll.Location = new System.Drawing.Point(575, 58);
            this.btnResetAll.Name = "btnResetAll";
            this.btnResetAll.Size = new System.Drawing.Size(99, 23);
            this.btnResetAll.TabIndex = 6;
            this.btnResetAll.Text = "Reset";
            this.btnResetAll.UseVisualStyleBackColor = true;
            this.btnResetAll.Click += new System.EventHandler(this.btnResetAll_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(5, 467);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 13);
            this.lblMsg.TabIndex = 7;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(575, 438);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(99, 23);
            this.btnReset.TabIndex = 8;
            this.btnReset.Text = "Reset Reg";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // chkDecaptcha
            // 
            this.chkDecaptcha.AutoSize = true;
            this.chkDecaptcha.Location = new System.Drawing.Point(458, 467);
            this.chkDecaptcha.Name = "chkDecaptcha";
            this.chkDecaptcha.Size = new System.Drawing.Size(107, 17);
            this.chkDecaptcha.TabIndex = 9;
            this.chkDecaptcha.Text = "Using decaptcha";
            this.chkDecaptcha.UseVisualStyleBackColor = true;
            this.chkDecaptcha.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // chkPlayPK
            // 
            this.chkPlayPK.AutoSize = true;
            this.chkPlayPK.Location = new System.Drawing.Point(570, 467);
            this.chkPlayPK.Name = "chkPlayPK";
            this.chkPlayPK.Size = new System.Drawing.Size(105, 17);
            this.chkPlayPK.TabIndex = 10;
            this.chkPlayPK.Text = "Play Pirate Kings";
            this.chkPlayPK.UseVisualStyleBackColor = true;
            this.chkPlayPK.CheckedChanged += new System.EventHandler(this.chk_CheckedChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 486);
            this.Controls.Add(this.chkPlayPK);
            this.Controls.Add(this.chkDecaptcha);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnResetAll);
            this.Controls.Add(this.btnLoginFB);
            this.Controls.Add(this.btnRegFB);
            this.Controls.Add(this.geckoWebBrowser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Reg Facebook";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Gecko.GeckoWebBrowser geckoWebBrowser;
        private System.Windows.Forms.Button btnRegFB;
        private System.Windows.Forms.Button btnLoginFB;
        private System.Windows.Forms.Button btnResetAll;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox chkDecaptcha;
        private System.Windows.Forms.CheckBox chkPlayPK;
    }
}

