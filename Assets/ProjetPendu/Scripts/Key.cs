using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    Button _btn;

    void Awake()
    {
        // On récupère le bouton associé à la touche
        _btn = GetComponent<Button>();
    }

    void Start()
    {
        // Au lancement de la partie, les touches ont un évènement qui se déclenche quand on clique sur le bouton.
        _btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        bool retour = Game.instance.OnKeyPressed(name); // On vérifie si, quand on appuie sur la touche, la lettre en question est dans le mot
        Game.instance.CheckEnd();
        var colorBlock = _btn.colors; // On copie la propriété "couleur" du bouton pour la modifier.
        // Si oui, on change la couleur de la touche en vert
        if (retour)
        {
            colorBlock.disabledColor = Color.green;
        }
        // Si non, on change la couleur de la touche en rouge
        else
        {
            colorBlock.disabledColor = Color.red;
        }
        _btn.colors = colorBlock; // On applique la couleur de la propriété du bouton copiée au "vrai" bouton.
        _btn.interactable = false; // On rend le bouton non cliquable.

    }
}
