using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monotheist.Human
{
    public class HumanInstaller : MonoBehaviour
    {
        [SerializeField] private HumanConfig _humanConfig;
        [SerializeField] private HumanManager _humanManager;
        [SerializeField] private HumanStatsSpy _humanStatsSpy;


        void Start()
        {
            _humanManager.Install(_humanConfig, _humanStatsSpy);
        }

        void Update()
        {

        }
    }
}
