using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var charGUIs = GameObject.FindObjectsOfType<CharacterGUI>();
        for (int i = 0; i < charGUIs.Length; i++)
        {
            charGUIs[i].SetCharacter(CharactersManager.Instance.chars[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
