using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Matter_text : MonoBehaviour
{
    private float player1oldMatter = -1;
    private float player2oldMatter = -1;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string prefix;

    private void Update()
    {
        if(player1oldMatter != MatterManager.player1matter)
        {
            text.text = prefix + Mathf.Floor(MatterManager.player1matter).ToString();
        }
        if (player2oldMatter != MatterManager.player2matter)
        {
        }
        player1oldMatter = MatterManager.player1matter;
        player2oldMatter = MatterManager.player2matter;
    }
}
