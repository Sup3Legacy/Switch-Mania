using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    public GameObject RedSprite;
    public GameObject GreenSprite;
    public GameObject BlueSprite;
    public GameObject YellowSprite;

    public int[] colours; //Couleurs sur lesquelles le switch agit

    void Start()
    {
        GameObject[] sprites = new GameObject[]{RedSprite, GreenSprite, BlueSprite, YellowSprite};
        foreach(GameObject sp in sprites)
        {
            sp.GetComponent<SpriteRenderer>().enabled = false;
        }
        foreach (int col in colours)
        {
            sprites[col].GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
