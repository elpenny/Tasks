# Tasks

### How to run:
1. download
2. open with Visual Studio 2019 with .NET Core 3.1 installed
3. build solution (CTRL+SHIFT+B)
4. run (CTRL+F5)

Solution has also second project with test written in NUnit 3.
There is not much of them, but they correctly capture behavior of needed services.

### Configuration
There is no settings file but solution is fully customizable. There is configuration object supplied to the rest of the program  at the startup.
Instance is configured in this way:

	var config = new Configuration
	(
		queueMaxSize: 1000, // Lenght of the queue
		producerPollingDelay: 2000, // How often producers check if queue is half empty
		verboseLogging: false, // change to true for more logs
		producerCount: 4, 
		consumerCount: 2, 
		taskOperationsMaxCount: 7,  // how many operands can be in expression (Range from 1 to n) 
		TaskNumbersMinMaxValue: 5000, // how big (or small) can be any single number in  expression
		consumerDelay: 50 // delay between consumer polling for tasks
	);


### Known issues:
- there is some "wonkiness" in 'BlockingQueue.Count', it might be beneficial to implement similar queued structure
from scratch to fulfill requirements better.

### Requirements
Create an app with Consumer/Producer scenario with specified characteristic:
* each producer and consumer working on another thread
* no less than 2 consumers and 4 producers
* consumers and producers should share a queue to exchange tasks
* producers should fill queue with tasks at first
* then producers should poll periodically for moment when queue is half empty
* at this point producers should start filling queue with tasks again and repeat the cycle ad infinitum.
* consumers should take from queue when it's not empty and wait for it
* task is a randomly generated string that has to be representation of simple algorithmic operation (e.g. 2+2 or 5*235/55-3)
* task is executed by evaluating string and returning value of expression to be logged to console by one of the consumers