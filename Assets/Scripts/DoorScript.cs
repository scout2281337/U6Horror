using UnityEngine;
using DG.Tweening;

public class DoorScript : MonoBehaviour , I_Interactable
{
    private bool isOpened = false;
    private bool isAbleToOpen = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isAbleToOpen) 
        {
            Interact();   
        }    
    }

    public void Interact()
    {
        if (!isOpened)
        {
            transform.DORotate(new Vector3(0, 90, 0), 1f);
            isOpened = true;
        }
        else 
        {
            transform.DORotate(new Vector3(0, 0, 0), 1f);
            isOpened= false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            isAbleToOpen = true;    
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isAbleToOpen = false;
        }
    }

}
