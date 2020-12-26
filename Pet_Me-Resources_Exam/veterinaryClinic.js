class VeterinaryClinic {
    constructor(clinicName, capacity) {
        this.clinicName = clinicName;
        this.capacity = capacity;
        this.clients = {};

        this.totalProfit = 0;
        this.currentWorkload = 0;
    }

    newCustomer(ownerName, petName, kind, procedures) {

        if (this.currentWorkload >= this.capacity) {
            throw new Error("Sorry, we are not able to accept more patients!");
        }

        let customer = this.clients[ownerName];

        // Before register: 
        // •	Check if the clinic is able to accept more pets. If the clinic is full throw an Error:

        if (!customer) {
            this.clients[ownerName] = [];
            customer = this.clients[ownerName];
        }

        // •	Check if the pet is already registered under this client name. If it's registered and still has full list of procedures, you should throw an Error:
        let pet = customer.find(p => p.petName === petName);



        if (!pet) {
            customer.push({ petName, kind: kind.toLowerCase(), procedures });
            this.currentWorkload++;
        } else {
            if (pet.procedures.length > 0) {
                throw new Error(`This pet is already registered under ${ownerName} name! ${petName} is on our lists, waiting for ${pet.procedures.join(', ')}`);
            }
            // •	Otherwise this function should add the customer and his pet, update the current clinic  workload and return:
            this.currentWorkload++;
            pet.procedures= procedures;
        }

        return `Welcome ${petName}!`;
    }

    onLeaving(ownerName, petName) {
        let customer = this.clients[ownerName];
        // ·	Check if the given ownerName corresponds to a client in the clients array, if not throw an Error:
        if (!customer) {
            throw new Error(`Sorry, there is no such client!`);
        }

        // •	Then check if the given petName is registered under this ownerName, if not or it is registered but the procedures array is empty because all his procederues are done , then throw an Error:

        let pet = customer.find(p => p.petName === petName)
        if (!pet || pet.procedures.length === 0) {
            throw new Error(`Sorry, there are no procedures for ${petName}!`);
        }
        // ·	Otherwise, on leaving you have to collect the current client bill, add it to the total clinic profit and save the data. Calculate the bill knowing that each procedure cost 500.00$. Do not forget to update the current workload. Clear the array with procedures for the current pet. 
        let bill = pet.procedures.length * 500;
        this.totalProfit += bill;
        this.currentWorkload--;
        pet.procedures = [];
        // When pet leaves the clinic, petName and kind should stay like information in our data, but with no more procedures in the array of procedures. After that return, the following string:
        return `Goodbye ${petName}. Stay safe!`;
    }

    toString() {
        let res = '';
        // Return the full information of the clinic.
        // ·	First, we have to calculate how busy the clinic is in percentages. Percentage represents all current pets that have procedures based on the full capacity of the clinic. The percentage is rounded to the nearest smaller number:
        let proc = 0;
        let petsWithProc = Object.keys(this.clients).forEach(client => {
            let petsWithProc = this.clients[client].filter(p => p.procedures.length > 0).length;
            proc += petsWithProc;
        });
        let percentage = Math.floor((proc / this.capacity) * 100);

        res += `${this.clinicName} is ${percentage}% busy today!`;
        // •	On the second line comes the collected profit, that must be fixed to the second digit after the decimal point: 
        res += `\nTotal profit: ${this.totalProfit.toFixed(2)}$`;
        // On the next lines, return the whole information for the owners in the following format. Print kind property with lowercase letters. All owners should be in alphabetical order, as also pets of each of them must be in alphabetical order too:
        Object.keys(this.clients).sort((a, b) => a.localeCompare(b)).forEach(key => {
            res += `\n`;
            res += `${key} with:`;
            this.clients[key].sort((a, b) => a.petName.localeCompare(b.petName)).forEach(pet => {
                res += '\n';
                res += `---${pet.petName} - a ${pet.kind.toLowerCase()} that needs: ${pet.procedures.join(', ')}`;
            });
        });
        // ---{ petName } - a { kind } that needs: { procedures separated by ', '}"

        return res.trim();
    }

}


let clinic = new VeterinaryClinic('SoftCare', 10);
console.log(clinic.newCustomer('Jim Jones', 'Tom', 'Cat', ['A154B', '2C32B', '12CDB']));
console.log(clinic.newCustomer('Anna Morgan', 'Max', 'Dog', ['SK456', 'DFG45', 'KS456']));
console.log(clinic.newCustomer('Jim Jones', 'Tiny', 'Cat', ['A154B']));
console.log(clinic.onLeaving('Jim Jones', 'Tiny'));
console.log(clinic.onLeaving('Jim Jones', 'To'));
console.log(clinic.toString());
clinic.newCustomer('Jim Jones', 'Sara', 'Dog', ['A154B']);
console.log(clinic.toString());
