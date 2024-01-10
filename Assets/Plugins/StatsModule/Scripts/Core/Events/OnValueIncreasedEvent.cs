using Sirenix.Serialization;
using System;
using UnityEngine.Events;

public class OnValueIncreasedEvent : ICoreEvent
{
    public Action<ICoreValue> Event { get; set; }
}
