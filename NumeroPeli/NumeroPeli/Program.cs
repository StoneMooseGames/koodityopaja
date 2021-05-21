using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumeroPeli
{
    // Peli jossa käyttäjältä ensin pyydetään valitsemaan 6 numeroa. Vaihtoehtona joko pieniä (luvut 1-10) 
    // tai suuria (luvut 25,50,75 ja 100) Pelaaja valitsee esim. 2 pientä ja 4 suurta. Sen jälkeen kone arpoo luvut 
    // luku-alueilta. Esim kyseisessä tapauksessa arvotut luvut voivat olla 2,4,25,25,75 ja 100. Tämä jälkeen kone arpoo 
    // vielä luvun välilä 100-999. Sen jälkeen pelaajalla on 30 sek aikaa ratkaista lukuja ja peruslaskutoimituksia käyttäen
    // lopputulos.Kutakin lukua voi käyttää vain kerran yhtälössä.
    // Esimerkkipeli: pelaaja haluaa 4 pientä ja 2 suurta. Kone arpoo luvut 2,5,5,8,25,100 sekä lopputuloksen 377. Kello lähtee käymään.
    // Pelaajan tehtävä on ratkoa miten päästään lukuun 377 käyttäen +,-,* tai / -laskuja. Kyseiseen lukuun pääsisi esimerkiksi
    // 8 * 25 + 100 - 5 * 5 + 2. Pisteitä pelaaja saa sen mukaan kuinka kauaksi pelaaja pääsee lopputuloksesta. Täsmälleen oikeasta
    // 10 pistettä, ja vähennetään siitä pisteitä sen mukaan kuinka kauaksi pelaaja pääsee numerosta. Esim. 375 tai 379 antaisi 8 pistettä.
    // (Tämä ei siis ole oma keksimäni konsepti vaan olen katsonut youtubesta sellaista brittiläistä ohjelmaa kuin "8 out of 10 cats does countdown")

    
    class Program
    {
        //Alustukset
        static int seed = DateTime.Now.Day * DateTime.Now.Hour * DateTime.Now.Minute * DateTime.Now.Second;
        static Random satunnaisLuku  = new Random(seed);
        static int tavoiteLuku = 0;
        static int[] arvotutNumerot = new int[6];
        static string pelaajanNimi = "Pelaaja";
        static bool onkoPeliLoppu = false;
        static int pelaajanPisteet = 0;

        static void Main(string[] args)
        {
            Console.WriteLine(seed);
            Console.WriteLine("Tervetuloa!");
            Console.WriteLine("Hei, " + pelaajanNimi + " Aloitetaan peli!");
            Valikko();
            Console.WriteLine("Paina enter lopettaaksesi pelin");
            Console.ReadLine();
        }

        static void Valikko()
        {
            
            while(!onkoPeliLoppu)
            {
                Console.WriteLine("Pelaajan " + pelaajanNimi + " pisteet tällä hetkellä: " + pelaajanPisteet);
                Console.WriteLine("(1) Aloita Peli");
                Console.WriteLine("(2) Vaihda nimesi");
                Console.WriteLine("(3) Tulosta ennätykset");
                Console.WriteLine("(4) Lopeta Peli");
                try
                {
                    int valinta = Convert.ToInt32(Console.ReadLine());
                    Valinnat(valinta);
                    if(valinta<1 || valinta>4)
                    {
                        Console.WriteLine("Väärä valinta");
                        continue;
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("Väärä valinta");
                   
                }
                
            }
           
            
            
            
        }

        static void Valinnat(int valinta)
            {

                switch (valinta)
                {
                    case 1:
                        //Aloitetaan peli
                        PeliLooppi();
                        break;
                    case 2:
                        Console.WriteLine("Anna Nimesi:");
                        pelaajanNimi = Console.ReadLine();
                        Valikko();
                        //Vaihda pelaajan nimi
                        break;
                    case 3:
                        //Ennätykset
                        break;
                    case 4:
                        //Lopetus
                        onkoPeliLoppu = true;
                        break;
                    default:
                        break;


                }
            }
        

        static void PeliLooppi()
        {
            Console.WriteLine("Peli aloitettu " + pelaajanNimi);
            //kuinka monta pientä lukua?
            
            int pikkuLukuMaara = ArvoPienetLuvut();
            
            //kuinka monta isoa lukua?
            ArvoIsotLuvut(pikkuLukuMaara);
            TulostaLuvut();
            //arvotaan tavoitenumero
            int tavoiteLuku = ArvoLoppuTulos();
            Console.WriteLine("Tavoiteluku: " + tavoiteLuku);
            //käynnistetään kello
            //kysytään vastausta pelaajalta

            PeliKelloKayntiin();
            Console.WriteLine("Anna vastauksesi:");
            string vastaus = Console.ReadLine();

            //kun pelaaja on antanut vastauksen tarkistetaan onko kello vielä päällä ja sen jälkeen tarkistetaan oliko laskun tulos oikein vai lähellä
            TarkistaVastaus(vastaus, tavoiteLuku);
            //annetaan pisteet
            //kysytään haluaako uuden kierroksen, jos ei palataan valikkoon
        }

        static int ArvoPienetLuvut()
        {
            //arvotaan 1-6 kappaletta lukuväliltä 1-10
            Console.WriteLine("Kuinka monta pikkulukua haluat (1-6 kpl) ");
            int pikkuLukuMaara = 0;
            bool onkoAnnettuOikeaLuku = false;
            while(!onkoAnnettuOikeaLuku)
            {
                try
                {
                    pikkuLukuMaara = Convert.ToInt32(Console.ReadLine());
                    if(pikkuLukuMaara <1 || pikkuLukuMaara > 6)
                    {
                        Console.WriteLine("Lukuväli on 1-6 numeroa, anna uusi numero");
                        continue;
                    }
                    for (int i = 0; i < pikkuLukuMaara; i++)
                    {
                        int pikkuLuku = SatunnaisLukuGeneraattori(0);
                        arvotutNumerot[i] = pikkuLuku;
                        Console.WriteLine(i + ". luku " + pikkuLuku);

                    }
                    onkoAnnettuOikeaLuku = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("et antanut kokonaislukua");
                    continue;
                    
                }
            }
            return pikkuLukuMaara;


        }

        static void ArvoIsotLuvut(int pikkuLukuMaara)
        {
            //arvotaan 1-6 kappaletta luvuista 25,50,75,100, riippuen siitä kuinka monta pikkulukua on otettu
            int isoLukuMaara = 6 - pikkuLukuMaara;
            Console.WriteLine("Isoja lukuja saat " + isoLukuMaara + ". Arvotaan luvut");
            for (int i = pikkuLukuMaara; i < 6 ; i++)
            {
                int isoLuku = SatunnaisLukuGeneraattori(1);
                arvotutNumerot[i] = isoLuku;
                Console.WriteLine(i + ". luku " + isoLuku);
            }
            
        }

        static int ArvoLoppuTulos()
        {
            //arvotaan luku 100-999
            Console.WriteLine();
            return tavoiteLuku = SatunnaisLukuGeneraattori(2);
            
            
        }

        static void PeliKelloKayntiin()
        {
            //laitetaan kello päälle ja odotetaan pelaajalta vastausta
            Console.WriteLine("Aika alkaa nyt!");
        }

        static void TarkistaVastaus(string vastaus, int tavoiteLuku)
        {
            //TODO mieti millaisessa formaatissa haluat vastauksen ja miten tarkistat.
            //nyt vastaus tulee pelkkänä lukuna, eikä yhtälön toimivuudesta tai siitä miten olet lukuun päässyt ole tarkistusta

            Console.WriteLine("annoit vastauksen: " + vastaus);
            Console.WriteLine("Tavoite oli saada: " + tavoiteLuku);
            int loppuTulos = Convert.ToInt32(vastaus) - tavoiteLuku;
            if(loppuTulos == 0)
            {
                Console.WriteLine("onneksi olkoon, sait täsmälleen oikean luvun, 10 pistettä");
                pelaajanPisteet += 10;
            }
            else
            {
                if (loppuTulos <= 10 && loppuTulos >= -10)
                {
                    loppuTulos = Math.Abs(loppuTulos);
                    Console.WriteLine("vastauksesi heitti " + loppuTulos + " pisteen arvoisesti");
                    pelaajanPisteet += loppuTulos;
                }
                else Console.WriteLine("jäit liian kauas tavoiteluvusta, ei pisteitä");
            }
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
        static void TulostaLuvut()
        {
            //Tulostetaan arvotut luvut
            Console.WriteLine("Arvotut luvut ovat: ");
            foreach(int i in arvotutNumerot)
            {
                Console.Write(i + "   ");
            }
        }
        
    }
}
