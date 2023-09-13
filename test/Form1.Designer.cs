namespace test
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
            btnTest = new Button();
            dgTestGrid = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgTestGrid).BeginInit();
            SuspendLayout();
            // 
            // btnTest
            // 
            btnTest.Location = new Point(517, 12);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(75, 23);
            btnTest.TabIndex = 0;
            btnTest.Text = "Test";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnTest_Click;
            // 
            // dgTestGrid
            // 
            dgTestGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgTestGrid.Location = new Point(12, 12);
            dgTestGrid.Name = "dgTestGrid";
            dgTestGrid.RowTemplate.Height = 25;
            dgTestGrid.Size = new Size(488, 379);
            dgTestGrid.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgTestGrid);
            Controls.Add(btnTest);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgTestGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnTest;
        private DataGridView dgTestGrid;
    }
}