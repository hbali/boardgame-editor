using Model;
using Transaction;
using UnityEngine;

namespace View
{
    public abstract class BaseViewModel : MonoBehaviour
    {
        public abstract string Id { get; }

        public abstract void LoadModel();

    }
}