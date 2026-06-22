using System;
using Blocks;
using Godot;

namespace Processors;

public class CameraProcessor : Processor
{
    public override void Start()
    {
        base.Start();
    }

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
        SpringArm3D camera = Game.World.Camera;
        if (Input.IsActionJustPressed("Zoom In") && camera.SpringLength > Game.World.MinSpringLength)
        {
            camera.SpringLength = Mathf.Lerp(
                camera.SpringLength, camera.SpringLength - 1.5f, 0.3f);
        }
        else if (Input.IsActionJustPressed("Zoom Out") &&
         camera.SpringLength < Game.World.MaxSpringLength)
        {
            camera.SpringLength = Mathf.Lerp(
                camera.SpringLength, camera.SpringLength + 1.5f, 0.3f);
        }
    }

    float horizontalRotation;
    float verticalRotation;
    public void RotateCamera(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion &&
            Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            horizontalRotation -= mouseMotion.Relative.X * Game.World.MouseSensitivity;
            verticalRotation -= mouseMotion.Relative.Y * Game.World.MouseSensitivity;

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

    // public void ApplyCamRelativeMovement()
    // {
    //     Vector3 forward = -SpringArm.GlobalTransform.Basis.Z;
    //     Vector3 right = SpringArm.GlobalTransform.Basis.X;
    //     forward.Y = 0;
    //     right.Y = 0;
    //     forward = forward.Normalized();
    //     right = right.Normalized();

    //     Vector3 vel = ComponentHost.GetComponent<CCharacter>().Character.cMovement.MovementVelocity;

    //     Vector3 direction = right * vel.X + forward * vel.Z;
    //     direction.Y = vel.Y;
    //     ComponentHost.GetComponent<CCharacter>().Character.cMovement.MovementVelocity = direction;
    // }
}

