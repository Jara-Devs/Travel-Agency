using Travel_Agency_Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Core;

public interface ICityService
{
    public Task<ApiResponse<IdResponse>> CreateCity(CityRequest cityRequest, UserBasic userBasic);

    public Task<ApiResponse> UpdateCity(Guid id, CityRequest cityRequest, UserBasic userBasic);

    public Task<ApiResponse> DeleteCity(Guid id, UserBasic userBasic);
}