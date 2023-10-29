using System.Collections;
using System.Collections.Generic;
using SteamAudio;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Ray = UnityEngine.Ray;
using Vector3 = UnityEngine.Vector3;

public class PlayerPawn : MonoBehaviour
{
    
    // Управление
    private NavMeshAgent agent;
    private bool isMoving = false;
    private Vector3 OldPosition;
    private bool Moveble = true;
    
    // Для триггеров
    public bool inTriggerBook;
    public bool inTriggerDialogue;
    public Book book;
    public Dialogue dialogue;
    
    // Харастеристики
    public short Health = 10;
    private short dmg = 1;
    
    [SerializeField] private Slider healthFill;
    [SerializeField] private Animator Animator;
    [SerializeField] private ParticleSystem damageGet;
    [SerializeField] private AudioClip[] punch;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] public AudioClip dead;
    private short da;
    
    private void Awake()
    {
        // Инициализация
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("IncreaseHealth", 3f, 3f);
    }
    
    private void Checker()
    {
        // Проверка на движение игрока
        if (OldPosition != transform.position)
            Animator.SetBool("walk", true);
        else
            Animator.SetBool("walk", false);
        OldPosition = transform.position;
        
        // Проверка на смерть игрока
        if (Health <= 0)
            Destroy(gameObject);
    }
    
    private void PlayerMovement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Управление игрока через NavMesh
        if (Input.GetMouseButtonDown(1) && Moveble) // по правой кнопке все
        {
            if (Physics.Raycast(ray, out hit) && !isMoving)
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    private void ReadBook()
    {
        // Чтение Книги
        if (!inTriggerBook && book)
        {
            // Если человек вышел с зоны тригера закрывает книгу принудительно
            book.ExtremeClose();
            book = null;
        }
        if (Input.GetMouseButton(0) && inTriggerBook)
        {
            // Открывает или закрывает книгу
            book.Switch();
        }
    }
    
    private void Dialogue()
    {
        if (dialogue && inTriggerDialogue)
        {
            dialogue.Use();
            Moveble = dialogue.used;
            if (Input.GetMouseButton(0))
            {
                dialogue.DialogueUI.SetActive(false);
                dialogue.used = true;
                Moveble = dialogue.used;
                Destroy(dialogue.gameObject);
            }
        }
    }
    
    private void Attack()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0) && !book && !dialogue)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Distance(hit.collider.transform.position, transform.position) >= 2f)
                {
                    return;
                }

                if (hit.collider.CompareTag("Enemy"))
                {
                    ParticleSystem damageInstance = Instantiate(damageGet, new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y + 0.693f, hit.collider.transform.position.z), Quaternion.identity);
                    damageInstance.transform.SetParent(hit.collider.transform);
                    switch (da)
                    {
                        case 1:
                            da = 0;
                            break;
                        case 0:
                            da = 1;
                            break;
                    }
                    _audioSource.PlayOneShot(punch[da]);
                    if (hit.collider.GetComponent<EnemyBasic>().HPEnemy <= 0)
                    {
                        _audioSource.PlayOneShot(dead);
                        Destroy(hit.collider.gameObject);
                    }
                    hit.collider.GetComponent<EnemyBasic>().HPEnemy -= dmg;
                }
            }
        }
    }
    
    private void IncreaseHealth()
    {
        if (Health <= 10)
        {
            // Add any condition or logic here before increasing health
            Health += 1;
        }
    }
    

    private void Update()
    {
        PlayerMovement();
        Checker();
        ReadBook();
        Dialogue();
        Attack();
        healthFill.value = Health;
    }
}
