using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using Newtonsoft.Json.Linq;

public class FetchData : MonoBehaviour {

    public static string userNickname;

	// Use this for initialization
	void Start () {

        userNickname = GetUserData();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    string GetUserData() {

        string url = "http://khan-vr-api.herokuapp.com/api/user?code=" + GenerateCode.code;
        Debug.Log("downloading from " + url);

        string json = "{ success: false }";
        try {
            json = new WebClient().DownloadString(url);
        } catch (WebException) {
            return null;
        }

        dynamic data = JObject.Parse(json);

        if (!((bool) data["success"]))
            return null;

        return (string) data["data"]["student_summary"]["nickname"];
    }
}
