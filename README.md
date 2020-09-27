# Tasks

### How to run:
1. download
2. open with Visual Studio 2019 with .NET Core 3.1 installed
3. build solution (CTRL+SHIFT+B)
4. run (CTRL+F5)

Solution has also second project with test written in NUnit 3.
There is not much of them, but they correctly capture behavior of needed services.

###Configuration
There is no settings file but solution is fully customizable.


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
and that must be evaluated and value of expression returned to be logged to console by one of consumers