// TP C# réalisé sous Visual Studio 2013
// Thème : application cliente du service web LE Vélo STAR de la ville de Rennes
// Auteur : JM CARTRON
// Dernière mise à jour : 16/9/2017

using System;
using System.Collections;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Xml;				// permet d'utiliser les classes XML

/* Cette classe fait le lien entre les services web XML et l'application */
namespace LEveloSTAR
{
	public class PasserelleXML : Passerelle
	{
		#region Membres privés
		private static String _adresseHebergeur = "http://data.keolis-rennes.com/xml/?version=2.0&key=LIJ1LX17HLSP31B&";

		private static String _urlListeStations = "cmd=getbikestations";
		private static String _urlDetailStation = "cmd=getbikestations&param[station]=number&param[value]=";
		private static String _urlListeStationsProches = "cmd=getbikestations&param[station]=proximity&param[mode]=coord&param[lat]=LATITUDE&param[long]=LONGITUDE";
		private static String _urlListeDistricts = "cmd=getbikedistricts";

		// fonction privée statique pour obtenir un document XML à partir de l'URL d'un service web
		private static XmlReader getDocumentXML(String urlDuServiceWeb)
		{	// création d'une requête http
			HttpWebRequest uneRequeteHttp = (HttpWebRequest)WebRequest.Create(urlDuServiceWeb);
			uneRequeteHttp.Method = WebRequestMethods.Http.Get;
			// récupération de la réponse
			WebResponse uneReponseHttp = uneRequeteHttp.GetResponse();
			// création d'un flux en lecture (SteamReader) à partir de la réponse
			StreamReader unFluxEnEntree = new StreamReader(uneReponseHttp.GetResponseStream());
			// création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
			XmlReader leDocument = XmlReader.Create(unFluxEnEntree);
			return leDocument;
		}

		// fonction privée statique pour valoriser l'objet uneStation à partir d'un jeu de balises XML
		private static void getDonneesStation(XmlReader leDocument, Station uneStation)
		{
			/* Exemple de données obtenues pour une station :
				  <station>
					<number>75</number>
					<name>ZAC SAINT SULPICE</name>
					<address>RUE DE FOUGÈRES</address>
					<state>1</state>
					<latitude>48.1321</latitude>
					<longitude>-1.63528</longitude>
					<slotsavailable>20</slotsavailable>
					<bikesavailable>8</bikesavailable>
					<pos>0</pos>
					<district>Maurepas - Patton</district>
					<lastupdate>2013-01-29T11:23:02+01:00</lastupdate>
				  </station>
			*/

			// parcours des balises XML
			leDocument.ReadToFollowing("number");
			leDocument.Read();
			String number = leDocument.Value;

			leDocument.ReadToFollowing("name");
			leDocument.Read();
			String name = leDocument.Value;

			leDocument.ReadToFollowing("address");
			leDocument.Read();
			String address = leDocument.Value;

			leDocument.ReadToFollowing("state");
			leDocument.Read();
			String state = leDocument.Value;
			bool open = false;
			if (state == "1") open = true;

			leDocument.ReadToFollowing("latitude");
			leDocument.Read();
			double latitude = Convert.ToDouble(leDocument.Value.Replace(".", ","));

			leDocument.ReadToFollowing("longitude");
			leDocument.Read();
			double longitude = Convert.ToDouble(leDocument.Value.Replace(".", ","));

			leDocument.ReadToFollowing("slotsavailable");
			leDocument.Read();
			int slotsavailable = Convert.ToInt32(leDocument.Value);

			leDocument.ReadToFollowing("bikesavailable");
			leDocument.Read();
			int bikesavailable = Convert.ToInt32(leDocument.Value);

			leDocument.ReadToFollowing("pos");
			leDocument.Read();
			String pos = leDocument.Value;
			bool paiementCarte = false;
			if (pos == "1") paiementCarte = true;

			leDocument.ReadToFollowing("district");
			leDocument.Read();
			String district = leDocument.Value;

			uneStation.valoriser(number, name, address, open, latitude, longitude, slotsavailable, bikesavailable, paiementCarte, district);
		}

		// fonction privée statique pour valoriser l'objet unDistrict à partir d'un jeu de balises XML
		private static void getDonneesDistrict(XmlReader leDocument, District unDistrict)
		{
			/* Exemple de données obtenues pour un district :
				  <district>
					<id>34</id>
					<name>Sud-Gare</name>
				  </district>
			*/

			// parcours des attributs d'un district
			leDocument.ReadToFollowing("id");
			leDocument.Read();
			String id = leDocument.Value;

			leDocument.ReadToFollowing("name");
			leDocument.Read();
			String name = leDocument.Value;

			unDistrict.valoriser(id, name);
		}
		#endregion

		#region Méthodes publiques d'instance
		// Méthode publique d'instance pour mettre à jour un objet ListeStations avec l'ensemble des stations de la ville
		public override String getListeStations(ListeStations laListeStations)
		{
			try
			{	// création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
				String url = _adresseHebergeur + _urlListeStations;
				XmlReader leDocument = getDocumentXML(url);

				// vide la liste actuelle des stations
				laListeStations.viderListeStations();

				// démarrer le parcours au premier noeud de type <station>
				leDocument.ReadToFollowing("station");
				do
				{	// crée un objet vide uneStation
					Station uneStation = new Station();

					// valorise l'objet uneStation à partir d'un jeu de balises XML
					getDonneesStation(leDocument, uneStation);

					// ajoute la station à l'objet ListeStations
					laListeStations.ajouteStation(uneStation);
				}
				while (leDocument.ReadToFollowing("station"));	// continue au noeud suivant de type <station>

				return "";						    // il n'y a pas de problème
			}
			catch (Exception ex)
			{
				return "Erreur : " + ex.Message;	// il y a un problème
			}
		}

		// Méthode publique d'instance pour mettre à jour un objet Station avec les données détaillées d'une station
		public override String getDetailStation(Station uneStation)
		{
			try
			{	// création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
				String url = _adresseHebergeur + _urlDetailStation + uneStation.Number;
				XmlReader leDocument = getDocumentXML(url);

				// valorise l'objet uneStation à partir d'un jeu de balises XML
				getDonneesStation(leDocument, uneStation);

				return "";						    // il n'y a pas de problème
			}
			catch (Exception ex)
			{
				return "Erreur : " + ex.Message;	// il y a un problème
			}
		}

		// Méthode publique d'instance pour mettre à jour un objet ListeDistricts avec l'ensemble des districts de la ville
		public override String getListeDistricts(ListeDistricts laListeDistricts)
		{
			try
			{	// création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
				String url = _adresseHebergeur + _urlListeDistricts;
				XmlReader leDocument = getDocumentXML(url);

				// vide la liste actuelle des districts
				laListeDistricts.viderListeDistricts();

				// démarrer le parcours au premier noeud de type <district>
				leDocument.ReadToFollowing("district");
				do
				{	// on crée un objet vide unDistrict
					District unDistrict = new District();

					// on valorise l'objet unDistrict à partir d'un jeu de balises JSON
					getDonneesDistrict(leDocument, unDistrict);

					// ajoute l'objet à laListeDistricts
					laListeDistricts.ajouteDistrict(unDistrict);
				}
				while (leDocument.ReadToFollowing("district"));		// continue au noeud suivant de type <district>
				return "";						    // il n'y a pas de problème
			}
			catch (Exception ex)
			{
				return "Erreur : " + ex.Message;	// il y a un problème
			}
		}

		// Méthode publique d'instance pour mettre à jour un objet ListeStations avec les 3 stations les plus proches d'une position géographique
		public override String getListeStationsProches(ListeStations laListeStations)
		{
			try
			{	// création d'un objet XmlReader à partir du flux ; il servira à parcourir le flux XML
				String strLatitude = Convert.ToString(laListeStations.PositionLatitude).Replace(",", ".");
				String strLongitude = Convert.ToString(laListeStations.PositionLongitude).Replace(",", ".");
				String url = _adresseHebergeur + _urlListeStationsProches.Replace("LATITUDE", strLatitude).Replace("LONGITUDE", strLongitude);
				XmlReader leDocument = getDocumentXML(url);

				// vide la liste actuelle des stations
				laListeStations.viderListeStations();

				// démarrer le parcours au premier noeud de type <station>
				leDocument.ReadToFollowing("station");
				do
				{	// crée un objet vide uneStation
					Station uneStation = new Station();

					// valorise l'objet uneStation à partir d'un jeu de balises XML
					getDonneesStation(leDocument, uneStation);

					// ajoute la station à l'objet ListeStations
					laListeStations.ajouteStation(uneStation);
				}
				while (leDocument.ReadToFollowing("station"));	// continue au noeud suivant de type <station>

				return "";						    // il n'y a pas de problème
			}
			catch (Exception ex)
			{
				return "Erreur : " + ex.Message;	// il y a un problème
			}
		}
		#endregion
	}
}
