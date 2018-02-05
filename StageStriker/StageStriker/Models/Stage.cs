using System;

namespace StageStriker
{
    public struct Stage
    {
        public string Name { get; }
        public string Image { get; }

        public Stage(string name, string image)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Cannot have an empty stage name.");
            }

            Name = name;
            Image = image;
        }

        public static bool operator ==(Stage left, Stage right)
        {
            return left.Name == right.Name;
        }

        public static bool operator !=(Stage left, Stage right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(Stage other)
        {
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            return obj is Stage && Equals((Stage)obj);
        }

        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }
    }
}