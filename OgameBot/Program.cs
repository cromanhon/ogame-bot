﻿using System;
using System.Globalization;
using OgameBot.Engine;
using OgameBot.Objects;
using OgameBot.Savers;
using OgameBot.Tasks;

namespace OgameBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            const string user = "Rockadingong";
            const string pass = "l0j7eZQjeueuQuvTHWtg";
            const string server = "s117-en.ogame.gameforge.com";

            // Setup
            OGameStringProvider stringProvider = OGameStringProvider.Load(@"Resources\strings-en.json");

            //stringProvider.SetLocalizedName(ResourceType.Metal, "Metal");
            //stringProvider.SetLocalizedName(ResourceType.Crystal, "Crystal");
            //stringProvider.SetLocalizedName(ResourceType.Deuterium, "Deuterium");
            //stringProvider.SetLocalizedName(ResourceType.Energy, "Energy");
            //stringProvider.SetLocalizedName(ShipType.LightFighter, "Light Fighter");
            //stringProvider.SetLocalizedName(ShipType.HeavyFighter, "Heavy Fighter");
            //stringProvider.Save("strings-en.json");

            CultureInfo clientServerCulture = CultureInfo.GetCultureInfo("da-DK");

            // Processing
            OGameClient client = new OGameClient(server, stringProvider, user, pass);
            client.Settings.ServerUtcOffset = TimeSpan.FromHours(1);
            client.Settings.Galaxies = 8;
            client.Settings.Systems = 499;
            client.ServerCulture = clientServerCulture;

            client.RegisterSaver(new GalaxyPageSaver());
            client.RegisterSaver(new EspionageReportSaver());
            client.RegisterSaver(new GalaxyPageDebrisSaver());

            OgameClientProxy xx = new OgameClientProxy("127.0.0.1", 9400, client);
            xx.SubstituteRoot = new Uri($"https://{server}");
            xx.Start();

            client.RegisterDefaultHeader("Accept-Language", "en-GB,en;q=0.8,da;q=0.6");
            client.RegisterDefaultHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            client.RegisterDefaultHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36");

            client.PerformLogin();

            // Preparing
            ScannerJob job = new ScannerJob(client, new SystemCoordinate(6, 60), new SystemCoordinate(6, 100));
            job.Start();

            // Work
            Console.ReadLine();
        }
    }
}
