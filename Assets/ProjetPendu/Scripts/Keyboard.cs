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
            if (i == 12) // On change de ligne
            {
                pos = startPos.position;
                pos += Vector3.down * 60f;
            }

        }
    }
    
    // On crée une touche (avec une position, un caractère et un sprite)
    void CreateKey(Vector3 pos, char v, Sprite sprite)
    {
        Key key = Instantiate(keyPrefab, pos, Quaternion.identity);
        key.transform.SetParent(keyboardParent.transform, true); // On met les touches en tant qu'enfants de keyboardPos
        Image image = key.GetComponent<Image>(); // On lui associe une image
        key.name = "" + v; // On lui donne un nom
        image.sprite = sprite; // On fait en sorte que l'image associée soit le sprite passé en paramètre
    }
    
}
