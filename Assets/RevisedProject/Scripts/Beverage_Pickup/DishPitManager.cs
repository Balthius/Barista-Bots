using UnityEngine;

public class DishPitManager : MonoBehaviour
{
    [SerializeField] private GameObject dirtyDish, dirtyCup;
    [SerializeField] Transform dirtyCupSpawn, dirtyDishSpawn;

    [SerializeField] GameObject gameManager;

    public static bool dishActive = false;

    private void OnEnable()
    {
        SucceedManager.SuccessEvent += SpawnDirty;
    }

    private void OnDisable()
    {
        SucceedManager.SuccessEvent -= SpawnDirty;
    }

    public void SpawnDirty()
    {
        if (dishActive == false)
        {
            GameManager gm = gameManager.GetComponent<GameManager>();
            if (gm.cleanCupCount > 1)
            {
                Instantiate(dirtyCup, dirtyCupSpawn.position, Quaternion.identity);
            }

            if (gm.cleanDishCount > 1)
            {
                Instantiate(dirtyDish, dirtyDishSpawn.position, Quaternion.identity);
            }
        }
    }
}
