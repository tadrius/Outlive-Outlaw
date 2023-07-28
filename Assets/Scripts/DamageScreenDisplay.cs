using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageScreenDisplay : MonoBehaviour
{

    [SerializeField] float fadeDuration = 1f;

    Image image;
    bool fading = true;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        image.CrossFadeAlpha(0f, 0f, true);
    }

    private void Update()
    {
        if (fading)
        {
            image.CrossFadeAlpha(0f, fadeDuration, false);
        } else
        {
            image.CrossFadeAlpha(1f, 0f, false);
            fading = true;
        }
    }

    public void ShowDisplay()
    {
        fading = false;
    }
}
