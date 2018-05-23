Feature: Budgets

Scenario: Add a budget
	Given open the budget adding page
	When I add budget 500 for 2018-10
	Then the result should be displayed
	| YearMonth | Amount |
	| 2018-10   | 500    |
