using Assets.Scripts.Gui;
using UnityEngine;

namespace Assets.Scripts
{
    public class LandingPad : MonoBehaviour
    {
        public Light AcitveLight;
        public Light NormalLight;
        public Material ActiveMaterial;
        private InGame _gameUi;
        private bool _beenActivated;

        private void Start()
        {
            _gameUi = GameObject.FindGameObjectWithTag("GUI").GetComponent<InGame>();
        }

        private void Update()
        {
        }

        public void ShipLanded()
        {
            if (_beenActivated)
                return;

            _beenActivated = true;
            AcitveLight.intensity = 1.5f;
            NormalLight.intensity = 0;
            var meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = ActiveMaterial;

            _gameUi.LandingPadActivated();
            var audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }
    }
}