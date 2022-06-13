using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTech : Tech
{
    [SerializeField] private float strengAdd;
    [SerializeField] private float speedAdd;

    public override void OnResearch()
    {
        UpdatesManager.addSpeed += speedAdd;
        UpdatesManager.addStreng += strengAdd;
    }
}
