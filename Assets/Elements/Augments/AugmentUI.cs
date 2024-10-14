using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AugmentUI : MonoBehaviour
{
    [SerializeField] Texture2D noneTex;
    [SerializeField] Texture2D detacherTex;
    [SerializeField] Texture2D operatorTex;
    [SerializeField] Texture2D scannerTex;

    RawImage image;

    private void Awake()
    {
        image = GetComponent<RawImage>();
    }

    private void Start()
    {
        image.enabled = true;
    }

    public void SetTexture(PlayerController.Augment augment)
    {
        switch (augment)
        {
            case PlayerController.Augment.None:
                image.texture = noneTex; break;
            case PlayerController.Augment.Detacher:
                image.texture = detacherTex; break;
        }
    }
}
