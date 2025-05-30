using UnityEngine;
using Common.CharInterface;

[RequireComponent(typeof(NewBoogie))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private IControllAble _boogie;

    private void Awake()
    {
       
    }
    private void Start()
    {
        _boogie = GetComponent<NewBoogie>() as IControllAble;
    }
    void Update()
    {
        if (_boogie.IsDeath()) return;

        HandleKeyEvent();
        HandleMouseEvent();
    }
    void HandleKeyEvent()
    {
        Vector2 moveDir = GetAxisRaw();
        _boogie.Move(moveDir);
    }
    void HandleMouseEvent()
    {
        Vector2 mousPos = MousePosition();
        _boogie.LookAt(mousPos);
        if (Input.GetMouseButtonDown(0)) _boogie.Attack();
    }
 
    Vector2 GetAxisRaw()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y);
    }
    Vector2 MousePosition()
    {
        Vector3 mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePos.x, mousePos.y);
    }
}