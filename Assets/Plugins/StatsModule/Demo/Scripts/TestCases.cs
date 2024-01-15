using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class TestCases : SerializedMonoBehaviour
{

    [OdinSerialize] private ICoreOwner enemy;

    [OdinSerialize] private Modifier modifier;
    [OdinSerialize] private Modifier modifier2;

    [OdinSerialize] private Modifier modifier3;

    [OdinSerialize] private StatusEffect statusEffect;

    [OdinSerialize] private StatusEffect statStatusEffect;


    [SerializeField] private Image enemyHealthBar;

    private void Start()
    {
        enemy.Attributes.GetCoreValue<Attribute>("Hp").CoreEvents.Register<OnValueModifiedEvent>(OnValueChanged);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            enemy.Attributes.GetCoreValue<Attribute>("Hp").ModifyValue(-10 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.H))
        {
            enemy.Attributes.GetCoreValue<Attribute>("Hp").ModifyValue(10 * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            enemy.Stats.GetCoreValue<Stat>("MaxHp").ApplyModifier(modifier);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            enemy.Stats.GetCoreValue<Stat>("MaxHp").ApplyModifier(modifier2);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            enemy.Stats.GetCoreValue<Stat>("MaxHp").ApplyModifier(modifier3);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            enemy.Stats.GetCoreValue<Stat>("MaxHp").RemoveModifier(modifier);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            enemy.Stats.GetCoreValue<Stat>("MaxHp").RemoveModifier(modifier2);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            enemy.Stats.GetCoreValue<Stat>("MaxHp").RemoveModifier(modifier3);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            StatsCore.Instance.StartStatusEffect(enemy, statusEffect);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            StatsCore.Instance.StartStatusEffect(enemy, statStatusEffect);
        }
    }

    public void OnValueChanged(ICoreValue value)
    {
        var valueOwner = (Attribute)value;
        enemyHealthBar.fillAmount = valueOwner.Value / valueOwner.MaxValue.Value;
    }

}
