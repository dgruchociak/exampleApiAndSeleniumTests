Feature: UITests

wpisać Apple w google searchboxa i zrobić dwie metody na wybieranie produktów
- pierwszego i dowolnego z page objectem

@FirstScenario
Scenario: As a user I want to open the Google Search page and click on first apple product
	Given User opens the google
	When User searches for 'apple' phrase
	Then User opens the "first" item

@SecondScenario
Scenario: As a user I want to open the Google Search page and click on random apple product
	Given User opens the google
	When User searches for 'apple' phrase
	Then User opens the "random" item
