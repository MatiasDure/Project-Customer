using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LightingAnim : MonoBehaviour
{

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (anim != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            { 
                anim.SetTrigger("TrVetEvent");
                //UnityEngine.Debug.LogWarning("Animation Is Playing");
            }
        }
    }
}
