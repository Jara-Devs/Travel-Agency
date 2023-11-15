using Travel_Agency_Core;
using Travel_Agency_Domain.Payments;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Core;

public interface IReserveService<T1, T2> where T1 : Reserve where T2 : Payment
{
    Task<ApiResponse<IdResponse>> CreateReserve(ReserveRequest<T1, T2> request, UserBasic userBasic);
}