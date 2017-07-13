using System.Collections;
using UnityEngine;

// Hides and shows the UI when transitioning scenes
public class UIToggle : MonoBehaviour {

    public float m_height;

    private bool m_toggle = false;
    private Vector3 m_initPos;

    public Renderer[] m_childRend;

    private void Start() {
        m_initPos = gameObject.transform.position;
        m_childRend = GetComponentsInChildren<Renderer>();
    }

    private void Update() {
        if (Input.GetButtonDown("Fire2")) {
            Toggle();
        }
        SetAlpha();
    }

    // First stops the toggle coroutine if its running, then run the toggle coroutine animation
    private void Toggle() {
        StopAllCoroutines();
        if (m_toggle) {
            StartCoroutine(ToggleAnim(m_initPos));
        } else {
            StartCoroutine(ToggleAnim(new Vector3(m_initPos.x, m_initPos.y - m_height, m_initPos.z)));
        }
        m_toggle = !m_toggle;
    }

    // Animates the toggle using a separate thread
    private IEnumerator ToggleAnim(Vector3 targetPos) {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.fixedDeltaTime);
        yield return new WaitForFixedUpdate();
        if (transform.position.y != targetPos.y) {
            StartCoroutine(ToggleAnim(targetPos));
        } else {
            StopAllCoroutines();
        }
    }

    private void SetAlpha() {
        foreach (Renderer child in m_childRend) {
            Color m_color = child.material.color;
            float m_alpha = 1f + (gameObject.transform.position.y - m_initPos.y) / (m_initPos.y - (m_initPos.y - m_height));
            m_color.a = m_alpha;
            child.material.color = m_color;
        }
    }
}
