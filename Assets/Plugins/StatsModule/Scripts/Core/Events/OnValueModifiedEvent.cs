using Sirenix.Serialization;
using System;
using UnityEngine.Events;

public class OnValueModifiedEvent : ICoreEvent
{
    public Action<ICoreValue> Event { get; set; }
}