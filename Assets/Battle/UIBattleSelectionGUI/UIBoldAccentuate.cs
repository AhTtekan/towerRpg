using UnityEngine;
using TMPro;

public class UIBoldAccentuate : MonoBehaviour, IUIAccentuate
{
#pragma warning disable 0649
    [SerializeField]
    private TextMeshProUGUI GUIText;
#pragma warning restore 0649

    public void Accentuate()
    {
        GUIText.fontStyle = FontStyles.Bold;
    }

    public void ResetAccent()
    {
        GUIText.fontStyle = FontStyles.Normal;
    }
}