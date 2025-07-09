using UnityEngine;

public class deneme : MonoBehaviour
{
    public GameObject Player; // Inspector'dan atanabilir

    private void Start()
    {
        Destroy(Player);
    }
}
