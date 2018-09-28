using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InitialiseApplicantView : MonoBehaviour
{
    public Sprite sprite;
    public Image image;

    // Use this for initialization
    void Start()
    {
        image.sprite = sprite;
    }
}
