using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFadeObject : MonoBehaviour
{
    public TextMeshProUGUI TMPtext;

    void Start()
    {
        //Position
        LeanTween.moveY(gameObject, transform.position.y + 1, 2f).setEase(LeanTweenType.easeOutCubic).setDestroyOnComplete(true);

        //Scale
        LeanTween.scale(gameObject, transform.localScale * 0.75f, 2f).setEase(LeanTweenType.easeInCubic);

        //Alpha
        LeanTween.value(gameObject, 1, 0, 2f).setEase(LeanTweenType.easeInExpo).setOnUpdate((float value) =>
        {
            TMPtext.color = new Color(TMPtext.color.r, TMPtext.color.g, TMPtext.color.b, value);
        });
    }

    public void SetText(string newText) { TMPtext.text = newText; }
    public void SetColor(Color newColor) { TMPtext.color = newColor; }
}