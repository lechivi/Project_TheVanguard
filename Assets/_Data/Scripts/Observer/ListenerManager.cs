using System;
using System.Collections.Generic;

public class ListenerManager : BaseManager<ListenerManager>
{
    public Dictionary<ListenType, ListenerGroup> listeners = new Dictionary<ListenType, ListenerGroup>();

    public void BroadCast(ListenType type, object value = null)
    {
        if (this.listeners.ContainsKey(type) && this.listeners[type] != null)
        {
            this.listeners[type].BroadCast(value);
        }
    }

    public void Register(ListenType type, Action<object> action)
    {
        if (!this.listeners.ContainsKey(type))
        {
            this.listeners.Add(type, new ListenerGroup());
        }

        if (this.listeners[type] != null)
        {
            this.listeners[type].Attach(action);
        }
    }

    public void Unregister(ListenType type, Action<object> action)
    {
        if (this.listeners.ContainsKey(type) && this.listeners[type] != null)
        {
            this.listeners[type].Detach(action);
        }
    }

    public void UnregisterAll(Action<object> action)
    {
        foreach (ListenType key in this.listeners.Keys)
        {
            Unregister(key, action);
        }
    }
}
