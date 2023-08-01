using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractSuccessTip : MonoBehaviour
{
    public static TextMeshProUGUI textMeshProUGUI;
    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    public static IEnumerator SetInteractSuccessTip(string str)
    {
        textMeshProUGUI.text = str;
        textMeshProUGUI.enabled = true;
        yield return new WaitForSeconds(2);
        if(textMeshProUGUI.text.Equals(str)) 
        {
            textMeshProUGUI.enabled = false;
        }
    }
}
