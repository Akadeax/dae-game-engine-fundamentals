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

    public void SetTexture(PlayerController.Augment augment)
    {
        var img = GetComponent<RawImage>();
        switch (augment)
        {
            case PlayerController.Augment.None:
                img.texture = noneTex; break;
            case PlayerController.Augment.Detacher:
                img.texture = detacherTex; break;
        }
    }
}
