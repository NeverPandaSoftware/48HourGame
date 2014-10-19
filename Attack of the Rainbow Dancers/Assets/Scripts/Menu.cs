using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect((Screen.width / 2) - (Screen.width / 16), (Screen.height / 2) + (Screen.height / 4), 200, Screen.height / 2));

        if(GUILayout.Button("Start Brutal"))
        {
            PlayerPrefs.SetFloat("BrutalMode", 1);
            Application.LoadLevel("Level1");
        }

        if (GUILayout.Button("Normal - Level 1"))
        {
            PlayerPrefs.SetFloat("BrutalMode", 0);
            Application.LoadLevel("Level1");
        }

        if (GUILayout.Button("Normal - Level 2"))
        {
            PlayerPrefs.SetFloat("BrutalMode", 0);
            Application.LoadLevel("Level1");
        }

        if (GUILayout.Button("Controls"))
        {
            Application.LoadLevel("Level1");
        }

        if (GUILayout.Button("Credits"))
        {
            Application.LoadLevel("Level1");
        }

        if (GUILayout.Button("Exit"))
        {
            Application.Quit();
        }

        GUILayout.EndArea();

    }
    	
}
