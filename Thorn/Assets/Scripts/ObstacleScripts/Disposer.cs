using UnityEngine;
using System.Collections;

public class Disposer : MonoBehaviour {

    public int DisposeTime;

    void Start()
    {
        StartCoroutine(WaitForDispose());
    }

    IEnumerator WaitForDispose()
    {
        yield return new WaitForSeconds(DisposeTime);

        Destroy(this.gameObject);
    }
}
