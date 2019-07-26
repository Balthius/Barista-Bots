using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponseManager : MonoBehaviour
{
    [SerializeField] private Sprite[] angryDialogOptions;
    [SerializeField] private Sprite[] happyDialogOptions;
    [SerializeField] private Sprite[] sadDialogOptions;

    [SerializeField] private GameObject dialogBox;

    public void UpdateDialog(int mood)
    {
        Sprite dialogSprite = dialogBox.GetComponent<Sprite>();
        switch (mood)
        {
            case 0:

                int ranNumAngry = Random.Range(0, angryDialogOptions.Length);
                dialogSprite = angryDialogOptions[ranNumAngry];
                break;
            case 1:

                int ranNumHappy = Random.Range(0, happyDialogOptions.Length);
                dialogSprite = happyDialogOptions[ranNumHappy];
                break;
            case 2:

                int ranNumSad = Random.Range(0, sadDialogOptions.Length);
                dialogSprite = sadDialogOptions[ranNumSad];
                break;
            default:
                break;
        }
    }

}
