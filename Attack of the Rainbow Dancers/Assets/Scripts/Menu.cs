using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
    public Texture2D startButton;
    public Texture2D controlsButton;
    public Texture2D creditsButton;
    public Texture2D exitButton;

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect((Screen.width / 2) + (Screen.width / 6), (Screen.height / 2) + (Screen.height / 6), 200, Screen.height / 2));

        if(GUILayout.Button(startButton, GUIStyle.none))
        {
            PlayerPrefs.SetFloat("BrutalMode", 1);
            Application.LoadLevel("Level1");
        }

        if (GUILayout.Button(controlsButton, GUIStyle.none))
        {
            Application.LoadLevel("Level1");
        }

        if (GUILayout.Button(creditsButton, GUIStyle.none))
        {
            Application.LoadLevel("Level2");
        }

        if (GUILayout.Button(exitButton, GUIStyle.none))
        {
            Application.Quit();
        }

        GUILayout.EndArea();

    }
    	
}
