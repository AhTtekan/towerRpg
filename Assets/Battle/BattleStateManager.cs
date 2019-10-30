using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class BattleStateManager : SingletonBase<BattleStateManager>
{
    private int _selectedCharacterIndex;
    public int SelectedCharacterIndex { get { return _selectedCharacterIndex; } set { _selectedCharacterIndex = Wrap(value); } }

    private BattleState _battleState;
    public BattleState BattleStateCurrent { get { return _battleState; } }

    public GameObject GuiBattleSelector;

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

        _battleState = BattleState.States(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Advance()
    {

    }

    void Retract()
    {

    }

    void Movement(Vector3 direction)
    {

    }

    public abstract class BattleState
    {
        public abstract float APBuildRate { get; set; }

        public abstract int Index { get; }

        public abstract void Move(Vector3 direction);

        public abstract int NextState { get; }

        private static BattleState[] _battleStates;
        public static BattleState States(int index)
        {
            if (_battleStates == null)
            {
                var states = Assembly.GetAssembly(typeof(BattleState)).GetTypes()
                    .Where(t => typeof(BattleState).IsAssignableFrom(t) && t.IsAbstract == false).ToArray();

                _battleStates = new BattleState[states.Length];

                for (int i = 0; i < states.Length; i++)
                {
                    BattleState s = Activator.CreateInstance(states[i]) as BattleState;
                    _battleStates[i] = s;
                }
            }

            index = MathUtility.Clamp(index, 0, _battleStates.Length);

            return _battleStates.First(x => x.Index == index);
        }
    }

    public class StartBattleState : BattleState
    {
        public override float APBuildRate { get; set; } = 0f;

        public override int Index => 0;

        public override int NextState => 1;

        public override void Move(Vector3 direction)
        {
            throw new NotImplementedException();
        }
    }

    public class BuildingBattleState : BattleState
    {
        public override float APBuildRate { get; set; } = 1f;

        public override int Index => 1;

        public override int NextState => 1;

        public override void Move(Vector3 direction)
        {
            //no-op
        }
    }

    public class CharacterSelectBattleState : BattleState
    {
        public override float APBuildRate { get; set; } = 0f;

        public override int Index => 2;

        public override int NextState => 3;

        public override void Move(Vector3 direction)
        {
            throw new NotImplementedException();
        }
    }

    public class SelectionGUIBattleState : BattleState
    {
        public override float APBuildRate { get; set; } = 0f;

        public override int Index => 3;

        public override int NextState => 1;

        public override void Move(Vector3 direction)
        {
            throw new NotImplementedException();
        }
    }

    public class EnemyActBattleState : BattleState
    {
        public override float APBuildRate { get; set; } = 0f;

        public override int Index => 4;

        public override int NextState => 1;

        public override void Move(Vector3 direction)
        {
            throw new NotImplementedException();
        }
    }
}
