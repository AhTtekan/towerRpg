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
        //TODO: take into account status effects.
        float amount = 0;

        float buildRate = _characterAPCore.APBuildRateInSeconds;
        buildRate = (buildRate * (400 - _characterSpeedCore.Agility)) / 400;
        amount = 1f / (buildRate / 10f);

        return amount;
    }

    public void Kill()
    {
        _kill = true;
    }

    public IEnumerator Increment()
    {
        do
        {
            yield return null;

            //TODO: check if AP growing
            _characterAPCore.AP_Current += GetIncrementAmount() * Time.deltaTime;
        }
        while (_kill == false);

        yield return null;
    }

    private bool _kill = false;
}
