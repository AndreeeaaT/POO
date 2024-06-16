namespace Proiect1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ListBox listBoxProduse;
        private System.Windows.Forms.ListBox listBoxProduseSelectate;
        private System.Windows.Forms.Button buttonAdaugaProdus;
        private System.Windows.Forms.Button buttonPlaseazaComanda;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.listBoxProduse = new System.Windows.Forms.ListBox();
            this.listBoxProduseSelectate = new System.Windows.Forms.ListBox();
            this.buttonAdaugaProdus = new System.Windows.Forms.Button();
            this.buttonPlaseazaComanda = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // toolStrip
            // 
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(800, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // listBoxProduse
            // 
            this.listBoxProduse.FormattingEnabled = true;
            this.listBoxProduse.Location = new System.Drawing.Point(12, 52);
            this.listBoxProduse.Name = "listBoxProduse";
            this.listBoxProduse.Size = new System.Drawing.Size(240, 381);
            this.listBoxProduse.TabIndex = 2;
            // 
            // listBoxProduseSelectate
            // 
            this.listBoxProduseSelectate.FormattingEnabled = true;
            this.listBoxProduseSelectate.Location = new System.Drawing.Point(528, 52);
            this.listBoxProduseSelectate.Name = "listBoxProduseSelectate";
            this.listBoxProduseSelectate.Size = new System.Drawing.Size(240, 381);
            this.listBoxProduseSelectate.TabIndex = 3;
            // 
            // buttonAdaugaProdus
            // 
            this.buttonAdaugaProdus.Location = new System.Drawing.Point(270, 52);
            this.buttonAdaugaProdus.Name = "buttonAdaugaProdus";
            this.buttonAdaugaProdus.Size = new System.Drawing.Size(240, 23);
            this.buttonAdaugaProdus.TabIndex = 4;
            this.buttonAdaugaProdus.Text = "Adauga Produs";
            this.buttonAdaugaProdus.UseVisualStyleBackColor = true;
            this.buttonAdaugaProdus.Click += new System.EventHandler(this.buttonAdaugaProdus_Click);
            // 
            // buttonPlaseazaComanda
            // 
            this.buttonPlaseazaComanda.Location = new System.Drawing.Point(270, 410);
            this.buttonPlaseazaComanda.Name = "buttonPlaseazaComanda";
            this.buttonPlaseazaComanda.Size = new System.Drawing.Size(240, 23);
            this.buttonPlaseazaComanda.TabIndex = 5;
            this.buttonPlaseazaComanda.Text = "Plaseaza Comanda";
            this.buttonPlaseazaComanda.UseVisualStyleBackColor = true;
            this.buttonPlaseazaComanda.Click += new System.EventHandler(this.buttonPlaseazaComanda_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonPlaseazaComanda);
            this.Controls.Add(this.buttonAdaugaProdus);
            this.Controls.Add(this.listBoxProduseSelectate);
            this.Controls.Add(this.listBoxProduse);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
