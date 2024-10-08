using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public WeaponScriptableObject weaponData;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, weaponData.Duration);
    }
    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirX = direction.x;  
        float dirY = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirX < 0 && dirY == 0) // left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        if (dirX == 0 && dirY > 0) // up
        {
            scale.x = scale.x * -1;
        }
        if (dirX == 0 && dirY < 0) // down
        {
            scale.y = scale.y * -1;
        }
        if (dirX > 0 && dirY > 0) // up right
        {
            rotation.z = 0f;
        }
        if (dirX < 0 && dirY > 0) // up left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        }
        if (dirX > 0 && dirY < 0) // down right
        {
            rotation.z = -90f;
        }
        if (dirX < 0 && dirY < 0) // down left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 0f;
        }
        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
