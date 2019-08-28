using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] drawerItems;

    private void Start()
    {
        ChooseItems(3);
    }

    public void ChooseItems(int amount)
    {
        for (int i = 0; i <= amount; i++)
        {
            StartCoroutine("SpawnItems");
        }
    }

    IEnumerator SpawnItems()
    {
        int x = Random.Range(-200, 200);

        int y = Random.Range(-400, 400);

        int z = Random.Range(0, drawerItems.Length);

        Vector3 objPos = new Vector3(x, y, -1);
        GameObject popUp = Instantiate(drawerItems[z], objPos, Quaternion.identity);
        popUp.transform.parent = this.transform;
        popUp.transform.position = this.transform.position + objPos;
        return null;
    }
}
