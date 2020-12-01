namespace Univer.BLL.Services
{
    public interface IMathService
    {
        int [,] MatrixMultiply(int rows_1, int columns_1, int rows_2, int columns_2);
    }
}