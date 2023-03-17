using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float jumpHeight;
    private float stepOffset;

    [Header("Physics Modifiers")]
    [SerializeField] [Tooltip("Gravity Multipliyer")] private float gravMult;

    [Header("Ground checks")]
    [SerializeField] private Transform grdchk;
    [SerializeField] [Tooltip("Radius")] private float grdchkr;
    [SerializeField] private LayerMask grdchkLayers;
    
    // References
    private CharacterController cc;

    void Awake() {
        cc = GetComponent<CharacterController>();
        this.stepOffset = cc.stepOffset;
    }

    bool isGrounded = false;
    float yvel;
    void Update() {
        isGrounded = Physics.CheckSphere(grdchk.position, grdchkr, grdchkLayers);

        // Movement
        // ---------------

        float speedMult = Input.GetAxis("Sprint") == 1 ? sprintSpeed : walkSpeed;

        float movex = Input.GetAxis("Horizontal");
        float movez = Input.GetAxis("Vertical");

        // Move: Controlled
        ///* CollisionFlags movcol = */ cc.Move(speedMult * Time.deltaTime * movevel);

        if (isGrounded) {
            if (yvel > 0.0f) yvel = 0.0f; // Reset vertical velocity

            // Jump
            if (Input.GetAxis("Jump") == 1) {
                yvel = Mathf.Sqrt(jumpHeight * gravMult * -Physics.gravity.y);
            }

            cc.stepOffset = stepOffset;
        } else {
            // Accelerate falling
            yvel += gravMult * Physics.gravity.y * Time.deltaTime;
            cc.stepOffset = 0;
        }

        Vector3 movevel = (transform.right * movex + transform.forward * movez) * speedMult + transform.up * yvel;

        // Move: Gravity induced fall
        /* CollisionFlags movcol = */ cc.Move(movevel * Time.deltaTime);
    }
}
