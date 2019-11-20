Feature: FeeCalculation
	As a MobilePay accountant
	I want merchants to be charged Transaction Percentage Fee (1% of transaction amount)
	so that MobilePay would still be cheapest solution in the market and we could earn enough money to cover our expenses  

Scenario: CalculateTransactionFees
	Given That following transactions are made
	| date       | merchantName | amount |
	| 2018-09-02 | CIRCLE_K     | 120    |
	| 2018-09-04 | TELIA        | 200    |
	| 2018-10-22 | CIRCLE_K     | 300    |
	| 2018-10-29 | CIRCLE_K     | 150    |                                  
	When fees calculation app is executed                                                                     
	Then the output is
	| date       | merchantName | fee  |
	| 2018-09-02 | CIRCLE_K     | 30.20 |
	| 2018-09-04 | TELIA        | 31.00 |
	| 2018-10-22 | CIRCLE_K     | 32.00 |
	| 2018-10-29 | CIRCLE_K     | 1.50 |
