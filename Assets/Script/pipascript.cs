using System.Collections;
using UnityEngine;

public class pipascript : MonoBehaviour
{
    [SerializeField]
    float movingspeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestroyMyself());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * movingspeed * Time.deltaTime);
    }
    IEnumerator DestroyMyself()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
