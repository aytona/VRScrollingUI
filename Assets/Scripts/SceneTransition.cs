using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    public string m_targetScene;

    public void ChangeScene() {
        SceneManager.LoadScene(m_targetScene);
    }
}
