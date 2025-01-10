using UnityEngine;
using Oculus.Interaction;

public class AutoGrabDrumstick : MonoBehaviour
{
    [SerializeField]
    private GameObject hand; // Objeto que representa a m�o no Unity (rastreador ou controlador)

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
        // Move o DrumstickA para a m�o e configura como filho dela
        transform.SetParent(hand.transform);
        transform.localPosition = Vector3.zero; // Ajuste a posi��o conforme necess�rio
        transform.localRotation = Quaternion.identity;

        // Desabilitar f�sica para evitar movimenta��o inesperada
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        Debug.Log("Drumstick attached to hand at start.");
    }
}
