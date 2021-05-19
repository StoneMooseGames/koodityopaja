using System;
using ktpLibrary;

namespace ktpUI
{
    class Paiva2
    {
        static void Main(string[] args)
        {
            
            //NumeroSarjanKasittely();
            int[] verrattavaLuku = {3,7};
             System.Console.WriteLine("anna luku 1");
            verrattavaLuku[0] = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("anna luku 2");
            verrattavaLuku[1] = Convert.ToInt32(Console.ReadLine());
            
            KysyLukua(verrattavaLuku);

        }

        static void NumeroSarjanKasittely()
        {
            System.Console.WriteLine("kuinka pitkän numerosarjan haluat?");
            int sarjanPituus = Convert.ToInt32(Console.ReadLine());
            int[] numeroSarja = new int[sarjanPituus];
           
            for(int i=0;i<sarjanPituus;i++)
            {
                numeroSarja[i] = i;
                System.Console.WriteLine(i);
            }
            
            for(int i=numeroSarja.Length; i>=0 ; i--)
            {
                System.Console.WriteLine(i);
                
            }
            int laskuri = numeroSarja.Length-1;
            while(laskuri >= 0)
            {
                tarkistaLuku(numeroSarja[laskuri]);
                laskuri--;
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
        
            
        }
        static void KysyLukua(int[] verrattavaLuku)
        {
            bool[] tarkistus ={false,false};
            int[] luvut = {0,0,0,0};
            int lukuValiMin = 6;
            System.Console.WriteLine("Anna Luku 1 lukuvälille");
            luvut[0] = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("Anna Luku 2 lukuvälille");
            luvut[1] = Convert.ToInt32(Console.ReadLine());
           
             if(luvut[1]-luvut[0] < lukuValiMin)
            {
                    int erotus = luvut[1]-luvut[0];
                    System.Console.WriteLine("lukualueen koko on " + erotus  + ", sen pitäis olla vähintään " + lukuValiMin);
                    KysyLukua(verrattavaLuku);

            }else System.Console.WriteLine("lukualue on riittävän iso");
            
           
            int summa = verrattavaLuku[0] + verrattavaLuku[1];
            float keskiarvo = summa/2;

            if( luvut[1] - luvut[0] < 0)
                {
                    
                    //System.Console.WriteLine(luvut[0] + " on suurempi kuin " + luvut[1]);
                    int temp = luvut[0];
                    luvut[0] = luvut[1];
                    luvut[1] = temp;
                    //System.Console.WriteLine("lukuväli: " + luvut[0] + " - " +  luvut[1]);
                }else
                {
                 System.Console.WriteLine(luvut[0] + " on pienempi kuin " + luvut[1]);
                 System.Console.WriteLine("lukuväli: " + luvut[0] + " - " +  luvut[1]);
                }
           

            for (int i=0; i < verrattavaLuku.Length;i++)
            {
                if(verrattavaLuku[i] >= luvut[0] && verrattavaLuku[i] <= luvut[1])
                {
                    tarkistus[i] = true;
                    //System.Console.WriteLine("luku " + verrattavaLuku[i] + " löytyy lukualueelta");
                }//else System.Console.WriteLine("luku " + verrattavaLuku[i] + " ei löydy lukualueelta");
            }
            
            if(tarkistus[0] && tarkistus[1])
            {
                System.Console.WriteLine("Antamasi luvut ovat lukualueella " + luvut[0] + " - " +  luvut[1]);
                System.Console.WriteLine("lukujen summa on:" + summa);
                System.Console.WriteLine("lukujen keskiarvo on:" + keskiarvo);
            }else System.Console.WriteLine("Antamasi luvut eivät ole lukualueella");
        }
    }

}
