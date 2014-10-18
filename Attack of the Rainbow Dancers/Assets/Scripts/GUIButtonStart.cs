using UnityEngine;
using System.Collections;

public class GUIButtonStart : MonoBehaviour 
{
    public Texture2D texture = null;

    private void OnGUI()
    {
        if(GUI.Button (new Rect(Screen.width / 2 - 63, Screen.height / 2 - 63, texture.width, texture.height), texture))
        {
            Application.LoadLevel("Level1);"
        }

    }

}
