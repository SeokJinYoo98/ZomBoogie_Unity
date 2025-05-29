using StateSystem.Core;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(NewBoogie))]
public class PlayerController : MonoBehaviour
{
    private IControllAble _boogie;
    private void Awake()
    {
        _boogie = GetComponent<NewBoogie>() as IControllAble;
    }
    void Update()
    {
        _boogie.Move(GetAxisRaw());
        _boogie.LookAt(MouseAxis());
    }
    Vector2 GetAxisRaw()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y);
    }
    Vector2 MouseAxis()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        return new Vector2(mouseX, mouseY);
    }
}