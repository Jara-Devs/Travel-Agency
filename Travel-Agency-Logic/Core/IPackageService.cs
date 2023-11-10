using Travel_Agency_Core;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Core;

public interface IPackageService
{
    Task<ApiResponse<IdResponse>> CreatePackage(PackageRequest request, UserBasic userBasic);

    Task<ApiResponse> UpdatePackage(int id, PackageRequest request, UserBasic userBasic);

    Task<ApiResponse> RemovePackage(int id, UserBasic userBasic);
}