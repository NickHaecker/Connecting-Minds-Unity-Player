using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockItemActivator : MonoBehaviour
{
    [SerializeField] private List<ItemData> items;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            foreach (ItemData item in items)
            {
                ItemObject itemData = item.GetData<ItemObject>();
                SendEvent sendEvent = new SendEvent("UNLOCK_ITEM");
                sendEvent.AddData("Item", itemData);


                NetworkSingleton.Instance.GetNetworkController().GetWebSocket().Send(sendEvent.ToJson());
            }
            this.gameObject.SetActive(false);
        }
    }
}
