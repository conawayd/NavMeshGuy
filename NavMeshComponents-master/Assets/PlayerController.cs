using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    private int count = 0;
    private int countLives = 3;
    public Text scoreText;
    public Text livesText;


    void Start()
    {
        agent.updateRotation = false;
        scoreText.text = "Score: 0";
        livesText.text = "Lives: 3";
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        } else
        {
            character.Move(Vector3.zero, false, false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Star")
        {
            other.gameObject.SetActive(false);
            count += 10;
            scoreText.text = "Score: " + count;
        }

        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
            countLives -= 1;
            livesText.text = "Lives: " + countLives;
        }
    }
}
