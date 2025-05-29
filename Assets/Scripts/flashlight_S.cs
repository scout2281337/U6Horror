using UnityEngine;

public class flashlight_S : MonoBehaviour
{
    [SerializeField] private Transform FLDirection; // player or playerCamera
    [SerializeField] private float speed = 1f;

    private Light _light;
    private bool _enabled = false;
    public float FLcharge = 30f;

    void Start()
    {
        _light = GetComponent<Light>();    
    }

    // Update is called once per frame
    void Update()
    {
        FLTransform();
        FlashlightLogic();   
    }
    
    void FLTransform() 
    {
        transform.position = FLDirection.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, FLDirection.rotation, speed * Time.deltaTime);
    }

    void FlashlightLogic() 
    {
        if (_enabled) 
        {
            FLcharge -= Time.deltaTime;
        }
        if (FLcharge <= 0) 
        {
            TurnOff();
        }
        
        if (!_enabled && Input.GetKeyDown(KeyCode.Mouse0) && FLcharge > 0)
        {
            TurnOn();
        }
        else if (_enabled && Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            TurnOff();
        }
    }

    void TurnOn() 
    {
        _light.intensity = 3f;
        _enabled = true;
    }
    void TurnOff() 
    {
        _light.intensity = 0f;
        _enabled = false;
    }
}
