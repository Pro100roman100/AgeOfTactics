using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject ss;
    public Tranform sd;
    public void Update()
    {
        Instantiate(ss, sd, Quaternion.reverse);
    }
}
