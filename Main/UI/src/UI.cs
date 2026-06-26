using System;
using System.Collections.Generic;
using Godot;

public partial class UI : CanvasLayer
{
    private readonly List<Controller> Controllers = [];
    void AddController<T>() where T : Controller, new()
    {
        T controller = new();
        Controllers.Add(controller);
        controller.Initialize();
    }

    public override void _Ready()
    {
        EventService.Subscribe<Events.Network.ConnectedToServer>(Initialize);
    }

    private void Initialize(Events.Network.ConnectedToServer evnt)
    {
        AddController<HUDContoller>();
    }

    public T GetUINode<T>(string path) where T : Control
    {
        var controlNode = GetNodeOrNull<T>(path) ?? throw new Exception($"Control Node Doesn't Exist : {path}");
        return controlNode;
    }

}

