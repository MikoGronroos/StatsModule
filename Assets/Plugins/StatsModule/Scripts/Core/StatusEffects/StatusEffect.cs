using System;
using UnityEngine;

[Serializable]
public class StatusEffect
{

    [Tooltip("Effected value id")]
    [field: SerializeField] public string EffectedValueId {  get; private set; }
    [field: SerializeField] public StatusEffectTarget Target { get; private set; }
    [field: SerializeField] public RepeatableType RepeatableType { get; private set; }
    [Tooltip("Status effect duration in seconds")]
    [field: SerializeField] public float Duration { get; private set; }
    [field: SerializeField] public float LapDuration { get; private set; }
    [field: SerializeField] public int NumberOfTimesToRepeat { get; private set; }
    [field: SerializeField] public Modifier AppliedModifier { get; private set; }
    [field: SerializeField] public float ModifyValue { get; private set; }
}

public enum RepeatableType
{
    Duration,
    NumberOfTimes
}

public enum StatusEffectTarget
{
    Attribute,
    Stat
}