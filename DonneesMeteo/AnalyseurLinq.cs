using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace DonneesMeteo
{
    class AnalyseurLinq
    {



        private List<DonnéesMois> _data;
        public List<DonnéesMois> Data
        {
            get { return _data; }
        }

        public AnalyseurLinq()
        {
            _data = new List<DonnéesMois>();
        }

        public void ChargerDonnées()
        {
            string chemin = @"C:\DonnéesMétéoParis.txt";

            int cpt = 0;
            using (StreamReader str = new StreamReader(chemin))
            {
                string ligne;

                while ((ligne = str.ReadLine()) != null)
                {
                    cpt++;
                    if (cpt == 1) continue; // On n'analyse pas la première ligne car elle contient les en-têtes

                    var tab = ligne.Split('\t');
                    try
                    {
                        var donnéesMois = new DonnéesMois
                        {
                            Mois = DateTime.Parse(tab[0]),
                            TMin = double.Parse(tab[1]),
                            TMax = double.Parse(tab[2]),
                            Précipitations = double.Parse(tab[3]),
                            Ensoleillement = double.Parse(tab[4])
                        };

                        // Ajout des données du mois à la liste
                        Data.Add(donnéesMois);
                    }
                    catch (FormatException)
                    {
                        // On ignore simplement la ligne
                        Console.WriteLine("Erreur de format à la ligne suivante :\r\n{0}", ligne);
                    }
                }
            }
        }
    

        public void AfficherStats()
        {
            // mois de la température min la plus basse
            var tempBasse1 = Data.OrderBy(dm => dm.TMin).FirstOrDefault().Mois;    
            Console.WriteLine("mois de la température min la plus basse : {0}", tempBasse1);

            // Sommes des précipitations de l'année 2016
            var precipitations = Data.Where(a => a.Mois.Year == 2016).Sum(p => p.Précipitations);
            Console.WriteLine("Sommes des précipitations de l'année 2016 : {0}", precipitations);

            // Durée d'ensoleillement moyenne du mois de Juillet sur toutes les années
            var ensoleillement = Data.Where(m => m.Mois.Month == 7).Average(e => e.Ensoleillement);
            Console.WriteLine("Durée d'ensoleillement moyenne du mois de Juillet sur toutes les années : {0}", ensoleillement);

            Console.WriteLine("");
            // Précipitations moyennes par année
            var annee = Data.Select(a => a.Mois.Year).Distinct();
           
            foreach (var an in annee)
            {
                var ap = Data.Where(a => a.Mois.Year == an).Average(pe => pe.Précipitations);
                Console.WriteLine("Précipitations moyennes par année : {0} en {1}", ap, an);
            }
      
        }
    }

    /// <summary>
    /// Classe contenant les données d'un mois de relevé météo
    /// </summary>
    public class DonnéesMois
    {
        public DateTime Mois { get; set; }
        public double TMin { get; set; }
        public double TMax { get; set; }
        public double Précipitations { get; set; }
        public double Ensoleillement { get; set; }
    }
}

