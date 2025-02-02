﻿using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UI.Core
{
    public class ComponentPoolFactory : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        private GameObject _prefab;
        [SerializeField] private int _count;
        [SerializeField] private Transform _content;
        [SerializeField] private Transform _poolStorage;

        private readonly HashSet<GameObject> _instances = new();
        private Queue<GameObject> _pool = new();

        public int CountInstances => _instances.Count;

        public void Initialize(GameObject prefab)
        {
            _prefab = prefab;
        }
        
        public T Get<T>() where T : Component
        {
            return Get<T>(_instances.Count);
        }

        public T Get<T>(int sublingIndex) where T : Component
        {
            bool isNewInstance = false;
            if (_pool.Count == 0)
            {
                GameObject result = _diContainer.InstantiatePrefab(_prefab);

                if (null == result)
                    return null;

                _pool.Enqueue(result);
                isNewInstance = true;
            }

            T resultComponent = _pool.Dequeue().GetComponent<T>();
            if (null == resultComponent)
            {
                return resultComponent;
            }

            var go = resultComponent.gameObject;
            var t = resultComponent.transform;
            if (isNewInstance || (_poolStorage != null && _poolStorage != _content))
            {
                t.SetParent(_content, false);
            }

            _instances.Add(go);

            if (!go.activeSelf)
            {
                go.SetActive(true);
            }

            if (t.GetSiblingIndex() != sublingIndex)
            {
                t.SetSiblingIndex(sublingIndex);
            }

            return resultComponent;
        }

        public void Release<T>(T component) where T : Component
        {
            var go = component.gameObject;
            if (_instances.Contains(go))
            {
                go.SetActive(false);
                if (_poolStorage)
                {
                    go.transform.SetParent(_poolStorage, false);
                }
                _pool.Enqueue(go);
                _instances.Remove(go);
            }
        }

        public void ReleaseAllInstances()
        {
            foreach (GameObject instance in _instances)
            {
                instance.SetActive(false);
                if (_poolStorage)
                {
                    instance.transform.SetParent(_poolStorage, false);
                }
                _pool.Enqueue(instance);
            }
            _instances.Clear();
        }

        public void Dispose()
        {
            ReleaseAllInstances();

            foreach (GameObject gameObject in _pool)
            {
                GameObject.Destroy(gameObject);
            }
            _pool.Clear();
        }
    }
}
