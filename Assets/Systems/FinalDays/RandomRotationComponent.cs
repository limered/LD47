using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemBase;
using SystemBase.StateMachineBase;
using Systems;
using GameState.States;
using UniRx;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Assets.Systems.FinalDays
{
    public class RandomRotationComponent : GameComponent
    {
        public bool IsBasedOnGameState;
        public string[] GameStates;

        private Vector3 _originalPosition;
        private Quaternion _originalRotation;

        protected override void OverwriteStart()
        {
            _originalPosition = transform.position;
            _originalRotation = transform.rotation;

            IoC.Game.GameStateContext.CurrentState.Where(state => !(state is GameOver))
                .Subscribe(Reset)
                .AddTo(this);
        }

        private void Reset(BaseState<Game> obj)
        {
            transform.rotation = _originalRotation;
            transform.position = _originalPosition;

            var body = GetComponent<Rigidbody>();
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            body.rotation = _originalRotation;
            body.position = _originalPosition;
        }

        private void Update()
        {
            if (IsBasedOnGameState && GameStateIsNotTheOne()) return;

            transform.Rotate(new Vector3(0,0,1), 1f);

            AnimateMovement();
        }

        private void AnimateMovement()
        {
            var x = Random.value * 2f - 1f;
            var y = Random.value * 2f - 1f;

            var body = GetComponent<Rigidbody>();
            body.AddForce(new Vector3(x, y, 0));

            if (transform.position.x > 5)
            {
                body.AddForce(Vector3.left * 2);
            }
            else if (transform.position.x < -5)
            {
                body.AddForce(Vector3.right * 2);
            }

            if (transform.position.y > 4)
            {
                body.AddForce(Vector3.down * 2);
            }
            else if (transform.position.y < -4)
            {
                body.AddForce(Vector3.up * 2);
            }
        }

        private bool GameStateIsNotTheOne()
        {
            var gameStateString = IoC.Game.GameStateContext.CurrentState.Value.ToString();
            return !GameStates.Contains(gameStateString);
        }
    }
}
