using UnityEngine;
using Oculus.Interaction;

public class AutoGrabDrumstick : MonoBehaviour
{
    [SerializeField]
    private GameObject hand; // Objeto que representa a mão no Unity (rastreador ou controlador)

    private Grabbable _grabbable;

    private void Start()
    {
        _grabbable = GetComponent<Grabbable>();

        if (hand != null && _grabbable != null)
        {
            AttachToHand();
        }
        else
        {
            Debug.LogWarning("Hand or Grabbable is not properly assigned!");
        }
    }

    private void AttachToHand()
    {
        // Move o DrumstickA para a mão e configura como filho dela
        transform.SetParent(hand.transform);
        transform.localPosition = Vector3.zero; // Ajuste a posição conforme necessário
        transform.localRotation = Quaternion.identity;

        // Desabilitar física para evitar movimentação inesperada
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Debug.Log("Drumstick attached to hand at start.");
    }
}
