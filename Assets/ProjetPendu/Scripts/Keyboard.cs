using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    public Key keyPrefab;

    public Transform startPos;
    public Sprite[] sprites;
    string alphabet = "abcdefghijklmnopqrstuvwxyz";

    public GameObject keyboardParent;

    void Awake()
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
    void CreateKey(Vector3 pos, char v, Sprite sprite)
    {
        Key key = Instantiate(keyPrefab, pos, Quaternion.identity);
        key.transform.parent = keyboardParent.transform;            // On met les touches en tant qu'enfants de keyboardPos
        Image image = key.GetComponent<Image>();
        key.name = "" + v;
        image.sprite = sprite;
    }
    
}
