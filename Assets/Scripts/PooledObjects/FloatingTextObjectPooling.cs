using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FloatingTextObjectPooling : ObjectPooling
{
    [SerializeField] private Canvas canvas;
 
    public static FloatingTextObjectPooling SharedFloatInstance { get; private set; }
    
    private void Awake()
    {
        SharedFloatInstance = this;
    }
    
    protected override GameObject InitializeObjects()
    {
        GameObject temp = base.InitializeObjects();
        temp.transform.SetParent(canvas.transform, false);
        return null;
    }
}
