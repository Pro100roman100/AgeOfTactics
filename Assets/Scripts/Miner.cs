using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : Building
{
    [SerializeField] private float matterAdd;
    public override void OnBuild()
    {
        base.OnBuild();
        MatterManager.Manager.matterPerSecond += matterAdd;
    }
    public override void OnUnbuild()
    {
        base.OnUnbuild();
        MatterManager.Manager.matterPerSecond -= matterAdd;
    }
}
