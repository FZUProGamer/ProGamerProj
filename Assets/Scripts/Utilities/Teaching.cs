using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teaching : MonoBehaviour
{
    [SerializeField]private GameObject WASD;
    [SerializeField]private GameObject E;

    WaitForSeconds wait = new WaitForSeconds(5f);

    private void Start()
    {
        StartCoroutine(changeImage());
    }

    IEnumerator changeImage()
    {
        yield return wait;

        E.SetActive(true);
        WASD.SetActive(false);
    }
}
