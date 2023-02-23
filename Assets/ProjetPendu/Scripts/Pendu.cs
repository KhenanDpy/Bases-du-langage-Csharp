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
    public Text output;
    public Text wordToGuess;
    public Text life;

    public Sprite[] hang;
    public GameObject hanging;
    Image hangingSprite;

    bool ended;


    void Init(bool gameStart)
    {

        //    gameStart
        if (gameStart)
        {
            hangingSprite = hanging.GetComponent<Image>();
            counter = 8;
            holes = "";
            ended = false;

            toGuess(word);

            Debug.Log("Lancement pendu. Le mot à trouver est " + word + ".");

            output.text = "Tentez de trouver le mot mystère";
            life.text = $"Vie restante : {counter - 1}";
            hangingSprite.sprite = hang[hang.Length - counter];
        }

    }

    // Start is called before the first frame update
    void Start()
    {

        Init(true);

    }


    public void onValidate()
    {
        Debug.Log("onValidate=" + input.text);

        // si la partie n'est pas finie, on limite l'entrée utilisateur
        if (!ended)
        {
            play();
        }
        else
        {// si on ne joue plus, on enlève les limitations
            restart();
        }
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

        // cherche dans le mot à deviner si la lettre entrée existe
        foreach (char letter in letters)
        {
            if (letter.ToString() == guess)
            {
                // si oui, on remplace le tiret par la lettre
                placed[i * 2] = letter;
                find = true;
            }
            i++;
        }
        // si aucune lettre n'est trouvée, on perd un pv
        if (!find)
        {
            counter--;
            Debug.Log($"vie restante : {counter - 1}");
            life.text = $"Vie restante : {counter - 1}";
            hangingSprite.sprite = hang[hang.Length - counter];
        }
        else
        {
            holes = placed.ToString();
            wordToGuess.text = holes;
        }
    }

    private void lose()
    {
        ended = true;
        Debug.Log("perdu");
        output.text = "Perdu ! Voulez-vous refaire une partie ? oui / non";
    }

    private void win()
    {
        ended = true;
        Debug.Log("victoire");
        output.text = "Victoire ! Voulez-vous refaire une partie ? oui / non";
    }

    private void play()
    {
        bool find = false;

        // si l'entrée utilisateur fait plus/moins qu'un caractère
        if (input.text.Length != 1)
        {
            output.text = "Veuillez entrer une lettre";
            input.text = "";
            return;
        }
        // sinon on remplace (si possible) les pointillés par le caractère choisi
        else
        {
            replace(input.text);
            output.text = "Tentez de trouver le mot mystère";
            input.text = "";
        }

        // on vérifie si on a gagné, si il n'y a plus de tiret, c'est gagné
        foreach (char _ in holes)
        {
            if (_ == '_')
            {
                find = true;
            }
        }
        if (!find)
        {
            win();
        }
        
        // si plus de pv, on perd (ajusté directement par rapport au jeu)
        if (counter < 2)
        {
            lose();
        }

        Init(false); //je sais pas vraiment à quoi il sert (je le laisse parce que c'est pas moi qui l'ait mis)
    }

    private void restart()
    {
        if (input.text == "oui")
        {
            input.text = "";
            Init(true);
        }
        else if (input.text == "non")
        {
            input.text = "";
            Init(false);
            Application.Quit();
        }
        else
        {
            input.text = "";
            output.text = "Voulez-vous refaire une partie ? oui / non";
        }
    }
}
