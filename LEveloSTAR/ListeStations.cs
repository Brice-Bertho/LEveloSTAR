// Classe ListeStations
// Auteur : JM CARTRON
// Dernière mise à jour : 19/9/2017

using System;
using System.Collections;

// Cette classe permet de stocker en mémoire la liste des stations VéloStar
namespace LEveloSTAR
{
    public class ListeStations
    {
        #region Attributs Privés
        private ArrayList _lesStations;			// pour contenir une liste de stations
        private double _positionLatitude;		// latitude de la position de l'utilisateur (0 si inconnue)
        private double _positionLongitude;		// longitude de la position de l'utilisateur (0 si inconnue)
        #endregion

        #region Constructeurs
        public ListeStations()
        {
            this._lesStations = new ArrayList();
            _positionLatitude = 0;
            _positionLongitude = 0;
        }
        public ListeStations(double unePositionLatitude, double unePositionLongitude)
        {	// création de la liste
            this._lesStations = new ArrayList();
            _positionLatitude = unePositionLatitude;
            _positionLongitude = unePositionLongitude;
        }
        #endregion

        #region Propriétés publiques
        public double PositionLatitude
        {
            get
            {
                return this._positionLatitude;
            }
            set
            {
                this._positionLatitude = value;
            }
        }

        public double PositionLongitude
        {
            get
            {
                return this._positionLongitude;
            }
            set
            {
                this._positionLongitude = value;
            }
        }
        #endregion

        #region Méthodes d'instance publiques

        public void ajouteStation(Station uneStation)
        {	// ajoute l'objet à la liste
            this._lesStations.Add(uneStation);
        }

        public Station getLaStation(int i)
        {	// fournit la station correspondant à l'index demandé (ou null si index incorrect)
            if (i < 0 || i >= _lesStations.Count) return null;
            else return (Station)this._lesStations[i];
        }

        public Station getLaStation(String numeroStation)
        {	// fournit la station correspondant au numéro de station demandé (ou null si numéro inexistant)
            // parcours de l'ensemble des stations
            for (int i = 0; i < _lesStations.Count; i++)
            {
                Station uneStation = (Station)this._lesStations[i];
                // si le numéro de station est trouvé, on quitte la fonction
                if (uneStation.Id.Trim() == numeroStation.Trim()) return uneStation;
            }
            return null; 		// si on arrive ici, c'est que le numéro n'a pas été trouvé
        }

        public int getNbStations()
        {	// fournit le nombre de stations dans la liste
            return this._lesStations.Count;
        }

        public void viderListeStations()
        {	// vide la collection des stations
            _lesStations.Clear();
        }

        public double getDistance(Station uneStation)
        {	// fournit la distance entre la position de l'utilisateur et la station
            if (_positionLatitude == 0 && _positionLongitude == 0)
                return -1;	// retourne la valeur -1 si la position de l'utilisateur n'est pas connue (0 , 0)
            else
            {	// appel de la méthode privée distanceBetween et récupération de la distance en km
                double distance = distanceBetween(_positionLatitude, _positionLongitude, uneStation.Latitude, uneStation.Longitude);
                // fournit la distance en mètres (et sous forme entière)
                return Math.Floor(distance * 1000);
            }
        }

        // fournit la distance en km entre 2 coordonnées GPS exprimées en degrés décimaux
        // ce code a été récupéré du forum JavaScript suivant :
        // www.clubic.com/forum/programmation/calcul-de-distance-entre-deux-coordonnees-gps-id178494-page1.html
        // A TESTER ABSOLUMENT
        // (on pourra par exemple utiliser le site http://www.lexilogos.com/calcul_distances.htm)
        private double distanceBetween(double latitude1, double longitude1, double latitude2, double longitude2)
        {
            double a = Math.PI / 180;
            latitude1 = latitude1 * a;
            latitude2 = latitude2 * a;
            longitude1 = longitude1 * a;
            longitude2 = longitude2 * a;
            double t1 = Math.Sin(latitude1) * Math.Sin(latitude2);
            double t2 = Math.Cos(latitude1) * Math.Cos(latitude2);
            double t3 = Math.Cos(longitude1 - longitude2);
            double t4 = t2 * t3;
            double t5 = t1 + t4;
            double rad_dist = Math.Atan(-t5 / Math.Sqrt(-t5 * t5 + 1)) + 2 * Math.Atan(1);
            return (rad_dist * 3437.74677 * 1.1508) * 1.6093470878864446;
        }
        #endregion
    }
}
