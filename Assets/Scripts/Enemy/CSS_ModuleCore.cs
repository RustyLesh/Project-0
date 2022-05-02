using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_ModuleCore : CSS_BossModules
{
    [Header("Module Stat")]
    private int defaultModHP;

    // Start is called before the first frame update
    void Start()
    {
        this.defaultModHP = 200;
        this.Init(this.defaultModHP);
    }

}
