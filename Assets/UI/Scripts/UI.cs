using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObjects[] objects;
    
    private bool[] active;
    private void Awake()
    {
        active = new bool[objects.Length];
        for (int i = 0; i < active.Length; i++)
        {
            active[i] = false;
        }
    }

    public void HideShowMenu(int index)
    {
        if (!active[index])
        {
            foreach(GameObject button in objects[index].objects)
            {
                button.SetActive(true);
            }
            active[index] = true;
        }
        else
        {
            foreach (GameObject button in objects[index].objects)
            {
                button.SetActive(false);
            }
            active[index] = false;
        }

    }
}

[System.Serializable]
class GameObjects
{
    public GameObject[] objects;
}