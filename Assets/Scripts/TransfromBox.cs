using UnityEngine;

public class TransfromBox : MonoBehaviour
{
    //public GameObject gj;
    [SerializeField] private float speed;
    Vector3 _direction;
    
    void Start()
    {
        //transform.rotation = Quaternion.Euler(45, 45, 45);    
        //float AngleGO = Quaternion.Angle(transform.rotation, gj.transform.rotation);
        //Debug.Log(AngleGO);
        _direction = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo(_direction);           
    }
    void RotateTo(Quaternion rotation) 
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 1 * Time.deltaTime);
    }
    void MoveTo(Vector3 Direction) 
    {
        transform.position += Direction * Time.deltaTime * speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("коллизия сработала");
        if (collision.gameObject.tag == "wall") 
        {
            Vector3 normal = collision.contacts[0].normal;
            
            _direction = Vector3.Reflect(_direction, normal);
            _direction.y = 0;
            _direction.Normalize();
        }
    }
}
