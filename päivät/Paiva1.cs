using System;
using ktpLibrary;



    class Paiva1
    {
        static void Main()
        {
            var random = new Random();
            int luku1 = 0;
            int luku2 = KysyLukua();
            int luku3 = random.Next(-50,50);
            
            tarkistaLuku(luku1);
            tarkistaLuku(luku2);
            tarkistaLuku(luku3);
            Aanesta();
            KysyJoukkuetta();
            TulostaLukujaTaulukosta();

        int KysyLukua()
        {
            Console.WriteLine("Anna luku");
            var annettuLuku = Console.ReadLine();
            int palautettavaLuku;
            try
            {
             Convert.ToInt32(annettuLuku);
            }
            catch(FormatException e)
            {
                System.Console.WriteLine("virhe: syötä vaio kokonaislukuja");
                KysyLukua();
            }
            finally
            {
               palautettavaLuku = Convert.ToInt32(annettuLuku);
            }
            System.Console.WriteLine("annoit luvun: " + palautettavaLuku);
            return palautettavaLuku;
        }

        void tarkistaLuku(int luku)
        {
        if(luku <=0)
           {
                if(luku == 0) System.Console.WriteLine("luku " + luku + " on nolla");
                if(luku <0 )System.Console.WriteLine("luku " + luku +" on negatiivinen");
            }   else System.Console.WriteLine("luku " + luku +" on positiivinen");
        
        TarkistaJaollisuus(luku);
          
        }
        void TarkistaJaollisuus(int luku)
        {
            bool onkoJaollinenKolmella = false;
            bool onkoJaollinenViidella = false;
            
            if(luku == 0)
            { 
                System.Console.WriteLine("Luku 0 ei ole parillinen eikä pariton");
                System.Console.WriteLine("luku " + luku +" on jaollinen kolmella");
                return;
            }

            if(luku % 2 == 0)
            {
                System.Console.WriteLine("luku " + luku +" on parillinen");
            }
            else System.Console.WriteLine("luku " + luku +" on pariton");

            
            if(luku % 3 == 0) 
            {
                onkoJaollinenKolmella = true;
                System.Console.WriteLine("luku " + luku +" on jaollinen kolmella");
            }else System.Console.WriteLine("luku " + luku +" ei ole jaollinen kolmella");
            
            if(luku % 5 == 0) 
            {
                onkoJaollinenViidella = true;
                System.Console.WriteLine("luku " + luku +" on jaollinen viidellä");
            }else System.Console.WriteLine("luku " + luku +" ei ole jaollinen viidellä");

            if(onkoJaollinenKolmella && onkoJaollinenViidella)
            {
                System.Console.WriteLine("luku " + luku +" on jaollinen sekä kolmella että viidellä");
            }

            for(int i=2; i<luku; i++)
            {
                
                int temp = luku % i;
                if(temp == 0)
                {
                     System.Console.WriteLine("luku " + luku + " on jaollinen luvulla " + i);
                }else System.Console.WriteLine("luku " + luku + " ei ole jaollinen luvulla " + i);

            }
            
        }

        void Aanesta()
        {
            
            System.Console.WriteLine("Sallitut vaihtoehdot ovat:");
            System.Console.WriteLine("J = Jaa");
            System.Console.WriteLine("E = Ei");
            System.Console.WriteLine("T = Tyhjä");
            System.Console.WriteLine("P = Poissa");
            System.Console.WriteLine("Anna valintasi");
            char valinta = Convert.ToChar(Console.ReadLine());
            
            
            switch(Char.ToUpper(valinta))
            {
                case 'J':
                    System.Console.WriteLine("Vastauksesi on Jaa");
                    break;
                case 'E':
                    System.Console.WriteLine("Vastauksesi on Ei");
                    break;
                case 'T':
                    System.Console.WriteLine("Vastauksesi on Tyhjä");
                    break;
                case 'P':
                    System.Console.WriteLine("Vastauksesi on Poissa");
                    break;
                default:
                    Aanesta();
                    break;
            }
                        
        }

        void KysyJoukkuetta()
        {
            System.Console.WriteLine("Anna joukkue 1:");
            string joukkue1 = Console.ReadLine();
            System.Console.WriteLine("Anna joukkue 2:");
            string joukkue2 = Console.ReadLine();
            var randomPisteet1 = random.Next(0,10);
            var randomPisteet2 = random.Next(0,10);

            if(randomPisteet1 < randomPisteet2)
            {
             System.Console.WriteLine("joukkue " + joukkue2 + " voitti pistein " + randomPisteet2 + "-" + randomPisteet1);
            }else System.Console.WriteLine("joukkue " + joukkue1 + " voitti pistein " + randomPisteet2 + "-" + randomPisteet1);

        
        }
        void TulostaLukujaTaulukosta()
        {
            int[] lukuTaulukko = {1,2,3,4,5};
            for(int i=0;i<lukuTaulukko.Length;i++)
            {
                System.Console.WriteLine(lukuTaulukko[i]);
            }
            for(int i=lukuTaulukko.Length-1 ;i>=0;i--)
            {
                System.Console.WriteLine(lukuTaulukko[i]);
            }
        }
        }
                
    }
    

