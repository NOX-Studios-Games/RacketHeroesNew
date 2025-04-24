using System;
using System.Collections.Generic;
using Core.EventBus;
using Core.EventBus.Events.Player;
using Core.StateMachine.Base;
using UnityEngine;

namespace Player
{
    public class PlayerStatesHandler : MonoBehaviour
    {
        [SerializeField] private StateMachine stateMachine;
        [SerializeField] private List<StateMapping> stateMapList = new();
        private readonly Dictionary<StateId, CharacterState> _statesDictionary = new();
        private StateId _currentStateId;
        
        private EventBinding<PlayerInputEvent> _playerInputEventBinding;
        
        private static bool IsInputZero(Vector2 input) => input.sqrMagnitude < 0.01f;
        
        private void Awake() => SetupStates();

        private void OnEnable()
        {
            _playerInputEventBinding = new EventBinding<PlayerInputEvent>(HandleStatesTransitions);
            
            EventBus<PlayerInputEvent>.Register(_playerInputEventBinding);
        }

        private void OnDisable() => EventBus<PlayerInputEvent>.Unregister(_playerInputEventBinding);

        private void Update()
        {
            Debug.Log($"Estado atual: {_currentStateId} => {_statesDictionary[_currentStateId]}");
        }

        private void SetupStates()
        {
            foreach (var stateMap in stateMapList)
            {
                stateMap.stateInstance.myStateMachine = stateMachine;
                _statesDictionary[stateMap.stateId] = stateMap.stateInstance;
            }
            
            stateMachine.SetState(GetState(StateId.Idle));
        }

        private void HandleStatesTransitions(PlayerInputEvent eventData)
        {
            var input = eventData.MovementInput;
            var isInputZero = IsInputZero(input);
            
            switch (_currentStateId)
            {
                case StateId.Idle:
                    if (!isInputZero)
                        SetStateById(StateId.Walking);
                    break;

                case StateId.Walking:
                    if (isInputZero)
                        SetStateById(StateId.Striking);
                    break;

                case StateId.Striking:
                    if (!isInputZero)
                        SetStateById(StateId.Walking);
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private State GetState(StateId id)
        {
            if (_statesDictionary.TryGetValue(id, out var state))
                return state;

            Debug.LogError($"Estado com Id '{id}' não encontrado.");
            return null;
        }

        private void SetStateById(StateId stateId)
        {
            var state = GetState(stateId);
            if(state == null) return;
            
            _currentStateId = stateId;
            stateMachine.SetState(state);
        }
    }

    internal enum StateId
    {
        Idle,
        Walking,
        Striking
    }

    [Serializable]
    internal struct StateMapping
    {
        public StateId stateId;
        public CharacterState stateInstance;
    }
}