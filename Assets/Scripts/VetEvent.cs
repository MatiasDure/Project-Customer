using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VetEvent : MonoBehaviour
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
            anim.SetTrigger("TrVetEvent");

            if (Input.GetKeyDown(KeyCode.Space))
            { 
                
            }
        }
    }
 
}
