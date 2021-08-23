using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour, ITopHudPanel
{
    [SerializeField]
    private Text cellDetailsField;
    
    public void DisplayCellDetails(string details)
    {
        cellDetailsField.text = details;
    }

    public void OnTurnComplete()
    {
        
    }
}
