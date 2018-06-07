using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

public class Interactive : MonoBehaviour {

    [SerializeField] private VRInteractiveItem interactiveItem;
    [SerializeField] private Material material;
    [SerializeField] private Material clickedMaterial;
    private Material[] materials;
    private Renderer r;
    private int i;

    private void Start() {
        r = GetComponent<Renderer>();
        materials = new Material[] { material, clickedMaterial };
        i = 0;
    }

    private void OnEnable() {
        interactiveItem.OnClick += HandleClick;
    }

    private void HandleClick() {
        r.material = materials[(i++) % materials.Length];
        Application.OpenURL("http://unity3d.com/");
    }
}
