// Classe abstraite Passerelle
// Cette classe abstraite sera dérivée en PasserelleJSON
// Auteur : JM CARTRON
// Dernière mise à jour : 26/9/2018

using System;
using System.Net;
using System.IO;
using System.Xml;				// permet d'utiliser les classes XML
using Newtonsoft.Json;          // permet d'utiliser les classes Json

namespace LEveloSTAR
{
	public abstract class Passerelle
	{
        // méthode protégée statique pour obtenir un flux en lecture 
        // à partir de l'adresse d'un fichier ou de l'URL d'un service web
        protected static StreamReader getFluxEnLecture(String adrFichierOuServiceWeb)
        {
            StreamReader unFluxEnLecture;
            if (adrFichierOuServiceWeb.StartsWith("http"))
            {   // l'adresse fournie est l'URL d'un service web car elle commence par "http"
                // création d'une requête http
                HttpWebRequest uneRequeteHttp = (HttpWebRequest)WebRequest.Create(adrFichierOuServiceWeb);
                uneRequeteHttp.Method = WebRequestMethods.Http.Get;
                // récupération de la réponse
                WebResponse uneReponseHttp = uneRequeteHttp.GetResponse();
                // création d'un flux en lecture (SteamReader) à partir de la réponse web
                unFluxEnLecture = new StreamReader(uneReponseHttp.GetResponseStream());
            }
            else
            {   // l'adresse fournie est celle d'un fichier
                // création d'un flux en lecture (StreamReader) depuis le fichier
                unFluxEnLecture = File.OpenText(adrFichierOuServiceWeb);
            }
            return unFluxEnLecture;
        }

        // méthode protégée statique pour obtenir un document XML à partir d'un flux de données en lecture
        // paramètre unFluxEnLecture : un flux de données en lecture (System.IO.StreamReader)
        // retourne : un document XML (System.Xml.Document)
        protected static XmlReader getDocumentXML(StreamReader unFluxEnLecture)
        {
            try
            {
                XmlReader leDocument = XmlReader.Create(unFluxEnLecture);
                return leDocument;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // méthode protégée statique pour obtenir un document Json à partir d'un flux de données en lecture
        // paramètre unFluxEnLecture : un flux de données en lecture (System.IO.StreamReader)
        // retourne : un document Json (JsonTextReader)
        protected static JsonTextReader getDocumentJson(StreamReader unFluxEnLecture)
        {
            // lecture complète et fermeture du fichier
            String leTexteJson = unFluxEnLecture.ReadToEnd();
            unFluxEnLecture.Close();

            // création d'un objet JsonTextReader à partir du flux ; il servira à parcourir le flux JSON
            JsonTextReader leDocument = new JsonTextReader(new StringReader(leTexteJson));
            return leDocument;
        }

		#region Méthodes abstraites
		// Méthode publique d'instance pour mettre à jour un objet ListeStations avec l'ensemble des stations de la ville
        public abstract String getListeStations(ListeStations laListeStations);

		// Méthode publique d'instance pour mettre à jour un objet Station avec les données détaillées d'une station
		public abstract String getDetailStation(Station uneStation);
		#endregion
	}
}
