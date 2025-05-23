namespace Misc.Sound
{
    public interface IPlaySound
    {
        void Play();

        void Stop();
    }

    public interface IPlaySound<T>
    {
        void Play(T val);

        void Stop();
    }
}
