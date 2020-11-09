// Test de la classe Station
// Auteur : JM CARTRON
// Dernière mise à jour : 19/9/2017

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LEveloSTAR;

namespace UnitTestLEveloSTAR
{
    [TestClass]
    public class UnitTestStation
    {
        // Test de la méthode valoriser
        [TestMethod]
        public void valoriserTest()
        {
            Station uneStation = new Station();
            uneStation.valoriser("75", "ZAC SAINT SULPICE", "RUE DE FOUGÈRES", 48.1321, -1.63528, true);
            Assert.AreEqual("75", uneStation.Id);
            Assert.AreEqual("ZAC SAINT SULPICE", uneStation.Nom);
            Assert.AreEqual("RUE DE FOUGÈRES", uneStation.Adresse);
            Assert.AreEqual(48.1321, uneStation.Latitude, 0.0001);
            Assert.AreEqual(-1.63528, uneStation.Longitude, 0.0001);
            Assert.AreEqual(true, uneStation.PossedeTpe);
            Assert.AreEqual(0, uneStation.SoclesDisponibles);
            Assert.AreEqual(0, uneStation.VelosDisponibles);
            Assert.AreEqual("", uneStation.Etat);

            uneStation.valoriser(20, 8, "En fonctionnement");
            Assert.AreEqual("75", uneStation.Id);
            Assert.AreEqual("ZAC SAINT SULPICE", uneStation.Nom);
            Assert.AreEqual("RUE DE FOUGÈRES", uneStation.Adresse);
            Assert.AreEqual(48.1321, uneStation.Latitude, 0.0001);
            Assert.AreEqual(-1.63528, uneStation.Longitude, 0.0001);
            Assert.AreEqual(true, uneStation.PossedeTpe);
            Assert.AreEqual(20, uneStation.SoclesDisponibles);
            Assert.AreEqual(8, uneStation.VelosDisponibles);
            Assert.AreEqual("En fonctionnement", uneStation.Etat);
        }

        // Test du Constructeur Station
        [TestMethod]
        public void StationConstructorTest()
        {
            Station uneStation = new Station();
            Assert.AreEqual("", uneStation.Id);
            Assert.AreEqual("", uneStation.Nom);
            Assert.AreEqual("", uneStation.Adresse);
            Assert.AreEqual(0, uneStation.Latitude, 0.0001);
            Assert.AreEqual(0, uneStation.Longitude, 0.0001);
            Assert.AreEqual(false, uneStation.PossedeTpe);
            Assert.AreEqual(0, uneStation.SoclesDisponibles);
            Assert.AreEqual(0, uneStation.VelosDisponibles);
            Assert.AreEqual("", uneStation.Etat);

            uneStation = new Station("75", "ZAC SAINT SULPICE", "RUE DE FOUGÈRES", 48.1321, -1.63528, true, 20, 8, "En fonctionnement");
            Assert.AreEqual("75", uneStation.Id);
            Assert.AreEqual("ZAC SAINT SULPICE", uneStation.Nom);
            Assert.AreEqual("RUE DE FOUGÈRES", uneStation.Adresse);
            Assert.AreEqual(48.1321, uneStation.Latitude, 0.0001);
            Assert.AreEqual(-1.63528, uneStation.Longitude, 0.0001);
            Assert.AreEqual(true, uneStation.PossedeTpe);
            Assert.AreEqual(20, uneStation.SoclesDisponibles);
            Assert.AreEqual(8, uneStation.VelosDisponibles);
            Assert.AreEqual("En fonctionnement", uneStation.Etat);
        }
    }
}
