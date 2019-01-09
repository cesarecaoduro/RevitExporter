namespace RevitExporter.UI
{
    partial class RevitExporterForm
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
            this.lbELementTypes = new System.Windows.Forms.CheckedListBox();
            this.lbRoomParameters = new System.Windows.Forms.CheckedListBox();
            this.lbELementParameters = new System.Windows.Forms.CheckedListBox();
            this.lbDocuments = new System.Windows.Forms.CheckedListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbELementTypes
            // 
            this.lbELementTypes.CheckOnClick = true;
            this.lbELementTypes.FormattingEnabled = true;
            this.lbELementTypes.HorizontalScrollbar = true;
            this.lbELementTypes.Location = new System.Drawing.Point(251, 174);
            this.lbELementTypes.Name = "lbELementTypes";
            this.lbELementTypes.Size = new System.Drawing.Size(354, 349);
            this.lbELementTypes.TabIndex = 2;
            // 
            // lbRoomParameters
            // 
            this.lbRoomParameters.FormattingEnabled = true;
            this.lbRoomParameters.Location = new System.Drawing.Point(12, 174);
            this.lbRoomParameters.Name = "lbRoomParameters";
            this.lbRoomParameters.Size = new System.Drawing.Size(221, 349);
            this.lbRoomParameters.TabIndex = 2;
            // 
            // lbELementParameters
            // 
            this.lbELementParameters.CheckOnClick = true;
            this.lbELementParameters.FormattingEnabled = true;
            this.lbELementParameters.HorizontalScrollbar = true;
            this.lbELementParameters.Location = new System.Drawing.Point(625, 174);
            this.lbELementParameters.Name = "lbELementParameters";
            this.lbELementParameters.Size = new System.Drawing.Size(354, 349);
            this.lbELementParameters.TabIndex = 3;
            // 
            // lbDocuments
            // 
            this.lbDocuments.CheckOnClick = true;
            this.lbDocuments.FormattingEnabled = true;
            this.lbDocuments.HorizontalScrollbar = true;
            this.lbDocuments.Location = new System.Drawing.Point(12, 52);
            this.lbDocuments.Name = "lbDocuments";
            this.lbDocuments.Size = new System.Drawing.Size(335, 109);
            this.lbDocuments.TabIndex = 4;
            this.lbDocuments.SelectedValueChanged += new System.EventHandler(this.lbDocuments_SelectedValueChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(372, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(605, 109);
            this.dataGridView1.TabIndex = 5;
            // 
            // RevitExporterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 567);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lbDocuments);
            this.Controls.Add(this.lbELementParameters);
            this.Controls.Add(this.lbELementTypes);
            this.Controls.Add(this.lbRoomParameters);
            this.Name = "RevitExporterForm";
            this.Text = "RevitExporterForm";
            this.Load += new System.EventHandler(this.RevitExporterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox lbELementTypes;
        private System.Windows.Forms.CheckedListBox lbRoomParameters;
        private System.Windows.Forms.CheckedListBox lbELementParameters;
        private System.Windows.Forms.CheckedListBox lbDocuments;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}