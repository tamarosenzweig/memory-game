namespace memoryGame
{
    partial class ChoosePartner
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView_partnerList = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_partnerList)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_partnerList
            // 
            this.dataGridView_partnerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_partnerList.Location = new System.Drawing.Point(216, 42);
            this.dataGridView_partnerList.Name = "dataGridView_partnerList";
            this.dataGridView_partnerList.Size = new System.Drawing.Size(322, 181);
            this.dataGridView_partnerList.TabIndex = 0;
            this.dataGridView_partnerList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_partnerList_CellDoubleClick);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ChoosePartner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView_partnerList);
            this.Name = "ChoosePartner";
            this.Text = "ChoosePartner";
            this.Load += new System.EventHandler(this.ChoosePartner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_partnerList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_partnerList;
        private System.Windows.Forms.Timer timer1;
    }
}