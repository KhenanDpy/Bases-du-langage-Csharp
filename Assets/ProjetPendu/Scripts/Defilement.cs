using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defilement : MonoBehaviour
{
    [SerializeField]
    public int x;

    [SerializeField]
    public int y;

    [SerializeField]
    public int largeur;

    [SerializeField]
    public int hauteur;

/*    [SerializeField]
    private Sprite sprite1;
*/
    

    
    void OnGUI()
    {
        if (GUI.Button(new Rect(x, y, largeur, hauteur), "Vérifier"))
        {
            print("You clicked the button!");
            
            //return state;   // peut-être ? Il faut que quand on clique, l'image du pendu change.         
        }
    }
    


}
