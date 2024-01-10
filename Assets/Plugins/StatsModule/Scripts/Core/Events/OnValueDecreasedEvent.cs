using Sirenix.Serialization;
using System;
using UnityEngine.Events;

public class OnValueDecreasedEvent : ICoreEvent
{
    public Action<ICoreValue> Event { get; set; }
}
