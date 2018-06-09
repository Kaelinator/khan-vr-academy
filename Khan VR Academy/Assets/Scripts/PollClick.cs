using System.Net;
using UnityEngine;
using VRStandardAssets.Utils;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class PollClick : MonoBehaviour {

    [SerializeField] private VRInteractiveItem item;
    public bool poll;
    private float nextActionTime = 0.0f;
    public float period = 3f;

    // Use this for initialization
    void Start() {
        Debug.Log("Poll enabled");
        poll = false;
        item.OnClick += StartPolling;
    }
	
	// Update is called once per frame
	void Update() {
        if (poll && Time.time > nextActionTime) {
            nextActionTime = Time.time + period;

            string url = "http://khan-vr-api.herokuapp.com/api?code=" + GenerateCode.code;
            Debug.Log("downloading from " + url);

            string json = "{ success: false }";
            try {
                json = new WebClient().DownloadString(url);

            } catch (WebException) {
                return;
            }
            
            dynamic data = JObject.Parse(json);
            Debug.Log(data["success"]);
            
            if ((bool) data["success"]) {
                SceneManager.LoadScene("StatViewer");
                poll = false;
            }
            
        }
    }

    void StartPolling() {
        poll = true;
    }
}
