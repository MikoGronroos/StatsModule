using System;

public interface ICoreValue
{
    string Id { get; }
    CoreEvents CoreEvents { get; }
    void OnStart();
}