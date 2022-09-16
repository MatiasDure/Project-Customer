using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDisplay : MonoBehaviour
{
    //game object to turn off and on
    [SerializeField] private GameObject toToggle;
    //If true, the display is active from the start
    [SerializeField] private bool displayFromStart;
    //whether the user wants to toggle by mouse click or hover
    [SerializeField] private bool toggleByClick;

    private void OnMouseOver()
    {
        if (toToggle == null) return;
        if (toggleByClick && Input.GetMouseButtonDown(0)) Toggle();
    }

    private void OnMouseEnter()
    {
        if (toToggle == null) return;
        if (!toggleByClick) Toggle();
    }

    private void OnMouseExit()
    {
        if (toToggle == null) return;
        if (!toggleByClick) Toggle();
    }

    private void Toggle() => toToggle.SetActive(!toToggle.activeInHierarchy);
}
