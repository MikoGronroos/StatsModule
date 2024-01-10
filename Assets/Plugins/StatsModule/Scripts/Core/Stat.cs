using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Stat : ICoreValue, IDetails
{

    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public float BaseValue { get; private set; }
    [field: SerializeField] public float Value {  get; private set; }
    public CoreEvents CoreEvents { get; private set; }
    [field: SerializeField] public StatUI StatUI { get; private set; }
    [field: OdinSerialize] private List<Modifier> modifiers { get; set; } = new List<Modifier>();

    public void OnStart()
    {
        CoreEvents = new CoreEvents();
        Value = BaseValue;
    }

    public void ApplyModifier(Modifier mod)
    {
        modifiers.Add(mod);
        CalculateValue();
    }

    public void RemoveModifier(Modifier mod)
    {
        modifiers.Remove(mod);
        CalculateValue();
    }

    private void CalculateValue()
    {
        float finalValue = BaseValue;
        modifiers = modifiers.OrderBy(t => t.Type).ToList();

        foreach (Modifier mod in modifiers)
        {
            switch (mod.Type)
            {
                case ModifierType.Constant:
                    finalValue += mod.ConstantValue;
                    break;
                case ModifierType.Percentage:
                    finalValue *= 1 + mod.PercentageValue;
                    break;
            }
        }

        Value = finalValue;

        if (CoreEvents.TryGetCoreEvent<OnValueModifiedEvent>(out var Event))
        {
            Event.Event?.Invoke(this);
        }
        if (Value < finalValue)
        {
            if (CoreEvents.TryGetCoreEvent<OnValueIncreasedEvent>(out var Increase))
            {
                Increase.Event?.Invoke(this);
            }
        }
        if (Value > finalValue)
        {
            if (CoreEvents.TryGetCoreEvent<OnValueDecreasedEvent>(out var Decrease))
            {
                Decrease.Event?.Invoke(this);
            }
        }
    }
}
