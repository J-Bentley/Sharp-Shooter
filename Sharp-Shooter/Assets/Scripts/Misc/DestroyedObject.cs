using System.Collections;
using UnityEngine;

public class DestroyedObject : MonoBehaviour {

    // Attached to parent of peices of destroyed crate and cracked enemy

    [SerializeField] float destroyPeicesTimer;

    void Start()
    {
        StartCoroutine(DestroyPrefabPeices());
    }

    IEnumerator DestroyPrefabPeices()
    {
        yield return new WaitForSeconds(destroyPeicesTimer);
        Destroy(this.gameObject);
    }

}