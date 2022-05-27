using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSS_ModuleCore : CSS_BossModules
{
    [Header("Module Stat")]
    private int defaultModHP;

    public void Init()
    {
        this.defaultModHP = 200;
        this.Init(this.defaultModHP);
    }

    private void Awake()
    {
        this.Init();
    }
}
