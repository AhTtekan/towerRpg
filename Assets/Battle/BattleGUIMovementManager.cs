using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGUIMovementManager : SingletonBase<BattleGUIMovementManager>
{
    public GameObject GUIBattleSelector;

    public InputReader InputReader;

    private GUISelectionState state;

    //track input state here:
    //state one: select character
    //state two: selection GUI

        //function enable GUIBattleSelector and pass in the referencial character?

    // Start is called before the first frame update
    void Start()
    {
        InputReader.OnSubmit += AdvanceGUI;
        InputReader.OnCancel += RetractGUI;
        InputReader.OnMovement += GUIMovement;

        state = GUISelectionState.None;
    }

    void AdvanceGUI()
    {
        var newState = state + 1;
        if(Enum.IsDefined(typeof(GUISelectionState), newState))
        {
            state = newState;
        }
    }

    void RetractGUI()
    {
        var newState = state - 1;
        if(Enum.IsDefined(typeof(GUISelectionState), newState))
        {
            state = newState;
        }
    }

    void GUIMovement(Vector3 direction)
    {
        if(state == GUISelectionState.CharacterSelect)
        {
            if(direction == Vector3.left)
            {
                BattleManager.Instance.SelectedCharacterIndex--;
            }
            else if(direction == Vector3.right)
            {
                BattleManager.Instance.SelectedCharacterIndex++;
            }
        }
    }

    enum GUISelectionState
    {
        None,
        CharacterSelect,
        SelectionGUI
    }
}
