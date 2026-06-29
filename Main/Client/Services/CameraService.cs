using System;
using Godot;

public static class CameraService
{
    public static void Process(double delta)
    {
        Follow(Client.Player?.Character?.GetNode<CharacterBody3D>()?.GetNode<Attachment3D>("Root"));
        ZoomCamera();
    }

    public static void InputProcess(InputEvent inputEvent)
    {
        RotateCamera(inputEvent);
    }

    static void ZoomCamera()
    {
        Camera camera = Game.World.Camera;
        if (Input.IsActionJustPressed("Zoom In") && camera.SpringLength > camera.MinSpringLength)
        {
            camera.SpringLength = Mathf.Lerp(
                camera.SpringLength, camera.SpringLength - 1.5f, 0.3f);
        }
        else if (Input.IsActionJustPressed("Zoom Out") && camera.SpringLength < camera.MaxSpringLength)
        {
            camera.SpringLength = Mathf.Lerp(
                camera.SpringLength, camera.SpringLength + 1.5f, 0.3f);
        }
    }

    static float horizontalRotation;
    static float verticalRotation;
    static void RotateCamera(InputEvent @event)
    {
        Camera camera = Game.World.Camera;
        if (@event is InputEventMouseMotion mouseMotion &&
            Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            horizontalRotation -= mouseMotion.Relative.X * camera.MouseSensitivity;
            verticalRotation -= mouseMotion.Relative.Y * camera.MouseSensitivity;

            verticalRotation = Mathf.Clamp(verticalRotation, Mathf.DegToRad(-75), Mathf.DegToRad(45));

            Game.World.Camera.Rotation = new Vector3(verticalRotation, horizontalRotation, 0);
        }
    }

    static void Follow(Node3D node3D)
    {
        if (node3D == null)
            return;
        Game.World.Camera.GlobalPosition = node3D.GlobalPosition;
    }
}
