﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using New_Physics.Entities;

namespace Frogs.src
{
    public static class Level_Loader
    {
        static TextReader tr = new StreamReader(@"Levels.txt");
        static string rawMapData = tr.ReadLine();

        public static void LoadLevel()
        {
            EntityHandler.entities.Clear();

            for (int i = 0; i < rawMapData.Split(':')[1].Split(';').Length; i++)
            {
                String[] rawData = rawMapData.Split(':')[1].Split(';')[i].Split(',');

                switch (rawData[0])
                {
                    case "player":
                        EntityHandler.entities.Add(new Player(float.Parse(rawData[1]), float.Parse(rawData[2])));
                        Console.WriteLine("Player Created");
                        break;
                    case "platform":
                        EntityHandler.entities.Add(new Platform(
                            float.Parse(rawData[1]),
                            float.Parse(rawData[2]),
                            float.Parse(rawData[3]),
                            float.Parse(rawData[4])));
                        Console.WriteLine("Platform Created");
                        break;
                    case "goal":
                        EntityHandler.entities.Add(new Player(
                            float.Parse(rawData[1]),
                            float.Parse(rawData[2])));
                        Console.WriteLine("Goal Created");
                        break;
                }
            }
        }
    }
}