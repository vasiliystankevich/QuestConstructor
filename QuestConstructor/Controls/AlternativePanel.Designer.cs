namespace QuestConstructorNS
{
    partial class AlternativePanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbId = new System.Windows.Forms.TextBox();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.btUp = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbCondition = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbId
            // 
            this.tbId.Location = new System.Drawing.Point(26, 4);
            this.tbId.Name = "tbId";
            this.tbId.Size = new System.Drawing.Size(41, 22);
            this.tbId.TabIndex = 1;
            this.tbId.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // tbTitle
            // 
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.Location = new System.Drawing.Point(73, 4);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(375, 22);
            this.tbTitle.TabIndex = 4;
            this.tbTitle.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // btUp
            // 
            this.btUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btUp.FlatAppearance.BorderSize = 0;
            this.btUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btUp.Image = global::QuestConstructorNS.Properties.Resources.up_alt;
            this.btUp.Location = new System.Drawing.Point(522, 4);
            this.btUp.Margin = new System.Windows.Forms.Padding(0);
            this.btUp.Name = "btUp";
            this.btUp.Size = new System.Drawing.Size(20, 24);
            this.btUp.TabIndex = 9;
            this.btUp.UseVisualStyleBackColor = true;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // btDown
            // 
            this.btDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDown.FlatAppearance.BorderSize = 0;
            this.btDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDown.Image = global::QuestConstructorNS.Properties.Resources.down_alt;
            this.btDown.Location = new System.Drawing.Point(544, 4);
            this.btDown.Margin = new System.Windows.Forms.Padding(0);
            this.btDown.Name = "btDown";
            this.btDown.Size = new System.Drawing.Size(21, 24);
            this.btDown.TabIndex = 8;
            this.btDown.UseVisualStyleBackColor = true;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // btDelete
            // 
            this.btDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btDelete.FlatAppearance.BorderSize = 0;
            this.btDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btDelete.Image = global::QuestConstructorNS.Properties.Resources.close_square;
            this.btDelete.Location = new System.Drawing.Point(567, 4);
            this.btDelete.Margin = new System.Windows.Forms.Padding(0);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(23, 23);
            this.btDelete.TabIndex = 7;
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::QuestConstructorNS.Properties.Resources.done_square;
            this.pictureBox1.Location = new System.Drawing.Point(3, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // lbCondition
            // 
            this.lbCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCondition.AutoSize = true;
            this.lbCondition.Location = new System.Drawing.Point(450, 6);
            this.lbCondition.MaximumSize = new System.Drawing.Size(65, 17);
            this.lbCondition.Name = "lbCondition";
            this.lbCondition.Size = new System.Drawing.Size(52, 17);
            this.lbCondition.TabIndex = 10;
            this.lbCondition.TabStop = true;
            this.lbCondition.Text = "Если...";
            this.lbCondition.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lbCondition.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbCondition_LinkClicked);
            // 
            // AlternativePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.btUp);
            this.Controls.Add(this.btDown);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tbId);
            this.Controls.Add(this.lbCondition);
            this.Name = "AlternativePanel";
            this.Size = new System.Drawing.Size(593, 31);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.LinkLabel lbCondition;
    }
}
