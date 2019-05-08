namespace Mus
{
    public class NoteCoord
    {

        public int x;
        public int y;

        public NoteCoord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

//============= Equals and GetHashCode ===================================================
        protected bool Equals(NoteCoord other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((NoteCoord) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x * 397) ^ y;
            }
        }
    }
}