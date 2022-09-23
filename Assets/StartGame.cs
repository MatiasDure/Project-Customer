using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private string sceneName;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) ButtonBehaviour.Instance.LoadScene(sceneName);   
    }
}
