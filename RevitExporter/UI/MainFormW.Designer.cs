namespace RevitExporter.UI
{
    partial class MainFormW
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
            this.lbCategories = new System.Windows.Forms.CheckedListBox();
            this.lbParameters = new System.Windows.Forms.CheckedListBox();
            this.dgRooms = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fIleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salvaTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.esportaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jSONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.guidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.btnSelectAllCat = new System.Windows.Forms.Button();
            this.btnSelectAllParams = new System.Windows.Forms.Button();
            this.grpSelection = new System.Windows.Forms.GroupBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpExport = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.lbExportObjects = new System.Windows.Forms.ListBox();
            this.dgDocuments = new System.Windows.Forms.DataGridView();
            this.grpData = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgRooms)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.grpSelection.SuspendLayout();
            this.grpExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDocuments)).BeginInit();
            this.grpData.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbCategories
            // 
            this.lbCategories.FormattingEnabled = true;
            this.lbCategories.Location = new System.Drawing.Point(6, 19);
            this.lbCategories.MultiColumn = true;
            this.lbCategories.Name = "lbCategories";
            this.lbCategories.Size = new System.Drawing.Size(204, 289);
            this.lbCategories.TabIndex = 0;
            this.lbCategories.SelectedIndexChanged += new System.EventHandler(this.lbCategories_SelectedIndexChanged);
            // 
            // lbParameters
            // 
            this.lbParameters.FormattingEnabled = true;
            this.lbParameters.Location = new System.Drawing.Point(216, 19);
            this.lbParameters.Name = "lbParameters";
            this.lbParameters.Size = new System.Drawing.Size(204, 289);
            this.lbParameters.TabIndex = 1;
            // 
            // dgRooms
            // 
            this.dgRooms.AllowUserToAddRows = false;
            this.dgRooms.AllowUserToDeleteRows = false;
            this.dgRooms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRooms.Location = new System.Drawing.Point(11, 112);
            this.dgRooms.Name = "dgRooms";
            this.dgRooms.ReadOnly = true;
            this.dgRooms.Size = new System.Drawing.Size(897, 93);
            this.dgRooms.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fIleToolStripMenuItem,
            this.esportaToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(934, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fIleToolStripMenuItem
            // 
            this.fIleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.apriToolStripMenuItem,
            this.salvaTemplateToolStripMenuItem,
            this.toolStripSeparator1,
            this.quitToolStripMenuItem});
            this.fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            this.fIleToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fIleToolStripMenuItem.Text = "File";
            // 
            // apriToolStripMenuItem
            // 
            this.apriToolStripMenuItem.Name = "apriToolStripMenuItem";
            this.apriToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.apriToolStripMenuItem.Text = "&Open";
            // 
            // salvaTemplateToolStripMenuItem
            // 
            this.salvaTemplateToolStripMenuItem.Name = "salvaTemplateToolStripMenuItem";
            this.salvaTemplateToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.salvaTemplateToolStripMenuItem.Text = "&Save";
            this.salvaTemplateToolStripMenuItem.Click += new System.EventHandler(this.salvaTemplateToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.quitToolStripMenuItem.Text = "&Close";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // esportaToolStripMenuItem
            // 
            this.esportaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelToolStripMenuItem,
            this.jSONToolStripMenuItem});
            this.esportaToolStripMenuItem.Name = "esportaToolStripMenuItem";
            this.esportaToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.esportaToolStripMenuItem.Text = "&Export";
            // 
            // excelToolStripMenuItem
            // 
            this.excelToolStripMenuItem.Name = "excelToolStripMenuItem";
            this.excelToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.excelToolStripMenuItem.Text = "E&xcel";
            // 
            // jSONToolStripMenuItem
            // 
            this.jSONToolStripMenuItem.Name = "jSONToolStripMenuItem";
            this.jSONToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.jSONToolStripMenuItem.Text = "&JSON";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.guidaToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem1.Text = "?";
            // 
            // guidaToolStripMenuItem
            // 
            this.guidaToolStripMenuItem.Name = "guidaToolStripMenuItem";
            this.guidaToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
            this.guidaToolStripMenuItem.Text = "&Guida";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 633);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(934, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // btnSelectAllCat
            // 
            this.btnSelectAllCat.Location = new System.Drawing.Point(135, 321);
            this.btnSelectAllCat.Name = "btnSelectAllCat";
            this.btnSelectAllCat.Size = new System.Drawing.Size(75, 25);
            this.btnSelectAllCat.TabIndex = 5;
            this.btnSelectAllCat.Text = "Select All";
            this.btnSelectAllCat.UseVisualStyleBackColor = true;
            // 
            // btnSelectAllParams
            // 
            this.btnSelectAllParams.Location = new System.Drawing.Point(345, 321);
            this.btnSelectAllParams.Name = "btnSelectAllParams";
            this.btnSelectAllParams.Size = new System.Drawing.Size(75, 25);
            this.btnSelectAllParams.TabIndex = 6;
            this.btnSelectAllParams.Text = "Select All";
            this.btnSelectAllParams.UseVisualStyleBackColor = true;
            // 
            // grpSelection
            // 
            this.grpSelection.Controls.Add(this.btnRemove);
            this.grpSelection.Controls.Add(this.btnAdd);
            this.grpSelection.Controls.Add(this.lbCategories);
            this.grpSelection.Controls.Add(this.btnSelectAllParams);
            this.grpSelection.Controls.Add(this.lbParameters);
            this.grpSelection.Controls.Add(this.btnSelectAllCat);
            this.grpSelection.Location = new System.Drawing.Point(14, 275);
            this.grpSelection.Name = "grpSelection";
            this.grpSelection.Size = new System.Drawing.Size(531, 353);
            this.grpSelection.TabIndex = 7;
            this.grpSelection.TabStop = false;
            this.grpSelection.Text = "Selection";
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(436, 176);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 25);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "<< Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(436, 145);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 25);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add >>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grpExport
            // 
            this.grpExport.Controls.Add(this.button3);
            this.grpExport.Controls.Add(this.lbExportObjects);
            this.grpExport.Location = new System.Drawing.Point(551, 275);
            this.grpExport.Name = "grpExport";
            this.grpExport.Size = new System.Drawing.Size(235, 353);
            this.grpExport.TabIndex = 9;
            this.grpExport.TabStop = false;
            this.grpExport.Text = "Export";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(141, 315);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 25);
            this.button3.TabIndex = 8;
            this.button3.Text = "Export";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // lbExportObjects
            // 
            this.lbExportObjects.FormattingEnabled = true;
            this.lbExportObjects.ItemHeight = 14;
            this.lbExportObjects.Location = new System.Drawing.Point(12, 19);
            this.lbExportObjects.Name = "lbExportObjects";
            this.lbExportObjects.Size = new System.Drawing.Size(204, 284);
            this.lbExportObjects.TabIndex = 0;
            // 
            // dgDocuments
            // 
            this.dgDocuments.AllowUserToAddRows = false;
            this.dgDocuments.AllowUserToDeleteRows = false;
            this.dgDocuments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDocuments.Location = new System.Drawing.Point(12, 19);
            this.dgDocuments.Name = "dgDocuments";
            this.dgDocuments.ReadOnly = true;
            this.dgDocuments.Size = new System.Drawing.Size(897, 87);
            this.dgDocuments.TabIndex = 2;
            // 
            // grpData
            // 
            this.grpData.Controls.Add(this.dgDocuments);
            this.grpData.Controls.Add(this.dgRooms);
            this.grpData.Location = new System.Drawing.Point(14, 37);
            this.grpData.Name = "grpData";
            this.grpData.Size = new System.Drawing.Size(920, 223);
            this.grpData.TabIndex = 10;
            this.grpData.TabStop = false;
            this.grpData.Text = "Project Data";
            // 
            // MainFormW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(934, 655);
            this.Controls.Add(this.grpData);
            this.Controls.Add(this.grpExport);
            this.Controls.Add(this.grpSelection);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainFormW";
            this.Text = "Connector";
            this.Load += new System.EventHandler(this.MainFormW_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgRooms)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpSelection.ResumeLayout(false);
            this.grpExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDocuments)).EndInit();
            this.grpData.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox lbCategories;
        private System.Windows.Forms.CheckedListBox lbParameters;
        private System.Windows.Forms.DataGridView dgRooms;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem fIleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.Button btnSelectAllCat;
        private System.Windows.Forms.Button btnSelectAllParams;
        private System.Windows.Forms.GroupBox grpSelection;
        private System.Windows.Forms.ToolStripMenuItem apriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salvaTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem esportaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jSONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem guidaToolStripMenuItem;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox grpExport;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox lbExportObjects;
        private System.Windows.Forms.DataGridView dgDocuments;
        private System.Windows.Forms.GroupBox grpData;
    }
}