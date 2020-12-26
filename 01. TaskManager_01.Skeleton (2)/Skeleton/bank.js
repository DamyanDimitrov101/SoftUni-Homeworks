class Bank {
    constructor(bankName) {
        // Receives 1 parameter at initialization of the class (bankName), and should be set as private property.
        // Should have these 2 properties:
        // ·	bankName - private property of type string 
        if (bankName==='') {
            return;
        }
        this._bankName = bankName;
        // ·	allCustomers -  initially an empty array
        this.allCustomers = [];
    }

    newCustomer({firstName,lastName,personalId}) {
        // The customer is of type object {firstName, lastName, personalId}.
        let customerCurrent = this.allCustomers.find(c => c.personalId === personalId);

        // •	Check if the customer is already a customer of the bank. If so you should throw an Error:
        if (customerCurrent) {
            throw new Error(`${firstName} ${lastName} is already our customer!`);
        }
        // •	Otherwise this function should add the customer as new one and return the customer details.
        let customer = {firstName,lastName,personalId};
        this.allCustomers.push(customer);

        return customer;
    }

    depositMoney(personalId, amount) {
        // Both the personalId and the amount are numbers.
        // ·	Check if the given personalId corresponds to a customer in the customers array, if not throw a new error:
        let customerCurrent = this.allCustomers.find(c => c.personalId === personalId);

        if (!customerCurrent) {
            throw new Error(`We have no customer with this ID!`);
        }

        // ·	Otherwise add the amount to the corresponding customer in a property named totalMoney and store the transaction information to this customer (for more clarity see the example below and the hints), then return the total money of the corresponding customer and a dollar sign:
        // “{totalMoney}$”

        if (customerCurrent.amount) {
            customerCurrent.amount += Number(amount);
        } else {
            customerCurrent.amount = Number(amount);
            customerCurrent.transactions = [];
        }

        customerCurrent.transactions.push(`${customerCurrent.firstName} ${customerCurrent.lastName} made deposit of ${amount}$!`);

        return `${customerCurrent.amount}$`;
    }

    withdrawMoney (personalId, amount){
        // Both the personalId and the amount are numbers.
        // ·	Check if the given personalId corresponds to a customer in the customers array, if not throw a new error:
        // “We have no customer with this ID!”
        let customerCurrent = this.allCustomers.find(c => c.personalId === personalId);

        if (!customerCurrent) {
            throw new Error(`We have no customer with this ID!`);
        }

        // •	If there is a customer with the given personalId, check if the customer has enough money in his account, to withdraw the given amount. If the money is not enough throw a new error:
        if (customerCurrent.amount < amount) {
            throw new Error(`${customerCurrent.firstName} ${customerCurrent.lastName} does not have enough money to withdraw that amount!`);
        }
        // ·	Otherwise subtract the amount from the totalMoney  of the customer and store the transaction information to this customer, then return the total money of the corresponding customer and a dollar sign:
        customerCurrent.amount -= amount;
        customerCurrent.transactions.push(`${customerCurrent.firstName} ${customerCurrent.lastName} withdrew ${amount}$!`);
        
        return `${customerCurrent.amount}$`;
    }
        

    
    customerInfo (personalId){
        // The personalId is of type number.
        let customerCurrent = this.allCustomers.find(c => c.personalId === personalId);

        // ·	Check if the given personalId corresponds to a customer in the customers array, if not throw a new error:
        if (!customerCurrent) {
            throw new Error(`We have no customer with this ID!`);
        }

        let res = ``;
        // •	Otherwise return the whole information for the customer in the following format:
        res+=`\nBank name: ${this._bankName}\n`;
        res+=`Customer name: ${customerCurrent.firstName} ${customerCurrent.lastName}\n`;
        res+=`Customer ID: ${customerCurrent.personalId}\n`;
        res+=`Total Money: ${customerCurrent.amount?customerCurrent.amount:0}$\n`;
   
        if (customerCurrent.transactions.length > 0) {
            res += 'Transactions:\n';
            for(let i = customerCurrent.transactions.length; i > 0; i-- ) {
                res += `${i}. ${customerCurrent.transactions[i - 1]}\n`;  
            }
        }
     
        return res.trim();
    }
        
}


let bank = new Bank("SoftUni Bank");
console.log(bank.newCustomer({ firstName: "Svetlin", lastName: "Nakov", personalId: 6233267 }));
console.log(bank.newCustomer({ firstName: "Mihaela", lastName: "Mileva", personalId: 4151596 }));
bank.depositMoney(6233267, 250);
console.log(bank.depositMoney(6233267, 250));
bank.depositMoney(4151596, 555);
console.log(bank.withdrawMoney(6233267, 125));
console.log(bank.customerInfo(6233267));
