using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class BattleGUIMovementManager : SingletonBase<BattleGUIMovementManager>
{
    public GameObject GUIBattleSelector;

    public InputReader InputReader;

    private State state;

    //track input state here:
    //state one: select character
    //state two: selection GUI

    //function enable GUIBattleSelector and pass in the referencial character?

    // Start is called before the first frame update
    void Start()
    {
        //InputReader.OnSubmit += AdvanceGUI;
        //InputReader.OnCancel += RetractGUI;
        //InputReader.OnMovement += GUIMovement;

        //state = State.States(0);
    }

    void AdvanceGUI()
    {
        state = State.States(state.Index + 1);
    }

    void RetractGUI()
    {
        state = State.States(state.Index - 1);
    }

    void GUIMovement(Vector3 direction)
    {
        state.GUIMoveFunc(direction);
    }

    public abstract class State
    {
        public abstract int Index { get; }

        public abstract void GUIMoveFunc(Vector3 direction);

        private static State[] _states;
        public static State States(int index)
        {
            if (_states == null)
            {
                var states = Assembly.GetAssembly(typeof(State)).GetTypes()
                    .Where(t => typeof(State).IsAssignableFrom(t) && t.IsAbstract == false).ToArray();

                _states = new State[states.Length];

                for (int i = 0; i < states.Length; i++)
                {
                    State s = Activator.CreateInstance(states[i]) as State;
                    _states[i] = s;
                }
            }

            index = MathUtility.Clamp(index, 0, _states.Length);

            return _states.First(x => x.Index == index);
        }
    }

    public class NonGUIState : State
    {
        public override int Index => 0;

        public override void GUIMoveFunc(Vector3 direction)
        {
            //no-op
        }
    }

    public class CharacterSelectState : State
    {
        public override int Index => 1;

        public override void GUIMoveFunc(Vector3 direction)
        {
            BattleStateManager.Instance.SelectedCharacterIndex += Convert.ToInt32(direction.x);
        }
    }

    public class SelectionGUIState : State
    {
        public override int Index => 2;

        public override void GUIMoveFunc(Vector3 direction)
        {
            //TODO: 
        }
    }
}
