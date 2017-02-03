using System.Linq;

namespace Welo.Bot.Luis
{

    public static class LuisResponseExtensions
    {
        public static Intent Winner(this LuisResponse luisResponse)
        {
            if (!luisResponse.Intents.Any())
                return null;

            Intent winner = luisResponse.Intents.First();
            for (int i = 1; i < luisResponse.Intents.Count; ++i)
            {
                if (luisResponse.Intents[i].Score > winner.Score)
                    winner = luisResponse.Intents[i];

            }

            return winner;
        }
    }
}