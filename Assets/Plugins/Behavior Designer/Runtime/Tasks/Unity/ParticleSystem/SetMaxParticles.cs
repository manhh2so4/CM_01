using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityParticleSystem
{
    [TaskCategory("Unity/ParticleSystem")]
    [TaskDescription("Sets the max particles of the Particle System.")]
    public class SetMaxParticles : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The max particles of the ParticleSystem")]
        public SharedInt maxParticles;

        private ParticleSystem particleSystem;
        private UnityEngine.GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                particleSystem = currentGameObject.GetComponent<ParticleSystem>();
                prevGameObject = currentGameObject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (particleSystem == null) {
                Debug.LogWarning("ParticleSystem is null");
                return TaskStatus.Failure;
            }
            ParticleSystem.MainModule mainParticleSystem = particleSystem.main;
            mainParticleSystem.maxParticles = maxParticles.Value;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            maxParticles = 0;
        }
    }
}
