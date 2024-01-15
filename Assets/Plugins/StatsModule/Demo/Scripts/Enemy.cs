using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class Enemy : MonoBehaviour, ICoreOwner
{

    [field: SerializeField] public Attribute[] Attributes { get; private set; }
    [field: SerializeField] public Stat[] Stats { get; private set; }

}
