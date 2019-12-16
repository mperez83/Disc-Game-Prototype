using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFadeObject : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Start()
    {
        LeanTween.moveY(gameObject, transform.position.y + 1, 1f).setEase(LeanTweenType.easeOutCubic).setOnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    public void SetText(string newText) { text.text = newText; }
    public void SetColor(Color newColor) { text.color = newColor; }
}