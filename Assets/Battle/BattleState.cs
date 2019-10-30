using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

public abstract class BattleState
{
    public virtual float APBuildRate { get; set; } = 0f;

    public abstract int Index { get; }

    public virtual void Move(Vector3 direction) { }

    public abstract int NextState { get; }

    public abstract int PreviousState { get; }

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

    public static BattleState operator ++(BattleState i)
    {
        return BattleState.States(i.NextState);
    }

    public static BattleState operator --(BattleState i)
    {
        return BattleState.States(i.PreviousState);
    }
}

public class StartBattleState : BattleState
{
    public override int Index => 0;

    public override int NextState => 1;

    public override int PreviousState => 1;
}

public class BuildingBattleState : BattleState
{
    public override float APBuildRate { get; set; } = 1f;

    public override int Index => 1;

    public override int NextState => 2;

    public override int PreviousState => 1;
}

public class CharacterSelectBattleState : BattleState
{
    public override int Index => 2;

    public override int NextState => 3;

    public override int PreviousState => 1;

    public override void Move(Vector3 direction)
    {
        throw new NotImplementedException();
    }
}

public class SelectionGUIBattleState : BattleState
{
    public override int Index => 3;

    public override int NextState => 5;

    public override int PreviousState => 2;

    public override void Move(Vector3 direction)
    {
        throw new NotImplementedException();
    }
}

public class EnemyActBattleState : BattleState
{
    public override int Index => 4;

    public override int NextState => 1;

    public override int PreviousState => 1;
}

public class PlayerActBattleState : BattleState
{
    public override int Index => 5;

    public override int NextState => 1;

    public override int PreviousState => 3;
}

