using System;
using System.Collections.Generic;

public static class TimerService
{
    private static readonly HashSet<Timer> Timers = [];

    public static Timer CreateTimer(float durationInSeconds, bool repeat, Action callback)
    {
        Timer timer = new() { Action = callback, Duration = durationInSeconds, Remaining = durationInSeconds, Repeat = repeat };
        Timers.Add(timer);

        return timer;
    }

    public static Timer CreateTimer<T>(T entity, float durationInSeconds, bool repeat, Action<T> callback) where T : Entity
    {
        Timer timer = new() { Action = () => callback(entity), Duration = durationInSeconds, Remaining = durationInSeconds, Repeat = repeat, Entity = entity };
        Timers.Add(timer);

        return timer;
    }

    public static void Process(double delta)
    {
        foreach (Timer timer in Timers)
        {
            Action callback = timer.Action;
            timer.Remaining -= (float)delta;

            if (timer.Remaining <= 0)
            {
                callback.Invoke();
                if (timer.Repeat)
                {
                    timer.Remaining = timer.Duration;
                }
                else
                {
                    Destroy(timer);
                }
            }
        }
    }

    public static void Connect(Timer timer, Action callback)
    {
        if (Timers.Contains(timer))
        {
            if (timer.Action != null)
            {
                timer.Action += callback;

            }
            else
            {
                timer.Action = callback;
            }
        }
    }

    public static void Disconnect(Timer timer, Action callback)
    {
        if (Timers.Contains(timer))
        {
            if (timer.Action != null)
            {
                timer.Action -= callback;
            }
        }
    }

    public static void Destroy(Timer timer)
    {
        if (Timers.Contains(timer))
        {
            Timers.Remove(timer);
        }
    }
}
