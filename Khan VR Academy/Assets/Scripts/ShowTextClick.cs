using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class ShowTextClick : MonoBehaviour {

    [SerializeField] private VRInteractiveItem item;
    private CanvasRenderer r;

    void Start() {
        r = GetComponent<CanvasRenderer>();
        r.SetAlpha(0);
        item.OnClick += HandleClick;
    }

    void HandleClick() {
        r.SetAlpha(255);
    }
}
