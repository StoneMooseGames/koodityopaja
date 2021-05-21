using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //Alustukset
        static int seed = DateTime.Now.Day * DateTime.Now.Hour * DateTime.Now.Minute * DateTime.Now.Second;
        static Random satunnaisLuku = new Random(seed);
        static int tavoiteLuku = 0;
        static int[] arvotutNumerot = new int[6];
        static string pelaajanNimi = "Mika";
        
        static int pelaajanPisteet = 0;
        static int pikkuLukujenMaara = 1;
        static List<Button> nappiLista = new List<Button>();
        static List<Button> alempiNappiLista = new List<Button>();
        static List<Button> aritmetiikkaNapit = new List<Button>();
        static List<String> aritmetiikkaMerkit = new List<String>();
        static Label tavoiteNumeroCanva = new Label();
        static Label kello = new Label();
        static int kelloAika = 30;
        static Timer kelloAjastin = new Timer();
        static Timer randomNumeroLabelinTImer = new Timer();
        static TextBox pelaajanLoppuTulos = new TextBox();
        static Label virheIkkuna = new Label();




        public Form1()
        {
            InitializeComponent();
            nappiLista.Add(button14);
            nappiLista.Add(button15);
            nappiLista.Add(button16);
            nappiLista.Add(button17);
            nappiLista.Add(button18);
            nappiLista.Add(button19);
            alempiNappiLista.Add(button8);
            alempiNappiLista.Add(button9);
            alempiNappiLista.Add(button10);
            alempiNappiLista.Add(button11);
            alempiNappiLista.Add(button12);
            alempiNappiLista.Add(button13);
            aritmetiikkaNapit.Add(button1);
            aritmetiikkaNapit.Add(button4);
            aritmetiikkaNapit.Add(button5);
            aritmetiikkaNapit.Add(button6);
            aritmetiikkaNapit.Add(button7);
            aritmetiikkaMerkit.Add("+");
            aritmetiikkaMerkit.Add("-");
            aritmetiikkaMerkit.Add("*");
            aritmetiikkaMerkit.Add("/");

            label10.Text = "";
            label6.Text = pelaajanPisteet.ToString();
            label9.Text = pelaajanNimi;
            tavoiteNumeroCanva = label2;
            kello = label3;
            randomNumeroLabelinTImer = timer2;
            label3.Text = kelloAika.ToString();
            kelloAjastin = timer1;
            
            virheIkkuna = label11;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < nappiLista.Count; i++)
            {
                nappiLista[i].Text = "";
            }
            for (int i = 0; i < alempiNappiLista.Count; i++)
            {
                alempiNappiLista[i].Text = "";
            }
            for (int i = 0; i < aritmetiikkaNapit.Count; i++)
            {
                aritmetiikkaNapit[i].Text = "";
            }
        }


        //Aloita
        private void button2_Click(object sender, EventArgs e)
        {
            button20.Enabled = false; //et voi enää valita pienten numeroiden määrää
            button2.Enabled = false; //et voi enää painaa aloita nappia kun peli on päällä
            PeliLooppi();
        }

        //Valitse pienten numeroiden määrä
        private void button20_Click(object sender, EventArgs e)
        {
            switch (button20.Text)
            {
                case "1":
                    button20.Text = "2";
                    pikkuLukujenMaara++;
                    break;
                case "2":
                    button20.Text = "3";
                    pikkuLukujenMaara++;
                    break;
                case "3":
                    button20.Text = "4";
                    pikkuLukujenMaara++;
                    break;
                case "4":
                    button20.Text = "5";
                    pikkuLukujenMaara++;
                    break;
                case "5":
                    button20.Text = "6";
                    pikkuLukujenMaara++;
                    break;
                case "6":
                    button20.Text = "1";
                    pikkuLukujenMaara = 1;
                    break;

            }

        }
        static void PeliLooppi()
        {
            
            int pikkuLukuMaara = ArvoPienetLuvut();
            ArvoIsotLuvut(pikkuLukuMaara);

            int tavoiteLuku = ArvoLoppuTulos();
            PeliKelloKayntiin();
            TarkistaVastaus();
            //annetaan pisteet
            //kysytään haluaako uuden kierroksen, jos ei palataan valikkoon
        }
        static int ArvoPienetLuvut()
        {
            //arvotaan 1-6 kappaletta lukuväliltä 1-10
            
            int pikkuLukuMaara = 0;
           
            pikkuLukuMaara = pikkuLukujenMaara;
                   
                    for (int i = 0; i <= pikkuLukuMaara; i++)
                    {
                        int pikkuLuku = SatunnaisLukuGeneraattori(0);
                        arvotutNumerot[i] = pikkuLuku;
                        nappiLista[i].Text = pikkuLuku.ToString();
                    }
              
                
            
            return pikkuLukuMaara;
        }
        static void ArvoIsotLuvut(int pikkuLukuMaara)
        {
            //arvotaan 1-6 kappaletta luvuista 25,50,75,100, riippuen siitä kuinka monta pikkulukua on otettu
            int isoLukuMaara = 6 - pikkuLukuMaara;
            
            for (int i = pikkuLukuMaara; i < 6; i++)
            {
                int isoLuku = SatunnaisLukuGeneraattori(1);
                arvotutNumerot[i] = isoLuku;
                nappiLista[i].Text = isoLuku.ToString();
            }

        }

        static int ArvoLoppuTulos()
        {
            //arvotaan luku 100-999
            tavoiteLuku = SatunnaisLukuGeneraattori(2);
            tavoiteNumeroCanva.Text = tavoiteLuku.ToString();
            return tavoiteLuku;


        }

        static void PeliKelloKayntiin()
        {
            
            kelloAjastin.Start();
            
        }

        static void LaskeLoppuTulos()
        {
            //lasketaan lopputulos reaaliajassa sitä mukaa kun pelaaja vaihtaa numeroita ja merkkejä
        }

        static void TarkistaVastaus()
        {
            //TODO mieti millaisessa formaatissa haluat vastauksen ja miten tarkistat.
            //nyt vastaus tulee pelkkänä lukuna, eikä yhtälön toimivuudesta tai siitä miten olet lukuun päässyt ole tarkistusta
            //tarkistetaan oliko pelaajan lasku oikein
            

            
        }
        static void PisteenLasku()
        {
            //annetaan pisteet sen mukaan miten lasku onnistui
        }

        static int SatunnaisLukuGeneraattori(int arvontamoodi)
        {
            //Jotta saataisiin mahdollisimman random numero, ehkä kellonaika numerona voisi olla mainio seedille randomiin?
            int arvottuNumero = 0;
            int[] arvottavatNumerot = { 25, 50, 75, 100 };
            switch (arvontamoodi)
            {
                case 0:
                    arvottuNumero = satunnaisLuku.Next(1, 10);
                    return arvottuNumero;
                case 1:
                    int valintaTaulukosta = satunnaisLuku.Next(0, 3);
                    arvottuNumero = arvottavatNumerot[valintaTaulukosta];
                    return arvottuNumero;
                case 2:
                    arvottuNumero = satunnaisLuku.Next(100, 999);
                    return arvottuNumero;
                default:
                    return 0;
            }


        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            kelloAika--;
            if (kelloAika == 0)
            {
                kelloAjastin.Stop();
                label10.Text = "Aika Loppui";
                for (int i = 0; i < nappiLista.Count; i++)
                {
                    nappiLista[i].Enabled = false;
                    alempiNappiLista[i].Enabled = false;
                    
                }
                for (int i = 0; i < aritmetiikkaNapit.Count; i++)
                {
                    aritmetiikkaNapit[i].Enabled = false;

                }
            }
            kello.Text = kelloAika.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int etsiVapaaRuutu = EtsiTyhjaNappiAlaRivista();
            if (etsiVapaaRuutu >= 0 && etsiVapaaRuutu <= 5)
            {
                alempiNappiLista[etsiVapaaRuutu].Text = nappiLista[0].Text;
                nappiLista[0].Text = "";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            int etsiVapaaRuutu = EtsiTyhjaNappiAlaRivista();
            if (etsiVapaaRuutu >= 0 && etsiVapaaRuutu <= 5)
            {
                alempiNappiLista[etsiVapaaRuutu].Text = nappiLista[1].Text;
                nappiLista[1].Text = "";
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            int etsiVapaaRuutu = EtsiTyhjaNappiAlaRivista();
            if (etsiVapaaRuutu >= 0 && etsiVapaaRuutu <= 5)
            {
                alempiNappiLista[etsiVapaaRuutu].Text = nappiLista[2].Text;
                nappiLista[2].Text = "";
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            int etsiVapaaRuutu = EtsiTyhjaNappiAlaRivista();
            if (etsiVapaaRuutu >= 0 && etsiVapaaRuutu <= 5)
            {
                alempiNappiLista[etsiVapaaRuutu].Text = nappiLista[3].Text;
                nappiLista[3].Text = "";
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            int etsiVapaaRuutu = EtsiTyhjaNappiAlaRivista();
            if (etsiVapaaRuutu >= 0 && etsiVapaaRuutu <= 5)
            {
                alempiNappiLista[etsiVapaaRuutu].Text = nappiLista[4].Text;
                nappiLista[4].Text = "";
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            int etsiVapaaRuutu = EtsiTyhjaNappiAlaRivista();
            if (etsiVapaaRuutu >= 0 && etsiVapaaRuutu <= 5)
            {
                alempiNappiLista[etsiVapaaRuutu].Text = nappiLista[5].Text;
                nappiLista[5].Text = "";
            }
        }

        private int EtsiTyhjaNappiAlaRivista()
        {
            int napinNumero = -1;
            for (int i = alempiNappiLista.Count -1 ; i>=0; i--)
            {
                if(alempiNappiLista[i].Text == "")
                {
                    napinNumero = i;
                }
            }
            return napinNumero;
        }

        private int EtsiTyhjaNappiYlaRivista()
        {
            int napinNumero = -1;
            for (int i = nappiLista.Count - 1; i >= 0; i--)
            {
                if (nappiLista[i].Text == "")
                {
                    napinNumero = i;
                }
            }
            return napinNumero;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int etsiVapaaRuutu = EtsiTyhjaNappiYlaRivista();
            if (etsiVapaaRuutu >= 0 && etsiVapaaRuutu <= 5)
            {
                nappiLista[etsiVapaaRuutu].Text = alempiNappiLista[0].Text;
                alempiNappiLista[0].Text = "";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int etsiVapaaRuutu = EtsiTyhjaNappiYlaRivista();
            if (etsiVapaaRuutu >= 0 && etsiVapaaRuutu <= 5)
            {
                nappiLista[etsiVapaaRuutu].Text = alempiNappiLista[1].Text;
                alempiNappiLista[1].Text = "";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int etsiVapaaRuutu = EtsiTyhjaNappiYlaRivista();
            if (etsiVapaaRuutu >= 0 && etsiVapaaRuutu <= 5)
            {
                nappiLista[etsiVapaaRuutu].Text = alempiNappiLista[2].Text;
                alempiNappiLista[2].Text = "";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int etsiVapaaRuutu = EtsiTyhjaNappiYlaRivista();
            if (etsiVapaaRuutu >= 0 && etsiVapaaRuutu <= 5)
            {
                nappiLista[etsiVapaaRuutu].Text = alempiNappiLista[3].Text;
                alempiNappiLista[3].Text = "";
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int etsiVapaaRuutu = EtsiTyhjaNappiYlaRivista();
            if (etsiVapaaRuutu >= 0 && etsiVapaaRuutu <= 5)
            {
                nappiLista[etsiVapaaRuutu].Text = alempiNappiLista[4].Text;
                alempiNappiLista[4].Text = "";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int etsiVapaaRuutu = EtsiTyhjaNappiYlaRivista();
            if (etsiVapaaRuutu >= 0 && etsiVapaaRuutu <= 5)
            {
                nappiLista[etsiVapaaRuutu].Text = alempiNappiLista[5].Text;
                alempiNappiLista[5].Text = "";
            }
        }

        private void VaihdaMerkkia(Button NappiNro)
        {
            switch (NappiNro.Text)
            {
                case "":
                    NappiNro.Text = "/";
                    
                    break;
                case "/":
                    NappiNro.Text = "+";
                   
                    break;
                case "+":
                    NappiNro.Text = "-";
                    
                    break;
                case "-":
                    NappiNro.Text = "X";
                    
                    break;
                case "X":
                    NappiNro.Text = "";
                    break;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VaihdaMerkkia(button1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            VaihdaMerkkia(button4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VaihdaMerkkia(button5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            VaihdaMerkkia(button6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            VaihdaMerkkia(button7);
        }
    }
}
