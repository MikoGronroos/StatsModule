using System.Collections.Generic;

public interface ICoreOwner
{

    ICoreValue[] attributes {  get; }
    ICoreValue[] stats { get; }

}
