using System;

namespace RacketHeroes.Core.EventBus
{
    /// <summary>
    /// Interface que define um binding para eventos no Event Bus.
    /// </summary>
    /// <typeparam name="T">O tipo de evento que será tratado.</typeparam>
    internal interface IEventBinding<T>
    {
        /// <summary>
        /// Ação a ser executada quando o evento ocorre, recebendo um argumento do tipo <typeparamref name="T"/>.
        /// </summary>
        public Action<T> OnEvent { get; set; }

        /// <summary>
        /// Ação a ser executada quando o evento ocorre, sem argumentos.
        /// </summary>
        public Action OnEventNoArgs { get; set; }
    }
}