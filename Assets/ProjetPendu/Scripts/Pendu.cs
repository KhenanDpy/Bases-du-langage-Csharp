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

        public InputField input;
        public Text ouput;


        void Init(bool gameStart)
        {

            //    gameStart

        }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Lancement pendu. Le mot à trouver est " + word + ".");

            ouput.text = "tapez une lettre";

            Init(true);

        }


        public void onValidate()
        {
            Debug.Log("onValidate=" + input.text);

            if (input.text.Length != 1) return;

            Init(false);

        }
    }
