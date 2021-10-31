
namespace MineControl
{
    partial class FormIntro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIntro));
            this.richTextBoxIntro = new System.Windows.Forms.RichTextBox();
            this.labelIntroTitle = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxIntro
            // 
            this.richTextBoxIntro.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBoxIntro.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxIntro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxIntro.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxIntro.Name = "richTextBoxIntro";
            this.richTextBoxIntro.ReadOnly = true;
            this.richTextBoxIntro.Size = new System.Drawing.Size(583, 402);
            this.richTextBoxIntro.TabIndex = 0;
            this.richTextBoxIntro.Text = "";
            // 
            // labelIntroTitle
            // 
            this.labelIntroTitle.AutoSize = true;
            this.labelIntroTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelIntroTitle.Location = new System.Drawing.Point(12, 6);
            this.labelIntroTitle.Name = "labelIntroTitle";
            this.labelIntroTitle.Size = new System.Drawing.Size(204, 21);
            this.labelIntroTitle.TabIndex = 1;
            this.labelIntroTitle.Text = "Welcome To MineControl";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.richTextBoxIntro);
            this.panel1.Location = new System.Drawing.Point(12, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(585, 404);
            this.panel1.TabIndex = 2;
            // 
            // FormIntro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 449);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelIntroTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormIntro";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Intro";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormIntro_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxIntro;
        private System.Windows.Forms.Label labelIntroTitle;
        private System.Windows.Forms.Panel panel1;
    }
}