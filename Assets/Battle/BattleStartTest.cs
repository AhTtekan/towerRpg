using UnityEngine;

public class BattleStartTest : MonoBehaviour
{
    APRateCalculator[] _calculators;

    // Start is called before the first frame update
    void Start()
    {
        var charGUIs = GameObject.FindObjectsOfType<CharacterGUI>();
        _calculators = new APRateCalculator[charGUIs.Length];
        for (int i = 0; i < charGUIs.Length; i++)
        {
            if(CharactersManager.Instance.Chars.Length < i)
            {
                charGUIs[i].enabled = false;

                continue;
            }

            Character character = CharactersManager.Instance.Chars[i];
            character.APCore.AP_Current = 0;

            charGUIs[i].SetCharacter(character);

            _calculators[i] = new APRateCalculator(character.APCore, character.SpeedCore);

            StartCoroutine(_calculators[i].Increment());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
