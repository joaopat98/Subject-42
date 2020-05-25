using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Linq;

public class DataCollector : MonoBehaviour
{
    static string fileName;

    [Serializable]
    private class Data
    {
        public class AreaTime
        {
            public float TotalTime;
            public Dictionary<string, float> AreaTimes = new Dictionary<string, float>();
        }

        public float PlayTime = 0;
        public int NumDeaths = 0;
        public int NumGuardDiscovered = 0;
        public int NumCollectibles = 0;
        public Dictionary<string, AreaTime> Levels = new Dictionary<string, AreaTime>();

        public Data()
        {
            Directory.CreateDirectory("DataCollection");
            fileName = "DataCollection/" + Guid.NewGuid().ToString() + ".json";
        }

    }

    static Data data = new Data();

    static void SaveData()
    {
        foreach (var level in data.Levels)
        {
            data.Levels[level.Key].TotalTime = level.Value.AreaTimes.Sum(area => area.Value);
        }
        File.WriteAllText(fileName, JsonConvert.SerializeObject(data, Formatting.Indented));
    }

    public static void AddDeath()
    {
        data.NumDeaths++;
    }

    public static void AddGuardDiscovery()
    {
        data.NumGuardDiscovered++;
    }

    public static void AddCollectible()
    {
        data.NumCollectibles++;
    }

    public static void AreaUpdate(string level, string area)
    {
        if (!data.Levels.ContainsKey(level))
        {
            data.Levels[level] = new Data.AreaTime();
        }
        if (!data.Levels[level].AreaTimes.ContainsKey(area))
        {
            data.Levels[level].AreaTimes[area] = 0;
        }
        data.Levels[level].AreaTimes[area] += Time.deltaTime;
    }

    void Update()
    {
        data.PlayTime = Time.time;
        SaveData();
    }
}