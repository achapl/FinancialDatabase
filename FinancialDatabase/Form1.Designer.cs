namespace FinancialDatabase
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
            this.manualQuery = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.QueryTab = new System.Windows.Forms.TabPage();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.QueryTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // manualQuery
            // 
            this.manualQuery.Location = new System.Drawing.Point(347, 371);
            this.manualQuery.Name = "manualQuery";
            this.manualQuery.Size = new System.Drawing.Size(94, 23);
            this.manualQuery.TabIndex = 1;
            this.manualQuery.Text = "Manual Query";
            this.manualQuery.UseVisualStyleBackColor = true;
            this.manualQuery.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(461, 36);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(672, 355);
            this.listBox1.TabIndex = 2;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 374);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(335, 20);
            this.textBox1.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.QueryTab);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1147, 426);
            this.tabControl1.TabIndex = 5;
            // 
            // QueryTab
            // 
            this.QueryTab.Controls.Add(this.checkBox5);
            this.QueryTab.Controls.Add(this.checkBox4);
            this.QueryTab.Controls.Add(this.checkBox3);
            this.QueryTab.Controls.Add(this.listBox1);
            this.QueryTab.Controls.Add(this.checkBox2);
            this.QueryTab.Controls.Add(this.checkBox1);
            this.QueryTab.Controls.Add(this.label2);
            this.QueryTab.Controls.Add(this.label1);
            this.QueryTab.Controls.Add(this.dateTimePicker2);
            this.QueryTab.Controls.Add(this.dateTimePicker1);
            this.QueryTab.Controls.Add(this.searchButton);
            this.QueryTab.Controls.Add(this.searchBox);
            this.QueryTab.Controls.Add(this.comboBox1);
            this.QueryTab.Controls.Add(this.textBox1);
            this.QueryTab.Controls.Add(this.manualQuery);
            this.QueryTab.Controls.Add(this.menuStrip1);
            this.QueryTab.Location = new System.Drawing.Point(4, 22);
            this.QueryTab.Name = "QueryTab";
            this.QueryTab.Padding = new System.Windows.Forms.Padding(3);
            this.QueryTab.Size = new System.Drawing.Size(1139, 400);
            this.QueryTab.TabIndex = 0;
            this.QueryTab.Text = "Query";
            this.QueryTab.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(220, 126);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(67, 17);
            this.checkBox2.TabIndex = 14;
            this.checkBox2.Text = "Sold Out";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(220, 102);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(66, 17);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "In Stock";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Bought Before";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(142, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Bought After";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(220, 75);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(203, 20);
            this.dateTimePicker2.TabIndex = 10;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(220, 49);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(203, 20);
            this.dateTimePicker1.TabIndex = 9;
            this.dateTimePicker1.Value = new System.DateTime(2020, 1, 1, 0, 1, 0, 0);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(347, 7);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(94, 23);
            this.searchButton.TabIndex = 8;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // searchBox
            // 
            this.searchBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.searchBox.Location = new System.Drawing.Point(6, 7);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(208, 20);
            this.searchBox.TabIndex = 7;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Item",
            "Fee"});
            this.comboBox1.Location = new System.Drawing.Point(220, 7);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(3, 3);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1133, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1151, 400);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(461, 11);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(49, 17);
            this.checkBox3.TabIndex = 15;
            this.checkBox3.Text = "Date";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Location = new System.Drawing.Point(516, 11);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(54, 17);
            this.checkBox4.TabIndex = 16;
            this.checkBox4.Text = "Name";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(576, 11);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(98, 17);
            this.checkBox5.TabIndex = 17;
            this.checkBox5.Text = "Purchase Price";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1171, 450);
            this.Controls.Add(this.tabControl1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.QueryTab.ResumeLayout(false);
            this.QueryTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button manualQuery;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage QueryTab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
    }
}

