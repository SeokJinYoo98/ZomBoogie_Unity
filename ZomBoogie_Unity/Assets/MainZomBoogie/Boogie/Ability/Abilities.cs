using UnityEngine;

public interface IAbility
{
    string Id { get; }
    string Name { get; }
    string Description { get; }
    int Level { get; }
    Sprite AbilityIcon{ get; }
    
    void LevelUp(int increased);
    void SetLevel(int level);
    void Activate(Boogie boogie);
}

public class SpeedAbility : IAbility
{
    private int     _level = 0;
    private Sprite  _abilityIcon = Resources.Load<Sprite>("Sprites/UI/UI_11");
    // 인터페이스 구현
    public string Id => "increase_move_speed";
    public string Name => "스피드 업";
    public string Description => "속도 증가";
    public int Level => _level;
    public Sprite AbilityIcon => _abilityIcon ??= Resources.Load<Sprite>( "Sprites/UI/UI_11" );
    public void Activate(Boogie boogie)
    {
    }

    public void LevelUp(int increased)
    {
        _level += increased;
    }

    public void SetLevel(int level)
    {
        _level = level;
    }
}
