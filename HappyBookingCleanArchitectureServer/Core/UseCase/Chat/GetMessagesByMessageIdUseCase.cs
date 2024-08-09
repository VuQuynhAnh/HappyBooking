using HappyBookingCleanArchitectureServer.Core.Interface.IRepository;
using HappyBookingCleanArchitectureServer.Core.Interface.IUseCase.Chat;
using HappyBookingShare.Common;
using HappyBookingShare.Request.Chat;
using HappyBookingShare.Response.Chat;
using HappyBookingShare.Response.Dtos;
using Microsoft.Extensions.Caching.Memory;

namespace HappyBookingCleanArchitectureServer.Core.UseCase.Chat;

public class GetMessagesByMessageIdUseCase : IGetMessagesByMessageIdUseCase
{
    private readonly IChatRepository _chatRepository;
    private readonly IMemoryCache _cache;

    public GetMessagesByMessageIdUseCase(IChatRepository chatRepository, IMemoryCache cache)
    {
        _chatRepository = chatRepository;
        _cache = cache;
    }

    public async Task<GetMessagesByMessageIdResponse> GetMessagesByMessageId(long userId, GetMessagesByMessageIdRequest request)
    {
        try
        {
            var result = await _chatRepository.GetMessagesByMessageId(request.MessageId);
            return new GetMessagesByMessageIdResponse(userId, new MessageDto(result), StatusEnum.Successed, _cache);
        }
        finally
        {
            await _chatRepository.ReleaseResource();
        }
    }
}
