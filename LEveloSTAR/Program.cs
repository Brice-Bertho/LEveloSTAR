// Programme de démarrage
// Auteur : JM CARTRON
// Dernière mise à jour : 19/9/2017

using System;
using System.Windows.Forms;

namespace LEveloSTAR
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //PasserelleJSON laPasserelle;			// objet déclaré à partir d'une classe abstraite
            //ListeStations laListeStations;
            //Station laStation;
            //String msg;
            //String titre;

            //laPasserelle = new PasserelleJSON();

            //// test de la méthode getListeStations(ListeStations laListeStations)
            //laListeStations = new ListeStations();
            //msg = laPasserelle.getListeStations(laListeStations);
            //if (msg == "") msg = ("Nombre de stations : " + laListeStations.getNbStations() + "\n");
            //titre = "Test de la méthode getListeStations";
            //MessageBox.Show(msg, titre, MessageBoxButtons.OK, MessageBoxIcon.Information);

            //// affichage de la première station trouvée
            //laStation = laListeStations.getLaStation(0);
            //msg = laStation.ToString();
            //MessageBox.Show(msg, titre, MessageBoxButtons.OK, MessageBoxIcon.Information);

            //// test de la méthode getDetailStation(Station laStation)
            //msg = laPasserelle.getDetailStation(laStation);
            //if (msg == "") msg = laStation.ToString();
            //titre = "Test de la méthode getDetailStation";
            //MessageBox.Show(msg, titre, MessageBoxButtons.OK, MessageBoxIcon.Information);


            // -------------------------------------------------------------------------------------------------
            // les 3 lignes suivantes seront réactivées au prochain TP, afin de lancer le formulaire FrmStations
            // -------------------------------------------------------------------------------------------------

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmStations());
        }
    }
}
