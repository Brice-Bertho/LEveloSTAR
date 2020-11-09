// Classe PasserelleJSON
// Auteur : JM CARTRON
// Dernière mise à jour : 26/9/2018

using System;
using System.IO;
using Newtonsoft.Json;

/* Cette classe fait le lien entre les services web Json et l'application */
namespace LEveloSTAR
{
    public class PasserelleJSON : Passerelle
    {
        #region Attributs privés
        // placez ici votre clé API obtenue avec votre compte Keolis
        private static String _apiKey = "af750df5339a6cdb784725b233576a1fbae134b647d3e34372eb0319";

        // adresse de l'hébergeur des servies web, avec quelques paramètres communs aux 2 services (lang, pretty_print et apikey)
        private static String _adresseHebergeur = "http://data.explore.star.fr/api/records/1.0/search/?lang=fr&pretty_print=true&apikey=" + _apiKey;

        // complément de l'URL du service web fournissant la liste des stations
        private static String _urlListeStations = "&dataset=vls-stations-topologie-td"
            + "&rows=100"
            + "&facet=coordonnees"
            + "&facet=nombreemplacementstheorique"
            + "&facet=possedetpe"
            + "&facet=id"
            + "&facet=nom"
            + "&facet=adressenumero"
            + "&facet=adressevoie";

        // complément de l'URL du service web fournissant le détail d'une station en temps réel
        private static String _urlDetailStation = "&dataset=vls-stations-etat-tr"
            + "&rows=10"
            + "&facet=nom"
            + "&facet=etat"
            + "&facet=nombreemplacementsdisponibles"
            + "&facet=nombrevelosdisponibles"
            + "&facet=idstation"
            + "&facet=coordonnees"
            + "&q=idstation=";

        // lit le document jusqu'au noeud demandé en paramètre ou jusqu'à la fin du document
        // fournit true si la fin du document a été atteinte, false sinon
        private static bool avancerJusqua(JsonTextReader leDocument, String noeud)
        {
            bool trouve = false;
            bool finDocument = false;
            while (trouve == false && finDocument == false)
            {
                finDocument = !leDocument.Read();  // Read fournit false quand la fin du document est atteinte
                try { if (leDocument.Value.ToString() == noeud) trouve = true; }
                catch (Exception ex) { }            // catch évite le plantage pour les noeuds où Value = null
            }
            return finDocument;
        }
        #endregion

        #region Méthodes publiques d'instance
        // Méthode publique d'instance pour mettre à jour un objet ListeStations avec l'ensemble des stations de la ville
        // L'objet ListeStations à mettre à jour est reçu en paramètre
        // Retourne un message d'erreur en cas d'erreur d'exécution, ou une chaine vide si tout s'est bien passé
        public override String getListeStations(ListeStations laListeStations)
        {
            /* Exemple de données obtenues pour une station :
                    "fields": {
                        "nom": "R\u00e9publique", 
                        "idstationmetrocorrespondance": "REP", 
                        "adressenumero": "19", 
                        "datemiseservice": "2009-06-22", 
                        "adressevoie": "Quai Lamartine", 
                        "nomcommune": "Rennes", 
                        "idstationproche1": 2, 
                        "possedetpe": "true", 
                        "nombreemplacementstheorique": 30, 
                        "codeinseecommune": "35238", 
                        "coordonnees": [
                            48.1100259201, 
                            -1.6780371631
                        ], 
                        "id": 1, 
                        "idstationproche2": 11, 
                        "idstationproche3": 10
                    },
            */

            // initialisation des variables
            Station uneStation = new Station();
            String id = "", nom = "", adressenumero = "", adressevoie = "", adresse = "";
            double latitude = 0, longitude = 0;
            bool possedetpe = false;

            try
            {
                // création d'un flux en lecture (StreamReader) à partir du service web
                String url = _adresseHebergeur + _urlListeStations;
                StreamReader unFluxEnLecture = getFluxEnLecture(url);
                // création d'un objet JsonTextReader à partir du flux ; il servira à parcourir le flux JSON
                JsonTextReader leDocument = getDocumentJson(unFluxEnLecture);

                // vide la liste actuelle des stations
                laListeStations.viderListeStations();

                // lire le document jusqu'à l'élément "fields" avec la méthode privée avancerJusqua
                bool finDocument = avancerJusqua(leDocument, "fields");

                // traiter jusqu'à la fin du document
                while (!finDocument)
                {
                    leDocument.Read();	// lire l'élément suivant
                    switch (leDocument.TokenType)	// traiter en fonction du type d'élément
                    {
                        case JsonToken.StartObject:	// l'élément est un début d'objet JSON (donc une station)
                            // on crée un objet vide uneStation
                            uneStation = new Station();
                            break;
                        case JsonToken.PropertyName:	// l'élément est une propriété de l'objet JSON
                            switch (leDocument.Value.ToString())	// traiter en fonction du nom de la propriété
                            {
                                case "id": leDocument.Read();
                                    id = leDocument.Value.ToString(); break;
                                case "nom": leDocument.Read();
                                    nom = leDocument.Value.ToString(); break;
                                case "adressenumero": leDocument.Read();
                                    adressenumero = leDocument.Value.ToString(); break;
                                case "adressevoie": leDocument.Read();
                                    adressevoie = leDocument.Value.ToString(); break;
                                case "possedetpe": leDocument.Read();
                                    possedetpe = Convert.ToBoolean(leDocument.Value.ToString()); break;
                                case "coordonnees":		// l'élément est un le tableau des coordonnées
                                    leDocument.Read();	// lit l'élément début de tableau
                                    leDocument.Read();	// lit le premier nombre 
                                    // (attention à la présence du point décimal à remplacer par virgule)
                                    latitude = Convert.ToDouble(leDocument.Value.ToString().Replace(".", ","));
                                    leDocument.Read();	// lit le second nombre 
                                    // (attention à la présence du point décimal à remplacer par virgule)
                                    longitude = Convert.ToDouble(leDocument.Value.ToString().Replace(".", ","));
                                    break;
                            } // fin switch interne
                            break;
                        case JsonToken.EndObject:	// l'élément est une fin d'objet JSON (donc une station)
                            // on valorise la station et on l'ajoute à l'objet laListeStations
                            adresse = adressenumero + " " + adressevoie;
                            uneStation.valoriser(id, nom, adresse, latitude, longitude, possedetpe);
                            laListeStations.ajouteStation(uneStation);
                            // lire le document jusqu'à l'élément "fields" avec la méthode privée avancerJusqua
                            finDocument = avancerJusqua(leDocument, "fields");
                            break;
                    } // fin switch externe
                }
                return "";					// il n'y a pas de problème, on retourne une chaine vide

            }
            catch (Exception ex)
            {
                return "Erreur : " + ex.Message;	// il y a un problème, on retourne le message d'ereur
            }
        }

        // Méthode publique d'instance pour mettre à jour un objet Station avec les données détaillées d'une station
        // L'objet Station à mettre à jour est reçu en paramètre
        // Retourne un message d'erreur en cas d'erreur d'exécution, ou une chaine vide si tout s'est bien passé
        public override String getDetailStation(Station uneStation)
        {
            /* Exemple de données obtenues pour une station :
                    "fields": {
                        "etat": "En fonctionnement", 
                        "lastupdate": "2017-01-31T21:31:04+00:00", 
                        "nombrevelosdisponibles": 1, 
                        "nombreemplacementsactuels": 20, 
                        "nom": "Gares Sud - F\u00e9val", 
                        "nombreemplacementsdisponibles": 19, 
                        "idstation": 45, 
                        "coordonnees": [
                            48.10225077, 
                            -1.673133314
                        ]
                    }, 
            */

            // initialisation des variables
            int soclesDisponibles = 0, velosDisponibles = 0;
            String etat = ""; 

            try
            {
                // création d'un flux en lecture (StreamReader) à partir du service web
                String url = _adresseHebergeur + _urlDetailStation + uneStation.Id;
                StreamReader unFluxEnLecture = getFluxEnLecture(url);
                // création d'un objet JsonTextReader à partir du flux ; il servira à parcourir le flux JSON
                JsonTextReader leDocument = getDocumentJson(unFluxEnLecture);
                // vide la liste actuelle des stations
                //laListeStations.viderListeStations();

                // lire le document jusqu'à l'élément "fields" avec la méthode privée avancerJusqua
                bool finDocument = avancerJusqua(leDocument, "fields");

                // traiter jusqu'à la fin du document
                while (!finDocument)
                {
                    leDocument.Read();	// lire l'élément suivant
                    switch (leDocument.TokenType)	// traiter en fonction du type d'élément
                    {
                        case JsonToken.StartObject:	// l'élément est un début d'objet JSON (donc une station)
                            break;
                        case JsonToken.PropertyName:	// l'élément est une propriété de l'objet JSON
                            switch (leDocument.Value.ToString())	// traiter en fonction du nom de la propriété
                            {
                                case "etat": leDocument.Read();
                                    etat = leDocument.Value.ToString(); break;
                                case "nombrevelosdisponibles": leDocument.Read();
                                    velosDisponibles = Convert.ToInt32(leDocument.Value); break;
                                case "nombreemplacementsdisponibles": leDocument.Read();
                                    soclesDisponibles = Convert.ToInt32(leDocument.Value); break;
                            } // fin switch interne
                            break;
                        case JsonToken.EndObject:	// l'élément est une fin d'objet JSON (donc une station)
                            // on valorise la station et on l'ajoute à l'objet laListeStations
                            uneStation.valoriser(soclesDisponibles, velosDisponibles, etat);
                            // lire le document jusqu'à l'élément "fields" avec la méthode privée avancerJusqua
                            finDocument = true;
                            break;
                    } // fin switch externe
                }
                return "";						    // il n'y a pas de problème, on retourne une chaine vide
            }
            catch (Exception ex)
            {
                return "Erreur : " + ex.Message;	// il y a un problème, on retourne le message d'ereur
            }
        }
        #endregion
    }
}
