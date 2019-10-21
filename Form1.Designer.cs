namespace DACSNM
{
    partial class Form1
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.RoutePrintButton = new System.Windows.Forms.Button();
            this.adaptersListButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(776, 368);
            this.dataGridView1.TabIndex = 0;
            // 
            // RoutePrintButton
            // 
            this.RoutePrintButton.Location = new System.Drawing.Point(655, 404);
            this.RoutePrintButton.Name = "RoutePrintButton";
            this.RoutePrintButton.Size = new System.Drawing.Size(133, 34);
            this.RoutePrintButton.TabIndex = 1;
            this.RoutePrintButton.Text = "Routing Table";
            this.RoutePrintButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.RoutePrintButton.UseVisualStyleBackColor = true;
            this.RoutePrintButton.Click += new System.EventHandler(this.RoutePrintButton_Click);
            // 
            // adaptersListButton
            // 
            this.adaptersListButton.Location = new System.Drawing.Point(459, 404);
            this.adaptersListButton.Name = "adaptersListButton";
            this.adaptersListButton.Size = new System.Drawing.Size(133, 34);
            this.adaptersListButton.TabIndex = 1;
            this.adaptersListButton.Text = "Adapters List";
            this.adaptersListButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.adaptersListButton.UseVisualStyleBackColor = true;
            this.adaptersListButton.Click += new System.EventHandler(this.adaptersListButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.adaptersListButton);
            this.Controls.Add(this.RoutePrintButton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button RoutePrintButton;
        private System.Windows.Forms.Button adaptersListButton;
    }
}

