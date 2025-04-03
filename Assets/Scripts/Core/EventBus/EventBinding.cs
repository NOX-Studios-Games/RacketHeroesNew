using System;

namespace Core.EventBus
{
    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        // Inicialização das variáveis '_onEvent' com uma ação vazia que recebe um argumento do tipo 'T'.
        // Isso é feito para garantir que a variável nunca seja 'null'. A ação vazia ( _ => { } )
        // não faz nada, mas garante que sempre exista uma implementação válida na variável,
        // evitando a necessidade de fazer verificações de 'null' antes de chamar a ação.
        private Action<T> _onEvent = _ => { };
        // Inicialização da variável '_onEventNoArgs' com uma ação vazia que não recebe argumentos.
        // Como no caso do '_onEvent', essa inicialização garante que a variável nunca seja 'null',
        // evitando a necessidade de fazer verificações de 'null' antes de invocar a ação.
        private Action _onEventNoArgs = () => { };
        
        /// <inheritdoc />
        /// <summary>
        /// Implementação explícita da interface <see cref="IEventBinding{T}"/> para garantir que a propriedade
        /// <c>OnEvent</c> seja usada diretamente como parte da interface e não do tipo concreto <see cref="EventBinding{T}"/>.
        /// Isso força o uso da interface ao invés do tipo concreto, garantindo um desacoplamento maior do código.
        /// </summary>
        Action<T> IEventBinding<T>.OnEvent 
        { 
            get => _onEvent; 
            set => _onEvent = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Implementação explícita da interface <see cref="IEventBinding{T}"/> para garantir que a propriedade
        /// <c>OnEventNoArgs</c> seja usada diretamente como parte da interface e não do tipo concreto <see cref="EventBinding{T}"/>.
        /// Isso força o uso da interface ao invés do tipo concreto, garantindo um desacoplamento maior do código.
        /// </summary>
        Action IEventBinding<T>.OnEventNoArgs 
        { 
            get => _onEventNoArgs; 
            set => _onEventNoArgs = value; 
        }
        
        #region Construtores
        /// <summary>
        /// Cria um novo binding de evento com uma ação que recebe um argumento do tipo <typeparamref name="T"/>.
        /// Este é o construtor da classe <see cref="EventBinding{T}"/> que permite associar um evento específico com uma ação
        /// que será executada quando o evento ocorrer, recebendo um argumento do tipo <typeparamref name="T"/>.
        /// </summary>
        /// <param name="onEvent">Ação a ser executada quando o evento ocorrer. Recebe um parâmetro do tipo <typeparamref name="T"/>.</param>
        /// <remarks>
        /// Este construtor é utilizado para inicializar um binding de evento onde será fornecida uma função que
        /// será chamada quando o evento for disparado, com o argumento do tipo <typeparamref name="T"/>.
        /// </remarks>
        public EventBinding(Action<T> onEvent) => _onEvent = onEvent;

        /// <summary>
        /// Cria um novo binding de evento com uma ação sem argumentos.
        /// Este é o construtor da classe <see cref="EventBinding{T}"/> que permite associar um evento específico com uma ação
        /// que será executada quando o evento ocorrer, mas sem parâmetros.
        /// </summary>
        /// <param name="onEventNoArgs">Ação a ser executada quando o evento ocorrer, sem parâmetros.</param>
        /// <remarks>
        /// Este construtor é utilizado para inicializar um binding de evento onde será fornecida uma função que
        /// será chamada quando o evento for disparado, mas sem nenhum argumento (sem parâmetros).
        /// </remarks>
        public EventBinding(Action onEventNoArgs) => _onEventNoArgs = onEventNoArgs;
        #endregion
        
        #region Metodos
        /// <summary>
        /// Adiciona uma nova ação ao binding, que será chamada quando o evento for disparado.
        /// </summary>
        /// <param name="onEvent">Ação a ser adicionada.</param>
        public void Add(Action<T> onEvent) => _onEvent += onEvent;

        /// <summary>
        /// Remove uma ação previamente registrada no binding.
        /// </summary>
        /// <param name="onEvent">Ação a ser removida.</param>
        public void Remove(Action<T> onEvent) => _onEvent -= onEvent;

        /// <summary>
        /// Adiciona uma nova ação ao binding, que será chamada quando o evento for disparado, sem argumentos.
        /// </summary>
        /// <param name="onEvent">Ação a ser adicionada.</param>
        public void Add(Action onEvent) => _onEventNoArgs += onEvent;

        /// <summary>
        /// Remove uma ação previamente registrada no binding, sem argumentos.
        /// </summary>
        /// <param name="onEvent">Ação a ser removida.</param>
        public void Remove(Action onEvent) => _onEventNoArgs -= onEvent;
        #endregion
    }
}