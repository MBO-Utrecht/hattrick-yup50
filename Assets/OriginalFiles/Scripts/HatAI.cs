using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatAI : MonoBehaviour
{
    private float moveSpeed = 20f;          
    private float dangerHeight = 5f;       
    private float safeDistance = 3f;       
    private float ballDetectionRange = 15f; 

    private GameObject targetBall;        
    private GameObject targetBomb;       

    void Update()
    {
        // Zoek naar een bom die de hoed moet vermijden
        FindBomb();
        
        // Zoek naar een bal om te vangen
        FindBall();

        // Prioriteit: eerst bommen vermijden, dan naar de bal bewegen
        if (targetBomb != null)
        {
            AvoidBomb();
        }
        else if (targetBall != null)
        {
            MoveTowards(targetBall.transform.position);
        }
    }

    // Functie om een bal te vinden die binnen het detectiebereik is
    void FindBall()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        targetBall = null; // Reset de huidige doelbal

        foreach (GameObject ball in balls)
        {
            // Zoek alleen naar ballen die binnen het detectiebereik vallen
            if (Vector3.Distance(transform.position, ball.transform.position) <= ballDetectionRange)
            {
                targetBall = ball;
                break; // Stop zodra een bal is gevonden
            }
        }
    }

    // Functie om een bom te vinden die vermeden moet worden (lager dan dangerHeight)
    void FindBomb()
    {
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
        targetBomb = null; // Reset de huidige doelbom

        foreach (GameObject bomb in bombs)
        {
            // Als de bom lager is dan dangerHeight en binnen safeDistance van de hoed, markeer als target
            if (bomb.transform.position.y < dangerHeight && Mathf.Abs(bomb.transform.position.x - transform.position.x) <= safeDistance)
            {
                targetBomb = bomb;
                break; // Stop zodra een gevaarlijke bom is gevonden
            }
        }
    }

    // Functie om van de bom weg te bewegen als deze gevaarlijk is
    void AvoidBomb()
    {
        if (targetBomb != null)
        {
            // Bepaal de richting waarin de hoed moet bewegen (weg van de bom)
            float moveDirection = targetBomb.transform.position.x > transform.position.x ? -1 : 1;
            Move(moveDirection);
        }
    }

    // Functie om naar een bepaalde positie te bewegen (bal vangen)
    void MoveTowards(Vector3 targetPosition)
    {
        float moveDirection = targetPosition.x > transform.position.x ? 1 : -1;
        Move(moveDirection);
    }

    // Algemene functie om de hoed te bewegen op de x-as
    void Move(float direction)
    {
        // Beweeg de hoed alleen op de x-as en houd de y-positie vast
        Vector3 newPosition = transform.position + new Vector3(direction, 0, 0) * moveSpeed * Time.deltaTime;
        transform.position = new Vector3(newPosition.x, transform.position.y, transform.position.z);
    }
}
