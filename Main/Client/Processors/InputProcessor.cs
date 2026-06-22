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
    public override void Process(double delta)
    {
        base.Process(delta);

        var movementBlock = Client.Player?.Character?.GetBlock<Blocks.MovementBlock>();

        if (Input.IsActionJustPressed("MouseCapture"))
        {
            Input.MouseMode = Input.MouseMode == Input.MouseModeEnum.Captured ?
                Input.MouseModeEnum.Visible : Input.MouseModeEnum.Captured;
        }
        if (movementBlock != null)
        {
            movementBlock.MoveDirection = Input.GetVector("Left", "Right", "Back", "Forward");
            NetworkService.SendToServer<RemoteEvents.MoveRequest>(Client.Player.Character.Id, movementBlock.MoveDirection);
        }
    }
}
