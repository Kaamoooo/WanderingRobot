using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractTip : MonoBehaviour
{
    public static TextMeshProUGUI TextMeshProUGUI;
    public static GameObject TextMeshProUGUIObj;
    private void Start()
    {
        TextMeshProUGUIObj = gameObject;
        TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
}
