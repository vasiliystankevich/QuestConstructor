using Quest.Controls.Properties;
using Quest.Localizable;

namespace Quest.Controls.QuestConstructor
{
    partial class QuestPanel
    {
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuestPanel));
            this.tbId = new System.Windows.Forms.TextBox();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.pnAlternatives = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbQuestType = new System.Windows.Forms.ComboBox();
            this.lbCondition = new System.Windows.Forms.LinkLabel();
            this.btAddAlt = new System.Windows.Forms.Button();
            this.btUp = new System.Windows.Forms.Button();
            this.btDown = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbId
            // 
            resources.ApplyResources(this.tbId, "tbId");
            this.tbId.Name = "tbId";
            this.tbId.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // tbTitle
            // 
            resources.ApplyResources(this.tbTitle, "tbTitle");
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.TextChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // pnAlternatives
            // 
            resources.ApplyResources(this.pnAlternatives, "pnAlternatives");
            this.pnAlternatives.Name = "pnAlternatives";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cbQuestType
            // 
            this.cbQuestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuestType.FormattingEnabled = true;
            resources.ApplyResources(this.cbQuestType, "cbQuestType");
            this.cbQuestType.Name = "cbQuestType";
            this.cbQuestType.SelectedIndexChanged += new System.EventHandler(this.tb_TextChanged);
            // 
            // lbCondition
            // 
            resources.ApplyResources(this.lbCondition, "lbCondition");
            this.lbCondition.Name = "lbCondition";
            this.lbCondition.TabStop = true;
            this.lbCondition.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lbCondition.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbCondition_LinkClicked);
            // 
            // btAddAlt
            // 
            resources.ApplyResources(this.btAddAlt, "btAddAlt");
            this.btAddAlt.FlatAppearance.BorderSize = 0;
            this.btAddAlt.Image = global::Quest.Controls.Properties.Resources.list_add;
            this.btAddAlt.Name = "btAddAlt";
            this.btAddAlt.UseVisualStyleBackColor = true;
            this.btAddAlt.Click += new System.EventHandler(this.btAddAlt_Click);
            // 
            // btUp
            // 
            resources.ApplyResources(this.btUp, "btUp");
            this.btUp.FlatAppearance.BorderSize = 0;
            this.btUp.Image = global::Quest.Controls.Properties.Resources.icons8_sort_up_16;
            this.btUp.Name = "btUp";
            this.btUp.UseVisualStyleBackColor = false;
            this.btUp.Click += new System.EventHandler(this.btUp_Click);
            // 
            // btDown
            // 
            resources.ApplyResources(this.btDown, "btDown");
            this.btDown.FlatAppearance.BorderSize = 0;
            this.btDown.Image = global::Quest.Controls.Properties.Resources.icons8_sort_down_16;
            this.btDown.Name = "btDown";
            this.btDown.UseVisualStyleBackColor = false;
            this.btDown.Click += new System.EventHandler(this.btDown_Click);
            // 
            // btDelete
            // 
            resources.ApplyResources(this.btDelete, "btDelete");
            this.btDelete.FlatAppearance.BorderSize = 0;
            this.btDelete.Image = global::Quest.Controls.Properties.Resources.icons8_delete_24;
            this.btDelete.Name = "btDelete";
            this.btDelete.UseVisualStyleBackColor = false;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Quest.Controls.Properties.Resources.gnome_dialog_question;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // QuestPanel
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbCondition);
            this.Controls.Add(this.cbQuestType);
            this.Controls.Add(this.btAddAlt);
            this.Controls.Add(this.btUp);
            this.Controls.Add(this.btDown);
            this.Controls.Add(this.btDelete);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pnAlternatives);
            this.Controls.Add(this.tbTitle);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tbId);
            this.Name = "QuestPanel";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox tbId;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.FlowLayoutPanel pnAlternatives;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btDown;
        private System.Windows.Forms.Button btUp;
        private System.Windows.Forms.Button btAddAlt;
        private System.Windows.Forms.ComboBox cbQuestType;
        private System.Windows.Forms.LinkLabel lbCondition;
    }
}
