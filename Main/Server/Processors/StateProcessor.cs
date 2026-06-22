using System;
using System.Reflection.Metadata.Ecma335;
using Blocks;
using Godot;
using RemoteEvents;


namespace Processors;

public class StateProcessor : Processor
{
    public override bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.StateBlock, Blocks.MovementBlock>();
    }

    public override void Start()
    {
        base.Start();
        stateData = PULib.JSONHelper.JSONToCSharp("Main/Shared/Data/StateData");
    }

    private Godot.Collections.Dictionary stateData;
    public override void ProcessEntities(Entity entity, double delta)
    {
        base.ProcessEntities(entity, delta);
        var stateBlock = entity.GetBlock<Blocks.StateBlock>();
        var movementBlock = entity.GetBlock<Blocks.MovementBlock>();

        //Main State
        if (entity.GetNode<CharacterBody3D>().IsOnFloor()) //* Not On Floor (need to add this)
        {
            if (movementBlock.Velocity.Y > 0)
                stateBlock.MainState = MainState.Jumping;
            else
                stateBlock.MainState = MainState.Falling;
        }
        else
        {
            Vector3 horizontal = movementBlock.Velocity;
            horizontal.Y = 0;

            if (horizontal.LengthSquared() > 0.01f)
                stateBlock.MainState = MainState.Moving;
            else
                stateBlock.MainState = MainState.Idle;
        }

        //Normal States
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
                    // if (ComponentHost.GetComponent<CHealth>().CurrentHealth <= 0)
                    // {
                    //     AddState("Dead");
                    // }
                    resultingJumpPower = (float)data["JumpPower"];
                    resultingSpeed = (float)data["Speed"];
                    continue;
                }
            }
        }
        movementBlock.Speed = resultingSpeed;
        movementBlock.JumpPower = resultingJumpPower;

        GD.Print(stateBlock.MainState);
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
}
