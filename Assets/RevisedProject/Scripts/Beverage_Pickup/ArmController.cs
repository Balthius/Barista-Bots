using UnityEngine;

public class ArmController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int reverseAtY = -1000;
    [SerializeField] float yMovement, reverseMovement;
    private bool hasCollided = false;

    public bool hasObj = false, hasCup = false;
    public bool hasHit;
    private bool emptyHanded = true;
    private bool isReturning;

    // Update is called once per frame
    private void Start()
    {
        hasHit = false;
        reverseMovement = (yMovement * -1) * 1.3f;
    }

    void Update()
    {
        if (emptyHanded)
        {
            transform.Translate(new Vector2(0, yMovement));
        }
        else if (!emptyHanded)
        {
            transform.Translate(new Vector2(0, reverseMovement));
        }

        if (!isReturning && !hasHit && transform.position.y <= reverseAtY)
        {
            Debug.Log("Reverse");
            isReturning = true;
            hasHit = true;
            emptyHanded = false;
            // errant hands keep getting past the wall when using trigger checks
            ObjectGrabbedCheck(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isReturning && !hasCollided)
        {
            hasCollided = true;
            CupController otherCup = other.gameObject.GetComponent<CupController>();
            if (otherCup != null && otherCup.combined && !hasObj)
            {
                ObjectGrabbedCheck(true);
                
                other.transform.parent = this.gameObject.transform;
                other.GetComponent<CupController>().OnMouseUp();

                Destroy(other.gameObject.GetComponent<BoxCollider2D>());
            }
            else
            {
                emptyHanded = true;
            }
        }
    }

    public void ObjectGrabbedCheck(bool grabbedCup)
    {
        emptyHanded = false;
        if (grabbedCup == false)
        {
            GetComponent<Animator>().SetTrigger("GrabCutlery");
            hasObj = true;
            hasCup = false;
        }
        else if (grabbedCup == true)
        {
            GetComponent<Animator>().SetTrigger("GrabCup");
            hasObj = true;
            hasCup = true;
        }
    }

}
