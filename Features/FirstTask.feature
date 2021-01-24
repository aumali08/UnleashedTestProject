Feature: First Task

@newProduct
Scenario: Create New Product
	Given I login to the Unleashed website
	And I navigate to the Add Products page
	When I create a new product
	Then I see the product is successfully created

@salesOrder
Scenario: Complete Sales Order
	Given I login to the Unleashed website
	And I retrieve the stock information of COUCH2
	When I navigate to the Add Sales Quote page
	And I accepted a new sales quote
		| product | quantity |
		| COUCH2  | 2        |
	And I complete a new sales order
	Then I see the stock information of COUCH2 is updated