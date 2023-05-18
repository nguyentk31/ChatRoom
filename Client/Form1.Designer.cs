namespace Client
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
            richTextBox1 = new RichTextBox();
            tbMess = new TextBox();
            btSend = new Button();
            label1 = new Label();
            label2 = new Label();
            tbName = new TextBox();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 12);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(660, 298);
            richTextBox1.TabIndex = 0;
            richTextBox1.Text = "";
            // 
            // tbMess
            // 
            tbMess.Location = new Point(12, 421);
            tbMess.Name = "tbMess";
            tbMess.Size = new Size(530, 25);
            tbMess.TabIndex = 1;
            // 
            // btSend
            // 
            btSend.Location = new Point(548, 416);
            btSend.Name = "btSend";
            btSend.Size = new Size(124, 33);
            btSend.TabIndex = 2;
            btSend.Text = "Send";
            btSend.UseVisualStyleBackColor = true;
            btSend.Click += btSend_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 393);
            label1.Name = "label1";
            label1.Size = new Size(63, 19);
            label1.TabIndex = 3;
            label1.Text = "Message";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 325);
            label2.Name = "label2";
            label2.Size = new Size(75, 19);
            label2.TabIndex = 5;
            label2.Text = "Your name";
            // 
            // tbName
            // 
            tbName.Location = new Point(12, 356);
            tbName.Name = "tbName";
            tbName.Size = new Size(302, 25);
            tbName.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(684, 461);
            Controls.Add(label2);
            Controls.Add(tbName);
            Controls.Add(label1);
            Controls.Add(btSend);
            Controls.Add(tbMess);
            Controls.Add(richTextBox1);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "Client";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox richTextBox1;
        private TextBox tbMess;
        private Button btSend;
        private Label label1;
        private Label label2;
        private TextBox tbName;
    }
}