using System.Collections.Generic;
using UnityEngine;

namespace Attack
{
    public class ShovelsController : MonoBehaviour
    {
        [SerializeField] private Transform shovelPrefabTransform;
        
        private List<Transform> _shovels;
        
        private int _shovelsCount;
        private const float RotationSpeed = 150.0f;

        public static ShovelsController Instance;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            
            _shovels = new List<Transform>();
        }

        private void Update()
        {
            transform.Rotate(0, 0,-RotationSpeed * Time.deltaTime);
        }

        public void AddOneShovel()
        {
            _shovelsCount += 1;
            _shovels.Add(Instantiate(shovelPrefabTransform, transform));
            UpdateShovelsPlacement();
        }

        private void UpdateShovelsPlacement()
        {
            float angleDiff = 360.0f / _shovelsCount;
            
            for (int i = 0; i < _shovelsCount; i++)
            {
                _shovels[i].localEulerAngles = new Vector3(0, 0, i * angleDiff);
            }
        }
    }
}
