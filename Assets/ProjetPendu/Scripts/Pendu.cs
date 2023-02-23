using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Pendu : MonoBehaviour
{

    [SerializeField]
    private string word;

    int counter;

    string chars;

    private string holes;

    char[] letters;

    public InputField input;
    public Text ouput;
    public Text wordToGuess;


    void Init(bool gameStart)
    {

        //    gameStart
        if (gameStart)
        {
            counter = 8;
            holes = "";

            toGuess(word);

            Debug.Log("Lancement pendu. Le mot à trouver est " + word + ".");

            ouput.text = "Le mot à trouver est " + word + ".";
        }

    }

    // Start is called before the first frame update
    void Start()
    {

        Init(true);

    }


    public void onValidate()
    {
        bool find = false;
        Debug.Log("onValidate=" + input.text);

        // si l'entrée utilisateur fait plus/moins qu'un caractère
        if (input.text.Length != 1)
        {
            ouput.text = "Veuillez entrer une lettre";
            return;
        }
        // sinon on remplace (si possible) les pointillés par le caractère choisi
        else
        {
            replace(input.text);
        }

        // on vérifie si on a gagné
        foreach (char _ in holes)
        {
            if (_ == '_')
            {
                find = true;
            }
        }
        if (find == false)
        {
            win();
        }

        Init(false);

    }

    // permet les trous pour faire deviner un mot
    private void toGuess(string word)
    {

        letters = new char[word.Length];


        for (int i = 0; i < word.Length; i++)
        {
            letters[i] = word[i];
        }

        foreach (char letter in letters)
        {
            holes += "_ ";
        }

        wordToGuess.text = holes;

    }

    // permet d'avoir l'emplacement (si elle existe) de la lettre entrée par l'utilisateur
    private void replace(string guess)
    {
        int i = 0;
        bool find = false;
        StringBuilder placed = new StringBuilder(holes);

        foreach (char letter in letters)
        {
            if (letter.ToString() == guess)
            {
                placed[i * 2] = letter;
                find = true;
            }
            i++;
        }
        if (!find)
        {
            counter--;
            Debug.Log($"vie restante : {counter}");
            if (counter < 1)
            {
                lose();
            }
        }
        else
        {
            holes = placed.ToString();
            wordToGuess.text = holes;
        }
    }

    private void lose()
    {
        Debug.Log("perdu");
        Init(true);
    }

    private void win()
    {
        Debug.Log("victoire");
        Init(true);
    }
}
