namespace ScavengerHuntMaker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog = new OpenFileDialog();
            this.loadButton = new Button();
            this.statusLabel = new Label();
            this.SuspendLayout();

            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "CSV Files|*.csv";

            // 
            // loadButton
            // 
            this.loadButton.Location = new Point(50, 50);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new Size(200, 50);
            this.loadButton.TabIndex = 0;
            this.loadButton.Text = "Load CSV";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new EventHandler(this.LoadCsv);

            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new Point(50, 120);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new Size(200, 25);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "No file loaded.";

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(400, 200);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.statusLabel);
            this.Name = "Form1";
            this.Text = "Scavenger Hunt Maker";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog;
        private Button loadButton;
        private Label statusLabel;
    }
}
