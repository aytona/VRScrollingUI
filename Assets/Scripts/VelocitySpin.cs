using UnityEngine;

// Just spins an object using force/torque
[RequireComponent(typeof(Rigidbody))]
public class VelocitySpin : MonoBehaviour {

    private Vector3 m_InitCast;
    private Vector3 m_FinalCast;

    private Rigidbody m_rb;

    private string m_sceneName = "";

    private void Start() {
        m_rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            m_InitCast = Input.mousePosition;
            
            // Checks if initial fire was on a scene object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "SceneUI") {
                    m_sceneName = hit.collider.name;
                    Debug.DrawLine(transform.position, hit.transform.position, Color.red);
                }
            }
        } else if (Input.GetButtonUp("Fire1")) {
            m_FinalCast = Input.mousePosition;
            
            // Checks again if after the button click, the raycast is still hitting a scene object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider.tag == "SceneUI" && m_sceneName == hit.collider.name) {
                    hit.collider.transform.SendMessage("ChangeScene");
                    m_sceneName = "";
                }
            } else {
                m_sceneName = "";
            }
            if (m_sceneName == "") {
                Rotate(CalculateForce(m_InitCast, m_FinalCast));
            }
        }
    }

    // Calculates the force applied to the rotation using the initial vector and the final vector
    private Vector3 CalculateForce(Vector3 init, Vector3 final) {
        return 10*(init - final);
    }

    // Rotates the actual UI using force
    private void Rotate(Vector3 force) {
        m_rb.AddTorque(0, -force.x, 0);
    }
}
