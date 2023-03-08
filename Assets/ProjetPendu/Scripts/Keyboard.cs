using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    public Keys keyPrefab;
    public Pendu pendu;

    public Transform startPos;
    public Sprite[] sprites;
    private string alphabet = "abcdefghijklmnopqrstuvwxyz";

    public GameObject keyboardParent;

    private void Awake()
    {
        keyboardParent = GameObject.Find("keyboardPos"); // On cherche l'objet keyboardPos
    }

    // On initialise 26 touches sur 2 lignes
    public void Init()
    {
        Vector3 pos = startPos.position;

        for (int i = 0; i < 26; i++)
        {
            CreateKey(pos, alphabet[i], sprites[i]);

            pos += Vector3.right * 40f;
            if (i == 12)
            {
                pos = startPos.position;
                pos += Vector3.down * 60f;
            }

        }
    }
    
    // On créer une touche
    private void CreateKey(Vector3 pos, char v, Sprite sprite)
    {
        Keys key = Instantiate(keyPrefab, pos, Quaternion.identity);
        key.transform.parent = keyboardParent.transform;            // On met les touches en tant qu'enfants de keyboardPos
        Image image = key.GetComponent<Image>();
        key.name = "" + v;
        image.sprite = sprite;
        key.pendu = pendu;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
