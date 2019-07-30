using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int cleanDishes, cleanCups, strikes, armSpawnRate = 5;
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
        ArmController.DrinkDrunk += DishGrabbed;
    }


    IEnumerator SpawnArms()
    {   CreateArm();
        yield return new WaitForSeconds(armSpawnRate);

        StartCoroutine(SpawnArms());
    }
    private void DishGrabbed()
    {
        cleanDishes--;
        cleanCups--;
    }
   

    public void RemoveLife()
    {
        strikes--;
    }
    public void CreateArm()
        {
            int x = Random.Range(-400,400);
            
            GameObject newArm = Instantiate(armObj, new Vector3(x,1250,0), Quaternion.identity);
        }
    private void CreateCleanObj(GameObject obj)
    {
        int x = Random.Range(-400,400);
        int y = Random.Range(-300,300);

        GameObject newObj = Instantiate(obj, new Vector3(x,y,0), Quaternion.identity);
    }

    public void CheckToRefill()
    {
        if(cleanDishes > 0)
        {
            CreateCleanObj(cleanDish);
        }
        if(cleanCups > 0)
        {
            CreateCleanObj(cleanCup);
        }
    }
     

   
}
