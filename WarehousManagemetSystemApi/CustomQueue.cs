using System.Collections.Generic;

namespace WarehousManagemetSystemApi
{
    public delegate void QueueEventHandler<T, U>(T sender, U eventArgs);

    public class CustomQueue<T> where T: IEntityPrimaryProperties, IEntityAditionalProperties
    {
        public event QueueEventHandler<CustomQueue<T>, QueueEventArgs> CustomQueueEvent;
        Queue<T> _queue = null;

        public CustomQueue()
        {
            _queue = new Queue<T>();
            QueueEventArgs queueEventArgs = new QueueEventArgs{Message=$"DateTime: {DateTime.Now.ToString(Constants.DateFormat)}, Id({item.Id}), Name({item.Name}), Type({item.Type}), Quantity({item.Quantity}), has been added to the queue."};
       
            OnQueueChanged(queueEventArgs);
        }


        public int QueueLength
        {
            get { return _queue.Count; }

        }

        public void AddItem(T item){
            _queue.Enqueue(item); 
        }

        public T GetItem(){
            T item = _queue.Dequeue();
             QueueEventArgs queueEventArgs = new QueueEventArgs{Message=$"DateTime: {DateTime.Now.ToString(Constants.DateFormat)}, Id({item.Id}), Name({item.Name}), Type({item.Type}), Quantity({item.Quantity}), has been processed."};
             OnQueueChanged(queueEventArgs);
             return item;
        }

        protected virtual void OnQueueChanged(QueueEventArgs a){
            CustomQueueEvent(this, a);
        }

        public IEnumerable<T> GetEnumerator(){
            return _queue.GetEnumerator();
        }

    }

    public class QueueEventArgs:System.EventArgs
    {
        public string Message {get; set;}
    }
}