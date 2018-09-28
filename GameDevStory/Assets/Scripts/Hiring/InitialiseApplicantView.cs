using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InitialiseApplicantView : MonoBehaviour
{
    public Sprite sprite;
    public Image image;
    public TextMeshProUGUI text;

    // Use this for initialization
    void Start()
    {
        text.text = "A paragraph(from the Ancient Greek παράγραφος paragraphos, \"to write beside\" or \"written beside\") is a self - contained unit of a discourse in writing dealing with a particular point or idea. A paragraph consists of one or more sentences.";
        image.sprite = sprite;
    }
}
