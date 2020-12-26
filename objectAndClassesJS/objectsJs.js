function townstoJSON(arr) {
    let headings = arr.shift()
                    .split('|')
                    .map(x=>x.trim())
                    .filter(y=>y!=='');


    let objects = [];

    arr.forEach(row => {
        let internalArr= row.split('|').map(x=>x.trim()).filter(x=>x!=='');
        
        let town = internalArr[0];
        let latitude = Number(Number(internalArr[1]).toFixed(2));
        let longitude = Number(Number(internalArr[2]).toFixed(2));
        
        let townHead = headings[0];
        let latitudeHead = headings[1];
        let longitudeHead= headings[2];
        
        let currentObject = {[townHead]:town, [latitudeHead]:latitude, [longitudeHead]:longitude};
        
        objects.push(currentObject);
    });

    let json = JSON.stringify(objects);
    return json;
}



// townstoJSON(['| Town | Latitude | Longitude |',
// '| Sofia | 42.696552 | 23.32601 |',
// '| Beijing | 39.913818 | 116.363625 |']
// );

// townstoJSON(['| Town | Latitude | Longitude |',
// '| Veliko Turnovo | 43.0757 | 25.6172 |',
// '| Monatevideo | 34.50 | 56.11 |']
// );


function sumByTown(arr) {
    let object = {};

    for (let i = 0; i < arr.length; i+=2) {
        let town = arr[i];
        let income = Number(arr[i+1]);

        if (object[town]) {
            object[town] += income;
        }else{
            object[town] = income;
        }
    }

    let json = JSON.stringify(object);

    return json;
}



function populationsInTowns(arr) {
    let towns = arr
                .map(x=>x.split('<->'))
                .reduce((a,t)=> {
                    let currentTown = t[0];
                    let currentPop = Number(t[1]);

                    if (!a[currentTown]) {
                        a[currentTown] = 0;
                    }

                    a[currentTown]+=currentPop;
                    return a;
                },{});

    Object.keys(towns).forEach(el=> {
        console.log(`${el}: ${towns[el]}`);
    });
    
}



function fromJSONToHTMLTable(arr) {
    let data = arr.shift();
    let products = JSON.parse(data);

    let firstRow = products[0];
    
    let html = '<table>';

    html+= `<tr>${Object.keys(firstRow).map(x=>`<th>x</th>`)}</tr>`;
    products.forEach(product=> {
        html+= '<tr>';
        html+= Object.values(product).map(x=> `<td>${x}</td>`).join('');
        html+= '</tr>';
    });
    html+= '</table>';

    return html;
}



//fromJSONToHTMLTable(['[{"Name":"Tomatoes & Chips","Price":2.35},{"Name":"J&B Chocolate","Price":0.96}]']);
//console.log(fromJSONToHTMLTable(['[{"Name":"Pesho <div>-a","Age":20,"City":"Sofia"},{"Name":"Gosho","Age":18,"City":"Plovdiv"},{"Name":"Angel","Age":18,"City":"Veliko Tarnovo"}]']));



function lowestPric(input) {
    let products = {};
    let sequence = [];
 
    for (let line of input){
        let arr = line.split(' | ');
        let town = arr[0];
        let product = arr[1];
        let price = Number(arr[2]);
 
        if (product in products){
            if (price < products[product].price){
                products[product].price = price;
                products[product].town = town;
            }
        }
        else{
            let obj = {};
            obj.price = price;
            obj.town = town;
            products[product] = obj;
            sequence.push(product);
        }
    }
 
    for (let pr of sequence){
        console.log(pr + " -> " + products[pr].price + ' (' + products[pr].town + ')');
    }
}


// lowestPric(['Sample Town | Sample Product | 1000',
//                         'Sample Town | Orange | 2',
//                         'Sample Town | Peach | 1',
//                         'Sofia | Orange | 3',
//                         'Sofia | Peach | 2',
//                         'New York | Sample Product | 1000.1',
//                         'New York | Burger | 10']
// );





function getPersons() {
    class Person{

        constructor(firstName=undefined,lastName=undefined,age=undefined,email=undefined){
             this.firstName = firstName;
             this.lastName = lastName;
             this.age = age;
             this.email= email;
        }
    
        toString(){
    
            return `${this.firstName} ${this.lastName} (age: ${this.age}, email: ${this.email})`;
        }
    }
    
    
    
    let person1 = new Person('Anna', 'Simpson', 22, 'anna@yahoo.com');
    let person2 = new Person('SoftUni');
    let person3 = new Person('Stephan','Johnson',25);
    let person4 = new Person('Gabriel','Peterson',24,'g.p@gmail.com');

    let arr = [person1,person2,person3,person4];

    return arr;
}


class Circle{
    diameterField;
    radius;

    constructor(radius){
        this.radius = radius;
    }



    get diameter() { return this.diameterField = 2*this.radius;};
    set diameter(value) { this.diameterField = value;   this.radius=value/2;};

    get area(){return (Math.PI*(this.radius**2))};
}



class Point{
    constructor(x,y){
        this.x=x;
        this.y= y;
    }

    static distance(point1, point2){
            return Math.sqrt(((point2.x-point1.x)**2)+((point2.y-point1.y)**2));
    }
}



function heroicInventory(input) {
    let result = [];

    for (const iterator of input) {
        let [name,level,items] = iterator.split(' / ');
        level = Number(level);
        items = items? items.split(', '):[];

        result.push({name,level,items});
    }

    let json = JSON.stringify(result);
    console.log(json);
}

// heroicInventory(['Isacc / 25 / Apple, GravityGun',
// 'Derek / 12 / BarrelVest, DestructionSword',
// 'Hes / 1 / Desolator, Sentinel, Antara']
// );

function jSONsTable(arr) {
   let result = [];
   
   let html = '<table>\n';
    for (const row of arr) {
        let object = JSON.parse(row);
        html+=' <tr>\n';

        html+= `    <td>${object.name}</td>\n`;
        html+= `    <td>${object.position}</td>\n`;
        html+= `    <td>${object.salary}</td>\n`;

        html+=' </tr>\n';
    }

   html += '</table>';

   return html;
}

// console.log(jSONsTable(['{"name":"Pesho","position":"Promenliva","salary":100000}',
// '{"name":"Teo","position":"Lecturer","salary":1000}',
// '{"name":"Georgi","position":"Lecturer","salary":1000}']
// ));

function cappyJuice(input) {
    let result = {};
    let juices = {};
    for (let i = 0; i < input.length; i++) {
        let [juice, quantity] = input[i].split(' => ');

        quantity = Number(quantity);
        if (!juices[juice]) {
            juices[juice] = 0;
        }
        juices[juice] += quantity;

        if (juices[juice]>=1000) {
            if (!result[juice]) {
                result[juice]=0;
            }
            result[juice]+=Math.floor(juices[juice]/1000);
            juices[juice]-= Math.floor(juices[juice]/1000)*1000;
            
        }
    }

    for (const x of Object.keys(result)) {
        console.log(`${x} => ${Math.floor(result[x])}`);
    }
}

// cappyJuice(['Kiwi => 234',
// 'Pear => 2345',
// 'Watermelon => 3456',
// 'Kiwi => 4567',
// 'Pear => 5678',
// 'Watermelon => 6789']
// );

function storeCatalogue(arr) {
    let result ={};

    for (let i = 0; i < arr.length; i++) {
        let [product,price] = arr[i].split(' : ');
        price = Number(price);

        result[product] = price;
    }

    let sortedLetters = Object.keys(result)
                        .sort((a,b)=> a.localeCompare(b));
    
    let letter ='';
    for (const product of sortedLetters) {
       
        if (letter!==product[0]) {
            letter = product[0];
            console.log(letter);
        }
        console.log(`${product}: ${result[product]}`);
    }
}

// storeCatalogue(['Appricot : 20.4',
// 'Fridge : 1500',
// 'TV : 1499',
// 'Deodorant : 10',
// 'Boiler : 300',
// 'Apple : 1.25',
// 'Anti-Bug Spray : 15',
// 'T-Shirt : 10']
// );


function autoEngineeringCompany(input) {
    let result = {};

    for (let i = 0; i < input.length; i++) {
        let [car,model,amount] = input[i].split(' | ');        
        amount = Number(amount);

        if (!result[car]) {
            result[car] = {};
        }

        if (!result[car][model]) {
            result[car][model] = 0;
        }
        
        result[car][model]+= amount;
    }

    for (const car of Object.keys(result)) {
        console.log(car);

        for (const model of Object.keys(result[car])) {
            console.log(`###${model} -> ${result[car][model]}`);
        }
    }
}

// autoEngineeringCompany(['Audi | Q7 | 1000',
//                         'Audi | Q6 | 100',
//                         'BMW | X5 | 1000',
//                         'BMW | X6 | 100',
//                         'Citroen | C4 | 123',
//                         'Volga | GAZ-24 | 1000000',
//                         'Lada | Niva | 1000000',
//                         'Lada | Jigula | 1000000',
//                         'Citroen | C4 | 22',
//                         'Citroen | C5 | 10']
//                         );

function systemComponents(input) {
    let result = {};
    for (let i = 0; i < input.length; i++) {
        let [system,component,subcomponent]   = input[i].split(' | ');
        
        if (!result[system]) {
            result[system] = {};
        }

        if (!result[system][component]) {
            result[system][component] = [];
        }

        result[system][component].push(subcomponent);
    }
    let systemOrdered = Object.keys(result)
    .sort((a,b)=> Object.keys(result[b]).length - Object.keys(result[a]).length 
    || a.localeCompare(b));


    for (const system of Object.values(systemOrdered)) {
        console.log(system);
        
        let componentsOrdered = Object.keys(result[system]).sort((a,b) => result[system][b].length - result[system][a].length);
        
        for (const component of Object.values(componentsOrdered)) {
            console.log(`|||${component}`);

            for (const sub of result[system][component]) {
                console.log(`||||||${sub}`);
            }
        }
    }
}

// systemComponents(['SULS | Main Site | Home Page',
//                     'SULS | Main Site | Login Page',
//                     'SULS | Main Site | Register Page',
//                     'SULS | Judge Site | Login Page',
//                     'SULS | Judge Site | Submittion Page',
//                     'Lambda | CoreA | A23',
//                     'SULS | Digital Site | Login Page',
//                     'Lambda | CoreB | B24',
//                     'Lambda | CoreA | A24',
//                     'Lambda | CoreA | A25',
//                     'Lambda | CoreC | C4',
//                     'Indice | Session | Default Storage',
//                     'Indice | Session | Default Security']
//                     );



function dataClass() {
    class Request{
        constructor(method,uri,version,message){
            this.method = method;
            this.uri = uri;
            this.version = version;
            this.message = message;
            this.response = undefined;
            this.fulfilled = false;
        }
    }
    

    let myData = new Request('GET', 'http://google.com', 'HTTP/1.1', '')
    console.log(myData);

}


function sortedList() {
    class result{
        constructor() {
            this.list = [];
            this.size = this.list.length;
        }

        add(element) {
            this.list.push(element);
            this.list.sort((a, b) => a - b);
            this.size++;
            return;
        }

        remove(index) {
            if (index < 0 || index >= this.list.length) {
                
            } else {
                this.list.splice(index, 1);
                this.size--;
                return;
            }
        }

        get(index) {
            if (index < 0 || index >= this.list.length) {
               
            } else {
                return this.list[index];
            }
        }

        
    }

    
    // Instantiate and test functionality
    var myList = new result();
    
    console.log(myList.add(5));;
    
    console.log(myList.add(3));;
    
    console.log(myList.remove(0));;
    
}


function ticketsF(arr,criteria) {
    class Ticket{
        constructor(destination,price,status){
            this.destination=destination;
            this.price=price;  
            this.status=status;
        }
    }
    let tickets = [];
    for (let i = 0; i < arr.length; i++) {
        let [destination,price,status] = arr[i].split('|');
        let currentTicket = new Ticket(destination,Number(price),status);

        tickets.push(currentTicket);
    }

    let sortedTickets=[];
    if (criteria==='destination') {
        
         sortedTickets=tickets.sort((a,b)=> a.destination.localeCompare(b.destination));
         
    }else if(criteria==='price'){
        sortedTickets = tickets.sort((a,b)=> a.price-b.price);
    }else if(criteria==='status'){
        let availableTickets = tickets.filter(x=> x.status==='available');
        let departedTickets= tickets.filter(x=> x.status==='departed');
        let soldTickets = tickets.filter(x=>x.status==='sold');

        sortedTickets.push(...availableTickets);
        sortedTickets.push(...departedTickets);
        sortedTickets.push(...soldTickets);
    }


    return sortedTickets;
}

// ticketsF(['Philadelphia|94.20|available',
// 'New York City|95.99|available',
// 'New York City|95.99|sold',
// 'Boston|126.20|departed'],
// 'destination'
// );

// ticketsF(['Philadelphia|94.20|available',
// 'New York City|95.99|available',
// 'New York City|95.99|sold',
// 'Boston|126.20|departed'],
// 'status'
// );


function lengthLimit() {
    class Stringer{
        constructor(string,length){
            this.innerString=  string;
            this.innerLength = length;
        }

        increase(length){
            if (length>0) {
                this.innerLength +=length;
            }
        }

        decrease(length){
            if (length>=0) {
                if (this.innerLength-length<0) {
                    this.innerLength=0;
                }else{
                this.innerLength-=length;
                }
            }
        }

        toString(){
            if (this.innerLength===0) {
                return '...';
            }
            if (this.innerLength<this.innerString.length) {
                let toReturn = this.innerString.substring(0,this.innerString.length-this.innerLength)+'...';

                
            return toReturn;
            }

            return this.innerString;
        }
    }

    let test = new Stringer("Test", 5);
    console.log(test.toString()); // Test

    test.decrease(3);
    console.log(test.toString()); // Te...

    test.decrease(5);
    console.log(test.toString()); // ...

    test.increase(4); 
    console.log(test.toString()); // Test

}

lengthLimit();