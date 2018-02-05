using System;

namespace StageStriker
{
    public class PlayerCollection<T>
    {
        public delegate void RefAction(out T entry);

        private readonly T[] collection;

        public T this[Player player]
        {
            get => collection[(int)player - 1];
            set => collection[(int)player - 1] = value;
        }

        public PlayerCollection(T player1, T player2)
        {
            collection = new []
            {
                player1,
                player2
            };
        }

        public void ForEach(Action<T> action)
        {
            action(collection[0]);
            action(collection[1]);
        }

        public void Set(T value)
        {
            collection[0] = value;
            collection[1] = value;
        }
    }
}
