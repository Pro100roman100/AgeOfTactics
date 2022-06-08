using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Matter_text : MonoBehaviour
{
    private float oldMatter = -1;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string prefix;

    private void Update()
    {
        if(oldMatter != MatterManager.matter)
        {
            text.text = prefix + Mathf.Floor(MatterManager.matter).ToString();
        }
        oldMatter = MatterManager.matter;
    }
}
