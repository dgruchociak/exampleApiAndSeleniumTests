using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using TrelloCoreTest.DataEntities;

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

        public async void CreateBoard(string name)
        {
            var request = new RestRequest($"/1/boards/?name={Uri.EscapeUriString(name)}&defaultLabels=true&defaultLists=false&keepFromSource=none&prefs_permissionLevel=private&prefs_voting=disabled&prefs_comments=members&prefs_invitations=members&prefs_selfJoin=true&prefs_cardCovers=true&prefs_background=blue&prefs_cardAging=regular&{AuthenticationString()}", Method.Post);
            var response = await client.ExecuteAsync(request);
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        public async void CreateList(string boardName, string newListName)
        {
            var request = new RestRequest($"/1/lists?name={Uri.EscapeUriString(newListName)}&idBoard={GetBoardIdByName(boardName)}&{AuthenticationString()}", Method.Post);
            var response = await client.ExecuteAsync(request);
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        public async void CreateCard(string boardName, string listName, string cardName)
        {
            var request = new RestRequest($"1/cards?name={Uri.EscapeUriString(cardName)}&idList={GetListIdByName(boardName, listName)}&keepFromSource=all&{AuthenticationString()}", Method.Post);
            var response = await client.ExecuteAsync(request);
            //response.
            
            Assert.AreEqual(200, (int)response.StatusCode);
        }

        public async Task<List<Board>>GetAllBoards()
        { 
            var request = new RestRequest($"/1/members/me/boards/?{AuthenticationString()}",
                Method.Get)
            {
                RequestFormat = DataFormat.Json
            };
            var response = await client.ExecuteAsync(request);
            Assert.AreEqual(200, (int)response.StatusCode);
            return JsonConvert.DeserializeObject<List<Board>>(response.Content).ToList();
        }

        public async Task<List<TrelloList>> GetAllListsInBoard(string boardName)
        {
            var request = new RestRequest($"/1/boards/{GetBoardIdByName(boardName)}/lists?{AuthenticationString()}",
                Method.Get)
            {
                RequestFormat = DataFormat.Json
            };

            var response = await client.ExecuteAsync(request);
            Assert.AreEqual(200, (int)response.StatusCode);
            return JsonConvert.DeserializeObject<List<TrelloList>>(response.Content);
        }

        public async Task<List<Card>> GetAllCardsInList(string listId)
        {
            var request = new RestRequest($"/1/lists/{listId}/cards?{AuthenticationString()}",
                Method.Get)
            {
                RequestFormat = DataFormat.Json
            };

            var response = await client.ExecuteAsync(request);
            Assert.AreEqual(200, (int)response.StatusCode);
            return JsonConvert.DeserializeObject<List<Card>>(response.Content);
        }

        public GetBoardIdByName(string boardName)
        {

            return GetAllBoards().GetAwaiter().GetResult().Find(b => b.name.Equals(boardName)).id;
        }

        public string GetListIdByName(string boardName, string listName)
        {
            return GetAllListsInBoard(GetBoardIdByName(boardName)).Result.Find(b => b.name.Equals(listName)).id;
        }

        public string GetCardIdByName(string boardName, string listName, string cardName)
        {
            return GetAllCardsInList(GetListIdByName(boardName, listName)).Result.Find(b => b.name.Equals(cardName)).id;
        }

        public bool IsBoardCreated(string id)
        {
            var test = GetAllBoards().Result.FirstOrDefault(b => b.id.Equals(id));
            return test != null;
        }

        public bool IsCardCreated(string boardName, string listName, string cardName)
        {
            var test = GetAllCardsInList(GetListIdByName(boardName, listName)).Result.FirstOrDefault(b => b.id.Equals(GetCardIdByName(boardName, listName, cardName)));
            return test != null;
        }

        public void DeleteAllBoards()
        {
            //b.closed=false
            foreach (string id in GetAllBoards().Result.Select(b => b.id).ToList())
            {
                var request = new RestRequest($"/1/boards/{id}?{AuthenticationString()}", Method.Delete);
                client.ExecuteAsync(request);
            }
        }


    }
}