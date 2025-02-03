using _project.Scripts.FishingRod;
using _project.Scripts.Services;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Zenject;

namespace _project.Scripts.Installers.Mono
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private ActionBasedController leftController;
        [SerializeField] private ActionBasedController rightController;
        [SerializeField] private XRGrabInteractable grabInteractable;
        private XRIDefaultInputActions _vrInput;
        
        public override void InstallBindings()
        {
            InitVRInput();
            
            BindVRInput();
            BindVRReeling();
            BindVRSwipe();
        }
        private void InitVRInput()
        {
            _vrInput = new();
        }
        private void BindVRInput()
        {
            Container
                .Bind<XRIDefaultInputActions>()
                .FromInstance(_vrInput)
                .AsSingle();
        }
        private void BindVRSwipe()
        {
            Container
                .Bind<VRSwipe>()
                .AsSingle()
                .WithArguments(_vrInput, leftController, rightController);
        }
        private void BindVRReeling()
        {
            Container
                .Bind<VRReeling>()
                .AsSingle()
                .WithArguments(grabInteractable, 2f);
        }
    }
}