using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
/// <remarks>
/// Great idea from https://www.youtube.com/watch?v=I2j6mQpCrWE
/// </remarks></remarsk>
public class DamageIndicator : MonoBehaviour
{
    TMPro.TextMeshProUGUI textMeshPro;
    Vector3 initialPosition;
    Vector3 targetPosition;
    float timer;

    public float lifetime = 2f;
    public float minDistance = 2f;
    public float maxDistance = 2f;



    // Start is called before the first frame update
    void Awake()
    {
        textMeshPro = GetComponentInChildren<TMPro.TextMeshProUGUI>();

        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        float direction = Mathf.Abs(Random.rotation.eulerAngles.z); //random value up
        initialPosition = transform.position;
        float distance = Random.Range(minDistance, maxDistance);
        targetPosition = initialPosition + (Quaternion.Euler(0, 0, direction) * new Vector3(distance / 2f, distance, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > lifetime) Destroy(gameObject);

        transform.position = Vector3.Lerp(initialPosition, targetPosition, Mathf.Sin(timer / lifetime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / lifetime));
    }

    public void SetText(string text) {
        textMeshPro.text = text;
    }
}
