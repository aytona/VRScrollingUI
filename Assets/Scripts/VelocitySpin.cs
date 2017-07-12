using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VelocitySpin : MonoBehaviour {

    private Vector3 m_InitCast;
    private Vector3 m_FinalCast;

    private Rigidbody m_rb;

    private void Start() {
        m_rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            m_InitCast = Input.mousePosition;
        } else if (Input.GetButtonUp("Fire1")) {
            m_FinalCast = Input.mousePosition;
            Rotate(CalculateForce(m_InitCast, m_FinalCast));
        }
    }

    // Calculates the force applied to the rotation using the initial vector and the final vector
    private Vector3 CalculateForce(Vector3 init, Vector3 final) {
        return 10*(init - final);
    }

    // Rotates the actual UI using force
    private void Rotate(Vector3 force) {
        m_rb.AddTorque(0, -force.y, 0);
    }
}
