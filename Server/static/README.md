# PreOrder 0.1.0

---

Quickly order your food and check its progress without relying on a waiter. Make a payment after eating and leave. The system will allow POS to check the status of a table.

### POS SYSTEMS WILL BE ABLE TO:
* Create Menus
* Create Tables
* Monitor a table's status, see its order and if the table's payment is outstanding
* Manually clear a table, ready for a new customer in the event of a physical transaction

### USERS WILL BE ABLE TO:
* Open a session at a table by scanning it
* Browse a menu and construct an order.
* Upon confirming their order the user will be charged.
* Check the progress of their order.
	
---

### Project Archetecture

##### Server
* Python 2.7.4
	* Flask
* MongoDB
* Redis (Maybe)
	
##### Client
* Windows 8 Mobile

##### POS
* Restful Web interface. A simple HTML5 compliant browser is all that is needed.