
namespace ParticulatesXMLLinq
{
    partial class Form
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
            this.LoadBtn = new System.Windows.Forms.Button();
            this.locationBtn = new System.Windows.Forms.Button();
            this.DateBtn = new System.Windows.Forms.Button();
            this.LargestParticulatesBtn = new System.Windows.Forms.Button();
            this.LocationcomboBox = new System.Windows.Forms.ComboBox();
            this.listBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // LoadBtn
            // 
            this.LoadBtn.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.LoadBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.LoadBtn.Location = new System.Drawing.Point(255, 12);
            this.LoadBtn.Name = "LoadBtn";
            this.LoadBtn.Size = new System.Drawing.Size(139, 67);
            this.LoadBtn.TabIndex = 0;
            this.LoadBtn.Text = "Load Data";
            this.LoadBtn.UseVisualStyleBackColor = false;
            this.LoadBtn.Click += new System.EventHandler(this.LoadBtn_Click);
            // 
            // locationBtn
            // 
            this.locationBtn.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.locationBtn.Enabled = false;
            this.locationBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.locationBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.locationBtn.Location = new System.Drawing.Point(110, 85);
            this.locationBtn.Name = "locationBtn";
            this.locationBtn.Size = new System.Drawing.Size(139, 67);
            this.locationBtn.TabIndex = 1;
            this.locationBtn.Text = "Display All Locations";
            this.locationBtn.UseVisualStyleBackColor = false;
            this.locationBtn.Click += new System.EventHandler(this.locationBtn_Click);
            // 
            // DateBtn
            // 
            this.DateBtn.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.DateBtn.Enabled = false;
            this.DateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DateBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.DateBtn.Location = new System.Drawing.Point(255, 85);
            this.DateBtn.Name = "DateBtn";
            this.DateBtn.Size = new System.Drawing.Size(139, 67);
            this.DateBtn.TabIndex = 2;
            this.DateBtn.Text = "Display All Dates";
            this.DateBtn.UseVisualStyleBackColor = false;
            this.DateBtn.Click += new System.EventHandler(this.DateBtn_Click);
            // 
            // LargestParticulatesBtn
            // 
            this.LargestParticulatesBtn.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.LargestParticulatesBtn.Enabled = false;
            this.LargestParticulatesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LargestParticulatesBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.LargestParticulatesBtn.Location = new System.Drawing.Point(412, 85);
            this.LargestParticulatesBtn.Name = "LargestParticulatesBtn";
            this.LargestParticulatesBtn.Size = new System.Drawing.Size(139, 67);
            this.LargestParticulatesBtn.TabIndex = 3;
            this.LargestParticulatesBtn.Text = "Display Biggest Reading";
            this.LargestParticulatesBtn.UseVisualStyleBackColor = false;
            this.LargestParticulatesBtn.Click += new System.EventHandler(this.LargestParticulatesBtn_Click);
            // 
            // LocationcomboBox
            // 
            this.LocationcomboBox.FormattingEnabled = true;
            this.LocationcomboBox.Location = new System.Drawing.Point(255, 158);
            this.LocationcomboBox.Name = "LocationcomboBox";
            this.LocationcomboBox.Size = new System.Drawing.Size(139, 24);
            this.LocationcomboBox.TabIndex = 4;
            this.LocationcomboBox.SelectedIndexChanged += new System.EventHandler(this.LocationcomboBox_SelectedIndexChanged);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 16;
            this.listBox.Location = new System.Drawing.Point(135, 208);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(416, 228);
            this.listBox.TabIndex = 5;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.LocationcomboBox);
            this.Controls.Add(this.LargestParticulatesBtn);
            this.Controls.Add(this.DateBtn);
            this.Controls.Add(this.locationBtn);
            this.Controls.Add(this.LoadBtn);
            this.Name = "Form";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoadBtn;
        private System.Windows.Forms.Button locationBtn;
        private System.Windows.Forms.Button DateBtn;
        private System.Windows.Forms.Button LargestParticulatesBtn;
        private System.Windows.Forms.ComboBox LocationcomboBox;
        private System.Windows.Forms.ListBox listBox;
    }
}

