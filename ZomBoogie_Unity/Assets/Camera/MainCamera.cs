using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Camera     mCamera;
    [SerializeField] private Transform  mTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(mTarget.position.x, mTarget.position.y, -1.0f);
    }
}
