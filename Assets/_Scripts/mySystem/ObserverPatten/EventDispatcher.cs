using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EventDispatcher : MonoBehaviour {
    public static EventDispatcher Instance { get; private set; }
    void Awake ()
    {
        if(Instance != null){
            Debug.LogError("Found more than one Game Events Manager in the scene.");
            Destroy(this);
        }
        else{

            Instance = this;
        }
    }
    private void OnDisable() {
        ClearAllListener();
    }
    
    Dictionary<EventID, Action<object>> _listeners = new Dictionary<EventID, Action<object>>();
    public void RegisterListener (EventID eventID, Action<object> callback)
    {
        // checking params
        Common.Assert(callback != null, "AddListener, event {0}, callback = null !!", eventID.ToString());
        Common.Assert(eventID != EventID.None, "RegisterListener, event = None !!");

        // check if listener exist in distionary
        if (_listeners.ContainsKey(eventID))
        {
            // add callback to our collection
            _listeners[eventID] += callback;
        }
        else
        {
            // add new key-value pair
            _listeners.Add(eventID, null);
            _listeners[eventID] += callback;
        }
    }
    public void PostEvent (EventID eventID, object param = null)
    {
        if (!_listeners.ContainsKey(eventID))
        {
            Common.Log("No listeners for this event : {0}", eventID);
            return;
        }

        // posting event
        Action<object> callbacks = _listeners[eventID];
        // if there's no listener remain, then do nothing
        if (callbacks != null)
        {
            callbacks(param);
        }
        else
        {
            Common.Log("PostEvent {0}, but no listener remain, Remove this key", eventID);
            _listeners.Remove(eventID);
        }
    }
    public void RemoveListener (EventID eventID, Action<object> callback)
    {
        // checking params
        Common.Assert(callback != null, "RemoveListener, event {0}, callback = null !!", eventID.ToString());
        Common.Assert(eventID != EventID.None, "AddListener, event = None !!");

        if (_listeners.ContainsKey(eventID))
        {
            _listeners[eventID] -= callback;
        }
        else
        {
            Common.Warning(false, "RemoveListener, not found key : " + eventID);
        }
    }
    public void ClearAllListener ()
    {
        _listeners.Clear();
    }
    
    
}
public static class EventExtension
{
    public static void RegisterListener (this MonoBehaviour listener, EventID eventID, Action<object> callback)
    {
        EventDispatcher.Instance.RegisterListener(eventID, callback);
    }
    public static void RemoveListener (this MonoBehaviour listener, EventID eventID, Action<object> callback)
    {
        EventDispatcher.Instance.RemoveListener(eventID, callback);
    }
    public static void PostEvent (this MonoBehaviour sender, EventID eventID, object param)
    {
        EventDispatcher.Instance.PostEvent(eventID, param);
    }

    public static void PostEvent (this MonoBehaviour sender, EventID eventID)
    {
        EventDispatcher.Instance.PostEvent(eventID, null);
    }
}