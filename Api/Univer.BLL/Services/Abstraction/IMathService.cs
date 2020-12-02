using System.Threading.Tasks;
using Univer.DAL.Models.Math;

namespace Univer.BLL.Services
{
    public interface IMathService
    {
        Task<int> MatrixMultiply(MatrixMultiplyRequest matrixMultiplyRequest);
    }
}