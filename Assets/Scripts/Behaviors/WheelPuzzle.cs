using System.Collections;
using Unity.Collections;
using UnityEngine;

public class WheelPuzzle : MonoBehaviour
{
    [SerializeField] Transform[] shipPositions;
    [SerializeField] Color[] lightColors;
    [SerializeField] GameObject[] ghosts;
    [SerializeField] GameObject[] islandObjects;
    [SerializeField] Light areaLight;
    [SerializeField] GameObject ship;
    int currentShipPos = 0;
    float shipSpeed = 2f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (areaLight.color != lightColors[currentShipPos]) areaLight.color = Color.Lerp(areaLight.color, lightColors[currentShipPos], Time.deltaTime * shipSpeed);

        if (ship.transform.position != shipPositions[currentShipPos].position)
        {
            ship.transform.position = Vector3.Lerp(ship.transform.position, shipPositions[currentShipPos].position, Time.deltaTime * shipSpeed);
        }

    }

    public void MoveRight()
    {
        if (currentShipPos == shipPositions.Length) return;
        if (currentShipPos == 3 && GameFlags.GetFlag("maritime_chest_unlocked") == false) return;
        if(currentShipPos + 1 >= shipPositions.Length) return;
        currentShipPos++;
        UnloadAllObjects();
        LoadObjects();
        ghosts[currentShipPos].SetActive(true);
        if (currentShipPos == 4)
        {
            GameFlags.SetFlag("maritime_puzzle_solved", true);
        }
        StartCoroutine(RotateOverTime(0.5f, 180));
    }

    public void MoveLeft()
    {
        if (currentShipPos == 0) return;
        if (currentShipPos + 1 <= 0) return;
        currentShipPos--;
        UnloadAllObjects();
        LoadObjects();
        ghosts[currentShipPos].SetActive(true);
        StartCoroutine(RotateOverTime(0.5f, -180));
    }

    void UnloadAllObjects()
    {
        foreach (GameObject g in ghosts)
        {
            g.SetActive(false);
        }

        foreach (GameObject g in islandObjects)
        {
            g.SetActive(false);
        }

    }

    void LoadObjects()
    {
        islandObjects[currentShipPos].SetActive(true);
    }

    private IEnumerator RotateOverTime(float duration, float degrees)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(degrees, 0f, 0f);

        float time = 0f;
        while (time < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }
}
