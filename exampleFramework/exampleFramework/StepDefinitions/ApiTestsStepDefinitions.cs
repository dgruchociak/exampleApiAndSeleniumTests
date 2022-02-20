using exampleFramework.Support;
using NUnit.Framework;

namespace exampleFramework.StepDefinitions
{
    [Binding]
    public class ApiTestsStepDefinitions
    {
        private ScenarioContext scenarioContext;
        private APIHelper apiHelper;

        public ApiTestsStepDefinitions(ScenarioContext scenarioContext)
        {
            this.apiHelper = new APIHelper();
            this.scenarioContext = scenarioContext;
        }

        [Given(@"User create a new board through the api")]
        public async Task GivenUserCreateANewBoardThroughTheApi()
        {
            var board = await apiHelper.CreateBoardAsync(GetRandomName("boardName"));
            scenarioContext["boardId"] = board.id;
        }

        [Given(@"User create a new list through the api")]
        public async Task GivenUserCreateANewListThroughTheApi()
        {
            var list = await apiHelper.CreateListAsync(scenarioContext["boardId"].ToString(), GetRandomName("listName"));
            //scenarioContext["listId"] = list.id;
            scenarioContext.Add("listId", list.id);
        }

        [Given(@"User create a new card through the api")]
        public async Task GivenUserCreateANewCardThroughTheApi()
        {
            scenarioContext["cardName"] = GetRandomName("cardName");
            var card = await apiHelper.CreateCardAsync(scenarioContext["listId"].ToString(), scenarioContext["cardName"].ToString());
            scenarioContext["cardId"] = card.id;
        }

        [Given(@"User add a member to card through the api")]
        public async Task GivenUserAddAMemberToCardThroughTheApi()
        {
            scenarioContext["memberId"] = "62015769c051f85bba98df4e";
            await apiHelper.AddMemberAsync(scenarioContext["cardId"].ToString(), scenarioContext["memberId"].ToString());
        }

        [Then(@"the board is successfully created")]
        public async Task ThenTheBoardIsSuccessfullyCreated()
        {
            Assert.IsTrue(await apiHelper.IsBoardCreatedAsync(scenarioContext["boardId"].ToString()));
        }

        [Then(@"the card is successfully created")]
        public async Task ThenTheCardIsSuccessfullyCreated()
        {
            Assert.IsTrue(await apiHelper.IsCardCreated(scenarioContext["listId"].ToString(), scenarioContext["cardId"].ToString()));
        }

        private string GetRandomName(string name)
        {
            Random rnd = new Random();
            return name + DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss");
        }
    }
}
