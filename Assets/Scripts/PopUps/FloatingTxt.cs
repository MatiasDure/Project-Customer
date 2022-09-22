using System.Collections;
using System.Collections.Generic;
using TMPro;
//using Unity.VisualScripting;
using UnityEngine;

public class FloatingTxt : MonoBehaviour
{
    [SerializeField] private float timeActive;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = timeActive;
    }

    private void Update()
    {
        if(gameObject.activeInHierarchy) DecreaseTime();
    }

    private void DecreaseTime()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else HideText();
    }

    private void HideText()
    {
        gameObject.SetActive(false);
        timer = timeActive;
    }

}
