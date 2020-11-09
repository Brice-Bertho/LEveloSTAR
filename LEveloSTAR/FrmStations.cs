// Formulaire FrmStations
// Auteur : Brice Bertho
// Dernière mise à jour : 02/10/2020

using System;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

// Cette classe représente le formulaire principal de l'application VéloStar
namespace LEveloSTAR
{
    public partial class FrmStations : Form
    {
        private Passerelle laPasserelle;

        private ListeStations laListeStations;
        private String msg;

        private PointLatLng THABOR = new PointLatLng(48.114208, -1.665977);
        private PointLatLng DELASALLE = new PointLatLng(48.12546, -1.668239);
        private PointLatLng MAIRIE = new PointLatLng(48.112102, -1.680228);

        private GMapOverlay leCalqueDesReperesFixes;
        private GMapOverlay leCalqueDesStations;
        private GMarkerGoogle unMarqueur;
        //le constructeur
        public FrmStations()
        {
            InitializeComponent();

            laCarte.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            laCarte.DragButton = MouseButtons.Left;

            laCarte.Left = 0;
            laCarte.Top = 0;
            laCarte.Width = this.ClientSize.Width;
            laCarte.Height = this.ClientSize.Height;

            leCalqueDesReperesFixes = new GMapOverlay("reperes fixes");
            laCarte.Overlays.Add(leCalqueDesReperesFixes);

            leCalqueDesStations = new GMapOverlay("marqueurs des stations");
            laCarte.Overlays.Add(leCalqueDesStations);

            laCarte.Position = THABOR;
            laCarte.Zoom = 13;
            afficherReperesFixes();

            laPasserelle = new PasserelleJSON();

            this.laListeStations = new ListeStations();
            msg = laPasserelle.getListeStations(laListeStations);
            if(msg != "")
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                afficherMarqueurs(null);
            }

        // modidifie les dimensions de la carte pour occuper tout l'espace disponible du formulaire
        private void FrmStations_Resize(object sender, EventArgs e)
        {
            laCarte.Width = this.ClientSize.Width;
            laCarte.Height = this.ClientSize.Height;
        }

        // cette procédure événementielle est activée par un clic sur un marqueur de station
        private void laCarte_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            String idStation = item.ToolTipText.Substring(0, 4).Trim();
            Station laStationChoisie = laListeStations.getLaStation(idStation);

            afficherMarqueurs(laStationChoisie);

            String msg = laPasserelle.getDetailStation(laStationChoisie);
            
            if(msg !="")
            {
                msg = "Informations non disponibles";
                MessageBox.Show(msg, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                msg = "Station :" + laStationChoisie.Id + "-" + laStationChoisie.Nom + "\r\n";
                msg += "Adresse :" + laStationChoisie.Adresse + "\r\n";
                if (laStationChoisie.PossedeTpe == true)
                    msg += "Paiement par carte possible" + "\r\n\r\n";
                else
                    msg += "Pas de paiement par carte" + "\r\n\r\n";

                msg += "Vélos disponibles :" + laStationChoisie.VelosDisponibles.ToString() + "\r\n";
                msg += "Socles disponibles :" + laStationChoisie.SoclesDisponibles.ToString() + "\r\n";
                msg += "Etat" + laStationChoisie.Etat + "\r\n";

                txtInfosStationChoisie.Text = msg;
            }

            laCarte.Position = new PointLatLng(laStationChoisie.Latitude, laStationChoisie.Longitude);
            laCarte.Zoom = 18;
        }

        // affiche les rpères fixes avec des punaises jeunes
        private void afficherReperesFixes()
        {
            unMarqueur = new GMarkerGoogle(MAIRIE, GMarkerGoogleType.yellow_pushpin);
            unMarqueur.ToolTipText = "Mairie de Rennes";
            leCalqueDesReperesFixes.Markers.Add(unMarqueur);

            unMarqueur = new GMarkerGoogle(DELASALLE, GMarkerGoogleType.yellow_pushpin);
            unMarqueur.ToolTipText = "Lycée De La Salle";
            leCalqueDesReperesFixes.Markers.Add(unMarqueur);
        }

        // affiche les stations avec des marqueurs bleus (sauf pour la station sélectionnée qui est en rouge)
        private void afficherMarqueurs(Station laStationChoisie)
        {
            leCalqueDesStations.Clear();

            for(int i=1; i<this.laListeStations.getNbStations(); i++)
            {
                Station uneStation = this.laListeStations.getLaStation(i);

                if (laStationChoisie != null && uneStation.Id == laStationChoisie.Id)
                    unMarqueur = new GMarkerGoogle(new PointLatLng(uneStation.Latitude, uneStation.Longitude), GMarkerGoogleType.red_pushpin);
                else
                    unMarqueur = new GMarkerGoogle(new PointLatLng(uneStation.Latitude, uneStation.Longitude), GMarkerGoogleType.blue_dot);

                unMarqueur.ToolTipText = uneStation.Id + "-" + uneStation.Nom;
                leCalqueDesStations.Markers.Add(unMarqueur);
            }
        }
    }
}
