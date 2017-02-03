using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Welo.Bot.Commands.Interfaces
{
    public interface IStartUpCommand : ICommand
    {
        Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument);
    }
}