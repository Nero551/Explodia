using System;
using Godot;

public static class MovementService
{

    static bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.MovementBlock, Blocks.TransformBlock>();
    }

    public static void Start()
    {
        EventService.Subscribe<RemoteEvents.MoveRequest>(OnMoveRequest);
        EventService.Subscribe<RemoteEvents.SprintRequest>(OnSprintRequest);
    }

    public static void PhysicsProcessEntities(Entity entity, double delta)
    {
        if (!HasRequiredBlocks(entity))
            return;

        var movementBlock = entity.GetBlock<Blocks.MovementBlock>();
        var transformBlock = entity.GetBlock<Blocks.TransformBlock>();


        VelocityDecay(entity);

        if (movementBlock.MoveDirection != Vector2.Zero)
        {
            Vector3 direction = (
                new Vector3(movementBlock.MoveDirection.X, 0, -movementBlock.MoveDirection.Y)
                ).Normalized();

            if (direction != Vector3.Zero)
            {
                movementBlock.Velocity.X = direction.X * movementBlock.Speed;
                movementBlock.Velocity.Z = direction.Z * movementBlock.Speed;
            }
            GD.Print(movementBlock.Speed);
        }

        BodyRotation(entity, delta);
        Gravity(entity, delta);

        entity.GetNode<CharacterBody3D>().Velocity = movementBlock.Velocity;
        entity.GetNode<CharacterBody3D>().MoveAndSlide();

        movementBlock.Velocity = entity.GetNode<CharacterBody3D>().Velocity;
        transformBlock.Position = entity.GetNode<CharacterBody3D>().Position;
    }

    static void Gravity(Entity entity, double delta)
    {
        var movementBlock = entity.GetBlock<Blocks.MovementBlock>();
        if (!entity.GetNode<CharacterBody3D>().IsOnFloor())
        {
            movementBlock.Velocity.Y += entity.GetNode<CharacterBody3D>().GetGravity().Y * (float)delta * 1.5f;
        }
        else if (movementBlock.Velocity.Y < 0)
        {
            movementBlock.Velocity.Y = 0;
        }
    }

    static void VelocityDecay(Entity entity)
    {
        var movementBlock = entity.GetBlock<Blocks.MovementBlock>();
        movementBlock.Velocity.X = Mathf.MoveToward(movementBlock.Velocity.X, 0, movementBlock.Speed);
        movementBlock.Velocity.Z = Mathf.MoveToward(movementBlock.Velocity.Z, 0, movementBlock.Speed);
    }

    static void BodyRotation(Entity entity, double delta)
    {
        var movementBlock = entity.GetBlock<Blocks.MovementBlock>();
        var transformBlock = entity.GetBlock<Blocks.TransformBlock>();
        Vector3 targetDir = movementBlock.Velocity;
        targetDir.Y = 0;
        targetDir = targetDir.Normalized();

        if (targetDir.LengthSquared() > 0.001f)
        {
            Basis target = Basis.LookingAt(targetDir, Vector3.Up).Orthonormalized();
            transformBlock.Basis = target;
        }
    }

    static void OnMoveRequest(RemoteEvents.MoveRequest evnt)
    {
        if (StateService.HasState(evnt.Player.Character, "Stunned"))
            return;

        var movementBlock = Entity.Get(evnt.Player.Character.Id).GetBlock<Blocks.MovementBlock>();
        movementBlock.MoveDirection = evnt.MoveDirection;
    }

    static void OnSprintRequest(RemoteEvents.SprintRequest evnt)
    {
        if (StateService.HasState(evnt.Player.Character, "Sprinting"))
        {
            StateService.RemoveState(evnt.Player.Character, "Sprinting");
        }
        else
        {
            StateService.AddState(evnt.Player.Character, "Sprinting");
        }
    }
}
