using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToStartUIController : UIControllerBase
{
    [SerializeField]
    UnityEngine.UI.Text textUI;

    private float blinkingSpeed = 2;

    private bool isBreak = false;

    private IEnumerator tryBlinkingText()
    {
        float t = 0;
        Color firstColor = textUI.color;
        Color blinkingColor = firstColor;
        int sign = 1;
        while (true)
        {
            if (isBreak) break;
            textUI.color = blinkingColor;
            blinkingColor.a = t;
            t += blinkingSpeed * Time.deltaTime * sign;
            if (t <= 0 || 1 <= t) sign *= -1;
            yield return null;
        }
        textUI.color = firstColor;
    }
    private void playBlinkingText()
    {
        StartCoroutine(tryBlinkingText());
    }

    protected override void Start()
    {
        base.Start();
        playBlinkingText();
    }
}
