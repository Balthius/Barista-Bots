using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
 {
    GameObject arm;
    GameObject plate;
    GameObject cup;
    
    public int plates;
    public int cups;
    private int dirtyPlates = 0;
    private int dirtyCups = 0;
    int strikes = 0;
    int cupsPickedUp = 0;
    public bool placeObj;
    public bool noPlates;
    public bool noCups;
    // Use this for initialization
    void Start () {
        arm = Resources.Load("Arm") as GameObject;
        plate = Resources.Load("Plate") as GameObject;
        cup = Resources.Load("Cup") as GameObject;
        makeArms();
        placeObj = true;
        noCups = false;
        noPlates = false;
        placeItem("cleanPlate");
        placeItem("cleanCup");
    }

    public void makeArms()
    {
        StartCoroutine(makeArm());
    }
	// Update is called once per frame
	void Update () 
    {

        if (plates <= 0)
        {
            noPlates = true;
        }
        if (cups <= 0)
        {
            noCups = true;
        }
        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        { 
            Camera.main.transform.position = new Vector3(19.6664f,0.1f, -9.25f);
            //Camera.main.transform.position = new Vector3(47.04f, 0.1f, -9.25f);
        }
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {    
            Camera.main.transform.position = new Vector3(-0.1f, 0.1f, -9.25f);
            Camera.main.transform.rotation = new Quaternion(0, 0, 180f, 0);
        }
        if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            Camera.main.transform.position = new Vector3(-0.1f, 0.1f, -9.25f);
            Camera.main.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }


    public void placeItem(string type)
    {
        if (type == "cleanPlate")
        {
            if (plates > 0)
            {
                plates--;
                cupsPickedUp++;
                GameObject newPlate = Instantiate(plate);
                newPlate.transform.position = new Vector3(-6.751727f, -2.34958f, 0);
                newPlate.GetComponent<Plate>().gc = this.GetComponent<GameController>();
                newPlate.GetComponent<Plate>().isDirty = false;
            }
        }
        else if (type == "dirtyPlate")
        {
            dirtyPlates++;
            GameObject dirtyPlate = Instantiate(plate);
            dirtyPlate.transform.position = new Vector3(13.67f, -1.99f, 0);
            dirtyPlate.GetComponent<Plate>().setDirty(true, 100f);
            dirtyPlate.GetComponent<SpriteRenderer>().sprite = dirtyPlate.GetComponent<Plate>().dirtySprite1;
            dirtyPlate.GetComponent<Plate>().gc = this.GetComponent<GameController>();
        }
        else if (type == "cleanCup")
        {
            if (cups > 0)
            {
                cups--;
                GameObject newCup = Instantiate(cup);
                newCup.transform.position = new Vector3(6.7f, -2.52f, 0);
                newCup.GetComponent<Cup>().gc = this.GetComponent<GameController>();
                newCup.GetComponent<Cup>().isDirty = false;
            }
        }
        else if (type == "dirtyCup")
        {
            dirtyCups++;
            GameObject dirtyCup = Instantiate(cup);
            dirtyCup.transform.position = new Vector3(13.67f, 1.99f, 0);
            dirtyCup.transform.localScale -= new Vector3(0.5f, 0.5f, 0);
            dirtyCup.GetComponent<Cup>().setDirty(true, 100f);
            dirtyCup.GetComponent<SpriteRenderer>().sprite = dirtyCup.GetComponent<Cup>().dirtySprite1;
            dirtyCup.GetComponent<Cup>().gc = this.GetComponent<GameController>();
        }
    }

    public IEnumerator makeArm()
    {
        yield return new WaitForSeconds(Random.Range(3f,7f));
        GameObject newArm = Instantiate(arm);
        newArm.GetComponent<Arm_Obj>().gc = this.GetComponent<GameController>();
        StopCoroutine("makeArm");
        makeArms();
    }
    public void cleanedPlate()
    {
        Debug.Log("Plate?");
        plates++;
        dirtyPlates--;
    }
    public void cleanedCup()
    {
        cups++;
        dirtyCups--;
    }
    public int missedCustomer()
    {
        return strikes;
    }
    public void hitBottom()
    {
        strikes++;
        if (strikes == 3)
        {

        }
    }

}
