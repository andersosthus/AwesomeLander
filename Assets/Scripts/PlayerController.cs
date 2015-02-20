using Assets.Scripts.Gui;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public ParticleEmitter BottomThruster;
        public GameObject[] Explosions;
        public ParticleEmitter LeftThruster;
        public ParticleEmitter RightThruster;
        public ParticleEmitter TopThruster;
        private AudioSource _audio;
        private InGame _gameUi;
        private Rigidbody _ship;

        private void Start()
        {
            _ship = GetComponent<Rigidbody>();
            _gameUi = GameObject.FindGameObjectWithTag("GUI").GetComponent<InGame>();
            _audio = GetComponent<AudioSource>();
        }

        private void ExplodeShip()
        {
            var explosion = Random.Range(0, Explosions.Length);

            Instantiate(Explosions[explosion], transform.position, transform.rotation);
            _gameUi.Lose();
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision hitInfo)
        {
            if (hitInfo.relativeVelocity.magnitude >= 2)
                ExplodeShip();

            if (hitInfo.gameObject.tag == "LandingPad")
            {
                var landingPad = hitInfo.gameObject.GetComponentInParent<LandingPad>();
                landingPad.ShipLanded();
            }
        }

        private void Update()
        {
            // ReSharper disable CompareOfFloatsByEqualityOperator
            if (Input.GetAxis("Horizontal") > 0)
            {
                LeftThruster.emit = true;
                _ship.AddForce(10f, 0, 0, ForceMode.Force);
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                RightThruster.emit = true;
                _ship.AddForce(-10f, 0, 0, ForceMode.Force);
            }
            if (Input.GetAxis("Horizontal") == 0)
            {
                LeftThruster.emit = false;
                RightThruster.emit = false;
            }

            if (Input.GetAxis("Vertical") > 0)
            {
                BottomThruster.emit = true;
                _ship.AddForce(0, 10f, 0, ForceMode.Force);
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                TopThruster.emit = true;
                _ship.AddForce(0, -10f, 0, ForceMode.Force);
            }
            if (Input.GetAxis("Vertical") == 0)
            {
                BottomThruster.emit = false;
                TopThruster.emit = false;
            }


            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                if (!_audio.isPlaying)
                    _audio.Play();
            }
            else
                _audio.Stop();

            // ReSharper restore CompareOfFloatsByEqualityOperator                
        }
    }
}