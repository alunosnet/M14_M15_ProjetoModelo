namespace M14_M15_ProjetoModelo
{
    partial class frDevolve
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
            this.cbLivros = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbLivros
            // 
            this.cbLivros.FormattingEnabled = true;
            this.cbLivros.Location = new System.Drawing.Point(90, 56);
            this.cbLivros.Name = "cbLivros";
            this.cbLivros.Size = new System.Drawing.Size(277, 21);
            this.cbLivros.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(104, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(253, 81);
            this.button1.TabIndex = 1;
            this.button1.Text = "Devolver";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frDevolve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 364);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbLivros);
            this.Name = "frDevolve";
            this.Text = "frDevolve";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLivros;
        private System.Windows.Forms.Button button1;
    }
}