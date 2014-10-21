using UnityEngine;
using System.Collections;

public class BacktoMenu : MonoBehaviour
{
    public Texture2D backButton;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Exit"))
            Application.LoadLevel("Menu");
	}

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(80, Screen.height - 80, 200, Screen.height / 2));

        if (GUILayout.Button(backButton, GUIStyle.none))
        {
            Application.LoadLevel("Menu");
        }

        GUILayout.EndArea();
    }
}
