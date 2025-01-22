namespace GlobalGameJam
{
    public interface IBindable
    {
        void Bind(int playerNumber);
        void Release();
    }
}