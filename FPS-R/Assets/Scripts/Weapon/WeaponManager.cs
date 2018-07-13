using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    //Esto iria en el FPSCONTROLLER, y una vez que esta como script, le tirariamos las armas (en este caso FPS-AK47)

    [SerializeField] private GameObject[] weapons;
    [SerializeField] private float switchDelay = 1f;

    private int index;
    private bool isSwitching;

    private void Start()
    {
        InitializeWeapons();
    }

    private void InitializeWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);            //Desactiva todas las armas
        }
        weapons[0].SetActive(true);                 //Selecciona si o si la primera
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && !isSwitching)
        {
            index++;

            if (index >= weapons.Length)
            {
                index = 0;
            }
            StartCoroutine(SwitchAfterDelay(index));
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && !isSwitching)
        {
            index--;

            if (index < 0)
            {
                index = weapons.Length - 1;
            }
            StartCoroutine(SwitchAfterDelay(index));
        }
    }

    private IEnumerator SwitchAfterDelay(int newIndex)
    {
        isSwitching = true;
        yield return new WaitForSeconds(switchDelay);
        isSwitching = false;
        SwitchWeapons(newIndex);
    }

    private void SwitchWeapons(int newIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(false);            //Desactiva todas las armas
        }
        weapons[newIndex].SetActive(true);          //Selecciona la que elegimos, que esta representado por el valor newIndex
    }
}
