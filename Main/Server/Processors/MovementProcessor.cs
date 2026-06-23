using System;
using Blocks;
using Godot;
using RemoteEvents;


namespace Processors;

public class MovementProcessor : Processor
{
    public override bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.MovementBlock, Blocks.TransformBlock>();
    }

    public override void Start()
    {
        base.Start();
        EventService.Subscribe<RemoteEvents.MoveRequest>(OnMoveRequest);
    }

    public override void PhysicsProcessEntities(Entity entity, double delta)
    {
        base.PhysicsProcessEntities(entity, delta);
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
                //TODO- Animations system. perhaps a processor? play animations here.
                //*Needs: 1- play/stop animations  2- track current animation 3- data about the current animation
                //*       4- Animation priority   5- add animations from library
            }
        }

        BodyRotation(entity, delta);

        entity.GetNode<CharacterBody3D>().Velocity = movementBlock.Velocity;
        entity.GetNode<CharacterBody3D>().MoveAndSlide();
        transformBlock.Position = entity.GetNode<CharacterBody3D>().Position;
        Gravity(entity, delta);
    }

    void Gravity(Entity entity, double delta)
    {
        var movementBlock = entity.GetBlock<Blocks.MovementBlock>();
        movementBlock.Velocity += entity.GetNode<CharacterBody3D>().GetGravity() * (float)delta * 1.5f;
    }

    void VelocityDecay(Entity entity)
    {
        var movementBlock = entity.GetBlock<Blocks.MovementBlock>();
        movementBlock.Velocity.X = Mathf.MoveToward(movementBlock.Velocity.X, 0, movementBlock.Speed);
        movementBlock.Velocity.Z = Mathf.MoveToward(movementBlock.Velocity.Z, 0, movementBlock.Speed);
    }

    void BodyRotation(Entity entity, double delta)
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

    void OnMoveRequest(RemoteEvents.MoveRequest evnt)
    {
        var movementBlock = Entity.Get(evnt.EntityId).GetBlock<Blocks.MovementBlock>();
        movementBlock.MoveDirection = evnt.MoveDirection;
    }
}
