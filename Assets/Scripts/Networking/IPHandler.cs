using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Networking
{
    class IPHandler : MonoBehaviour
    {
        [SerializeField] private Text first;
        [SerializeField] private Text second;
        [SerializeField] private Text third;
        [SerializeField] private Text fourth;


        public string GetIPFromInput()
        {
            string address = first.text + "." + second.text + "." + third.text + "." + fourth.text;
            return address;
        }
    }
}
