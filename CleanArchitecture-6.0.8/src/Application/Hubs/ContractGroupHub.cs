
using CleanArchitecture.Domain.Interface;
using Microsoft.AspNetCore.SignalR;

namespace CleanArchitecture.Application.Hubs;
public class ContractGroupHub : Hub
{
    public async Task SendContractGroupUpdate(int contractGroupId)
    {
        await Clients.All.SendAsync("ContractGroupUpdated", contractGroupId);
    }
}