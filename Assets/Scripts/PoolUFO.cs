using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolUFO : Pool
{
    // Start is called before the first frame update

    public static PoolUFO SharedInstance;
    protected void Awake()
    {
        SharedInstance = this;
    }
    protected override Color GetColor()
    {
        return new Color(1.0f, 0, 0);
    }
    
    
}
