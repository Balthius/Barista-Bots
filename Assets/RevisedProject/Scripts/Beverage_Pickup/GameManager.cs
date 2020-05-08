using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int cleanDishCount, cleanCupCount, strikes, armSpawnRate = 5;

    [SerializeField] private int negY, posY, negX, posX;// Y 400, X 800
    [SerializeField] GameObject cleanDish, cleanCup, armObj, scorePanel;

    private int currentScore;

    private void OnEnable()
    {
        StartCoroutine(SpawnArms());
        SucceedManager.SuccessEvent += DishGrabbed;
        SucceedManager.FailEvent += RemoveLife;
    }

    private void OnDisable()
    {
        SucceedManager.SuccessEvent -= DishGrabbed;
        SucceedManager.FailEvent -= RemoveLife;
    }

    void Update() {
        CheckToRefill();
    }

    IEnumerator SpawnArms()
    {
        yield return new WaitForSeconds(armSpawnRate);
        CreateArm();
        StartCoroutine(SpawnArms());
    }

    private void DishGrabbed()
    {
        cleanDishCount--;
        cleanCupCount--;
        currentScore++;
    }

    public void RemoveLife()
    {
        strikes--;
        if (strikes <= 0)
        {
            GameOver(currentScore);
        }
    }

    public void CreateArm()
    {
        int x = Random.Range(negX, posX);

        GameObject newArm = Instantiate(armObj, new Vector3(x, 1250, 0), Quaternion.identity);
    }

    private void CreateCleanObj(GameObject obj)
    {
        int x = Random.Range(negX, posX);
        int y = Random.Range(negY, posY);

        GameObject newObj = Instantiate(obj, new Vector3(x, y, 0), Quaternion.identity);
    }

    public void CheckToRefill()
    {
        var cups = FindObjectsOfType<CupController>();
        var existsCombined = false;
        foreach (var cup in cups) {
            if (cup.combined) {
                existsCombined = true;
            }
        }
        if (existsCombined) {
            return;
        }
        
        var activeCleanCups = cups.Length;
        var activeCleanDishes = FindObjectsOfType<DishController>().Length;

        if (cleanDishCount > 0 && activeCleanDishes < 1)
        {
            CreateCleanObj(cleanDish);
        }
        if (cleanCupCount > 0 && activeCleanCups < 1)
        {
            CreateCleanObj(cleanCup);
        }
    }

    private void GameOver(int score)
    {
        StopAllCoroutines();

        if (score <= 3)
        {
            score = 3;
        }
        int scoreToPass = score / 3;

        if (scoreToPass > 5)
        {
            scoreToPass = 5;
        }

        scorePanel.GetComponent<ScoreManager>().ChooseSprite(scoreToPass);
    }
}
