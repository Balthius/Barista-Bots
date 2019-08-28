using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int cleanDishCount, cleanCupCount, strikes, armSpawnRate = 5;

    [SerializeField]private int negY, posY, negX, posX;
    [SerializeField] GameObject cleanDish, cleanCup, armObj, scorePanel;

    private int currentScore;

    private void OnEnable() 
    {
        StartCoroutine(SpawnArms());
        ArmController.DrinkDrunk += DishGrabbed;
    }


    IEnumerator SpawnArms()
    {   CreateArm();
        yield return new WaitForSeconds(armSpawnRate);

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
        
        if(strikes <= 0)
        {
            GameOver(currentScore);
        }
    }
    public void CreateArm()
        {
            int x = Random.Range(-400,400);
            
            GameObject newArm = Instantiate(armObj, new Vector3(x,1100,0), Quaternion.identity);
        }
    public void CreateCleanObj(GameObject obj)
    {
        int x = Random.Range(-400,400);
        int y = Random.Range(-300,300);

        GameObject newObj = Instantiate(obj, new Vector3(x,y,0), Quaternion.identity);
    }
     
    private void GameOver(int score)
    {
        if(score <= 3)
        {
            score = 3;
        }
        int scoreToPass = score / 3;
        
        if(scoreToPass > 5)
        {
        scoreToPass = 5;
        }
        scorePanel.GetComponent<ScoreManager>().ChooseSprite(scoreToPass);
    }
   
}
