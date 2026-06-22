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
        // var node = entity.GetNode<CharacterBody3D>();
        // var target = entity.GetBlock<TransformBlock>().Position;
        // node.Position = node.Position.Lerp(target, 0.05f);
        // var movementBlock = Client.Player.GetBlock<Blocks.MovementBlock>();
        // movementBlock.MoveDirection = Input.GetVector("Left", "Right", "Back", "Forward");
        // NetworkService.SendToServer<RemoteEvents.MoveRequest>(Client.Player.Id, movementBlock.MoveDirection);
        
        if (Input.IsActionJustPressed("MouseCapture"))
        {
            Input.MouseMode = Input.MouseMode == Input.MouseModeEnum.Captured ?
                Input.MouseModeEnum.Visible : Input.MouseModeEnum.Captured;
        }


    }
}
