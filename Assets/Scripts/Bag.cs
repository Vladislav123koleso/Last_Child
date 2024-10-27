using UnityEngine;


    public class Bag : MonoBehaviour
    {
        private int _seedsCount;
        public int SeedsCount => _seedsCount;

        public void AddSeed()
        {
            _seedsCount++;
        }
    }

