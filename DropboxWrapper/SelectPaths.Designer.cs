namespace DropboxWrapper
{
    partial class SelectPaths
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.uploadTypeLabel = new System.Windows.Forms.Label();
            this.wrapFolderLabel = new System.Windows.Forms.Label();
            this.wrapFolderPathLabel = new System.Windows.Forms.Label();
            this.chooseWrapFolderBtn = new System.Windows.Forms.Button();
            this.chooseDropboxFolderBtn = new System.Windows.Forms.Button();
            this.dropboxFolderLabel = new System.Windows.Forms.Label();
            this.dropboxFolderPathLabel = new System.Windows.Forms.Label();
            this.chooseCompressionAppFolderBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.compressionApplicationFolderPathLabel = new System.Windows.Forms.Label();
            this.uploadTypeCheckbox = new System.Windows.Forms.ComboBox();
            this.folderSelect = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.uploadTypeLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.wrapFolderLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.wrapFolderPathLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chooseWrapFolderBtn, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.chooseDropboxFolderBtn, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.dropboxFolderLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dropboxFolderPathLabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.chooseCompressionAppFolderBtn, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(683, 90);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // uploadTypeLabel
            // 
            this.uploadTypeLabel.AutoSize = true;
            this.uploadTypeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uploadTypeLabel.Location = new System.Drawing.Point(3, 60);
            this.uploadTypeLabel.Name = "uploadTypeLabel";
            this.uploadTypeLabel.Size = new System.Drawing.Size(198, 30);
            this.uploadTypeLabel.TabIndex = 10;
            this.uploadTypeLabel.Text = "Upload type";
            this.uploadTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // wrapFolderLabel
            // 
            this.wrapFolderLabel.AutoSize = true;
            this.wrapFolderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wrapFolderLabel.Location = new System.Drawing.Point(3, 0);
            this.wrapFolderLabel.Name = "wrapFolderLabel";
            this.wrapFolderLabel.Size = new System.Drawing.Size(198, 30);
            this.wrapFolderLabel.TabIndex = 1;
            this.wrapFolderLabel.Text = "Wrap folder";
            this.wrapFolderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // wrapFolderPathLabel
            // 
            this.wrapFolderPathLabel.AutoSize = true;
            this.wrapFolderPathLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.wrapFolderPathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wrapFolderPathLabel.Location = new System.Drawing.Point(207, 3);
            this.wrapFolderPathLabel.Margin = new System.Windows.Forms.Padding(3);
            this.wrapFolderPathLabel.Name = "wrapFolderPathLabel";
            this.wrapFolderPathLabel.Size = new System.Drawing.Size(403, 24);
            this.wrapFolderPathLabel.TabIndex = 2;
            this.wrapFolderPathLabel.Text = "Path to wrap folder";
            this.wrapFolderPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chooseWrapFolderBtn
            // 
            this.chooseWrapFolderBtn.Location = new System.Drawing.Point(633, 3);
            this.chooseWrapFolderBtn.Margin = new System.Windows.Forms.Padding(20, 3, 10, 3);
            this.chooseWrapFolderBtn.Name = "chooseWrapFolderBtn";
            this.chooseWrapFolderBtn.Size = new System.Drawing.Size(40, 22);
            this.chooseWrapFolderBtn.TabIndex = 3;
            this.chooseWrapFolderBtn.Text = "...";
            this.chooseWrapFolderBtn.UseVisualStyleBackColor = true;
            this.chooseWrapFolderBtn.Click += new System.EventHandler(this.chooseWrapFolderBtn_Click);
            // 
            // chooseDropboxFolderBtn
            // 
            this.chooseDropboxFolderBtn.Location = new System.Drawing.Point(633, 33);
            this.chooseDropboxFolderBtn.Margin = new System.Windows.Forms.Padding(20, 3, 10, 3);
            this.chooseDropboxFolderBtn.Name = "chooseDropboxFolderBtn";
            this.chooseDropboxFolderBtn.Size = new System.Drawing.Size(40, 23);
            this.chooseDropboxFolderBtn.TabIndex = 4;
            this.chooseDropboxFolderBtn.Text = "...";
            this.chooseDropboxFolderBtn.UseVisualStyleBackColor = true;
            this.chooseDropboxFolderBtn.Click += new System.EventHandler(this.chooseDropboxFolderBtn_Click);
            // 
            // dropboxFolderLabel
            // 
            this.dropboxFolderLabel.AutoSize = true;
            this.dropboxFolderLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dropboxFolderLabel.Location = new System.Drawing.Point(3, 30);
            this.dropboxFolderLabel.Name = "dropboxFolderLabel";
            this.dropboxFolderLabel.Size = new System.Drawing.Size(198, 30);
            this.dropboxFolderLabel.TabIndex = 6;
            this.dropboxFolderLabel.Text = "Dropbox folder";
            this.dropboxFolderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dropboxFolderPathLabel
            // 
            this.dropboxFolderPathLabel.AutoSize = true;
            this.dropboxFolderPathLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dropboxFolderPathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dropboxFolderPathLabel.Location = new System.Drawing.Point(207, 33);
            this.dropboxFolderPathLabel.Margin = new System.Windows.Forms.Padding(3);
            this.dropboxFolderPathLabel.Name = "dropboxFolderPathLabel";
            this.dropboxFolderPathLabel.Size = new System.Drawing.Size(403, 24);
            this.dropboxFolderPathLabel.TabIndex = 8;
            this.dropboxFolderPathLabel.Text = "Path to Dropbox folder";
            this.dropboxFolderPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chooseCompressionAppFolderBtn
            // 
            this.chooseCompressionAppFolderBtn.Location = new System.Drawing.Point(633, 63);
            this.chooseCompressionAppFolderBtn.Margin = new System.Windows.Forms.Padding(20, 3, 10, 3);
            this.chooseCompressionAppFolderBtn.Name = "chooseCompressionAppFolderBtn";
            this.chooseCompressionAppFolderBtn.Size = new System.Drawing.Size(40, 23);
            this.chooseCompressionAppFolderBtn.TabIndex = 11;
            this.chooseCompressionAppFolderBtn.Text = "...";
            this.chooseCompressionAppFolderBtn.UseVisualStyleBackColor = true;
            this.chooseCompressionAppFolderBtn.Click += new System.EventHandler(this.chooseCompressionAppFolderBtn_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.Controls.Add(this.compressionApplicationFolderPathLabel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.uploadTypeCheckbox, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(207, 63);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(403, 24);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // compressionApplicationFolderPathLabel
            // 
            this.compressionApplicationFolderPathLabel.AutoSize = true;
            this.compressionApplicationFolderPathLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.compressionApplicationFolderPathLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.compressionApplicationFolderPathLabel.Location = new System.Drawing.Point(123, 3);
            this.compressionApplicationFolderPathLabel.Margin = new System.Windows.Forms.Padding(3);
            this.compressionApplicationFolderPathLabel.Name = "compressionApplicationFolderPathLabel";
            this.compressionApplicationFolderPathLabel.Size = new System.Drawing.Size(277, 18);
            this.compressionApplicationFolderPathLabel.TabIndex = 9;
            this.compressionApplicationFolderPathLabel.Text = "Compression application folder";
            this.compressionApplicationFolderPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uploadTypeCheckbox
            // 
            this.uploadTypeCheckbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uploadTypeCheckbox.FormattingEnabled = true;
            this.uploadTypeCheckbox.Location = new System.Drawing.Point(3, 3);
            this.uploadTypeCheckbox.Name = "uploadTypeCheckbox";
            this.uploadTypeCheckbox.Size = new System.Drawing.Size(114, 21);
            this.uploadTypeCheckbox.Sorted = true;
            this.uploadTypeCheckbox.TabIndex = 0;
            this.uploadTypeCheckbox.SelectedIndexChanged += new System.EventHandler(this.uploadTypeCheckbox_SelectedIndexChanged);
            // 
            // SelectPaths
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 90);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SelectPaths";
            this.Text = "Select paths";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label wrapFolderLabel;
        private System.Windows.Forms.Label wrapFolderPathLabel;
        private System.Windows.Forms.Button chooseWrapFolderBtn;
        private System.Windows.Forms.Button chooseDropboxFolderBtn;
        private System.Windows.Forms.Label dropboxFolderLabel;
        private System.Windows.Forms.Label dropboxFolderPathLabel;
        private System.Windows.Forms.FolderBrowserDialog folderSelect;
        private System.Windows.Forms.Label uploadTypeLabel;
        private System.Windows.Forms.Button chooseCompressionAppFolderBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label compressionApplicationFolderPathLabel;
        private System.Windows.Forms.ComboBox uploadTypeCheckbox;
    }
}