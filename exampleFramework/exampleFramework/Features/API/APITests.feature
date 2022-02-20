Feature: ApiTests

logowanie trello plus utworzenie board + utworzenie card

@API
Scenario: As a user I want to create new board
	Given User create a new board through the api
	Then the board is successfully created

@API
Scenario: As a user I want to create new card
	Given User create a new board through the api
		And User create a new list through the api
		And User create a new card through the api
	Then the card is successfully created

@API
Scenario: As a user I want to create new card with member
	Given User create a new board through the api
		And User create a new list through the api
		And User create a new card through the api
		And User add a member to card through the api
	Then the card is successfully created
