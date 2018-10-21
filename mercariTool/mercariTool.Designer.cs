namespace mercariTool
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.login_label = new System.Windows.Forms.Label();
            this.getItemList_button = new System.Windows.Forms.Button();
            this.itemList_dataGridView = new System.Windows.Forms.DataGridView();
            this.allSell_button = new System.Windows.Forms.Button();
            this.checkDelete_button = new System.Windows.Forms.Button();
            this.itemCont_label = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.itemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IIne = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sale = new System.Windows.Forms.DataGridViewButtonColumn();
            this.chkdel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.itemList_dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // login_label
            // 
            this.login_label.AutoSize = true;
            this.login_label.BackColor = System.Drawing.Color.Orange;
            this.login_label.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.login_label.Location = new System.Drawing.Point(254, 27);
            this.login_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.login_label.Name = "login_label";
            this.login_label.Size = new System.Drawing.Size(122, 24);
            this.login_label.TabIndex = 6;
            this.login_label.Text = "　　　　　　　";
            // 
            // getItemList_button
            // 
            this.getItemList_button.Location = new System.Drawing.Point(704, 28);
            this.getItemList_button.Margin = new System.Windows.Forms.Padding(4);
            this.getItemList_button.Name = "getItemList_button";
            this.getItemList_button.Size = new System.Drawing.Size(120, 30);
            this.getItemList_button.TabIndex = 7;
            this.getItemList_button.Text = "出品一覧取得";
            this.getItemList_button.UseVisualStyleBackColor = true;
            this.getItemList_button.Click += new System.EventHandler(this.getItemList_button_click);
            // 
            // itemList_dataGridView
            // 
            this.itemList_dataGridView.AllowUserToAddRows = false;
            this.itemList_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.itemList_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.itemId,
            this.status,
            this.itemName,
            this.IIne,
            this.comment,
            this.sale,
            this.chkdel});
            this.itemList_dataGridView.Location = new System.Drawing.Point(13, 66);
            this.itemList_dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.itemList_dataGridView.Name = "itemList_dataGridView";
            this.itemList_dataGridView.RowTemplate.Height = 21;
            this.itemList_dataGridView.Size = new System.Drawing.Size(1150, 550);
            this.itemList_dataGridView.TabIndex = 8;
            this.itemList_dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.itemList_dataGridView_CellClick);
            // 
            // allSell_button
            // 
            this.allSell_button.Location = new System.Drawing.Point(870, 28);
            this.allSell_button.Margin = new System.Windows.Forms.Padding(4);
            this.allSell_button.Name = "allSell_button";
            this.allSell_button.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.allSell_button.Size = new System.Drawing.Size(100, 30);
            this.allSell_button.TabIndex = 9;
            this.allSell_button.Text = "全部再出品";
            this.allSell_button.UseVisualStyleBackColor = true;
            this.allSell_button.Click += new System.EventHandler(this.allSell_button_Click);
            // 
            // checkDelete_button
            // 
            this.checkDelete_button.Location = new System.Drawing.Point(978, 28);
            this.checkDelete_button.Margin = new System.Windows.Forms.Padding(4);
            this.checkDelete_button.Name = "checkDelete_button";
            this.checkDelete_button.Size = new System.Drawing.Size(100, 30);
            this.checkDelete_button.TabIndex = 10;
            this.checkDelete_button.Text = "選択削除";
            this.checkDelete_button.UseVisualStyleBackColor = true;
            this.checkDelete_button.Click += new System.EventHandler(this.checkDelete_button_Click);
            // 
            // itemCont_label
            // 
            this.itemCont_label.AutoSize = true;
            this.itemCont_label.Location = new System.Drawing.Point(832, 36);
            this.itemCont_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.itemCont_label.Name = "itemCont_label";
            this.itemCont_label.Size = new System.Drawing.Size(30, 15);
            this.itemCont_label.TabIndex = 11;
            this.itemCont_label.Text = "0件";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(442, 28);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 30);
            this.button1.TabIndex = 14;
            this.button1.Text = "PHPSESSIDログイン";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.PHPSESSID_Click);
            // 
            // itemId
            // 
            this.itemId.HeaderText = "id";
            this.itemId.Name = "itemId";
            this.itemId.ReadOnly = true;
            // 
            // status
            // 
            this.status.HeaderText = "ステータス";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            // 
            // itemName
            // 
            this.itemName.HeaderText = "出品名";
            this.itemName.Name = "itemName";
            this.itemName.ReadOnly = true;
            this.itemName.Width = 300;
            // 
            // IIne
            // 
            this.IIne.HeaderText = "いいね";
            this.IIne.Name = "IIne";
            this.IIne.ReadOnly = true;
            this.IIne.Width = 70;
            // 
            // comment
            // 
            this.comment.HeaderText = "コメント";
            this.comment.Name = "comment";
            this.comment.ReadOnly = true;
            this.comment.Width = 70;
            // 
            // sale
            // 
            this.sale.HeaderText = "出品";
            this.sale.Name = "sale";
            this.sale.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sale.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.sale.Text = "出品";
            this.sale.UseColumnTextForButtonValue = true;
            // 
            // chkdel
            // 
            this.chkdel.HeaderText = "選択削除";
            this.chkdel.Name = "chkdel";
            this.chkdel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 653);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.itemCont_label);
            this.Controls.Add(this.checkDelete_button);
            this.Controls.Add(this.allSell_button);
            this.Controls.Add(this.itemList_dataGridView);
            this.Controls.Add(this.getItemList_button);
            this.Controls.Add(this.login_label);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.itemList_dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label login_label;
        private System.Windows.Forms.Button getItemList_button;
        private System.Windows.Forms.DataGridView itemList_dataGridView;
        private System.Windows.Forms.Button allSell_button;
        private System.Windows.Forms.Button checkDelete_button;
        private System.Windows.Forms.Label itemCont_label;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IIne;
        private System.Windows.Forms.DataGridViewTextBoxColumn comment;
        private System.Windows.Forms.DataGridViewButtonColumn sale;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkdel;
    }
}

