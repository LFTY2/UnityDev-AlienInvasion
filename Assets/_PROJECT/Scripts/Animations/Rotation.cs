using System;
using DG.Tweening;
using UnityEngine;

namespace _PROJECT.Scripts.Animations
{
    public class Rotation : MonoBehaviour
    {
        private Tween _tween;
        private void OnEnable()
        {
            _tween = transform.DORotate(Vector3.forward * 360, 1f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
        }

        private void OnDisable()
        {
            _tween.Kill();
        } 
    }
}