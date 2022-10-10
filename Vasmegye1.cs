﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Vasmegye1
{
    class Vasmegye1
    {
        static List<Szemelyiszam> szemelyiszamok = new List<Szemelyiszam>();
        static void Main(string[] args)
        {
            Console.WriteLine("\nA program kezdődik...");

            Console.WriteLine("\n2. feladat: Adatok beolvasása, tárolása");
            adatokBeolvasasa("vas.txt", Encoding.Default);

            Console.WriteLine("\n4. feladat: Ellenőrzés");
            feladat04();

            Console.WriteLine($"\n5. feladat: Vas megyében a vizsgált évek alatt {szemelyiszamok.Count} csecsemő született");

            Console.WriteLine($"\n6 feladat: Fiúk száma {szemelyiszamok.FindAll(a => a.Szam[0] == '1' || a.Szam[0] == '3').Count}");

            Console.WriteLine($"\n7. feladat: Vizsgált időszak {szemelyiszamok.Min(a => a.evSzam())} - {szemelyiszamok.Max(a => a.evSzam())}");

            if (szokoevbeszuletett())
            {

                Console.WriteLine($"\n8. feladat: Szökőnapon született baba");

            }
            else
            {

                Console.WriteLine($"\n8. feladat: Szökőnapon nem született baba");

            }

            feladat09();

            Console.WriteLine("\nA program vége!");
            Console.ReadKey();
        }

        private static void feladat09()
        {

            Console.WriteLine("\n9. feladat: Statisztika");

            var statisztika = szemelyiszamok.GroupBy(a => a.evSzam()).Select(b => new { ev = b.Key, fo = b.Count() });
            foreach (var item in statisztika)
            {

                Console.WriteLine($"\t{item.ev} + {item.fo} fő");

            }

        }

        private static bool szokoevbeszuletett()
        {

            var szokoEv = szemelyiszamok.Find(a => a.evSzam() % 4 == 0 && a.Szam.Substring(4,4).Equals("0224"));
            return szokoEv != null;

        }

        private static void feladat04()
        {

            List<Szemelyiszam> hibasSzamok = szemelyiszamok.FindAll(a => !CdvEll(a.Szam));
            foreach (Szemelyiszam item in hibasSzamok)
            {

                Console.WriteLine($"Hibás a {item.Szam} személyi azonosító!");
                szemelyiszamok.Remove(item);

            }

        }

        public static bool CdvEll(string szam)
        {

            //-- 3.feladat
            string szamNumeric = new string(szam.Where(a => char.IsDigit(a)).ToArray());
            if (szamNumeric.Length != 11)
            {

                return false;

            }

            double szum = 0;

            for (int i = 0; i < szamNumeric.Length; i++)
            {

                szum += char.GetNumericValue(szamNumeric[i]) * (10 - i);

            }
            double s = char.GetNumericValue(szamNumeric[10]);
            double k = szum % 11;
            return char.GetNumericValue(szamNumeric[10]) == szum % 11;

        }

        private static void adatokBeolvasasa(string adatFile, Encoding @default)
        {

            if (!File.Exists(adatFile))
            {

                Console.WriteLine("A forrás adatok hiányoznak!");
                Console.ReadLine();

                Environment.Exit(0);
            }

            using (StreamReader sr = new StreamReader(adatFile))
            {
                while (!sr.EndOfStream)
                {
                    szemelyiszamok.Add(new Szemelyiszam(sr.ReadLine()));
                }

            }

        }
    }
}
