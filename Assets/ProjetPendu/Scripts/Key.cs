using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    Button _btn;

    void Awake()
    {
        // On r�cup�re le bouton associ� � la touche
        _btn = GetComponent<Button>();
    }

    void Start()
    {
        // Au lancement de la partie, les touches ont un �v�nement qui se d�clenche quand on clique sur le bouton.
        _btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        bool retour = Game.instance.OnKeyPressed(name); // On v�rifie si, quand on appuie sur la touche, la lettre en question est dans le mot
        Game.instance.CheckEnd();
        var colorBlock = _btn.colors; // On copie la propri�t� "couleur" du bouton pour la modifier.
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
        _btn.colors = colorBlock; // On applique la couleur de la propri�t� du bouton copi�e au "vrai" bouton.
        _btn.interactable = false; // On rend le bouton non cliquable.

    }
}
