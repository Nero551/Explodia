using System;
using System.Reflection.Metadata.Ecma335;
using Blocks;
using Godot;
using RemoteEvents;


namespace Processors;

public class StateProcessor : Processor
{
    Godot.Collections.Dictionary stateData;

    public override bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.StateBlock, Blocks.MovementBlock, Blocks.AnimationBlock>();
    }

    public override void Start()
    {
        base.Start();
        stateData = PULib.JSONHelper.JSONToCSharp("Main/Shared/Data/StateData");
    }

    public override void ProcessEntities(Entity entity, double delta)
    {
        base.ProcessEntities(entity, delta);
        HandleMainStates(entity);
        HandleStates(entity, delta);
    }

    public void AddState(Entity entity, string stateName, double duration = -1)
    {
        var stateBlock = entity.GetBlock<Blocks.StateBlock>();
        if (duration > 0)
        {
            stateBlock.ActiveStates[stateName] = duration;

        }
        else
        {
            stateBlock.ActiveStates[stateName] = double.MaxValue;
        }
    }

    public void RemoveState(Entity entity, params string[] stateNames)
    {
        var stateBlock = entity.GetBlock<Blocks.StateBlock>();
        foreach (string name in stateNames)
        {
            stateBlock.ActiveStates.Remove(name);
        }

    }

    public bool CheckState(Entity entity, params string[] stateNames)
    {
        var stateBlock = entity.GetBlock<Blocks.StateBlock>();
        foreach (string name in stateNames)
        {
            if (stateBlock.ActiveStates.ContainsKey(name))
            {
                return true;
            }
        }
        return false;
    }

    void HandleStates(Entity entity, double delta)
    {
        var stateBlock = entity.GetBlock<Blocks.StateBlock>();
        var movementBlock = entity.GetBlock<Blocks.MovementBlock>();

        float resultingSpeed = 5;
        float resultingJumpPower = 5f;
        foreach (string key in stateBlock.ActiveStates.Keys)
        {
            //Durations
            if (stateBlock.ActiveStates[key] != double.MaxValue)
            {
                stateBlock.ActiveStates[key] -= delta;

                if (stateBlock.ActiveStates[key] <= 0)
                    stateBlock.ActiveStates.Remove(key);
            }

            //Adjusting JumpPower and Speed
            if (stateData.ContainsKey(key))
            {
                Godot.Collections.Dictionary data = (Godot.Collections.Dictionary)stateData[key];
                if (data != null)
                {
                    resultingJumpPower = (float)data["JumpPower"];
                    resultingSpeed = (float)data["Speed"];
                    continue;
                }
            }
        }
        movementBlock.Speed = resultingSpeed;
        movementBlock.JumpPower = resultingJumpPower;
    }

    void HandleMainStates(Entity entity)
    {
        var stateBlock = entity.GetBlock<Blocks.StateBlock>();
        var movementBlock = entity.GetBlock<Blocks.MovementBlock>();

        if (false) //* If Health = 0 then dead.
        {
            stateBlock.MainState = MainState.Dead;
            return;
        }

        if (entity.GetNode<CharacterBody3D>().IsOnFloor())
        {
            Vector3 horizontal = movementBlock.Velocity;
            horizontal.Y = 0;

            if (horizontal.LengthSquared() > 0.01f)
                stateBlock.MainState = MainState.Moving;
            else
                stateBlock.MainState = MainState.Idle;
        }
        else
        {
            if (movementBlock.Velocity.Y > 0)
                stateBlock.MainState = MainState.Jumping;
            else
                stateBlock.MainState = MainState.Falling;
        }
    }
}
