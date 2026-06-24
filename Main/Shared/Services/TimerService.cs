using System;
using System.Collections.Generic;

public static class TimerService
{
    private static readonly Dictionary<Timer, Delegate> Timers = [];

    public static Timer CreateTimer(float durationInSeconds, bool repeat, Action callback)
    {
        Timer timer = new() { Duration = durationInSeconds, Remaining = durationInSeconds, Repeat = repeat };
        Timers.Add(timer, callback);

        return timer;
    }

    public static Timer CreateTimer<T>(T entity, float durationInSeconds, bool repeat, Action<T> callback) where T : Entity
    {
        Timer timer = new() { Duration = durationInSeconds, Remaining = durationInSeconds, Repeat = repeat, Entity = entity };
        Timers.Add(timer, callback);

        return timer;
    }

    //TODO- a way to pass in the entity as a parameter to make per entity timers
    //* the most obvious way is a timer block inside entities, but i dont like it.
    //* am thinking a way to pass the entity as a parameter that runs on StartEntities() smth like:
    //* StartEntities(Entity entity){ TimerService.CreateTimer(10, true, OnTimer(Entity entity)); } 
    public static void Process(double delta)
    {
        foreach (var pair in Timers)
        {
            Timer timer = pair.Key;
            Delegate callback = pair.Value;
            timer.Remaining -= (float)delta;

            if (timer.Remaining <= 0)
            {
                if (timer.Entity != null)
                {
                    ((Action<Entity>)callback).Invoke(timer.Entity);

                }
                else
                {
                    ((Action)callback).Invoke();
                }

                if (timer.Repeat)
                {
                    timer.Remaining = timer.Duration;
                }
                else
                {
                    Timers.Remove(timer);
                }
            }
        }
    }

    public static void Connect(Timer timer, Action callback)
    {
        if (Timers.ContainsKey(timer))
        {
            if (Timers.TryGetValue(timer, out var existing))
            {
                Timers[timer] = Delegate.Combine(existing, callback);
            }
            else
            {
                Timers[timer] = callback;
            }
        }
    }

    public static void Disconnect(Timer timer, Action callback)
    {
        if (Timers.ContainsKey(timer))
        {
            if (Timers.TryGetValue(timer, out var existing))
            {
                Timers[timer] = Delegate.Remove(existing, callback);
            }
        }
    }

    public static void Destroy(Timer timer)
    {
        if (Timers.ContainsKey(timer))
        {
            Timers.Remove(timer);
        }
    }
}
