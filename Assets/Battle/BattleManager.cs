using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : SingletonBase<BattleManager>
{
    public int SelectedCharacterIndex { get { return SelectedCharacterIndex; }; set { SelectedCharacterIndex = Wrap(value); } }

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
