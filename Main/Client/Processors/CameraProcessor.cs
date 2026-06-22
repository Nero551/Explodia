using System;
using Blocks;
using Godot;

namespace Processors;

public class CameraProcessor : Processor
{
    public override void Process(double delta)
    {
        base.Process(delta);
        Follow(Client.Player?.Character?.GetNode<CharacterBody3D>()?.GetNode<Attachment3D>("Root"));
        ZoomCamera();
    }

    public override void InputProcess(InputEvent inputEvent)
    {
        base.InputProcess(inputEvent);
        RotateCamera(inputEvent);
    }

    public void ZoomCamera()
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

    float horizontalRotation;
    float verticalRotation;
    public void RotateCamera(InputEvent @event)
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

    public void Follow(Node3D node3D)
    {
        if (node3D == null)
            return;
        Game.World.Camera.GlobalPosition = node3D.GlobalPosition;
    }
}

