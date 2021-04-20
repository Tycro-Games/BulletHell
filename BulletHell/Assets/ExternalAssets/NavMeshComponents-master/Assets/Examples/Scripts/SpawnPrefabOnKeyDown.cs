using UnityEngine;
using UnityEngine.Events;

public class SpawnPrefabOnKeyDown : MonoBehaviour
{
    public GameObject m_Prefab;
    public KeyCode m_KeyCode;
    public float range = 1.5f;
    [SerializeField]
    private UnityEvent OnSpawn=null;
    private void Update()
    {
        if (Input.GetKeyDown(m_KeyCode) && m_Prefab != null)
        {
            OnSpawn?.Invoke();
            Instantiate(m_Prefab, (Vector2)transform.position + Random.insideUnitCircle * range, Quaternion.identity);
        }
    }
}