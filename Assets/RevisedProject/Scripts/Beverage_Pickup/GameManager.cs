using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int cleanDishCount, cleanCupCount, strikes, armSpawnRate = 5;

    [SerializeField]private int negY, posY, negX, posX;
    [SerializeField] GameObject cleanDish, cleanCup, armObj;


    private void Update() 
    {
        if(strikes <= 0)
        {
            //game over
        }

    }

    private void OnEnable() 
    {
        StartCoroutine(SpawnArms());
        SucceedManager.SuccessEvent += DishGrabbed;
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
    }
   

    public void RemoveLife()
    {
        strikes--;
    }
    public void CreateArm()
        {
            int x = Random.Range(negX,posX);
            
            GameObject newArm = Instantiate(armObj, new Vector3(x,1250,0), Quaternion.identity);
        }
    private void CreateCleanObj(GameObject obj)
    {
        int x = Random.Range(negX,posX);
        int y = Random.Range(negY,posY);

        GameObject newObj = Instantiate(obj, new Vector3(x,y,0), Quaternion.identity);
    }

    public void CheckToRefill()
    {
        if(cleanDishCount > 0)
        {
            CreateCleanObj(cleanDish);
        }
        if(cleanCupCount > 0)
        {
            CreateCleanObj(cleanCup);
        }
    }
     

   
}
