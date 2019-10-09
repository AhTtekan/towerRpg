using System;

public class BattleStateManager : SingletonBase<BattleStateManager>
{
    public int SelectedCharacterIndex { get { return SelectedCharacterIndex; } set { SelectedCharacterIndex = Wrap(value); } }

    public BattleState BattleStateCurrent { get { return BattleStateCurrent; }set { BattleStateCurrent = Clamp(value); } }

    private int Wrap(int value)
    {
        int maxCharacters = CharactersManager.Instance.Chars.Length;

        if (value < 0)
            return maxCharacters;
        else if (value > maxCharacters)
            return 0;
        return value;
    }

    public BattleState Clamp (BattleState value)
    {
        if ((int)value == 0)
        {
            return BattleState.Start;
        }

        if(!Enum.IsDefined(typeof(BattleState), value))
        {
            return BattleState.SelectionGUI;
        }

        return value;
    }

    // Start is called before the first frame update
    void Start()
    {
        BattleStateCurrent = BattleState.Building;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum BattleState
    {
        Start,
        Building,
        EnemyAct,
        CharacterSelect,
        SelectionGUI,
        PlayerAct
    }
}
