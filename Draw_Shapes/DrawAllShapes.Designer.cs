namespace Draw_Shapes
{
    partial class DrawAllShapes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawAllShapes));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gradientPanel1 = new Draw_Shapes.GradientPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.canvasBox = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.errorBox = new System.Windows.Forms.RichTextBox();
            this.syntaxChecker = new System.Windows.Forms.Button();
            this.multiLineCommandBox = new System.Windows.Forms.RichTextBox();
            this.singleLineCommandBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.gradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Indigo;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(953, 25);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Calisto MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.fileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fileToolStripMenuItem.Image")));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(57, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openFileToolStripMenuItem.Image")));
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveFileToolStripMenuItem.Image")));
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.saveFileToolStripMenuItem.Text = "Save File";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolStripMenuItem.Image")));
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Angle = 0F;
            this.gradientPanel1.BottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.gradientPanel1.Controls.Add(this.textBox1);
            this.gradientPanel1.Controls.Add(this.button4);
            this.gradientPanel1.Controls.Add(this.canvasBox);
            this.gradientPanel1.Controls.Add(this.button3);
            this.gradientPanel1.Controls.Add(this.button2);
            this.gradientPanel1.Controls.Add(this.errorBox);
            this.gradientPanel1.Controls.Add(this.syntaxChecker);
            this.gradientPanel1.Controls.Add(this.multiLineCommandBox);
            this.gradientPanel1.Controls.Add(this.singleLineCommandBox);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(953, 576);
            this.gradientPanel1.TabIndex = 5;
            this.gradientPanel1.TopColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBox1.Font = new System.Drawing.Font("Lucida Fax", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(456, 484);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(60, 20);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "0 Errors";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Navy;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Calisto MT", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.button4.Location = new System.Drawing.Point(64, 49);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(102, 24);
            this.button4.TabIndex = 9;
            this.button4.Text = "Command Box";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // canvasBox
            // 
            this.canvasBox.BackColor = System.Drawing.Color.White;
            this.canvasBox.Font = new System.Drawing.Font("Cambria", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.canvasBox.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.canvasBox.Location = new System.Drawing.Point(264, 75);
            this.canvasBox.Name = "canvasBox";
            this.canvasBox.Size = new System.Drawing.Size(686, 401);
            this.canvasBox.TabIndex = 0;
            this.canvasBox.Tag = "";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Navy;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Calisto MT", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.button3.Location = new System.Drawing.Point(548, 49);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 24);
            this.button3.TabIndex = 8;
            this.button3.Text = "Canvas ";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Navy;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Calisto MT", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.button2.Location = new System.Drawing.Point(665, 482);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 7;
            this.button2.Text = "Error Box";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // errorBox
            // 
            this.errorBox.BackColor = System.Drawing.Color.Black;
            this.errorBox.Font = new System.Drawing.Font("Modern No. 20", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorBox.ForeColor = System.Drawing.Color.Maroon;
            this.errorBox.Location = new System.Drawing.Point(403, 495);
            this.errorBox.Name = "errorBox";
            this.errorBox.Size = new System.Drawing.Size(547, 69);
            this.errorBox.TabIndex = 6;
            this.errorBox.Text = "";
            // 
            // syntaxChecker
            // 
            this.syntaxChecker.BackColor = System.Drawing.Color.MidnightBlue;
            this.syntaxChecker.FlatAppearance.BorderSize = 0;
            this.syntaxChecker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.syntaxChecker.Font = new System.Drawing.Font("Calisto MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.syntaxChecker.ForeColor = System.Drawing.SystemColors.Control;
            this.syntaxChecker.Location = new System.Drawing.Point(264, 511);
            this.syntaxChecker.Name = "syntaxChecker";
            this.syntaxChecker.Size = new System.Drawing.Size(133, 38);
            this.syntaxChecker.TabIndex = 5;
            this.syntaxChecker.Text = "Syntax Check";
            this.syntaxChecker.UseVisualStyleBackColor = false;
            this.syntaxChecker.Click += new System.EventHandler(this.button1_Click);
            this.syntaxChecker.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            this.syntaxChecker.MouseHover += new System.EventHandler(this.button1_MouseHover);
            // 
            // multiLineCommandBox
            // 
            this.multiLineCommandBox.BackColor = System.Drawing.Color.Linen;
            this.multiLineCommandBox.Font = new System.Drawing.Font("Lucida Fax", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.multiLineCommandBox.ForeColor = System.Drawing.Color.MidnightBlue;
            this.multiLineCommandBox.Location = new System.Drawing.Point(3, 75);
            this.multiLineCommandBox.Name = "multiLineCommandBox";
            this.multiLineCommandBox.Size = new System.Drawing.Size(255, 450);
            this.multiLineCommandBox.TabIndex = 1;
            this.multiLineCommandBox.Text = "";
            // 
            // singleLineCommandBox
            // 
            this.singleLineCommandBox.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.singleLineCommandBox.Font = new System.Drawing.Font("Lucida Fax", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.singleLineCommandBox.Location = new System.Drawing.Point(3, 531);
            this.singleLineCommandBox.Multiline = true;
            this.singleLineCommandBox.Name = "singleLineCommandBox";
            this.singleLineCommandBox.Size = new System.Drawing.Size(255, 38);
            this.singleLineCommandBox.TabIndex = 3;
            this.singleLineCommandBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            // 
            // DrawAllShapes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 576);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.gradientPanel1);
            this.Name = "DrawAllShapes";
            this.Text = "DrawAllShapes";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel canvasBox;
        private System.Windows.Forms.RichTextBox multiLineCommandBox;
        private System.Windows.Forms.TextBox singleLineCommandBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private GradientPanel gradientPanel1;
        private System.Windows.Forms.Button syntaxChecker;
        private System.Windows.Forms.RichTextBox errorBox;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox1;
    }
}