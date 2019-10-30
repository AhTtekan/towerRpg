using System;
using System.Collections;
using UnityEngine;

public class APRateCalculator
{
    private readonly APCore _characterAPCore;
    private readonly SpeedCore _characterSpeedCore;

    public APRateCalculator(APCore characterAPCore, SpeedCore speedCore)
    {
        _characterAPCore = characterAPCore;
        _characterSpeedCore = speedCore;
    }

    public float GetIncrementAmount()
    {

        float amount = 3.87981F / (1F + (-(0.66F * ((float)Math.Pow(Math.E, ((-0.005) * _characterSpeedCore.Agility))))));

        return 1f / (amount / 5.6f);
    }

    public void Kill()
    {
        _kill = true;
    }

    public IEnumerator Increment()
    {
        DateTime start = DateTime.Now;
        do
        {
            yield return null;

            _characterAPCore.AP_Current += GetIncrementAmount() * Time.deltaTime *
            BattleStateManager.Instance.BattleStateCurrent.APBuildRate;

            if (_characterAPCore.AP_Current == _characterAPCore.AP_Max)
            {
                Debug.Log($"Hit {_characterAPCore.AP_Max} AP in {(DateTime.Now - start).TotalSeconds} with Agility = {_characterSpeedCore.Agility}");
                break;
            }
        }
        while (_kill == false);

        yield return null;
    }

    private bool _kill = false;
}
