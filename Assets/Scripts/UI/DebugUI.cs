using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DebugUI : MonoBehaviour 
{
    private Text tDebug;
    private GameObject goPlayer;

    void Awake ()
    {
        tDebug = GameObject.Find("Debug Text").GetComponent("Text") as Text;
        goPlayer = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update () 
	{
        Clear();

        EnterNewLine("Health: " + goPlayer.GetComponent<Health>().CurrentStat);
        EnterNewLine("Stamina: " + goPlayer.GetComponent<Stamina>().CurrentStat);
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
