using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DebugUI : MonoBehaviour 
{
    Text tDebug;
    GameObject goPlayer;

    void Awake ()
    {
        tDebug = GameObject.Find("Debug Text").GetComponent("Text") as Text;
        goPlayer = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update () 
	{
        Clear();

        EnterNewLine("Health: " + goPlayer.GetComponent<Health>().CurrentHealth);
        EnterNewLine("Stamina: " + goPlayer.GetComponent<Stamina>().CurrentStamina);
        EnterNewLine("Buttons: ");
        EnterNewLine("    Run   = " + Input.GetButton("Run").ToString());
        EnterNewLine("    Sneak = " + Input.GetButton("Sneak").ToString());
    }

    private void Clear()
    {
        tDebug.text = "";
    }

    private void EnterNewLine(string csLine)
    {
        tDebug.text += csLine + "\n\r";
    }
}
