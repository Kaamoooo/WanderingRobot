using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public  class GameController : MonoBehaviour
{

    public static bool StartPersuading=false;
    public static WalaSound WalaSound;
    public static GameObject CaptureDroid;
    public static bool IsGameOver=false;
    public static float CurBattery=4;
    public static float MinDroidWalaDistance = 100f;

    public static List<string> Props= new List<string>();

    public static bool IsWarning=false;
    public DroidMovement[] DroidMovements;
    public WalaMovement WalaMovement;
    public static float MinWarningDistance = 10f;

    public static bool StartMoving = false;

    private void Start()
    {
        MinWarningDistance=GameObject.Find("Warning").GetComponent<WarningUI>().MinWarningDistance;
        WalaSound=GameObject.Find("Wala").GetComponent<WalaSound>();
    }
    private void Update()
    {
        foreach (DroidMovement droidMovement in DroidMovements)
        {
            float distance = Vector3.Distance(WalaMovement.transform.position, droidMovement.transform.position);
            if(distance < MinWarningDistance)
            {
                IsWarning = true;
                MinDroidWalaDistance=distance;
                return;
            }
        }
        MinDroidWalaDistance = 100f;
        IsWarning = false;
    }
    public static void EscapeFailed()
    {
        IsGameOver=true;
        GameObject.Find("GameOver").GetComponent<GameOver>().EscapeFailed();
    }

    public static void EscapeSucceeded()
    {
        GameObject.Find("GameOver").GetComponent<GameOver>().EscapeSucceeded();
    }

    public static bool hasBeenCaptured=false;
    public static bool HasProp(string name)
    {
        foreach (string str in GameController.Props)
        {
            if (str.Equals(name))
            {
                return true;
            }
        }
        return false;
    }
    public static void UseProp(string name)
    {
        if(HasProp(name)) {
             GameController.Props.Remove(name);
        }
    }
    public static void AddProp(string name) { 
        Props.Add(name);
    }

    public static void Reset()
    {
        StartPersuading = false;
        Props=new List<string>();
        CaptureDroid = null;
        hasBeenCaptured = false;
        IsGameOver = false;
        StartMoving = false;
        CurBattery = 4;
        MinDroidWalaDistance = 100f;
    }

    private void OnDestroy()
    {
        Reset();
    }
}
