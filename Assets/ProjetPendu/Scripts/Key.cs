using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
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
        bool retour = Pendu.instance.OnKeyPressed(name);
        Pendu.instance.CheckEnd();
        var colorBlock = _btn.colors;
        if (retour)
        {
            colorBlock.disabledColor = Color.green;
        }
        else
        {
            colorBlock.disabledColor = Color.red;
        }
        _btn.colors = colorBlock;
        _btn.interactable = false;

    }
}
