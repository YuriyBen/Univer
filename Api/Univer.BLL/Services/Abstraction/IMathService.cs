﻿using System.Threading;
using System.Threading.Tasks;
using Univer.DAL.Models.Math;

namespace Univer.BLL.Services
{
    public interface IMathService
    {
        Task<object> MatrixMultiply(MatrixMultiplyRequest matrixMultiplyRequest, CancellationToken cancellationToken);
    }
}