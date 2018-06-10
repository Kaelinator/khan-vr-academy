using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class FetchData : MonoBehaviour {

    public GameObject prefab;
    public static string userNickname;

	// Use this for initialization
	void Start () {

        userNickname = GetUserData();
        JArray exercises = GetUserExercises();

        if (exercises == null)
            return;
        
        for (int i = 0; i < exercises.Count; i++) {

            Quaternion q = new Quaternion(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
            Vector3 v = new Vector3(Random.Range(-3f, 3f), Random.Range(1f, 4f), Random.Range(2f, 3f));

            
            Text[] text = prefab.GetComponentsInChildren<Text>();

            text[0].text = "Exercise:\n" + SpliceText(((string) exercises[i]["exercise"]), 10);
            text[1].text = "Streak:\n" + exercises[i]["streak"];
            text[2].text = "Level:\n" + exercises[i]["fpm_mastery_level"];
            text[3].text = ((JArray) exercises[i]["exercise_model"]["prerequisites"]).Count + "\nPrereqs";

            Color c;
            switch ((string) exercises[i]["fpm_mastery_level"]) {


                case "unfamiliar":
                    c = Color.red;
                    break;

                case "familiar":
                    c = Color.blue;
                    break;

                case "proficient":
                    c = Color.green;
                    break;

                case "mastered":
                    c = Color.magenta;
                    break;

                default:
                    c = Color.white;
                    break;
            }

            GameObject proto = Instantiate(prefab, v, q);
            proto.GetComponentInChildren<MeshRenderer>().material.color = c;

        }

    }

    public static string SpliceText(string text, int lineLength) {
        return Regex.Replace(text, "(.{" + lineLength + "})", "$1\n");
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

    JArray GetUserExercises() {

        string url = "http://khan-vr-api.herokuapp.com/api/userExercises?code=" + GenerateCode.code;
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

        return data["data"];
    }
}
