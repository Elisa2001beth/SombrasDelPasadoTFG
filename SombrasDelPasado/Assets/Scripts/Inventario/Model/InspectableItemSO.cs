using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class InspectableItemSO : ItemSO, IItemAction
    {
        public string ActionName => "Ver";

        [field: SerializeField]
        public AudioClip actionSFX { get; private set; }

        public UnityEvent<InspectableItemSO> onInspect = new UnityEvent<InspectableItemSO>(); // Evento para inspeccionar el objeto

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            onInspect.Invoke(this); // Invocar el evento de inspección
            return true;
        }
    }
}
