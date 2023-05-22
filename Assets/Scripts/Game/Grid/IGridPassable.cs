namespace Assets.Scripts.Game.Grid
{
    public interface IGridPassable
    {
        public bool IsLevelPassable(int shapeWidth, int shapeHeight);
    }
}