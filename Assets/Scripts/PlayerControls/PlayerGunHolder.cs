using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGunHolder : MonoBehaviour
{
    [SerializeField] List<GameObject> myGuns = new List<GameObject>();
    GameObject _currentGun;
    int currentGunIndex = 0;

    private void Start()
    {
        if (myGuns.Count > 0)
        {
            SetCurrentGun(0);
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed && _currentGun != null)
        {
            //_currentGun.GetComponent<GunsBase>().CallShootFunc();
        }
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        if (context.performed && myGuns.Count > 0)
        {
            float scrollValue = context.ReadValue<Vector2>().y;

            if (scrollValue > 0)
            {
                currentGunIndex = (currentGunIndex - 1 + myGuns.Count) % myGuns.Count;
            }
            else if (scrollValue < 0)
            {
                currentGunIndex = (currentGunIndex + 1) % myGuns.Count;
            }

            SetCurrentGun(currentGunIndex);
        }
    }

    private void SetCurrentGun(int index)
    {
        if (_currentGun != null)
        {
            _currentGun.SetActive(false);
        }

        _currentGun = myGuns[index];
        _currentGun.SetActive(true);
    }
}
