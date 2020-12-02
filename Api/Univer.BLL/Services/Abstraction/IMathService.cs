using System.Threading.Tasks;
using Univer.DAL.Models.Math;

namespace Univer.BLL.Services
{
    public interface IMathService
    {
        Task<long> MatrixMultiply(MatrixMultiplyRequest matrixMultiplyRequest);
    }
}