using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Pendu : MonoBehaviour
{
    public static Pendu instance;

    string word;
    public string[] words;
    public Keyboard keyboard;
    public AudioClip winSound;
    public AudioClip loseSound;

    int counter;
    string holes;
    char[] letters;

    public InputField input;
    public Text output;
    public Text wordToGuess;
    public Text life;

    public Sprite[] hang;
    public GameObject hanging;
    Image hangingSprite;

    GameObject restartYes;
    GameObject resartNo;
    GameObject audio;
    AudioSource m_audio;

    bool ended;

    void Awake()
    {
        instance = this;
        restartYes = GameObject.Find("Restart(oui)");
        resartNo = GameObject.Find("Restart(non)");
        audio = GameObject.Find("audio");
        m_audio = audio.GetComponent<AudioSource>();
    }

    void Init()
    {
        restartYes.SetActive(false);
        resartNo.SetActive(false);
        
        keyboard.Init();
        var rand = new System.Random();
        word = words[rand.Next(words.Length)];
        hangingSprite = hanging.GetComponent<Image>();
        counter = 8;
        holes = "";
        ended = false;

        ToGuess(word);

        Debug.Log("Lancement pendu. Le mot à trouver est " + word + ".");

        output.text = "Tentez de trouver le mot mystère";
        hangingSprite.sprite = hang[hang.Length - counter];
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }


    public void OnValidate()
    {
        // si la partie n'est pas finie, on limite l'entrée utilisateur
        if (!ended)
        {
            Play();
        }
        else
        {// si on ne joue plus, on enlève les limitations
            Restart("");
        }
    }

    // permet les trous pour faire deviner un mot
    void ToGuess(string word)
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
    public bool OnKeyPressed(string guess)
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
            hangingSprite.sprite = hang[hang.Length - counter];
        }
        else
        {
            holes = placed.ToString();
            wordToGuess.text = holes;
        }

        return find;
    }

    void End(string state)
    {
        if(state == "win")
        {
            m_audio.clip = winSound;
            output.text = "Victoire !\nVoulez-vous refaire une partie ?";
        }
        else if(state == "lose")
        {
            m_audio.clip = loseSound;
            output.text = $"Perdu ! Le mot à trouver était {word}.\nVoulez-vous refaire une partie ?";
        }

        m_audio.Play();
        ended = true;
        // On détruit l'ancien clavier
        for (int i = 0; i < keyboard.keyboardParent.transform.childCount; i++)
        {
            Destroy(keyboard.keyboardParent.transform.GetChild(i).gameObject);
        }
        restartYes.SetActive(true);
        resartNo.SetActive(true);
    }

    void Play()
    {
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
            OnKeyPressed(input.text);
            output.text = "Tentez de trouver le mot mystère";
            input.text = "";
        }

        CheckEnd();
    }

    public void CheckEnd()
    {
        bool find = false;
     
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
            End("win");
        }

        // si plus de pv, on perd (ajusté directement par rapport au jeu)
        if (counter < 2)
        {
            End("lose");
        }
    }

    public void Restart(string restartAnswer)
    {
        if (input.text == "oui" || restartAnswer == "oui")
        {
            input.text = "";
            Init();
        }
        else if (input.text == "non" || restartAnswer == "non")
        {
            input.text = "";
            Application.Quit();
        }
        else
        {
            input.text = "";
            output.text = "Voulez-vous refaire une partie ? oui / non";
        }
    }
}
