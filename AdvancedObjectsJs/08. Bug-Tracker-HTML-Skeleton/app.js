// function solve () {

// }


// function solve() {
//     let fighter = function(name = '') {
//         [this.name, this.health,this.stamina] = [name,100,100];


//         this.fight = function() {
//             this.stamina--;
//             console.log(`${this.name} slashes at the foe!`);
//             console.log(this);
//         }


//     };


//     let mage = function(name)  {
//         [this.name,this.health,this.mana] = [name,100,100];

//         this.cast = (spell)=> {
//             this.mana--;
//             console.log(`${this.name} cast ${spell}`);
//             console.log(this);
//         }

//     }

//     return {
//         mage: (name = '') => new mage(name),
//         fighter: (name = '') => new fighter(name)
//     }
// }


// let create = solve();
// const scorcher = create.mage("Scorcher");
// scorcher.cast("fireball")
// scorcher.cast("thunder")
// scorcher.cast("light")

// const scorcher2 = create.fighter("Scorcher 2");
// scorcher2.fight()

// console.log(scorcher2.stamina);
// console.log(scorcher.mana);


// function solve(worker) {
//     if (worker.dizziness) {
//         worker.levelOfHydrated+= (0.1*worker.weight)*worker.experience;
//         worker.dizziness = false;
//     }

//     return worker;
// }

// console.log(solve({ weight: 120,
//     experience: 20,
//     levelOfHydrated: 200,
//     dizziness: true }
// ));

//input Obj is: {
//     weight: Number,
//         experience: Number,
//             levelOfHydrated: Number,
//                 dizziness: Boolean
// }


// function solve(input) {
//     let engines = [{ power: 90, volume: 1800 }, 
//         { power: 120, volume: 2400 },
//         { power: 200, volume: 3500 }];

//     return {
//         model: input.model,
//         engine: engines.find(e=> e.power>= input.power),
//         carriage: {type: input.carriage, color: input.color},
//         wheels: [0,0,0,0].fill(input.wheelsize%2===0?--input.wheelsize:input.wheelsize)
//     };
// }

// console.log(solve({ model: 'Opel Vectra',
// power: 110,
// color: 'grey',
// carriage: 'coupe',
// wheelsize: 17 }
// ));


// obj = ()=> ({
//     __proto__: {},
//     extend: function(temp) {
//         Object.entries(temp).forEach(([key,value])=> {
//             if ((typeof value)=='function') {
//                 console.log('eeee');
//                 Object.getPrototypeOf(this)[key] = value;
//             }else{
//                 this[key] = value;
//             }
//         })
//     }
// })

// console.log(obj().extend({
//     extensionMethod: function (){},
//     extensionProperty: 'someString'
//   }));


// (() => {
//     String.prototype.ensureStart = function(str=''){
//         if (!this.startsWith(str)) {
//             return str + this;
//         }
//         return this.toString();
//     }

//     String.prototype.ensureEnd = function (str) {
//         if (!this.endsWith(str)) {
//             return this + str;
//         }
//         return this.toString();
//     }

//     String.prototype.isEmpty =function () {
//         return !this.length; 
//     }

//     String.prototype.truncate = function (n){
//         if (n<4) {
//             return '.'.repeat(n);
//         }

//         if (this.length<n) {
//             return this.toString();
//         }

//         let lastInterval = this.lastIndexOf(' ');
//         if (lastInterval>=0) {
//             let current = this.substr(0,n-1);
//             lastInterval = current.lastIndexOf(' ');
//             return current.substr(0,lastInterval).toString()+'...';
//         }else{
//             return this.substr(0,n-3).toString()+'...';
//         }
//     }

//     String.format = (str, ...params) => {
//         params.forEach((key, index)=>{
//             str= str.replace(`{${index}}`,key);
//         })
//         return str;
//     }
// })();

// let str = 'my string';
// str = str.ensureStart('my');
// str = str.ensureStart('hello ');
// str = str.truncate(16);
// str = str.truncate(14);
// str = str.truncate(8);
// str = str.truncate(4);
// str = str.truncate(2);
//  str = String.format('The {0} {1} fox',
//    'quick', 'brown');
//  str = String.format('jumps {0} {1}',
//    'dog');


// var testString = 'the quick brown fox jumps over the lazy dog';
// console.log(testString.truncate(10));


// list= () => ({
//     add: (elemenent) => {},- adds a new element to the collection
//     remove(index) - removes the element at position index
//     get(index) - returns the value of the element at position index
//     size - number of elements stored in the collection

// })