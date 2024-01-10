using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using System.Collections.Generic;

public class Enemy : SerializedMonoBehaviour, ICoreOwner
{

    [field: OdinSerialize] public ICoreValue[] attributes { get; private set; }
    [field: OdinSerialize] public ICoreValue[] stats { get; private set; }

}
