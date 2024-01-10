using System;
using UnityEngine;

[Serializable]
public class Modifier
{
    [field: SerializeField] public ModifierType Type { get; private set; }
    [field: SerializeField] public float ConstantValue { get; private set; }
    [field: Range(-1,1)]
    [field: SerializeField] public float PercentageValue { get; private set; }
}

public enum ModifierType
{
    Constant,
    Percentage
}