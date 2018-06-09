using System;
using UnityEngine.UI;
using UnityEngine;

public class GenerateCode : MonoBehaviour {

    public static string code;
    
	void Start () {
        code = Guid.NewGuid().ToString("N").Substring(0, 4).ToUpper();
        Debug.Log(code);
        GetComponent<Text>().text = "Code: " + code;
    }
}
