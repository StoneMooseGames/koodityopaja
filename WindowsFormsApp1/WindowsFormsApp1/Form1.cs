//NumeroPeli 1.0
//© Mika Ahlsten PTIVIS20E 


//Peli jossa käyttäjältä ensin pyydetään valitsemaan 6 numeroa.Vaihtoehtona joko pieniä(luvut 1-10) 
//tai suuria(luvut 25,50,75 ja 100) Pelaaja valitsee esim. 2 pientä ja 4 suurta.Sen jälkeen kone arpoo
//luvut luku-alueilta.Esim kyseisessä tapauksessa arvotut luvut voivat olla 2,4,25,25,75 ja 100. Tämä 
//jälkeen kone arpoo vielä luvun välilä 100-999. Sen jälkeen pelaajalla on 60 sek aikaa ratkaista lukuja
//ja peruslaskutoimituksia käyttäen lopputulos.Kutakin lukua voi käyttää vain kerran yhtälössä.
//Pelissä alkaa seuraava kierros kun tehtävä on ratkaistu ja valittu uudet numerot. Tällöin kellosta
//vähennetään 5 sekuntia. Viimeinen kierros alkaa kun pelikellossa on 5 sek jäljellä ratkaisuaikaa.

//Esimerkkipeli: 
//    pelaaja haluaa 4 pientä ja 2 suurta.Kone arpoo luvut 2,5,5,8,25,100 sekä lopputuloksen 377. 
//    Kello lähtee käymään.Pelaajan tehtävä on ratkoa miten päästään lukuun 377 käyttäen +,-,* tai / -laskuja.
//    Kyseiseen lukuun pääsisi esimerkiksi 8 * 25 + 100 - 5 * 5 + 2. Pisteitä pelaaja saa sen mukaan kuinka
//    kauaksi pelaaja pääsee lopputuloksesta. Täsmälleen oikeasta10 pistettä, ja vähennetään siitä pisteitä
//    sen mukaan kuinka kauaksi pelaaja pääsee numerosta.Esim. 375 tai 379 antaisi 8 pistettä. Pisteet 
//    lisätään edellisen kierroksen pisteisiin.


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

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
        static Label pelaajanPisteetCanva = new Label();
        static int pikkuLukujenMaara = 1;
        static List<Button> nappiLista = new List<Button>();
        static List<Button> alempiNappiLista = new List<Button>();
        static List<Button> aritmetiikkaNapit = new List<Button>();
        static List<string> aritmetiikkaMerkit = new List<string>();
        static List<Label> sulkeet = new List<Label>();
        static Label tavoiteNumeroCanva = new Label();
        static Label kello = new Label();
        static int kelloAika = 65;
        static int ajastimenAika = 65;
        static Timer kelloAjastin = new Timer();
        static Timer randomNumeroLabelinTImer = new Timer();
        static TextBox pelaajanLoppuTulos = new TextBox();
        static Label debug = new Label();
        static bool[] sulkeetEnabled = new bool[10];
        static Label vastausRuutu = new Label();
        static int[] aritmetiikkaTilat = new int[5];
        static bool isGameOver = false;      
        static Button AloitaPainike = new Button();
        static Button numeroidenMaaraPainike = new Button();
        static double loppuTulos = 0;
        static Label vastausPelaajalle = new Label();
        static Label aikaIlmoitus = new Label();
        static Label pisteet = new Label();
        static Button pelaajaNappi = new Button();
        static Label edellisenKierroksenPisteetLabel = new Label();
        static int edellisenKierroksenPisteet;
        //Sounds
        static SoundPlayer tenPoints = new SoundPlayer(@"10points.wav");
        static SoundPlayer points = new SoundPlayer(@"points.wav");
        static SoundPlayer tick = new SoundPlayer(@"tick.wav");


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
            sulkeet.Add(label12);
            sulkeet.Add(label13);
            sulkeet.Add(label21);
            sulkeet.Add(label14);
            sulkeet.Add(label20);
            sulkeet.Add(label15);
            sulkeet.Add(label19);
            sulkeet.Add(label16);
            sulkeet.Add(label18);
            sulkeet.Add(label22);
            vastausRuutu = label5;

            label10.Text = "";
            pelaajanPisteetCanva.Text = pelaajanPisteet.ToString();
            
            pelaajaNappi.Text = pelaajanNimi;
            tavoiteNumeroCanva = label2;
            kello = label3;
            randomNumeroLabelinTImer = timer2;
            label3.Text = kelloAika.ToString();
            kelloAjastin = timer1;
            AloitaPainike = button2;
            numeroidenMaaraPainike = button20;
            debug = label11;
            debug.Text = "";
            vastausPelaajalle = label17;
            pisteet = label6;
            aikaIlmoitus = label10;
            kello.Text = (kelloAika- 5).ToString();
            edellisenKierroksenPisteet = 0;
            edellisenKierroksenPisteetLabel = label7;
            edellisenKierroksenPisteetLabel.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            TyhjennaPeli();
 
        }

        static void TyhjennaPeli()
        {
            
            
            vastausPelaajalle.Text = "";
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
            for (int i = 0; i < sulkeet.Count; i++)
            {
                sulkeet[i].ForeColor = Color.Teal;
            }
            for (int i = 0; i <= 9; i++)
            {
                sulkeetEnabled[i] = false;
                sulkeet[i].Enabled = true;
                if (i % 2 == 0)
                {
                    sulkeet[i].Text = ")";
                }
                else sulkeet[i].Text = "(";
            }
            sulkeet[0].Text = "(";
            sulkeet[9].Text = ")";
            vastausRuutu.Text = "000";
            aikaIlmoitus.Text = "";
            

        }
        //Aloita
        private void button2_Click(object sender, EventArgs e)
        {
            TyhjennaPeli();
            AloitaUusiPeli();
            ajastimenAika -= 5;
            if (ajastimenAika == 0)
            {
                edellisenKierroksenPisteet = pelaajanPisteet;
                aikaIlmoitus.Text = "Uusi peli alkoi";
                edellisenKierroksenPisteetLabel.Text = "Edellisen kierroksen pisteet: " + edellisenKierroksenPisteet.ToString();
                ajastimenAika = 60;
                pelaajanPisteet = 0;
                pisteet.Text = "Pelajaan kokonaispisteet: " + pelaajanPisteet.ToString();
            }
            kelloAika = ajastimenAika; 
            int pikkuLukuMaara = ArvoPienetLuvut();
            ArvoIsotLuvut(pikkuLukuMaara);

            tavoiteLuku = ArvoLoppuTulos();
            kelloAjastin.Start();
            button20.Enabled = false;
            //et voi enää valita pienten numeroiden määrää
            button2.Enabled = false;
            //et voi enää painaa aloita nappia kun peli on päällä
  
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

        static void TarkistaVastaus()
        {
            //tarkistetaan oliko pelaajan lasku oikein
            isGameOver = true;
            korjaaStringi();
            //alla oleva string on todella pitkä, mietin saisiko sitä lyhennettyä
            string laskuTemp = sulkeet[0].Text + alempiNappiLista[0].Text + aritmetiikkaNapit[0].Text + sulkeet[1].Text + alempiNappiLista[1].Text + sulkeet[2].Text + aritmetiikkaNapit[1].Text + sulkeet[3].Text + alempiNappiLista[2].Text + sulkeet[4].Text + aritmetiikkaNapit[2].Text + sulkeet[5].Text + alempiNappiLista[3].Text + sulkeet[6].Text + aritmetiikkaNapit[3].Text + sulkeet[7].Text + alempiNappiLista[4].Text + sulkeet[8].Text + aritmetiikkaNapit[4].Text + alempiNappiLista[5].Text + sulkeet[9].Text;
            try
            {
                string arvo = new DataTable().Compute(laskuTemp, null).ToString();
                loppuTulos = Convert.ToDouble(arvo);
                vastausRuutu.Text = arvo;
                debug.Text = laskuTemp + "=" + loppuTulos.ToString();
                
            }
            catch
            {
                debug.Text = "Virhe vastauksen annossa"; //Tämä tulee enää vain silloin kun sulkeet ovat laitettu väärin
            }
                
        }
        static void korjaaStringi()
        {
            
            for (int i=0;i<5;i++)
            {
                if (aritmetiikkaNapit[i].Text == "X") aritmetiikkaNapit[i].Text = "*";
                if (aritmetiikkaNapit[i].Text == "") aritmetiikkaNapit[i].Text = "+";
                
            }
            for(int i=0;i<=9;i++)
            {
                if (!sulkeetEnabled[i]) sulkeet[i].Text = "";
            }
            for(int i=0;i<=5;i++)
            {
                if (alempiNappiLista[i].Text == "") alempiNappiLista[i].Text = "0";
            }
       
        }

        static void PisteenLasku()
        {
            
            double lisattavatPisteet = 0;
            double erotus = 0;
            if (loppuTulos > tavoiteLuku)
            {
                erotus = loppuTulos - tavoiteLuku;
            }
            else erotus =  tavoiteLuku - loppuTulos;

            if (erotus > 10)
            {
                vastausPelaajalle.Text = "jäit liian kauas tavoitteesta. ei pisteitä";
                lisattavatPisteet = 0;
            }
           
            else
            {
                if(erotus == 0) tenPoints.Play();
                if(erotus <=10 && erotus >0) points.Play();
                lisattavatPisteet = 10 - erotus;
                vastausPelaajalle.Text = "sait pisteitä:" + lisattavatPisteet.ToString();
            }
            //annetaan pisteet sen mukaan miten lasku onnistui
            
            pelaajanPisteet = Convert.ToInt32(pelaajanPisteet) + Convert.ToInt32(lisattavatPisteet);
            pelaajanPisteetCanva.Text = pelaajanPisteet.ToString();
            pisteet.Text = "Pelajaan kokonaispisteet: " + pelaajanPisteet.ToString();
            isGameOver = false;
            AloitaUusiPeli();

        }
      
        static void AloitaUusiPeli()
        {
           
            AloitaPainike.Enabled = true;
           
            numeroidenMaaraPainike.Enabled = true;
            for (int i = 0; i < nappiLista.Count; i++)
            {
                nappiLista[i].Enabled = true;
                alempiNappiLista[i].Enabled = true;

            }
            for (int i = 0; i < aritmetiikkaNapit.Count; i++)
            {
                aritmetiikkaNapit[i].Enabled = true;

            }

        }

        static int SatunnaisLukuGeneraattori(int arvontamoodi)
        {
            //Jotta saataisiin mahdollisimman random numero, ehkä kellonaika numerona voisi olla mainio seedille randomiin? (rivi 16-17)
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
            tick.Play();
            kelloAika--;
            if (kelloAika == 0)
            {
                kelloAjastin.Stop();
                isGameOver = true;
                label10.Text = "Aika Loppui";
                TarkistaVastaus();
                PisteenLasku();
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
            //tällä on tarkoitus laittaa numeronvalintalaitteeseen pientä kestoa ennenkuin varsinainen numero paljastuu
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

        private void label12_Click(object sender, EventArgs e)
        {
            if (label12.ForeColor == Color.Teal)
            {
                label12.ForeColor = Color.Black;
                sulkeetEnabled[0] = true;
            }
            else
            {
                label12.ForeColor = Color.Teal;
                sulkeetEnabled[0] = false;
            }
            

        }

        private void label13_Click(object sender, EventArgs e)
        {
            if (label13.ForeColor == Color.Teal)
            {
                label13.ForeColor = Color.Black;
                sulkeetEnabled[1] = true;
            }
            else
            {
                label13.ForeColor = Color.Teal;
                sulkeetEnabled[1] = false;
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {
            if (label21.ForeColor == Color.Teal)
            {
                label21.ForeColor = Color.Black;
                sulkeetEnabled[2] = true;
            }
            else
            {
                label21.ForeColor = Color.Teal;
                sulkeetEnabled[2] = false;
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            if (label14.ForeColor == Color.Teal)
            {
                label14.ForeColor = Color.Black;
                sulkeetEnabled[3] = true;
            }
            else
            {
                label14.ForeColor = Color.Teal;
                sulkeetEnabled[3] = false;
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
            if (label20.ForeColor == Color.Teal)
            {
                label20.ForeColor = Color.Black;
                sulkeetEnabled[4] = true;
            }
            else
            {
                label20.ForeColor = Color.Teal;
                sulkeetEnabled[4] = false;
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            if (label15.ForeColor == Color.Teal)
            {
                label15.ForeColor = Color.Black;
                sulkeetEnabled[5] = true;
            }
            else
            {
                label15.ForeColor = Color.Teal;
                sulkeetEnabled[5] = false;
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            if (label19.ForeColor == Color.Teal)
            {
                label19.ForeColor = Color.Black;
                sulkeetEnabled[6] = true;
            }
            else
            {
                label19.ForeColor = Color.Teal;
                sulkeetEnabled[6] = false;
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            if (label16.ForeColor == Color.Teal)
            {
                label16.ForeColor = Color.Black;
                sulkeetEnabled[7] = true;
            }
            else
            {
                label16.ForeColor = Color.Teal;
                sulkeetEnabled[7] = false;
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            if (label18.ForeColor == Color.Teal)
            {
                label18.ForeColor = Color.Black;
                sulkeetEnabled[8] = true;
            }
            else
            {
                label18.ForeColor = Color.Teal;
                sulkeetEnabled[8] = false;
            }
        }

        private void label22_Click(object sender, EventArgs e)
        {
            if (label22.ForeColor == Color.Teal)
            {
                label22.ForeColor = Color.Black;
                sulkeetEnabled[9] = true;
            }
            else
            {
                label22.ForeColor = Color.Teal;
                sulkeetEnabled[9] = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
