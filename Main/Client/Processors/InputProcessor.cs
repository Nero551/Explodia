using System;
using Blocks;
using Godot;


namespace Processors;

public class InputProcessor : Processor
{
    public override void Start()
    {
        base.Start();
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    double elapsed = 0;
    public override void Process(double delta)
    {
        base.Process(delta);
        if (Client.Player == null || Client.Player.Character == null)
            return;

        if (Input.IsActionJustPressed("MouseCapture"))
        {
            Input.MouseMode = Input.MouseMode == Input.MouseModeEnum.Captured ?
                Input.MouseModeEnum.Visible : Input.MouseModeEnum.Captured;
        }

        elapsed += delta;
        if (elapsed >= 0.05)
        {
            var moveDirection = Input.GetVector("Left", "Right", "Back", "Forward");
            NetworkService.SendToServer<RemoteEvents.MoveRequest>(Client.Player.Character.Id, moveDirection);
            elapsed = 0;
        }
    }
}
