using Newtonsoft.Json;
using RestSharp;
using exampleFramework.Extensions;
using exampleFramework.DataEntities;

namespace exampleFramework.Support
{
    public class APIHelper
    {
        private RestClient client;
        protected ConfigurationHelper configuration = new ConfigurationHelper();

        public APIHelper()
        {
            client = new RestClient(configuration.GetConfiguration()["api:uri"]);
        }

        public string AuthenticationString()
        {
            return $"key={configuration.GetConfiguration()["api:key"]}&token={configuration.GetConfiguration()["api:token"]}";
        }

        public async Task<Board> CreateBoardAsync(string name)
        {
            var request = new RestRequest($"/1/boards/?name={Uri.EscapeUriString(name)}&defaultLabels=true&defaultLists=false&keepFromSource=none&prefs_permissionLevel=private&prefs_voting=disabled&prefs_comments=members&prefs_invitations=members&prefs_selfJoin=true&prefs_cardCovers=true&prefs_background=blue&prefs_cardAging=regular&{AuthenticationString()}", Method.Post);
            var response = await client.ExecuteAsync(request);
            response.EnsureStatusSuccess();
            return JsonConvert.DeserializeObject<Board>(response.Content);
        }

        public async Task<TrelloList>CreateListAsync(string boardId, string newListName)
        {
            var request = new RestRequest($"/1/lists?name={Uri.EscapeUriString(newListName)}&idBoard={boardId}&{AuthenticationString()}", Method.Post);
            var response = await client.ExecuteAsync(request);
            response.EnsureStatusSuccess();
            return JsonConvert.DeserializeObject<TrelloList>(response.Content);
        }

        public async Task<Card>CreateCardAsync(string listId, string cardName)
        {
            var request = new RestRequest($"1/cards?name={Uri.EscapeUriString(cardName)}&idList={listId}&keepFromSource=all&{AuthenticationString()}", Method.Post);
            var response = await client.ExecuteAsync(request);
            response.EnsureStatusSuccess();
            return JsonConvert.DeserializeObject<Card>(response.Content);
        }

        public async Task AddMemberAsync(string cardId, string memberId)
        {
            var request = new RestRequest($"1/cards/{cardId}/IdMembers/?value={memberId}&{AuthenticationString()}", Method.Post);
            var response = await client.ExecuteAsync(request);
            response.EnsureStatusSuccess();
        }

        public async Task<List<Board>>GetAllBoardsAsync()
        { 
            var request = new RestRequest($"/1/members/me/boards/?{AuthenticationString()}",
                Method.Get)
            {
                RequestFormat = DataFormat.Json
            };
            var response = await client.ExecuteAsync(request);
            response.EnsureStatusSuccess();
            return JsonConvert.DeserializeObject<List<Board>>(response.Content).ToList();
        }

        public async Task<List<TrelloList>> GetAllListsInBoardAsync(string boardName)
        {
            var request = new RestRequest($"/1/boards/{GetBoardIdByNameAsync(boardName)}/lists?{AuthenticationString()}",
                Method.Get)
            {
                RequestFormat = DataFormat.Json
            };

            var response = await client.ExecuteAsync(request);
            response.EnsureStatusSuccess();
            return JsonConvert.DeserializeObject<List<TrelloList>>(response.Content);
        }

        public async Task<List<Card>> GetAllCardsInListAsync(string listId)
        {
            var request = new RestRequest($"/1/lists/{listId}/cards?{AuthenticationString()}",
                Method.Get)
            {
                RequestFormat = DataFormat.Json
            };

            var response = await client.ExecuteAsync(request);
            response.EnsureStatusSuccess();
            return JsonConvert.DeserializeObject<List<Card>>(response.Content);
        }

        public async Task<string>GetBoardIdByNameAsync(string boardName)
        {
            var allBoards = await GetAllBoardsAsync();
            var board = allBoards.Single(b => b.name.Equals(boardName, StringComparison.InvariantCultureIgnoreCase));
            return board.id;
        }

        public async Task<string> GetListIdByNameAsync(string boardName, string listName)
        {
            var allLists = await GetAllListsInBoardAsync(boardName);
            var list = allLists.Single(b => b.name.Equals(listName, StringComparison.InvariantCultureIgnoreCase));
            return list.id;
        }

        public async Task<string> GetCardIdByNameAsync(string listId, string cardName)
        {
            var allCards = await GetAllCardsInListAsync(listId);
            var card = allCards.Single(b => b.name.Equals(cardName, StringComparison.InvariantCultureIgnoreCase));
            return card.id;
        }

        public async Task<bool> IsBoardCreatedAsync(string boardId)
        {
            var boards = await GetAllBoardsAsync();
            return boards.Any((b => b.id.Equals(boardId, StringComparison.InvariantCultureIgnoreCase)));
        }

        public async Task<bool> IsCardCreated(string listId, string cardId)
        {
            var cards = await GetAllCardsInListAsync(listId);
            return cards.Any((b => b.id.Equals(cardId, StringComparison.InvariantCultureIgnoreCase)));
        }

        public void DeleteAllBoards()
        {
            foreach (string id in GetAllBoardsAsync().Result.Select(b => b.id).ToList())
            {
                var request = new RestRequest($"/1/boards/{id}?{AuthenticationString()}", Method.Delete);
                client.ExecuteAsync(request);
            }
        }


    }
}