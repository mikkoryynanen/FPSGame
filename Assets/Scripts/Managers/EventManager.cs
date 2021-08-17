
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class BaseEvent
{
}

public class OnFireWeapon : BaseEvent
{
    public int maxAmmo;
    public int currentAmmo;
}

public class OnFireWeaponEnd : BaseEvent
{
}

public class OnWeaponReload : BaseEvent { }

public class OnEnemyKilled : BaseEvent
{    
}

public class EventManager
{
    Dictionary<Type, List<System.Action<BaseEvent>>> events = new Dictionary<Type, List<Action<BaseEvent>>>();
    private static EventManager _eventManager = null;

    public static EventManager instance
    {
        get
        {
            if (_eventManager == null)
                _eventManager = new EventManager();

            return _eventManager;
        }
    }

    public static void Subscribe<T>(System.Action<T> callback) where T : BaseEvent
    {
        if (instance.events.ContainsKey(typeof(T)))
        {
            instance.events[typeof(T)].Add((e) => { callback.Invoke((T)e); });
        }
        else
        {
            List<Action<BaseEvent>> evs = new List<Action<BaseEvent>>();
            evs.Add((e) => { callback.Invoke((T)e); });
            instance.events.Add(typeof(T), evs);
        }
    }

    public static void Unsubscribe<T>() where T : BaseEvent
    {
        instance.events.Remove(typeof(T));
    }

    public static void Send(BaseEvent baseEvent)
    {
        if (instance.events.TryGetValue(baseEvent.GetType(), out List<System.Action<BaseEvent>> callbacks))
        {
            foreach (var cb in callbacks)
                cb.Invoke(baseEvent);
        }
    }
}
