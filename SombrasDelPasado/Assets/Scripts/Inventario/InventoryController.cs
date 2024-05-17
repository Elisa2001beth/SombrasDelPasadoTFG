

using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DialogueEditor;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private Image itemFlashback; // Agrega una referencia al componente Image que mostrará la imagen del ítem en el panel de inspección



        [SerializeField]
        private UIInventoryPage inventoryUI;

        [SerializeField]
        private InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        [SerializeField]
        private AudioClip dropClip;

        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private GameObject inspectPanel; // Referencia al panel de inspección

        [SerializeField]
        private GameObject fourthObject;

        [SerializeField]
        private string sceneToLoad;
        private HashSet<string> inspectedItems = new HashSet<string>();
        private bool fourthItemUnlocked = false;

        public NPCConversation conversation; // Referencia a la conversación que deseas iniciar

        private bool justViewedFourthFlashback = false;

        [SerializeField]
        private ItemSO fourthFlashbackItem;

        private void Start()
        {
            fourthObject.SetActive(false);
            PrepareUI();
            PrepareInventoryData();
        }

        public void StartDialogueAfterFourthFlashback()
        {
            
            ConversationManager.Instance.StartConversation(conversation);
        }



        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage,
                    item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }


        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
            }

            InspectableItemSO inspectableItem = inventoryItem.item as InspectableItemSO;
            if (inspectableItem != null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(inspectableItem.ActionName, () => InspectItem(inspectableItem));
            }
        }


        private void DropItem(int itemIndex, int quantity)
        {
            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();
            audioSource.PlayOneShot(dropClip);
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState);
                audioSource.PlayOneShot(itemAction.actionSFX);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    inventoryUI.ResetSelection();
            }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage,
                item.name, description);
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName} " +
                    $": {inventoryItem.itemState[i].value} / " +
                    $"{inventoryItem.item.DefaultParametersList[i].value}");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        private void InspectItem(InspectableItemSO item)
        {
            if (inspectPanel != null)
            {
                inventoryUI.Hide(); // Ocultar el panel de inventario
                inspectPanel.SetActive(true); // Activar el panel de inspección
                itemFlashback.sprite = item.specificImage;

                if (!inspectedItems.Contains(item.name))
                {
                    inspectedItems.Add(item.name);

                    if (inspectedItems.Count >= 3 && !fourthItemUnlocked)
                    {
                        fourthObject.SetActive(true);
                        fourthItemUnlocked = true;
                    }
                }

                // Verificar si es el cuarto flashback específico
                if (fourthItemUnlocked && item == fourthFlashbackItem)
                {
                    justViewedFourthFlashback = true;
                }
                else
                {
                    justViewedFourthFlashback = false;
                }
            }
        }


        // Método para cerrar el panel de inspección y reactivar el panel de inventario
        public void CloseInspectPanel()
        {
            if (inspectPanel != null)
            {
                inspectPanel.SetActive(false); // Desactivar el panel de inspección
                inventoryUI.Show(); // Mostrar el panel de inventario

                // Verificar si el cuarto flashback fue el último que se vio y se ha regresado al inventario
                if (justViewedFourthFlashback)
                {
                    // Iniciar el diálogo
                    StartDialogueAfterFourthFlashback();
                    justViewedFourthFlashback = false; // Resetear la bandera
                }
            }
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key,
                            item.Value.item.ItemImage,
                            item.Value.quantity);
                    }
                }
                else
                {
                    inventoryUI.Hide();
                }
            }
        }
    }
}


