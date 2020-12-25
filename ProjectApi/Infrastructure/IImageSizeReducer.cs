namespace ProjectApi.Infrastructure
{
    using System.Drawing;

    public interface IImageSizeReducer
    {
        string GetReducedSizeImage(string imageString, int width, int height);
    }
}
