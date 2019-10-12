using System;

public class BattleStateManager : SingletonBase<BattleStateManager>
{
    private int _selectedCharacterIndex;
    public int SelectedCharacterIndex { get { return _selectedCharacterIndex; } set { _selectedCharacterIndex = Wrap(value); } }

    private BattleState _battleState;
    public BattleState BattleStateCurrent { get { return _battleState; } set { _battleState = Clamp(value); } }

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
