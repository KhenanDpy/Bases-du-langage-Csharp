using System.Text;
using UnityEngine;
using UnityEngine.UI;



public class Game : MonoBehaviour
{
    public static Game instance;

    public bool testMod;

    string word;
    public string[] words;
    public Keyboard keyboard;
    public AudioClip winSound;
    public AudioClip loseSound;

    int counter;
    string holes;
    char[] letters;

    public Text output;
    public Text wordToGuess;

    public Sprite[] hang;
    public GameObject hanging;
    
    GameObject restartYes;
    GameObject resartNo;
    GameObject endAudio;
    AudioSource m_audio;
    Image hangingSprite;

    // A la création du pendu :
    void Awake()
    {
        instance = this; // On crée une instance de game
        restartYes = GameObject.Find("Restart(oui)"); // On associe le bouton restartYes
        resartNo = GameObject.Find("Restart(non)"); // On associe le bouton resartNo
        endAudio = GameObject.Find("audio"); // On associe l'audio
        m_audio = endAudio.GetComponent<AudioSource>(); // On crée une variable pour changer l'audio associé
        hangingSprite = hanging.GetComponent<Image>(); // On crée une variable pour changer l'image du pendu
    }

    // On initialise la partie
    void Init()
    {
        // On cache les boutons "oui" et "non"
        restartYes.SetActive(false);
        resartNo.SetActive(false);
        
        keyboard.Init(); // On crée le clavier
        var rand = new System.Random(); // On crée un nombre aléatoire
        word = words[rand.Next(words.Length)]; // On pioche un mot aléatoirement parmi une liste de mot
        counter = 11; // On met la vie au max
        holes = ""; // On crée le mot à trou qui s'affiche et qui correspond au mot que l'on doit deviner

        ToGuess(word); // On met à jour "holes" pour que l'on voit un nombre de tiret égal au nombre de lettre du mot à deviner

        if (testMod)
        {
            Debug.Log("Lancement pendu. Le mot à trouver est " + word + ".");
        }

        output.text = "Tentez de trouver le mot mystère";
        hangingSprite.sprite = hang[hang.Length - counter]; // On associe la bonne image à l'état de base du pendu
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Permet de créer le mot à trou pour faire deviner un mot
    void ToGuess(string word)
    {

        letters = new char[word.Length]; // On crée un tableau de caractères de la taille de notre mot

        // A chaque lettre de notre mot, on associe cette dernière à une case de notre tableau de caractères
        for (int i = 0; i < word.Length; i++)
        {
            letters[i] = word[i];
        }

        // Pour chaque caractères dans notre tableau de caractères, on écrit "_ "
        foreach (char letter in letters)
        {
            holes += "_ ";
        }

        wordToGuess.text = holes; // On affiche "holes"

    }

    // Permet de savoir si une lettre est bien dans le mot ou pas
    public bool OnKeyPressed(string guess)
    {
        int i = 0;
        bool find = false;
        StringBuilder placed = new StringBuilder(holes); // Permet de créer une copie de notre chaîne de caractères pour la modifier

        // On cherche dans le mot à deviner si la lettre entrée existe
        foreach (char letter in letters)
        {
            if (letter.ToString() == guess)
            {
                // Si oui, on remplace le tiret par la lettre
                placed[i * 2] = letter;
                find = true;
            }
            i++;
        }
        // Si aucune lettre n'est trouvée, on perd un pv
        if (!find)
        {
            counter--;
            hangingSprite.sprite = hang[hang.Length - counter];
        }
        else
        {
            holes = placed.ToString(); // On actualise la chaîne de caractère par rapport à la copie pour la modifier
            wordToGuess.text = holes; // On affiche la chaîne de caractère
        }

        return find; // On dit si on a trouvé ou non la lettre
    }

    // Si la partie est finie :
    void End(string state)
    {
        // Si c'est gagné, on associe un son de victoire et on demande si on rejoue
        if(state == "win")
        {
            m_audio.clip = winSound;
            output.text = "Victoire !\nVoulez-vous refaire une partie ?";
        }
        // Si c'est perdu, on associe un son de défaite, on affiche le mot qu'il fallait trouver et on demande si on rejoue
        else if (state == "lose")
        {
            m_audio.clip = loseSound;
            output.text = $"Perdu ! Le mot à trouver était {word}.\nVoulez-vous refaire une partie ?";
        }

        m_audio.Play(); // On joue le son associé précédemment

        // On détruit l'ancien clavier
        for (int i = 0; i < keyboard.keyboardParent.transform.childCount; i++)
        {
            Destroy(keyboard.keyboardParent.transform.GetChild(i).gameObject);
        }

        // On affiche les boutons "oui" et "non"
        restartYes.SetActive(true);
        resartNo.SetActive(true);
    }

    // On vérifie si la partie est finie
    public void CheckEnd()
    {
        bool find = false;
     
        // On cherche si il y a des tirets
        foreach (char _ in holes)
        {
            if (_ == '_')
            {
                find = true;
            }
        }
        // Si il n'y a plus de tiret, c'est gagné
        if (!find)
        {
            End("win");
        }

        // Si on a plus de pv, on perd (ajusté directement par rapport au jeu)
        if (counter < 2)
        {
            End("lose");
        }
    }

    // Pour relancer la partie ou quitter le jeu
    public void Restart(string restartAnswer)
    {
        // Si oui, on recommence une partie
        if (restartAnswer == "oui")
        {
            Init();
        }
        // Si non, on quitte le jeu
        else if (restartAnswer == "non")
        {
            Application.Quit();
        }
    }
}