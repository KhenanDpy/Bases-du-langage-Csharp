using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class Keys : MonoBehaviour
{
    public Pendu pendu;
    private Button _btn;

    private void Awake()
    {
        _btn = GetComponent<Button>();
    }

    void Start()
    {
        _btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("Vous avez cliqué sur le bouton " + name);
        bool retour = pendu.replace(name);
        pendu.CheckEnd();
        var colorBlock = _btn.colors;
        if (retour)
        {
            colorBlock.disabledColor = Color.green;
        }
        else
        {
            colorBlock.disabledColor = Color.red;
        }
        _btn.interactable = false;

    }
}
