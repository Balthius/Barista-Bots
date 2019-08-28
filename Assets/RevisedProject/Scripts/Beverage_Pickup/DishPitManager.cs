using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishPitManager : MonoBehaviour
{

    [SerializeField]private GameObject dirtyDish, dirtyCup;
    [SerializeField]Transform dirtyCupSpawn, dirtyDishSpawn;

    [SerializeField]GameObject gameManager;

    public static bool dishActive = false;

    private void OnEnable() 
    {
        ArmController.DrinkDrunk += SpawnDirty;
    }

    public void SpawnDirty()
    {
        if(dishActive == false)
        {
            GameManager gm = gameManager.GetComponent<GameManager>();
            if(gm.cleanCupCount > 1)
            {
            Instantiate(dirtyCup, dirtyCupSpawn.position, Quaternion.identity);
            }

            if(gm.cleanDishCount > 1)
            {
            Instantiate(dirtyDish, dirtyDishSpawn.position, Quaternion.identity);
            }
        
        
            
        }
    }
    
}
