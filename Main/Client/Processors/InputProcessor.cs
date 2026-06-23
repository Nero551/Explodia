using System;
using Blocks;
using Godot;


namespace Processors;

public class InputProcessor : Processor
{
    public override void Start()
    {
        base.Start();
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

        // if (Input.IsActionPressed("M1"))
        // {
        //     character.cCombat.M1();
        // }
        // if (Input.IsActionPressed("M2"))
        // {
        //     character.cCombat.M2();
        // }

        // if (Input.IsActionJustPressed("Jump"))
        // {
        //     character.cMovement.Jump();
        // }

        if (Input.IsActionJustPressed("Sprint"))
        {
            NetworkService.SendToServer<RemoteEvents.SprintRequest>();
        }

        elapsed += delta;
        if (elapsed >= 0.1)
        {
            MoveInput();
        }
    }

    void MoveInput()
    {
        var moveDirection = Input.GetVector("Left", "Right", "Back", "Forward");
        var cameraForward = Game.World.Camera.GlobalTransform.Basis.Z;
        var cameraRight = Game.World.Camera.GlobalTransform.Basis.X;

        cameraForward.Y = 0;
        cameraRight.Y = 0;

        cameraForward = cameraForward.Normalized();
        cameraRight = cameraRight.Normalized();

        Vector2 cameraRelativeMoveDirection = new(
            -cameraForward.X * moveDirection.Y + cameraRight.X * moveDirection.X,
            cameraForward.Z * moveDirection.Y - cameraRight.Z * moveDirection.X);
        cameraRelativeMoveDirection = cameraRelativeMoveDirection.Normalized();
        NetworkService.SendToServer<RemoteEvents.MoveRequest>(cameraRelativeMoveDirection);
        elapsed = 0;
    }
}
