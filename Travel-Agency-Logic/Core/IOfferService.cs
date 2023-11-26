using Travel_Agency_Core;
using Travel_Agency_Domain.Offers;
using Travel_Agency_Domain.Services;
using Travel_Agency_Logic.Request;
using Travel_Agency_Logic.Response;

namespace Travel_Agency_Logic.Core;

public interface IOfferService<T> where T : Offer
{
    Task<ApiResponse<IdResponse>> CreateOffer(OfferRequest<T> offer, UserBasic user);
    Task<ApiResponse> UpdateOffer(Guid id, OfferRequest<T> offer, UserBasic user);
    Task<ApiResponse> DeleteOffer(Guid id, UserBasic user);
}