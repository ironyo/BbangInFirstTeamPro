using TMPro;
using UnityEngine;
public class Customer : MonoBehaviour
{ public GuestType guestType;
    public string customerName;
    public Color typeColor;
    public TextMeshPro nameText;
    public void Init(GuestType type, string name)
    { 
        guestType = type;
        customerName = name;
        switch (guestType) {
            case GuestType.None:
                break;
            case GuestType.customer:
                typeColor = Color.black;
                break;
            case GuestType.ObnoxiousCustomer:
                typeColor = Color.red;
                break;
            case GuestType.Rich:
                typeColor = Color.yellow;
                break;
            case GuestType.Celebrity:
                typeColor = Color.white;
                break;
            case GuestType.Alien:
                typeColor = Color.green;
                break;
        }
        if (nameText != null) 
        {
            nameText.text = customerName;
            nameText.color = typeColor;
        }
    }
}