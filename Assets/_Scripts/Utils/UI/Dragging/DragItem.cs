using System;
using UnityEngine;
using UnityEngine.EventSystems;
namespace HStrong.Core.UI.Dragging{
    public class DragItem<T> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
        where T : class
    {
        Vector3 startPosition;
        Transform originalParent;
        IDragSource<T> source;

        Canvas parentCanvas;
        private void Awake() {
            parentCanvas = GetComponentInParent<Canvas>();
            source = GetComponentInParent<IDragSource<T>>();
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            startPosition = transform.position;
            originalParent = transform.parent;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            transform.SetParent(parentCanvas.transform,true);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            transform.position = startPosition;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            transform.SetParent(originalParent,true);

            IDragDestination<T> container;
            if(!EventSystem.current.IsPointerOverGameObject()){
                container = parentCanvas.GetComponent<IDragDestination<T>>();
            }else{
                container = GetContainer(eventData);
                
            }

            if(container != null){
                DropItemIntoContainer(container);
            }
        }

        private void DropItemIntoContainer(IDragDestination<T> destination)
        {
            if( object.ReferenceEquals(destination,source)) return;
            var destinationContainer = destination as IDragContainer<T>;
            var sourceContainer = source as IDragContainer<T>;

            if(destinationContainer == null || sourceContainer == null ||
               destinationContainer.GetItem() == null || 
               object.ReferenceEquals(destinationContainer.GetItem(), sourceContainer.GetItem()))
            {
                AttempSimpleTransfer(destination);
                
                return;
            }
            AttemptSwap(destinationContainer, sourceContainer);
            
        }

        private void AttemptSwap(IDragContainer<T> destination, IDragContainer<T> _source)
        {
            int removedSourceNumber = _source.GetNumber();
            var removedSourceItem = _source.GetItem();
            int removedDestinationNumber = destination.GetNumber();
            var removedDestinationItem = destination.GetItem();

            _source.RemoveItems(removedSourceNumber);
            destination.RemoveItems(removedDestinationNumber);

            int sourceTakeBackNumber = CalculateTakeBack(removedSourceItem, removedSourceNumber, _source, destination);
            int destinationTakeBackNumber = CalculateTakeBack(removedDestinationItem, removedDestinationNumber, destination, _source);

            if(sourceTakeBackNumber > 0 ){
                _source.AddItems(removedSourceItem,sourceTakeBackNumber);
                removedSourceNumber -= sourceTakeBackNumber;
            }
            if(destinationTakeBackNumber > 0){
                destination.AddItems(removedDestinationItem,destinationTakeBackNumber);
                removedDestinationNumber -= destinationTakeBackNumber;
            }

            if(_source.MaxAcceptable(removedDestinationItem) < removedDestinationNumber ||
               destination.MaxAcceptable(removedSourceItem) < removedSourceNumber){
                destination.AddItems(removedDestinationItem, removedDestinationNumber);
                _source.AddItems(removedSourceItem, removedSourceNumber);
                return;
            }
            
            if (removedDestinationNumber > 0)
            {
                _source.AddItems(removedDestinationItem, removedDestinationNumber);
            }
            if (removedSourceNumber > 0)
            {
                destination.AddItems(removedSourceItem, removedSourceNumber);
            }
            
        }
        private int CalculateTakeBack(T removeItem, int removeNumber, IDragContainer<T> removeSource, IDragContainer<T> destination){
            int takeBacknumber = 0;
            var destinationMaxAcceptable = destination.MaxAcceptable(removeItem);
            if(destinationMaxAcceptable < removeNumber){
                takeBacknumber = removeNumber - destinationMaxAcceptable;
                var sorceTakeBackAcceptable = removeSource.MaxAcceptable(removeItem);
                if(sorceTakeBackAcceptable < takeBacknumber){
                    return 0;
                }
            }
            return takeBacknumber;
        }

        private bool AttempSimpleTransfer(IDragDestination<T> destination)
        {
            var draggingItem = source.GetItem();
            int draggingNumber = source.GetNumber();
            int acceptable = destination.MaxAcceptable(draggingItem);

            int toTransfer = Mathf.Min(acceptable,draggingNumber); 

            if(toTransfer > 0){

                source.RemoveItems(toTransfer);
                destination.AddItems(draggingItem,toTransfer);
                return false;
            }
            
            return true;
        }

        private IDragDestination<T> GetContainer(PointerEventData eventData)
        {
            if(eventData.pointerEnter)
            {
                var container = eventData.pointerEnter.GetComponentInParent<IDragDestination<T>>();
                return container;
            }
            return null;
        }
    }
}       