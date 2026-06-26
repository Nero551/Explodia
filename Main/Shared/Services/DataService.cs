using System;
using System.IO;
using Godot;

public static class DataService
{
    public static T Load<T>(string filepath) where T : Data, new()
    {
        Godot.Collections.Dictionary json = PULib.JSONHelper.JSONToCSharp($"res://Main/Shared/Data/JSON/{filepath}.json");
        T data = new();
        data.Load(json);

        return data;
    }
}

