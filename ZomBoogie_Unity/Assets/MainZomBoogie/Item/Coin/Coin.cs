using UnityEngine;

public class Coin : BaseItem
{
    protected override void SpecialFunc(BoogieStatus target)
    {
        target.IncreaseXp( 1 );
    }
}