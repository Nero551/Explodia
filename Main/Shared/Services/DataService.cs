using System;
using System.Collections.Generic;
using Godot;

public static class DataService
{
    public static Dictionary<string, Data> DataRegistry = [];
    static readonly string MainPath = "res://Main/Shared/Data/JSON";

    static readonly Dictionary<string, Func<Data>> DataTypes = new()
    {
        { "Weapon", () => new WeaponData() },
    };

    public static void Start()
    {
        SearchRecursive(MainPath);
    }

    static void SearchRecursive(string path)
    {
        DirAccess.GetFilesAt(path);
        foreach (string file in DirAccess.GetFilesAt(path))
        {
            if (file.EndsWith(".json"))
            {
                Load(file.Split(".json")[0], $"{path}/{file}");
            }
        }

        foreach (string folder in DirAccess.GetDirectoriesAt(path))
        {
            SearchRecursive($"{path}/{folder}");
        }
    }

    static void Load(string name, string filepath)
    {
        Godot.Collections.Dictionary json = PULib.JSONHelper.JSONToCSharp(filepath);

        if (json.ContainsKey("Type"))
        {
            string dataType = (string)json["Type"];
            if (dataType != null && DataTypes.ContainsKey(dataType))
            {
                Data data = DataTypes[dataType]();
                data.Load(json);
                DataRegistry.Add(name, data);
            }
        }
    }

    // TODO- Replace string lookups with Id once the data registry exists.

    public static bool Find<T>(string name, out T data) where T : Data, new()
    {
        if (DataRegistry.TryGetValue(name, out Data value) && value is T)
        {
            data = value as T;
            return true;
        }
        data = null;
        return false;
    }

    public static T Get<T>(string name) where T : Data, new()
    {
        if (DataRegistry.TryGetValue(name, out Data value) && value is T)
        {
            return value as T;
        }
        throw new Exception($"Data Doesn't Exist: {name}");
    }
}