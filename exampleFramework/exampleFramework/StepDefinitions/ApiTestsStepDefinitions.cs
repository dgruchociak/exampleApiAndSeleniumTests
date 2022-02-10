using exampleFramework.Support;
using NUnit.Framework;

namespace exampleFramework.StepDefinitions
{
    [Binding]
    public class ApiTestsStepDefinitions
    {
        private ScenarioContext scenarioContext;

        public ApiTestsStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Given(@"User create a new board through the api")]
        public void GivenUserCreateANewBoardThroughTheApi()
        {
            var api = new APIHelper();
            scenarioContext["boardName"] = GetRandomName("boardName");
            api.CreateBoard(scenarioContext["boardName"].ToString());
        }

        [Given(@"User create a new list through the api")]
        public void GivenUserCreateANewListThroughTheApi()
        {
            var api = new APIHelper();
            scenarioContext["listName"] = GetRandomName("listName");
            api.CreateList(scenarioContext["boardName"].ToString(), scenarioContext["listName"].ToString());
        }

        [Given(@"User create a new card through the api")]
        public void GivenUserCreateANewCardThroughTheApi()
        {
            var api = new APIHelper();
            scenarioContext["cardName"] = GetRandomName("cardName");
            api.CreateCard(scenarioContext["boardName"].ToString(), scenarioContext["listName"].ToString(), scenarioContext["cardName"].ToString());
        }

        [Then(@"the board is successfully created")]
        public void ThenTheBoardIsSuccessfullyCreated()
        {
            var api = new APIHelper();
            Assert.IsTrue(api.IsBoardCreated(api.GetBoardIdByName(scenarioContext["boardName"].ToString())));
        }

        [Then(@"the card is successfully created")]
        public void ThenTheCardIsSuccessfullyCreated()
        {
            var api = new APIHelper();
            Assert.IsTrue(api.IsCardCreated(scenarioContext["boardName"].ToString(), scenarioContext["listName"].ToString(), scenarioContext["cardName"].ToString()));
        }

        private string GetRandomName(string name)
        {
            Random rnd = new Random();
            return name + DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss");
        }
    }
}
