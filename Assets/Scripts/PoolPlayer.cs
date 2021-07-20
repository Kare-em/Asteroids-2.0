using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolPlayer : Pool
{
    public static PoolPlayer SharedInstance;
    protected void Awake()
    {
        SharedInstance = this;
    }
}
