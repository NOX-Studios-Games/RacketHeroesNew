using System.Collections.Generic;
using RacketHeroes.Core.EventBus;
using UnityEngine;

namespace Core.EventBus.Events
{
    /// <summary>
    /// Sistema de Event Bus genérico para gerenciar eventos desacoplados no jogo.
    /// Permite registrar, remover e publicar eventos que seguem a interface IEvent.
    /// </summary>
    /// <typeparam name="T">O tipo de evento que será tratado, deve implementar IEvent.</typeparam>
    public static class EventBus<T> where T : IEvent
    {
        /// <summary>
        /// Conjunto de bindings registrados para o evento do tipo <typeparamref name="T"/>.
        /// </summary>
        private static readonly HashSet<IEventBinding<T>> Bindings = new();

        /// <summary>
        /// Registra um novo binding no Event Bus.
        /// </summary>
        /// <param name="binding">O binding contendo as ações a serem executadas quando o evento for publicado.</param>
        public static void Register(EventBinding<T> binding) => Bindings.Add(binding);

        /// <summary>
        /// Remove um binding do Event Bus, evitando que ele receba eventos futuros.
        /// </summary>
        /// <param name="binding">O binding a ser removido.</param>
        public static void Unregister(EventBinding<T> binding) => Bindings.Remove(binding);

        /// <summary>
        /// Publica um evento para todos os bindings registrados.
        /// Todos os métodos associados ao evento serão chamados em ordem de registro.
        /// </summary>
        /// <param name="tEvent">O evento a ser publicado.</param>
        public static void Publish(T tEvent)
        {
            foreach (var binding in Bindings)
            {
                binding.OnEvent(tEvent); // Chama o método que recebe o evento como argumento.
                binding.OnEventNoArgs(); // Chama o método que não recebe argumentos.
            }
        }

        /// <summary>
        /// Remove todos os bindings registrados no Hashset <see cref="Bindings"/>
        /// </summary>
        private static void Clear()
        {
            Debug.Log($"Clearing {typeof(T).Name} bindings");
            Bindings.Clear();
        }
    }
}