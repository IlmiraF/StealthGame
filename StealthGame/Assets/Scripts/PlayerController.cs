using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float runSpeed = 20f;
    public float walkSpeed = 10f;
    public float rotateSpeed = 180;

    public bool IsStealth = false;
    public static bool IsHidden = false;
    public bool IsMoving = false;
    public bool IsDead = false;

    Animator animator;
    private bool IsHidable = false;

    private Rigidbody rbody;
    private CapsuleCollider myCollider;
    private GameObject graphics;
    private Health health;

    private void Start()
    {
        graphics = transform.GetChild(0).gameObject;
        animator = graphics.GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<CapsuleCollider>();
        health = FindObjectOfType<Health>();
    }

    private void Update()
    {
        if(IsDead)
        {
            return;
        }

        CheckHide();
        if(!IsHidden)
        {
            CheckIsStealth();
            Move();
            Rotate();
        }
    }

    public void KillMe()
    {
        StartCoroutine(Kill());
    }
 
    #region Methods
    IEnumerator Kill()
    {
        IsDead = true;
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
        //Debug.Log(IsDead);

    }

    void CheckHide()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsHidable)
        {
            IsHidden = !IsHidden;
            rbody.useGravity = !IsHidden;
            myCollider.enabled = !IsHidden;
            graphics.SetActive(!IsHidden);
        }
    }

    void Move()
    {
        float vMove = Input.GetAxis("Vertical");
        
        if(IsStealth)
            transform.Translate(Vector3.forward * vMove * walkSpeed * Time.deltaTime);
        else
            transform.Translate(Vector3.forward * vMove * runSpeed * Time.deltaTime);

        animator.SetFloat("MoveSpeed", vMove);
        IsMoving = vMove != 0;
    }

    void Rotate()
    {
        float hMove = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, hMove * rotateSpeed * Time.deltaTime);
    }

    void CheckIsStealth()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            IsStealth = !IsStealth;
            animator.SetBool("IsStealth", IsStealth);
            Debug.Log(IsStealth);
        }
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hidable")
        {
            IsHidable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hidable")
        {
            IsHidable = false;
        }
    }
}
