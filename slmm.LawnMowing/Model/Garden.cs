namespace SmartLawnMowing.Domain.Model
{
    public class Garden
    {
        private readonly int length;
        private readonly int width;

        public Garden(int length, int width)
        {
            this.length = length;
            this.width = width;
        }

        internal bool CellIsInsideGarden(Coordinates coordinates)
        {
            return coordinates.X >= 0 && coordinates.X <= this.length
                && coordinates.Y >= 0 && coordinates.Y <= this.width;
        }
    }
}
