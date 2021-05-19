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
    //vielä luvun välilä 100-999. Sen jälkeen pelaajalla on 30 sek aikaa ratkaista lukuja ja peruslaskutoimituksia käyttäen
    //lopputulos.Kutakin lukua voi käyttää vain kerran yhtälössä.
    //Esimerkkipeli: pelaaja haluaa 4 pientä ja 2 suurta. Kone arpoo luvut 2,5,5,8,25,100 sekä lopputuloksen 377. Kello lähtee käymään.
    //Pelaajan tehtävä on ratkoa miten päästään lukuun 377 käyttäen +,-,* tai / -laskuja. Kyseiseen lukuun pääsisi esimerkiksi
    // 8 * 25 + 100 - 5 * 5 + 2. Pisteitä pelaaja saa sen mukaan kuinka kauaksi pelaaja pääsee lopputuloksesta. Täsmälleen oikeasta
    //10 pistettä, ja vähennetään siitä pisteitä sen mukaan kuinka kauaksi pelaaja pääsee numerosta. Esim. 375 tai 379 antaisi 8 pistettä.
    // (Tämä ei siis ole oma keksimäni konsepti vaan olen katsonut youtubesta sellaista brittiläistä ohjelmaa kuin "8 out of 10 cats does countdown")

    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Tervetuloa!");
            Console.WriteLine("Anna Nimesi:");
            string pelaajanNimi = Console.ReadLine();
            Console.WriteLine("Hei, " + pelaajanNimi + " Aloitetaan peli!");
            Valikko(pelaajanNimi);
            Console.ReadLine();
        }

        static void Valikko(string pelaajanNimi)
        {
            Console.WriteLine("(1) Aloita Peli");
            int valinta = Convert.ToInt32(Console.ReadLine());

            switch (valinta)
            {
                case 1:
                    AloitaPeli(pelaajanNimi);
                    break;
                default:
                    break;
                   
                    
            }
        }

        static void AloitaPeli(string pelaajanNimi)
        {
            Console.WriteLine("Peli aloitettu " + pelaajanNimi);
        }
    }
}
