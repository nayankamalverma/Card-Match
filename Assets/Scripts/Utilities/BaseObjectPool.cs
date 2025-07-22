using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatch.Scripts.Utilities
{
    public class BaseObjectPool<T>
    {
        public List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();

        protected virtual T GetItem()
        {
            if (pooledItems.Count > 0)
            {
                var item = pooledItems.Find(i => !i.isUsed);
                if (item != null)
                {
                    item.isUsed = true;
                    Activate(item.item);
                    return item.item;
                }
            }

            return CreateNewPooledItem();
        }

        private T CreateNewPooledItem()
        {
            var pooledItem = new PooledItem<T>();
            pooledItem.item = CreateItem();
            pooledItem.isUsed = true;
            pooledItems.Add(pooledItem);
            return pooledItem.item;
        }

        protected virtual T CreateItem()
        {
            throw new NotImplementedException("CreateItem() must be implemented in a derived class.");
        }

        protected virtual void ReturnItem(PooledItem<T> item)
        {
            Deactivate(item.item);
            item.isUsed = false;
        }

        public void ReturnAllItems()
        {
            foreach (var item in pooledItems)
            {
                Deactivate(item.item);
                item.isUsed = false;
            }
        }

        protected virtual void Activate(T item)
        {
            // Override this in derived class if T has activation logic
        }

        protected virtual void Deactivate(T item)
        {
            // Override this in derived class if T has deactivation logic
        }
    }

    public class PooledItem<T>
    {
        public T item;
        public bool isUsed;
    }
}