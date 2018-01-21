using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonneesMeteo
{
    class Program
    {
        static void Main(string[] args)
        {

            AnalyseurLinq al = new AnalyseurLinq();
            al.ChargerDonnées();
            al.AfficherStats();

            Console.ReadKey();
        }
    }
}
