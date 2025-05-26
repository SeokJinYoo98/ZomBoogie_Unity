using UnityEngine;

public class Vaccine : BaseItem
{
    protected override void SpecialFunc(BoogieStatus target)
    {
        if (target.CurrHp < target.MaxHp)
            target.CurrHp++;
    }
}
