using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite[] scoreSprites;
    [SerializeField] private Image overlayImage;
    [SerializeField] private Button button;
    [SerializeField] private ScoreHandler scoreHandler = null;

    private void Start()
    {
        overlayImage.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
    }

    public void ChooseSprite(int score)
    {
        overlayImage.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        overlayImage.sprite = scoreSprites[score - 1];
        scoreHandler.IncreaseCurrency(score);
    }
}
