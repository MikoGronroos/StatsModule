using Sirenix.Serialization;
using System;
using UnityEngine;


[CreateAssetMenu(menuName = "StatsModule/Attribute")]
public class Attribute : ScriptableObject, ICoreValue, IDetails
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public float MinValue { get; private set; }
    [field: SerializeField] public Stat MaxValue { get; private set; }
    [field: SerializeField] public float Value { get; private set; }
    [field: Range(0,1)]
    [field: SerializeField] public float StartingPercentage { get; private set; }

    public CoreEvents CoreEvents { get; private set; } = new CoreEvents();
    [field: SerializeField] public StatUI StatUI { get; private set; }

    public void OnStart()
    {
        CoreEvents = new CoreEvents();
        SetValue(MaxValue.Value * StartingPercentage);
        MaxValue.CoreEvents.Register<OnValueDecreasedEvent>((ICoreValue value) => {
            var statsValue = ((Stat)value);
            if (statsValue.Value < Value)
            {
                SetValue(statsValue.Value);
            }
        });
        MaxValue.CoreEvents.Register<OnValueModifiedEvent>((ICoreValue value) => {
            SetValue(Value);
        });
    }

    public void ModifyValue(float value)
    {
        Value = Mathf.Clamp(Value + value, MinValue, MaxValue.Value);
        if (CoreEvents.TryGetCoreEvent<OnValueModifiedEvent>(out var Event))
        {
            Event.Event?.Invoke(this);
        }
    }

    public void SetValue(float value)
    {
        Value = Mathf.Clamp(Value = value, MinValue, MaxValue.Value);
        if (CoreEvents.TryGetCoreEvent<OnValueModifiedEvent>(out var Event))
        {
            Event.Event?.Invoke(this);
        }
    }

    public ICoreValue Copy(ICoreOwner owner)
    {
        Attribute attribute = ScriptableObject.CreateInstance<Attribute>();
        attribute.Id = Id;
        attribute.MinValue = MinValue;
        attribute.MaxValue = owner.Stats.GetCoreValue<Stat>(MaxValue.Id);
        attribute.Value = Value;
        attribute.StartingPercentage = StartingPercentage;
        return attribute;
    }

}
