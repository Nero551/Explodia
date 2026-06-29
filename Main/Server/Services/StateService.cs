using System;
using Godot;

public static class StateService
{
    static Godot.Collections.Dictionary stateData;

    static bool HasRequiredBlocks(Entity entity)
    {
        return entity.HasBlock<Blocks.StateBlock, Blocks.MovementBlock, Blocks.HealthBlock>();
    }

    public static void Start()
    {
        stateData = PULib.JSONHelper.JSONToCSharp("res://Main/Shared/Data/JSON/StateData.json");
    }

    public static void ProcessEntities(Entity entity, double delta)
    {
        if (!HasRequiredBlocks(entity))
            return;
            
        HandleMainStates(entity);
        HandleStates(entity, delta);
    }

    public static void AddState(Entity entity, string stateName, double duration = -1)
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

    public static void RemoveState(Entity entity, params string[] stateNames)
    {
        var stateBlock = entity.GetBlock<Blocks.StateBlock>();
        foreach (string name in stateNames)
        {
            stateBlock.ActiveStates.Remove(name);
        }

    }

    public static bool HasState(Entity entity, params string[] stateNames)
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

    static void HandleStates(Entity entity, double delta)
    {
        var stateBlock = entity.GetBlock<Blocks.StateBlock>();
        var movementBlock = entity.GetBlock<Blocks.MovementBlock>();

        float resultingSpeed = 10;
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
                var data = (Godot.Collections.Dictionary)stateData[key];
                if (data != null)
                {
                    resultingJumpPower = (float)data["JumpPower"];
                    resultingSpeed = (float)data["Speed"];
                }
            }
        }
        movementBlock.Speed = resultingSpeed;
        movementBlock.JumpPower = resultingJumpPower;
    }

    static void HandleMainStates(Entity entity)
    {
        var stateBlock = entity.GetBlock<Blocks.StateBlock>();
        var movementBlock = entity.GetBlock<Blocks.MovementBlock>();
        var healthblock = entity.GetBlock<Blocks.HealthBlock>();

        if (healthblock.Health <= 0)
        {
            stateBlock.MainState = MainState.Idle;
            stateBlock.MainState = MainState.Dead;
            EventService.Fire(new Events.Died(entity));
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
