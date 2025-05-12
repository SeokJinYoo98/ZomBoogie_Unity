using UnityEngine;

public class Magnetic : BaseItem
{
    protected override void SpecialFunc(BoogieStatus target)
    {
        target.MagTime = 2.0f;
    }
}
