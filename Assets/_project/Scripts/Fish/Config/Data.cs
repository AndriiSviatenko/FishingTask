using System;
using UnityEngine;

namespace _project.Scripts.Fish.Config
{
    [Serializable]
    public class Data
    {
        public string Name;
        public Type Type;
        public Sprite Icon;
        [Range(3,100)] public float Speed;
        [Range(0,100)] public int Price;
    }
}