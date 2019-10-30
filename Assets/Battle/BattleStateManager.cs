using UnityEngine;

public class BattleStateManager : SingletonBase<BattleStateManager>
{
    private int _selectedCharacterIndex;
    public int SelectedCharacterIndex { get { return _selectedCharacterIndex; } set { _selectedCharacterIndex = Wrap(value); } }

    public BattleState BattleStateCurrent { get; private set; }

    public InputReader InputReader;

    private int Wrap(int value)
    {
        int maxCharacters = CharactersManager.Instance.Chars.Length;

        if (value < 0)
            return maxCharacters;
        else if (value > maxCharacters)
            return 0;
        return value;
    }

    // Start is called before the first frame update
    void Start()
    {
        InputReader.OnSubmit += Advance;
        InputReader.OnCancel += Retract;
        InputReader.OnMovement += Movement;

        BattleStateCurrent = BattleState.States(1);
        Debug.Log($"State: {BattleStateCurrent.GetType().Name}");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Advance()
    {
        BattleStateCurrent++;
        Debug.Log($"State switched to: {BattleStateCurrent.GetType().Name}");
    }

    void Retract()
    {
        BattleStateCurrent--;
        Debug.Log($"State switched to: {BattleStateCurrent.GetType().Name}");
    }

    void Movement(Vector3 direction)
    {
        BattleStateCurrent.Move(direction);
    }
}
