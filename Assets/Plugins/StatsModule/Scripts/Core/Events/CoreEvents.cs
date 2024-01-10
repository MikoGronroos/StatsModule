using System;
using System.Linq;

public class CoreEvents
{

    private ICoreEvent[] coreEvents;

    public CoreEvents() 
    {
        coreEvents = new ICoreEvent[] { new OnValueModifiedEvent(), new OnValueIncreasedEvent(), new OnValueDecreasedEvent() };
    }

    public bool TryGetCoreEvent<T>(out ICoreEvent coreEvent)
    {
        coreEvent = coreEvents.Where(t => t is T).FirstOrDefault();
        return coreEvent != null;
    }

    public void Register<T>(Action<ICoreValue> action)
    {
        coreEvents.Where(t => t is T).FirstOrDefault().Event += action;
    }

    public void Deregister<T>(Action<ICoreValue> action)
    {
        coreEvents.Where(t => t is T).FirstOrDefault().Event -= action;
    }

}

public interface ICoreEvent
{
    Action<ICoreValue> Event { get; set; }
}