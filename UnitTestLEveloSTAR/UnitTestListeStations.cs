// Test de la classe ListeStations
// Auteur : JM CARTRON
// Dernière mise à jour : 19/9/2017

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LEveloSTAR;

namespace UnitTestLEveloSTAR
{
    [TestClass]
    public class UnitTestListeStations
    {
        // Test de la méthode ajouteStation
        [TestMethod]
        public void ajouteStationTest()
        {
            ListeStations laListeStations = new ListeStations();
            Assert.AreEqual(0, laListeStations.getNbStations());

            Station laStation = new Station("75", "ZAC SAINT SULPICE", "RUE DE FOUGÈRES", 48.1321, -1.63528, true, 20, 8, "En fonctionnement");
            laListeStations.ajouteStation(laStation);
            Assert.AreEqual(1, laListeStations.getNbStations());

            laStation = new Station("54", "PONTCHAILLOU (TER)", "26 AVENUE DU 41ÈME RÉGIMENT D'INFANTERIE", 48.119316, -1.691517, false, 13, 5, "En panne");
            laListeStations.ajouteStation(laStation);
            Assert.AreEqual(2, laListeStations.getNbStations());

            // vérifier que la première station a bien été créée
            laStation = laListeStations.getLaStation("75");
            Assert.AreEqual("75", laStation.Id);
            Assert.AreEqual("ZAC SAINT SULPICE", laStation.Nom);
            Assert.AreEqual("RUE DE FOUGÈRES", laStation.Adresse);
            Assert.AreEqual(48.1321, laStation.Latitude, 0.0001);
            Assert.AreEqual(-1.63528, laStation.Longitude, 0.0001);
            Assert.AreEqual(true, laStation.PossedeTpe);
            Assert.AreEqual(20, laStation.SoclesDisponibles);
            Assert.AreEqual(8, laStation.VelosDisponibles);
            Assert.AreEqual("En fonctionnement", laStation.Etat);

            // vérifier que la deuxième station a bien été créée
            laStation = laListeStations.getLaStation("54");
            Assert.AreEqual("54", laStation.Id);
            Assert.AreEqual("PONTCHAILLOU (TER)", laStation.Nom);
            Assert.AreEqual("26 AVENUE DU 41ÈME RÉGIMENT D'INFANTERIE", laStation.Adresse);
            Assert.AreEqual(48.119316, laStation.Latitude, 0.0001);
            Assert.AreEqual(-1.691517, laStation.Longitude, 0.0001);
            Assert.AreEqual(false, laStation.PossedeTpe);
            Assert.AreEqual(13, laStation.SoclesDisponibles);
            Assert.AreEqual(5, laStation.VelosDisponibles);
            Assert.AreEqual("En panne", laStation.Etat);
        }

        // Test de la méthode getLaStation
        [TestMethod]
        public void getLaStationsTest()
        {
            ListeStations laListeStations = new ListeStations();
            Station laStation = new Station("75", "ZAC SAINT SULPICE", "RUE DE FOUGÈRES", 48.1321, -1.63528, true, 20, 8, "En fonctionnement");
            laListeStations.ajouteStation(laStation);
            laStation = new Station("54", "PONTCHAILLOU (TER)", "26 AVENUE DU 41ÈME RÉGIMENT D'INFANTERIE", 48.119316, -1.691517, false, 13, 5, "En panne");
            laListeStations.ajouteStation(laStation);

            // test de la surcharge 1 utilisant le numéro de station (String)
            laStation = laListeStations.getLaStation("75");
            Assert.AreEqual("75", laStation.Id);

            laStation = laListeStations.getLaStation("54");
            Assert.AreEqual("54", laStation.Id);

            // test de la surcharge 2 utilisant la position de la station dans la collection (int)
            laStation = laListeStations.getLaStation(0);
            Assert.AreEqual("75", laStation.Id);

            laStation = laListeStations.getLaStation(1);
            Assert.AreEqual("54", laStation.Id);
        }

        // Test de la méthode getNbStations
        [TestMethod]
        public void getNbStationsTest()
        {
            ListeStations laListeStations = new ListeStations();
            Assert.AreEqual(0, laListeStations.getNbStations());

            Station laStation = new Station("75", "ZAC SAINT SULPICE", "RUE DE FOUGÈRES", 48.1321, -1.63528, true, 20, 8, "En fonctionnement");
            laListeStations.ajouteStation(laStation);
            Assert.AreEqual(1, laListeStations.getNbStations());

            laStation = new Station("54", "PONTCHAILLOU (TER)", "26 AVENUE DU 41ÈME RÉGIMENT D'INFANTERIE", 48.119316, -1.691517, false, 13, 5, "En panne");
            laListeStations.ajouteStation(laStation);
            Assert.AreEqual(2, laListeStations.getNbStations());
        }

        // Test de la méthode getDistance
        [TestMethod]
        public void getDistanceTest()
        {
            // on simule un positionnement à la station 75 (rue de Fougères)
            ListeStations laListeStations = new ListeStations(48.1321, -1.63528);
            Assert.AreEqual(48.1321, laListeStations.PositionLatitude, 0.0001);
            Assert.AreEqual(-1.63528, laListeStations.PositionLongitude, 0.0001);

            Station laStation = new Station("75", "ZAC SAINT SULPICE", "RUE DE FOUGÈRES", 48.1321, -1.63528, true, 20, 8, "En fonctionnement");
            laListeStations.ajouteStation(laStation);
            // pour cette station, la distance attendue est nulle
            Assert.AreEqual(0, laListeStations.getDistance(laListeStations.getLaStation("75")), 0.001);

            laStation = new Station("54", "PONTCHAILLOU (TER)", "26 AVENUE DU 41ÈME RÉGIMENT D'INFANTERIE", 48.119316, -1.691517, false, 13, 5, "En panne");
            laListeStations.ajouteStation(laStation);
            // on tolère une approximation de 5 m pour une distance attendue de 4409 m
            Assert.AreEqual(4409.0, laListeStations.getDistance(laListeStations.getLaStation("54")), 5);
        }
    }
}
