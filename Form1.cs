using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Proiect1
{
    public partial class Form1 : Form
    {
        private List<Produs> produse;
        private List<Produs> produseSelectate;
        private const string FacturiDirectory = "Facturi";
        private const string ResourcesDirectory = "Resources";
        private const string ModelFacturaFile = "Model_factura.pdf";

        public Form1()
        {
            InitializeComponent();
            produse = new List<Produs>();
            produseSelectate = new List<Produs>();
            CitesteProduseDinXml();
            AfiseazaProduse();
            CreazaDirectorDacaNuExista(FacturiDirectory);
            CreazaDirectorDacaNuExista(ResourcesDirectory);
        }

        private void CreazaDirectorDacaNuExista(string director)
        {
            if (!Directory.Exists(director))
            {
                Directory.CreateDirectory(director);
            }
        }

        private void CitesteProduseDinXml()
        {
            XDocument xdoc = XDocument.Load("produse.xml");
            produse = (from p in xdoc.Descendants("produs")
                       select new Produs
                       {
                           Cod = (int)p.Element("cod"),
                           Denumire = (string)p.Element("denumire"),
                           Descriere = (string)p.Element("descriere"),
                           Pret = (decimal)p.Element("pret"),
                           Stoc = (int)p.Element("stoc")
                       }).ToList();
        }

        private void AfiseazaProduse()
        {
            listBoxProduse.Items.Clear();
            foreach (var produs in produse)
            {
                listBoxProduse.Items.Add(produs);
            }
        }

        private void buttonAdaugaProdus_Click(object sender, EventArgs e)
        {
            foreach (Produs produsSelectat in listBoxProduse.SelectedItems)
            {
                if (produsSelectat != null && produsSelectat.Stoc > 0)
                {
                    produseSelectate.Add(produsSelectat);
                    produsSelectat.Stoc--;
                }
            }
            AfiseazaProduseSelectate();
            AfiseazaProduse();
        }

        private void AfiseazaProduseSelectate()
        {
            listBoxProduseSelectate.Items.Clear();
            foreach (var produs in produseSelectate)
            {
                listBoxProduseSelectate.Items.Add(produs);
            }
        }

        private void buttonStergeProdus_Click(object sender, EventArgs e)
        {
            var produseDeSters = listBoxProduseSelectate.SelectedItems.Cast<Produs>().ToList();

            foreach (var produs in produseDeSters)
            {
                produseSelectate.Remove(produs);
                produs.Stoc++;
            }

            AfiseazaProduseSelectate();
            AfiseazaProduse();
        }

        private void buttonPlaseazaComanda_Click(object sender, EventArgs e)
        {
            ActualizeazaStocInXml();
            GenereazaFacturaPdf();
            MessageBox.Show("Comanda a fost plasată și factura a fost generată!");

            // Afiseaza dialog pentru introducerea datelor clientului
            var dialog = new ClientDialog();
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Salveaza datele clientului
                string nume = dialog.Nume;
                string prenume = dialog.Prenume;
                string adresa = dialog.Adresa;

                // Scrie comanda in fisier text
                ScrieComanda(nume, prenume, adresa);
            }
        }

        private void ActualizeazaStocInXml()
        {
            XDocument xdoc = XDocument.Load("produse.xml");

            foreach (var produs in produseSelectate)
            {
                var xmlProdus = xdoc.Descendants("produs")
                                    .FirstOrDefault(p => (int)p.Element("cod") == produs.Cod);

                if (xmlProdus != null)
                {
                    int stocActual = int.Parse(xmlProdus.Element("stoc").Value);
                    xmlProdus.Element("stoc").Value = (stocActual - 1).ToString();
                }
            }

            xdoc.Save("produse.xml");
        }

        private void GenereazaFacturaPdf()
        {
            try
            {
                string modelPath = Path.Combine(ResourcesDirectory, ModelFacturaFile);
                string numeFisierFactura = Path.Combine(FacturiDirectory, $"Factura_{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf");

                using (PdfDocument document = PdfReader.Open(modelPath, PdfDocumentOpenMode.Modify))
                {
                    PdfPage page = document.Pages[0];
                    XGraphics gfx = XGraphics.FromPdfPage(page);

                    XFont font = new XFont("Verdana", 12, XFontStyle.Regular);

                    // Afișează detalii client
                    gfx.DrawString("Nume Client: Exemplu", font, XBrushes.Black, new XRect(150, 100, page.Width, page.Height), XStringFormats.TopLeft);
                    gfx.DrawString("Adresa: Str. Exemplu nr. 1", font, XBrushes.Black, new XRect(150, 120, page.Width, page.Height), XStringFormats.TopLeft);

                    // Afișează detalii produse
                    int yPoint = 200;

                    foreach (var produs in produseSelectate)
                    {
                        gfx.DrawString(produs.Denumire, font, XBrushes.Black, new XRect(50, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                        gfx.DrawString(produs.Pret.ToString(), font, XBrushes.Black, new XRect(300, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                        gfx.DrawString("1", font, XBrushes.Black, new XRect(350, yPoint, page.Width, page.Height), XStringFormats.TopLeft); // Cantitate
                        gfx.DrawString((produs.Pret * 0.19m).ToString(), font, XBrushes.Black, new XRect(400, yPoint, page.Width, page.Height), XStringFormats.TopLeft); // TVA
                        gfx.DrawString((produs.Pret * 1.19m).ToString(), font, XBrushes.Black, new XRect(450, yPoint, page.Width, page.Height), XStringFormats.TopLeft); // Total

                        yPoint += 20;
                    }

                    // Salvează factura generată
                    document.Save(numeFisierFactura);

                    // Verificare dacă fișierul a fost creat
                    if (File.Exists(numeFisierFactura))
                    {
                        Console.WriteLine($"Factura generată cu succes: {numeFisierFactura}");
                    }
                    else
                    {
                        Console.WriteLine("Eroare: Fișierul facturii nu a fost creat.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la generarea facturii: {ex.Message}");
            }
        }


        private void ScrieComanda(string nume, string prenume, string adresa)
        {
            // Generarea numelui fisierului pentru comanda
            string numeFisier = Path.Combine(FacturiDirectory, $"{nume}_{prenume}_Comanda.txt");

            using (StreamWriter writer = new StreamWriter(numeFisier))
            {
                // Scrierea datelor clientului
                writer.WriteLine($"Nume: {nume} {prenume}");
                writer.WriteLine($"Adresa: {adresa}");

                // Scrierea detaliilor comenzii (cod produs - cantitate)
                foreach (var produs in produseSelectate)
                {
                    writer.WriteLine($"{produs.Cod} - 1"); // Se poate modifica cantitatea dupa necesitate
                }
            }
        }




        // Clasa Produs

        public class Produs
        {
            public int Cod { get; set; }
            public string Denumire { get; set; }
            public string Descriere { get; set; }
            public decimal Pret { get; set; }
            public int Stoc { get; set; }

            public override string ToString()
            {
                return $"{Denumire} - {Pret} RON (Stoc: {Stoc})";
            }
        }
    }

    public class ClientDialog : Form
    {
        private Label labelNume;
        private Label labelPrenume;
        private Label labelAdresa;
        private TextBox textBoxNume;
        private TextBox textBoxPrenume;
        private TextBox textBoxAdresa;
        private Button buttonOK;
        private Button buttonCancel;

        public string Nume => textBoxNume.Text;
        public string Prenume => textBoxPrenume.Text;
        public string Adresa => textBoxAdresa.Text;

        public ClientDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {

            // Initializare si pozitionare controale
            this.labelNume = new Label();
            this.labelPrenume = new Label();
            this.labelAdresa = new Label();
            this.textBoxNume = new TextBox();
            this.textBoxPrenume = new TextBox();
            this.textBoxAdresa = new TextBox();
            this.buttonOK = new Button();
            this.buttonCancel = new Button();
            this.SuspendLayout();

            // labelNume
            this.labelNume.AutoSize = true;
            this.labelNume.Location = new Point(12, 15);
            this.labelNume.Name = "labelNume";
            this.labelNume.Size = new Size(45, 13);
            this.labelNume.TabIndex = 0;
            this.labelNume.Text = "Nume:";

            // labelPrenume
            this.labelPrenume.AutoSize = true;
            this.labelPrenume.Location = new Point(12, 41);
            this.labelPrenume.Name = "labelPrenume";
            this.labelPrenume.Size = new Size(55, 13);
            this.labelPrenume.TabIndex = 1;
            this.labelPrenume.Text = "Prenume:";

            // labelAdresa
            this.labelAdresa.AutoSize = true;
            this.labelAdresa.Location = new Point(12, 67);
            this.labelAdresa.Name = "labelAdresa";
            this.labelAdresa.Size = new Size(43, 13);
            this.labelAdresa.TabIndex = 2;
            this.labelAdresa.Text = "Adresa:";

            // textBoxNume
            this.textBoxNume.Location = new Point(73, 12);
            this.textBoxNume.Name = "textBoxNume";
            this.textBoxNume.Size = new Size(200, 20);
            this.textBoxNume.TabIndex = 3;

            // textBoxPrenume
            this.textBoxPrenume.Location = new Point(73, 38);
            this.textBoxPrenume.Name = "textBoxPrenume";
            this.textBoxPrenume.Size = new Size(200, 20);
            this.textBoxPrenume.TabIndex = 4;

            // textBoxAdresa
            this.textBoxAdresa.Location = new Point(73, 64);
            this.textBoxAdresa.Name = "textBoxAdresa";
            this.textBoxAdresa.Size = new Size(200, 20);
            this.textBoxAdresa.TabIndex = 5;

            // buttonOK
            this.buttonOK.Location = new Point(116, 100);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new EventHandler(this.buttonOK_Click);

            // buttonCancel
            this.buttonCancel.Location = new Point(197, 100);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);

            // Adaugare controale la form
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxAdresa);
            this.Controls.Add(this.textBoxPrenume);
            this.Controls.Add(this.textBoxNume);
            this.Controls.Add(this.labelAdresa);
            this.Controls.Add(this.labelPrenume);
            this.Controls.Add(this.labelNume);
            this.ResumeLayout(false);
            this.PerformLayout();



        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}