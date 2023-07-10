using TMPro;
using UnityEngine;

[RequireComponent (typeof(BoxCollider))]

public class GateController : MonoBehaviour
{
    [SerializeField] private int amount;
    [SerializeField] private GateType gateType;
    [SerializeField] private TMP_Text text;
    private BoxCollider boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        string prefix = gateType == GateType.Enhancer ? "+ " : "x ";
        text.text = prefix + amount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCrowdController player))
        {
            boxCollider.enabled = false; //Deactivate the gate.
            player.GenerateCrowd(amount , gateType);
            AudioReactor.Play(AudioReactor.lib.generateCrowdSound);
        }
    }
}
public enum GateType
{
    Enhancer,
    Multiplier
}
