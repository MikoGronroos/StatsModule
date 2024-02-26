using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Linq;
using Sirenix.Utilities;

public class StatsCore : MonoBehaviour
{

    public static StatsCore Instance;

    private Dictionary<StatusEffect, Coroutine> StatusEffects { get; set; } = new Dictionary<StatusEffect, Coroutine>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void BeforeSceneLoad()
    {
        Assert.IsNull(Instance);
        var go = new GameObject(nameof(StatsCore));
        DontDestroyOnLoad(go);
        var instance = go.AddComponent<StatsCore>();
        Instance = instance;
    }

    private void Start()
    {
        var coreOwners = FindObjectsOfType<MonoBehaviour>().OfType<ICoreOwner>();

        foreach (var owner in coreOwners)
        {
            RegisterNewStats(owner);
        }
    }
    
    public void RegisterNewStats(ICoreOwner owner)
    {
        for (int i = 0; i < owner.Stats.Length; i++)
        {
            owner.Stats[i] = (Stat)owner.Stats[i].Copy(owner);
        }
        for (int i = 0; i < owner.Attributes.Length; i++)
        {
            owner.Attributes[i] = (Attribute)owner.Attributes[i].Copy(owner);
        }
        
        owner.Attributes.ForEach(t => t.OnStart());
        owner.Stats.ForEach(t => t.OnStart());
    }

    public void StartStatusEffect(ICoreOwner target, StatusEffect effect)
    {
        switch (effect.Target)
        {
            case StatusEffectTarget.Attribute:
                StatusEffects.Add(effect, StartCoroutine(AttributeStatusEffectCoroutine(target, effect)));
                break;
            case StatusEffectTarget.Stat:
                StatusEffects.Add(effect, StartCoroutine(StatStatusEffectCoroutine(target, effect)));
                break;
        }
    }

    private IEnumerator StatStatusEffectCoroutine(ICoreOwner target, StatusEffect effect)
    {
        Stat effectedValue = target.Stats.GetCoreValue<Stat>(effect.EffectedValueId);
        effectedValue.ApplyModifier(effect.AppliedModifier);
        yield return new WaitForSeconds(effect.Duration);
        effectedValue.RemoveModifier(effect.AppliedModifier);
        StopStatusEffect(effect);
    }

    private IEnumerator AttributeStatusEffectCoroutine(ICoreOwner target, StatusEffect effect)
    {
        Attribute effectedValue = target.Attributes.GetCoreValue<Attribute>(effect.EffectedValueId);
        switch (effect.RepeatableType)
        {
            case RepeatableType.Duration:
                float currentTime = 0;

                float maxTime = effect.Duration;
                while (currentTime < maxTime)
                {
                    effectedValue.ModifyValue(effect.ModifyValue);
                    currentTime = Mathf.Clamp(currentTime + effect.LapDuration, 0, maxTime);
                    yield return new WaitForSeconds(effect.LapDuration);
                }
                break;
            case RepeatableType.NumberOfTimes:
                for (int i = 0; i < effect.NumberOfTimesToRepeat; i++)
                {
                    effectedValue.ModifyValue(effect.ModifyValue);
                    yield return new WaitForSeconds(effect.LapDuration);
                }
                break;
        }
        StopStatusEffect(effect);
    }

    public void StopStatusEffect(StatusEffect effect)
    {
        if (StatusEffects[effect] != null)
        {
            StopCoroutine(StatusEffects[effect]);
        }
        StatusEffects.Remove(effect);
    }

}
