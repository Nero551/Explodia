using System;
using Godot;

public static class InputService
{
    public static void Start()
    {
        TimerService.CreateTimer(0.1f, true, MoveInput);
        TimerService.CreateTimer(0.05f, true, AttackInput);
    }

    public static void Process(double delta)
    {
        if (Client.Player == null || Client.Player.Character == null)
            return;

        MouseCaptureInput();
        SprintInput();

        // if (Input.IsActionJustPressed("Jump"))
        // {
        //     character.cMovement.Jump();
        // }
    }

    static void MouseCaptureInput()
    {
        if (Input.IsActionJustPressed("MouseCapture"))
        {
            Input.MouseMode = Input.MouseMode == Input.MouseModeEnum.Captured ?
                Input.MouseModeEnum.Visible : Input.MouseModeEnum.Captured;
        }
    }

    static void SprintInput()
    {
        if (Input.IsActionJustPressed("Sprint"))
        {
            NetworkService.SendToServer<RemoteEvents.SprintRequest>();
        }
    }

    static void AttackInput()
    {
        if (Client.Player == null || Client.Player.Character == null)
            return;

        if (Input.IsActionPressed("PrimaryAttack"))
        {
            NetworkService.SendToServer<RemoteEvents.PrimaryAttackRequest>();
        }

        if (Input.IsActionPressed("SecondaryAttack"))
        {
            NetworkService.SendToServer<RemoteEvents.SecondaryAttackRequest>();
        }
    }

    static void MoveInput()
    {
        if (Client.Player == null || Client.Player.Character == null)
            return;

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
    }
}
