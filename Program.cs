using System;
using System.Collections.Generic;

namespace Hafıza_Kartı_Oyunu
{
    internal class Program
    {
        class Program
        {
            static void Main(string[] args)
            {
                List<Kart> kartlar = KartlariOlustur();

                Kart[,] kartOyunu = KartOyununuOlustur(kartlar);

                Random rnd = new Random();
                int adimSayisi = 0;
                DateTime baslangicZamani = DateTime.Now;

                while (!OyunBittiMi(kartOyunu))
                {
                    Console.WriteLine("------------------------------");
                    OyunTahtasiniYazdir(kartOyunu);

                    int secilenKart1 = SecilenKartNoAl();

                    if (!KartGecerliMi(secilenKart1, kartOyunu))
                    {
                        Console.WriteLine("Geçersiz bir kart seçtiniz. Lütfen tekrar seçim yapınız.");
                        continue;
                    }

                    Kart kart1 = KartBul(secilenKart1, kartOyunu);
                    kart1.AcikMi = true;

                    Console.WriteLine("------------------------------");
                    OyunTahtasiniYazdir(kartOyunu);

                    int secilenKart2 = SecilenKartNoAl();

                    if (!KartGecerliMi(secilenKart2, kartOyunu))
                    {
                        Console.WriteLine("Geçersiz bir kart seçtiniz. Lütfen tekrar seçim yapınız.");
                        kart1.AcikMi = false;
                        continue;
                    }

                    Kart kart2 = KartBul(secilenKart2, kartOyunu);
                    kart2.AcikMi = true;

                    adimSayisi++;

                    Console.WriteLine("------------------------------");
                    OyunTahtasiniYazdir(kartOyunu);

                    if (KartlarEslesiyorMu(kart1, kart2))
                    {
                        Console.WriteLine("Tebrikler! Bir çift buldunuz.");
                    }
                    else
                    {
                        Console.WriteLine("Üzgünüm! Seçtiğiniz kartlar eşleşmedi.");
                        kart1.AcikMi = false;
                        kart2.AcikMi = false;
                        adimSayisi++;
                    }
                }

                DateTime bitisZamani = DateTime.Now;
                TimeSpan oyunSuresi = bitisZamani - baslangicZamani;

                Console.WriteLine("------------------------------");
                Console.WriteLine("<<OYUN BİTTİ>>");
                Console.WriteLine("TOPLAM ADIM SAYISI: " + adimSayisi);
                Console.WriteLine("TOPLAM SÜRE: " + oyunSuresi);
            }
            static List<Kart> KartlariOlustur()
            {
                List<Kart> kartlar = new List<Kart>();

                char[] harfler = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
                Random rnd = new Random();

                // Harfleri karıştır
                for (int i = 0; i < harfler.Length - 1; i++)
                {
                    int j = rnd.Next(i, harfler.Length);
                    char temp = harfler[i];
                    harfler[i] = harfler[j];
                    harfler[j] = temp;
                }

                foreach (char harf in harfler)
                {
                    Kart kart1 = new Kart { Numara = kartlar.Count + 1, Harf = harf, AcikMi = false };


                    kartlar.Add(kart1);

                }
                //Eklendi çünkü diğer şekil 1 ile 9,2 ile 10 eşit oluyordu.
                for (int i = 0; i < harfler.Length - 1; i++)
                {
                    int j = rnd.Next(i, harfler.Length);
                    char temp = harfler[i];
                    harfler[i] = harfler[j];
                    harfler[j] = temp;
                }

                foreach (char harf in harfler)
                {
                    Kart kart2 = new Kart { Numara = kartlar.Count + 1, Harf = harf, AcikMi = false };
                    kartlar.Add(kart2);
                }

                return kartlar;
            }

            static Kart[,] KartOyununuOlustur(List<Kart> kartlar)
            {
                Kart[,] kartOyunu = new Kart[4, 4];

                int index = 0;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {

                        kartOyunu[i, j] = kartlar[index];
                        index++;
                    }
                }

                return kartOyunu;
            }

            static void OyunTahtasiniYazdir(Kart[,] kartOyunu)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Kart kart = kartOyunu[i, j];
                        if (kart.AcikMi)
                            Console.Write("|  " + kart.Harf + " " + "  ");
                        else
                            Console.Write("|  " + kart.Numara.ToString("00") + "  ");
                    }
                    Console.WriteLine("|");
                }
                Console.WriteLine("------------------------------");
            }

            static int SecilenKartNoAl()
            {
                Console.Write("Lütfen bir kart seçiniz >> ");
                string secimStr = Console.ReadLine();
                int secim;

                while (!int.TryParse(secimStr, out secim))
                {

                    Console.WriteLine(secim);
                    Console.WriteLine("Geçersiz bir değer girdiniz. Lütfen tekrar deneyin.");
                    Console.Write("Lütfen bir kart seçiniz >> ");
                    secimStr = Console.ReadLine();
                }

                return secim;
            }

            static bool KartGecerliMi(int secilenKart, Kart[,] kartOyunu)
            {
                return secilenKart >= 1 && secilenKart <= 17 && !KartAcikMi(secilenKart, kartOyunu);
            }

            static bool KartAcikMi(int secilenKart, Kart[,] kartOyunu)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Kart kart = kartOyunu[i, j];
                        if (kart.Numara == secilenKart && kart.AcikMi)
                            return true;
                    }
                }
                return false;
            }

            static Kart KartBul(int secilenKart, Kart[,] kartOyunu)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Kart kart = kartOyunu[i, j];
                        if (kart.Numara == secilenKart)
                            return kart;
                    }
                }
                return null;
            }

            static bool KartlarEslesiyorMu(Kart kart1, Kart kart2)
            {
                return kart1.Harf == kart2.Harf;
            }

            public static bool OyunBittiMi(Kart[,] kartOyunu)
            {
                foreach (Kart kart in kartOyunu)
                {
                    if (!kart.AcikMi)
                        return false;
                }
                return true;
            }
        }
        class Kart
        {
            public int Numara { get; set; }
            public char Harf { get; set; }
            public bool AcikMi { get; set; }
        }
    }
}
