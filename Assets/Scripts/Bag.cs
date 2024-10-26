using UnityEngine;


    public class Bag : MonoBehaviour
    {
        [SerializeField] private int _seedsCount;
        public int SeedsCount => _seedsCount;

        public void AddSeed()
        {
            _seedsCount++;
        }
    }

