using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
namespace ProjectSurvivor
{
    public class ProjectSurvivorController : MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture()
        {
            return ProjectSurvivor.Instance;
        }
    }

}
