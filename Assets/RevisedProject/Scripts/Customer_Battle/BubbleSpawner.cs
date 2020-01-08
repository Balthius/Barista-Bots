using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject neutralBubble, positiveBubble, negativeBubble;
    [SerializeField] private int negX, posX, negY, posY, neutralChance, positiveChance, negativeChance ;//Chances not implemented


    private void Update() 
    {

    }

    private void Start() {
        
        StartCoroutine(SpawnBubble());
    }

    private IEnumerator SpawnBubble()
    {
        GameObject bubble = Instantiate(ChooseBubble(), ChooseLocation(), Quaternion.identity);
        bubble.GetComponent<BubbleController>().SetTargetLocation(ChooseLocation());
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpawnBubble());

    }

    private GameObject ChooseBubble()
    {
         int chooseBubble = Random.Range(0,4);

          switch(chooseBubble)
          {
              case 0:
                return positiveBubble;
              case 1:
                return negativeBubble;
              case 2:
                return neutralBubble;
              case 3:
                return neutralBubble;
              default:
                return neutralBubble;
          }
    }

    private Vector3 ChooseLocation()
    {
        int chooseOrientation = Random.Range(0,4);
        int x = posX;
        int y = posY;
       switch(chooseOrientation)
       {
           case 0:
                x = negX;
                y = Random.Range(negY,posY);
                break;
           case 1:
                x = posX;
                y = Random.Range(negY,posY);
                break;
           case 2:
                y = negY;
                x = Random.Range(negX,posX);
                break;
           case 3:
                y = posY;
                x = Random.Range(negX,posX);
                break;
           default:
                break;
       }

       return new Vector3(x, y, 0);
    }


}
