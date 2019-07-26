using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cooldown : MonoBehaviour
{
    [SerializeField]
    int clockCoolDown;
    [SerializeField] private Sprite[] timerStatusSprites;
    int x = 0;

    private void Start()
    {
        Image clockImage = this.gameObject.GetComponent<Image>();
    }
    //
    IEnumerator UpdateTimer()
    {
        float timePass = ((float)clockCoolDown) / timerStatusSprites.Length;
        yield return new WaitForSeconds(timePass);
        Image clockImage = this.gameObject.GetComponent<Image>();
        clockImage.sprite = timerStatusSprites[x];
        x++;
        IEnumerator timerSprite = UpdateTimer();
        if(x < timerStatusSprites.Length)
        {
            StartCoroutine(timerSprite);
        }
        else
        {
            x = 0;
            clockImage.sprite = timerStatusSprites[x];
            this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void BeginCountdown(int cooldown)
    {
        this.gameObject.transform.localScale = new Vector3(1.3f, 1.3f, 1f);

        clockCoolDown = cooldown;
        
        IEnumerator timerSprite = UpdateTimer();
        StartCoroutine(timerSprite);
    }

}
