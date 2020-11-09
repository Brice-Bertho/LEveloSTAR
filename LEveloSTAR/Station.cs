// Classe Station
// Auteur : JM CARTRON
// Dernière mise à jour : 19/9/2017

using System;

// Cette classe représente une station VéloStar
namespace LEveloSTAR
{
    public class Station
    {
        #region Attributs privés
        private String _id;				    // numéro de la station
        private String _nom;				// nom de la station
        private String _adresse;			// adresse de la station
        private double _latitude;           // coordonnées GPS
        private double _longitude;          // coordonnées GPS
        private bool _possedeTpe;		    // indique si le paiement par carte est possible
        private int _soclesDisponibles;	    // nombre de socles disponibles
        private int _velosDisponibles;	    // nombre de vélos disponibles
        private String _etat;				// "En fonctionnement" ou "En panne"
        #endregion

        #region Constructeurs
        public Station()
        {
            this._id = "";
            this._nom = "";
            this._adresse = "";
            this._latitude = 0;
            this._longitude = 0;
            this._possedeTpe = false;
            this._soclesDisponibles = 0;
            this._velosDisponibles = 0;
            this._etat = "";
        }
        public Station(String id, String nom, String addresse, double latitude, double longitude, bool possedeTpe, int soclesDisponibles, int velosDisponibles, String etat)
        {
            this._id = id.Trim();
            this._nom = nom.Trim();
            this._adresse = addresse.Trim();
            this._latitude = latitude;
            this._longitude = longitude;
            this._possedeTpe = possedeTpe;
            this._soclesDisponibles = soclesDisponibles;
            this._velosDisponibles = velosDisponibles;
            this._etat = etat;
        }
        #endregion

        #region Propriétés publiques
        public String Id
        {
            get
            {
                return this._id;
            }
        }

        public String Nom
        {
            get
            {
                return this._nom;
            }
        }

        public String Adresse
        {
            get
            {
                return this._adresse;
            }
        }

        public double Latitude
        {
            get
            {
                return this._latitude;
            }
        }

        public double Longitude
        {
            get
            {
                return this._longitude;
            }
        }

        public bool PossedeTpe
        {
            get
            {
                return this._possedeTpe;
            }
        }

        public int SoclesDisponibles
        {
            get
            {
                return this._soclesDisponibles;
            }
        }

        public int VelosDisponibles
        {
            get
            {
                return this._velosDisponibles;
            }
        }

        public String Etat
        {
            get
            {
                return this._etat;
            }
        }
        #endregion

        #region Méthodes publiques
        // valorise la station avec ses données stables
        public void valoriser(String id, String nom, String addresse, double latitude, double longitude, bool possedeTpe)
        {
            this._id = id.Trim();
            this._nom = nom.Trim();
            this._adresse = addresse.Trim();
            this._latitude = latitude;
            this._longitude = longitude;
            this._possedeTpe = possedeTpe;
        }

        // valorise la station avec ses données variables dans le temps
        public void valoriser(int soclesDisponibles, int velosDisponibles, String etat)
        {
            this._soclesDisponibles = soclesDisponibles;
            this._velosDisponibles = velosDisponibles;
            this._etat = etat;
        }

        public override String ToString()
        {
            String msg = "";
            msg += "Numéro :\t\t" + this.Id + "\n";
            msg += "Nom :\t\t" + this.Nom + "\n";
            msg += "Adresse :\t\t" + this.Adresse + "\n";
            msg += "Latitude :\t" + this.Latitude + "\n";
            msg += "Longitude :\t" + this.Longitude + "\n";
            msg += "Paiement carte :\t" + this.PossedeTpe + "\n";
            msg += "Attaches libres :\t" + this.SoclesDisponibles + "\n";
            msg += "Vélos libres :\t" + this.VelosDisponibles + "\n";
            msg += "Etat :\t\t" + this.Etat + "\n";
            return msg;
        }
        #endregion
    }
}

