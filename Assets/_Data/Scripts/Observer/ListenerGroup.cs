using System;
using System.Collections.Generic;

public class ListenerGroup
{
    private List<Action<object>> actions = new List<Action<object>>();

    public void BroadCast(object value)
    {
        for (int i = 0; i < this.actions.Count; i++)
        {
            this.actions[i](value);
        }
    }

    public void Attach(Action<object> action)
    {
        for (int i = 0; i < this.actions.Count; i++)
        {
            if (this.actions[i] == action)
                return;
        }

        this.actions.Add(action);
    }

    public void Detach(Action<object> action)
    {
        for (int i = 0; i < this.actions.Count; i++)
        {
            if (this.actions[i] == action)
            {
                this.actions.Remove(action);
                break;
            }
        }
    }
}
